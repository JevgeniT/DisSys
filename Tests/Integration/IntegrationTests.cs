using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.Identity;
using WebApp;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Public.DTO;
using Public.DTO.Reservation;
using Public.DTO.Room;


namespace Tests
{
    public class IntegrationTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;


        public IntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("TestDB", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});
        }

        [Fact]
        public async Task CanAuthorize()
        {
            var user = new LoginDTO
            {
                Email = "host@host.com",
                Password = "qweqwe"
            };
            
            var getTestResponse = await _client.PostAsJsonAsync("/api/v1/account/login", user);
            
            getTestResponse.EnsureSuccessStatusCode();
            var definition = new {token = ""};
            var token = await getTestResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeAnonymousType(token, definition);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(data?.token) as JwtSecurityToken;
            Assert.True(securityToken?.ValidTo > DateTime.Now );
            
            var getFailedResponse = await _client.PostAsJsonAsync("/api/v1/account/login", new LoginDTO { Email = "asdf@afd.com", Password = "aaaa"});
            getFailedResponse.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task CanFindProperty()
        {
            var param = "test";
            var getResponse = await _client.GetAsync($"/api/v1/property/find?input={param}");
            getResponse.EnsureSuccessStatusCode();
            var data = JsonConvert.DeserializeObject<PropertyViewDTO[]>(await getResponse.Content.ReadAsStringAsync())?[0];
            Assert.True(data?.Address?.ToLower().Contains(param));
        }
        
        [Fact]
        public async Task HostCanCreateProperty()
        {
            var token = await GetHostToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var property = new PropertyCreateDTO
            {
                Name = "Test3",
                Country = "Test3",
                Address = "Test3",
                Description = "Test3",
                Type = "Hotel",
            };
            
            var getResponse = await _client.PostAsJsonAsync("/api/v1/property", property);
            getResponse.EnsureSuccessStatusCode();
            var data = JsonConvert.DeserializeObject<PropertyDTO>(await getResponse.Content.ReadAsStringAsync());
            
            Assert.NotEmpty(data?.Address);
            Assert.NotNull(data?.AppUserId);
            Assert.IsType<Guid>(data?.Id);
        }

        [Fact]
        public async Task HostCanCRUDHisProperty()
        {
            var token = await GetHostToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            
            var property = new PropertyCreateDTO
            {
                Name = "Test4",
                Country = "Test4",
                Address = "Test4",
                Description = "Test4",
                Type = "Hotel",
            };
            
            var getResponse = await _client.PostAsJsonAsync("/api/v1/property", property);
            
            getResponse.EnsureSuccessStatusCode();
            
            var data = JsonConvert.DeserializeObject<PropertyDTO>(await getResponse.Content.ReadAsStringAsync());
            
            Assert.NotEmpty(data?.Address);
            Assert.NotNull(data?.AppUserId);
            Assert.IsType<Guid>(data?.Id);

            var userId = data!.AppUserId;
            var propertyId = data?.Id;
            
            var putProperty = new PropertyCreateDTO
            {
                Name = data!.Name,
                Country = data!.Country,
                Address = data!.Address,
                Description = data!.Description,
                Type = "Hostel",
                Id = propertyId
            };
            
            var putResponse = await _client.PutAsJsonAsync($"/api/v1/property/{propertyId}", putProperty);
            getResponse.EnsureSuccessStatusCode();
            
            var updated = JsonConvert.DeserializeObject<PropertyCreateDTO>(await putResponse.Content.ReadAsStringAsync());
            
            Assert.NotSame(property.Type, updated!.Type);
            Assert.True(updated?.Type == "Hostel");
            
            var deleteResponse = await _client.DeleteAsync($"/api/v1/property/{propertyId}");
            deleteResponse.EnsureSuccessStatusCode();
            
            var ensureDeleted = await _client.GetAsync($"/api/v1/property/{propertyId}");

            ensureDeleted.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
        
        [Fact]
        public async Task GuestShouldNotEditPropertyData()
        {
            var token = await GetUserToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            
            var getResponse = await _client.PostAsJsonAsync("/api/v1/property", new PropertyCreateDTO());
            
            getResponse.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
            
            var randomProperty = await _client.GetAsync("/api/v1/property/find?input=test");

            var property = JsonConvert.DeserializeObject<PropertyViewDTO[]>(await randomProperty.Content.ReadAsStringAsync())?[0];

            randomProperty.EnsureSuccessStatusCode();
            Assert.NotNull(property);
            
            var propertyId = property?.Id;
            var putResponse =  _client.PutAsJsonAsync($"/api/v1/property/{propertyId}", new PropertyCreateDTO() { Id = propertyId });
            var deleteResponse =  _client.DeleteAsync($"/api/v1/property/{propertyId}");
            var ensureNotDeleted = _client.GetAsync($"/api/v1/property/{propertyId}");
            
            await Task.WhenAll(putResponse, deleteResponse, ensureNotDeleted);

            putResponse.Result.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
            deleteResponse.Result.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
            ensureNotDeleted.Result.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task HostCanCreateAddRoomAndDates()
        {
            var token = await GetHostToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            
            var data = new PropertyCreateDTO
            {
                Name = "Test31",
                Country = "Test31",
                Address = "Test31",
                Description = "Test31",
                Type = "Hotel",
            };
            
            var getResponse = await _client.PostAsJsonAsync("/api/v1/property", data);
            getResponse.EnsureSuccessStatusCode();
            
            var property = JsonConvert.DeserializeObject<PropertyDTO>(await getResponse.Content.ReadAsStringAsync());
            Assert.NotNull(property);
            
            var postRoom = await _client.PostAsJsonAsync("/api/v1/room", new RoomDTO
            {
                PropertyId = property!.Id,
                Name = "Room",
                Size = 22,
                AdultsOccupancy = 2,
                ChildOccupancy = 2,
                Description = "test",
                FacilityDtos = null,
                BedTypes = new []{"1 large"},
            });
            
            postRoom.EnsureSuccessStatusCode();
            var room = JsonConvert.DeserializeObject<RoomDTO>(await postRoom.Content.ReadAsStringAsync());
            Assert.NotNull(room);
            Assert.NotNull(room?.Id);

            var postDates = await _client.PostAsJsonAsync("/api/v1/availability", new AvailabilityDTO
            {
                From = DateTime.Now.AddDays(9),
                To = DateTime.Now.AddDays(10),
                RoomId = room!.Id.Value,
                Active = true,
                PricePerNightForAdult = 10,
                PricePerNightForChild = 10,
                PricePerPerson = false,
                RoomsAvailable = 2
            });
            _testOutputHelper.WriteLine(postDates!.ReasonPhrase);
            postDates.EnsureSuccessStatusCode();
            var dates = JsonConvert.DeserializeObject<AvailabilityDTO>(await postDates.Content.ReadAsStringAsync());
            Assert.NotNull(dates);
            
            var checkDates = await _client.GetAsync($"/api/v1/availability?rId={room.Id}");
            checkDates.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GuestCanMakeReservation()
        {
            var token = await GetUserToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var available = await _client.GetAsync("/api/v1/property/find?input=ForRooms");
            available.EnsureSuccessStatusCode();
            var property = JsonConvert.DeserializeObject<PropertyViewDTO[]>(await available.Content.ReadAsStringAsync())?[0];
            
            var propertyView = await _client.GetAsync($"/api/v1/property/{property!.Id}");
            
            propertyView.EnsureSuccessStatusCode();

            var propertyFound = JsonConvert.DeserializeObject<RoomDTO[]>(await available.Content.ReadAsStringAsync())![0];

            Assert.NotNull(property);
            
            var reservation = new ReservationCreateDTO
            {
                CheckInDate =  DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(3),
                PropertyId = property.Id,
                Adults = 1,
                Children = 1,
                TotalPrice = 1,
                ArrivalTime = "any",
                Message = "any",
                ReservationExtras = null,
                RoomDtos = new []
                {
                    new ReservationRoomDTO
                    {
                        RoomId = propertyFound.Id.Value,
                        PolicyId = Guid.NewGuid(),
                        GuestFirstLastName = string.Empty,
                        RoomTotalPrice = 0,
                        BedType = string.Empty
                    }
                }
            };

            var reservationResponse = await _client.PostAsJsonAsync("/api/v1/reservation", reservation);
            _testOutputHelper.WriteLine(reservationResponse.Content.ReadAsStringAsync().Result);
            reservationResponse.EnsureSuccessStatusCode();
        }

        private async ValueTask<string?> GetHostToken()
        {
            var user = new LoginDTO
            {
                Email = "host@host.com",
                Password = "qweqwe"
            };
            
            var getTestResponse = await _client.PostAsJsonAsync("/api/v1/account/login", user);
            
            var definition = new {token = ""};
            var token = await getTestResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(token, definition)?.token;
        }
        
        
        private async ValueTask<string?> GetUserToken()
        {
            var user = new LoginDTO
            {
                Email = "user@user.com",
                Password = "qweqwe"
            };
            
            var getTestResponse = await _client.PostAsJsonAsync("/api/v1/account/login", user);
            
            var definition = new {token = ""};
            var token = await getTestResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(token, definition)?.token;
        }
    }
}