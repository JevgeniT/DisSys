using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CancellationBefore = table.Column<int>(nullable: true),
                    PrepaymentBefore = table.Column<int>(nullable: true),
                    CancellationFee = table.Column<int>(nullable: true),
                    PriceCoefficient = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extras_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    CheckInFrom = table.Column<TimeSpan>(nullable: false),
                    CheckInTo = table.Column<TimeSpan>(nullable: false),
                    CheckOutBefore = table.Column<TimeSpan>(nullable: false),
                    DamageDepositRequired = table.Column<bool>(nullable: false),
                    DamageDeposit = table.Column<decimal>(type: "decimal(5, 2)", nullable: true),
                    PaymentMethodsAccepted = table.Column<string>(nullable: true),
                    AllowPets = table.Column<bool>(nullable: false),
                    AllowParties = table.Column<bool>(nullable: true),
                    CheckInAge = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyRules_Properties_Id",
                        column: x => x.Id,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ReservationNumber = table.Column<int>(nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "date", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "date", nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Adults = table.Column<int>(nullable: false),
                    Children = table.Column<int>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(maxLength: 128, nullable: true),
                    ArrivalTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AdultsOccupancy = table.Column<int>(nullable: false),
                    ChildOccupancy = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AllowSmoking = table.Column<bool>(nullable: false),
                    BedTypes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationExtras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false),
                    ExtraId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationExtras_Extras_ExtraId",
                        column: x => x.ExtraId,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationExtras_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    From = table.Column<DateTime>(type: "date", nullable: false),
                    To = table.Column<DateTime>(type: "date", nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PricePerNightForAdult = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    PricePerNightForChild = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    PricePerPerson = table.Column<bool>(nullable: false),
                    RoomsAvailable = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false),
                    GuestFirstLastName = table.Column<string>(nullable: true),
                    BedType = table.Column<string>(nullable: true),
                    RoomTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRooms_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomFacilities_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d50c848a-221d-4f91-97f5-9ffa1c5c1f5c"), "fee1d22c-64c8-4580-aa73-1e46ff76bb75", "gust", "Guest" },
                    { new Guid("f739fa1d-6bf4-4799-ad6c-b28d65774553"), "77aa6c23-30c9-4ada-9002-594b70e2fd1e", "host", "Host" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("9242384f-8c2f-4fd4-8714-c4d1f3951ecf"), 0, "809c175a-1f3f-4540-8196-22163f100928", "host@host.com", true, "host", "host", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "HOST@HOST.COM", "HOST@HOST.COM", "AQAAAAEAACcQAAAAEE4j6dzZlf+jWgIISBazY42qn2ExO4ywKDZDKvNCG2wyCPEc6gogrr9r0mlwe/ucRA==", "", false, "", false, "host@host.com" },
                    { new Guid("e9158f9b-79d1-466e-a60d-d1c9e4f09f45"), 0, "bf5d3557-9f67-4895-8685-bac98ad2df3b", "user@user.com", true, "user", "user", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "USER@USER.COM", "USER@USER.COM", "AQAAAAEAACcQAAAAEHOSeR0svTluhuf4PoBbfdWy4cf1B+ss8EwIZ8ZY/XrIVDTlRFN2pIEQmzeB8ndlxg==", "", false, "", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "ChangedAt", "ChangedBy", "CreatedAt", "CreatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("d7596e30-b83f-4329-86e6-52aae7583c61"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2407), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2410), "migration", "Free Wi-Fi" },
                    { new Guid("1c62654d-e9b8-4571-abbb-1d1161d8de66"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2377), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2379), "migration", "Wake-up service" },
                    { new Guid("54f63558-72c3-4828-b2cf-5fdcc8c392dc"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2349), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2352), "migration", "Outdoor furniture" },
                    { new Guid("2cb8d405-44ba-4d75-ac80-d915aa691d11"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2322), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2324), "migration", "Balcony" },
                    { new Guid("566cfeff-fbec-46d3-8554-e4a869e5ce08"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2294), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2297), "migration", "Flat-screen TV" },
                    { new Guid("cc51d276-d719-4ab4-a620-381f18c52dc0"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2267), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2269), "migration", "Satellite channels" },
                    { new Guid("3a585312-0989-4b16-b64b-d8514297b894"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2239), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2241), "migration", "Desk" },
                    { new Guid("4d2a6f10-269f-4b4b-b303-bb169f373f00"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2211), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2214), "migration", "Sofa" },
                    { new Guid("60e90c90-f8a9-455f-b723-664c61817d5a"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2155), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2157), "migration", "Coffee machine" },
                    { new Guid("193de572-b33e-41fc-bddf-5c62cf860572"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2127), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2130), "migration", "Heating" },
                    { new Guid("f3b69b73-cbaf-41de-bcaf-3034bc37d6c9"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2099), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2101), "migration", "Iron" },
                    { new Guid("a39c7ee8-02e4-4045-9357-8713c00f8cbb"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2068), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2071), "migration", "Safe" },
                    { new Guid("aa13165d-ecb3-48ab-a1ec-3dd66ebcd241"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2034), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2036), "migration", "Air conditioning" },
                    { new Guid("5c6b9b68-cf13-4b3c-8879-98183c0e5181"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2006), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2008), "migration", "Minibar" },
                    { new Guid("3c0d53a6-075d-4bc7-8719-4a25ae688948"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(1977), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(1980), "migration", "Wardrobe" },
                    { new Guid("9d6f88c1-adb3-423a-94fe-e63178dee603"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(1909), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(1922), "migration", "Linens" },
                    { new Guid("71468a74-1fe5-4e57-a5c1-51cda47d349d"), new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2184), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 238, DateTimeKind.Local).AddTicks(2186), "migration", "Kettle" },
                    { new Guid("dbc4b414-de72-4da6-9c21-123c7c0ba4aa"), new DateTime(2020, 11, 26, 14, 36, 17, 232, DateTimeKind.Local).AddTicks(4186), "migration", new DateTime(2020, 11, 26, 14, 36, 17, 237, DateTimeKind.Local).AddTicks(8601), "migration", "Elevator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("9242384f-8c2f-4fd4-8714-c4d1f3951ecf"), new Guid("f739fa1d-6bf4-4799-ad6c-b28d65774553") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_RoomId",
                table: "Availabilities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_PropertyId",
                table: "Extras",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AppUserId",
                table: "Properties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationExtras_ExtraId",
                table: "ReservationExtras",
                column: "ExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationExtras_ReservationId",
                table: "ReservationExtras",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_ReservationId",
                table: "ReservationRooms",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_RoomId",
                table: "ReservationRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AppUserId",
                table: "Reservations",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PropertyId",
                table: "Reservations",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PropertyId",
                table: "Reviews",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReservationId",
                table: "Reviews",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_FacilityId",
                table: "RoomFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PropertyId",
                table: "Rooms",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "PropertyRules");

            migrationBuilder.DropTable(
                name: "ReservationExtras");

            migrationBuilder.DropTable(
                name: "ReservationRooms");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoomFacilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
