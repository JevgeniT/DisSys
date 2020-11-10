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
                    Active = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Adults = table.Column<int>(nullable: false),
                    Children = table.Column<int>(nullable: false)
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
                    Bed = table.Column<string>(nullable: false)
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
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Reservations_ReservationId",
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
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRooms_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "AvailabilityPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AvailabilityId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailabilityPolicies_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvailabilityPolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("56dbce5e-cac1-4eb6-8344-b87f2f4e4c81"), "bfb33f69-1877-4900-8a92-836380c30d7d", "gust", "Guest" },
                    { new Guid("a83b6650-1243-4b25-8867-36b696815a0d"), "03a14470-ab3e-4fa2-8827-669bfea45eba", "host", "Host" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c535e396-55cf-42f2-8aef-d21d7438afa6"), 0, "d146df01-34ad-47f2-9ce7-f3397336d3ce", "host@host.com", true, "host", "host", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "HOST@HOST.COM", "HOST@HOST.COM", "AQAAAAEAACcQAAAAEBvMh5R6xPf2zTGqDMD6rDxJE/OA5Z+j7GNKNHzMbfsu2mhvA9EP9RMhCoWR/ol8LA==", "", false, "", false, "host@host.com" },
                    { new Guid("3a65c66d-34a4-4ffb-95b1-bb2749403720"), 0, "745b5fa5-1243-4c35-a953-9ad916b1068c", "user@user.com", true, "user", "user", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "USER@USER.COM", "USER@USER.COM", "AQAAAAEAACcQAAAAEONh64d6P7pRC6Ul0RjpW9QNNaRJw1tf9+1xf0zylKP01rZJnxvqfj3r5zxR/AP2jg==", "", false, "", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "ChangedAt", "ChangedBy", "CreatedAt", "CreatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("08be5da0-e53b-489f-8cc1-b62a3f145277"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3158), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3160), "migration", "Free Wi-Fi" },
                    { new Guid("3cb69c95-244a-4300-818e-92ea90f5c36f"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3130), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3133), "migration", "Wake-up service" },
                    { new Guid("42d68492-7738-45f3-be39-5847b5702c3d"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3006), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(3008), "migration", "Outdoor furniture" },
                    { new Guid("7df60302-1e67-44fa-8dc7-8a6445f10620"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2980), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2983), "migration", "Balcony" },
                    { new Guid("ff9ff3c6-9e44-4743-83dc-d5e3fc131416"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2955), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2957), "migration", "Flat-screen TV" },
                    { new Guid("a952cb7b-da91-42ee-9a8b-be8f3e26db7b"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2929), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2931), "migration", "Satellite channels" },
                    { new Guid("b506ef7b-54d3-4843-b287-3e4383f5ecbc"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2903), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2906), "migration", "Desk" },
                    { new Guid("8dd27044-daa4-4c86-b695-f4f422502235"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2878), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2880), "migration", "Sofa" },
                    { new Guid("368c438f-0d80-44a8-95f3-78d8af9e07aa"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2826), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2829), "migration", "Coffee machine" },
                    { new Guid("6b9f50b7-7bee-47f5-b036-16839fb98c0c"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2799), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2801), "migration", "Heating" },
                    { new Guid("b15b75b2-26c7-4dcc-8d3a-48f2adc115c6"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2773), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2775), "migration", "Iron" },
                    { new Guid("1488f6ee-4b04-4c0a-afe9-1bf6c7009b49"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2747), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2749), "migration", "Ironing facilities" },
                    { new Guid("c92eab10-ebae-41e7-b25a-30459cd10743"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2721), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2724), "migration", "Safe" },
                    { new Guid("346141cf-7b75-4d02-bf82-20815ba971ab"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2688), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2690), "migration", "Air conditioning" },
                    { new Guid("defe95b3-1982-419e-a871-a6c116e504eb"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2661), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2664), "migration", "Minibar" },
                    { new Guid("f13720d4-c808-4a41-b30e-3819e3986a86"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2634), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2637), "migration", "Wardrobe or closet" },
                    { new Guid("8590fc1f-021c-48c8-82ec-f7b8ba260ac7"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2565), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2583), "migration", "Linens" },
                    { new Guid("08ba89d4-19c1-432f-b2bb-e04d4d112d0f"), new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2852), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 448, DateTimeKind.Local).AddTicks(2855), "migration", "Electric kettle" },
                    { new Guid("d175bdc2-2569-4e16-a7a2-7e77567fbb2b"), new DateTime(2020, 11, 10, 16, 48, 28, 442, DateTimeKind.Local).AddTicks(4378), "migration", new DateTime(2020, 11, 10, 16, 48, 28, 447, DateTimeKind.Local).AddTicks(9135), "migration", "Upper floors accessible by elevator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("c535e396-55cf-42f2-8aef-d21d7438afa6"), new Guid("a83b6650-1243-4b25-8867-36b696815a0d") });

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
                name: "IX_AvailabilityPolicies_AvailabilityId",
                table: "AvailabilityPolicies",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityPolicies_PolicyId",
                table: "AvailabilityPolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_PropertyId",
                table: "Extras",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AppUserId",
                table: "Invoices",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReservationId",
                table: "Invoices",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AppUserId",
                table: "Properties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_PolicyId",
                table: "ReservationRooms",
                column: "PolicyId");

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
                name: "AvailabilityPolicies");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PropertyRules");

            migrationBuilder.DropTable(
                name: "ReservationRooms");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoomFacilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Policies");

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
