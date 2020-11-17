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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationId = table.Column<Guid>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Extras_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
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
                    PolicyId = table.Column<Guid>(nullable: false),
                    GuestFirstLastName = table.Column<string>(nullable: true),
                    BedType = table.Column<string>(nullable: true)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("878d34ac-4fe7-4f57-86a3-196eb31c5804"), "de5ca697-caf3-4a1d-9432-9f09ccfb93c5", "gust", "Guest" },
                    { new Guid("0e93e56a-9907-48da-8b0b-33536b9e60a0"), "2566cf23-f574-4f68-b528-51112f1ff34a", "host", "Host" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("3ec1288a-c4d6-411a-91da-a60716fd327c"), 0, "98d87b77-6a1c-446c-a6dd-093cb204c1c3", "host@host.com", true, "host", "host", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "HOST@HOST.COM", "HOST@HOST.COM", "AQAAAAEAACcQAAAAEFCEYWptJFOgIsSiAchTvzX/ewRiDcrloXprM3z0K7LCoG1rpBj6+Tb8pWrUnOgcKw==", "", false, "", false, "host@host.com" },
                    { new Guid("4d0fe7b4-999c-45b9-bc0a-ed71c4c47d66"), 0, "5a81be59-a57c-437a-a34a-3164a2ac1228", "user@user.com", true, "user", "user", false, new DateTimeOffset(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 0, 0, 0, 0)), "USER@USER.COM", "USER@USER.COM", "AQAAAAEAACcQAAAAEL+PkZzhbRBn04X7u9hI9egVJ3hMgCZ6Fnj2HTjEMzJ945xwTxP803qDQxgYaCCWjg==", "", false, "", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "ChangedAt", "ChangedBy", "CreatedAt", "CreatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("2423e8af-f301-4fec-aa4b-729c0cceffa2"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1452), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1454), "migration", "Free Wi-Fi" },
                    { new Guid("e31a6df9-ee3c-481f-b516-01e7174ae847"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1426), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1428), "migration", "Wake-up service" },
                    { new Guid("c3634061-fd8a-4f70-a476-7c6838eaa1aa"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1399), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1401), "migration", "Outdoor furniture" },
                    { new Guid("da510e10-3842-431a-9ec4-b64c8513aa15"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1373), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1375), "migration", "Balcony" },
                    { new Guid("44befb59-568f-4d0a-8933-a3f30c3feeab"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1348), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1350), "migration", "Flat-screen TV" },
                    { new Guid("b229d581-77ac-494d-9cf3-3da4d66c5aea"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1322), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1325), "migration", "Satellite channels" },
                    { new Guid("c74d127d-1c28-4dca-9dd6-3fb18c33544c"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1297), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1299), "migration", "Desk" },
                    { new Guid("c2e2bb65-cbcb-4da7-a3d7-d3b1488b678c"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1272), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1274), "migration", "Sofa" },
                    { new Guid("7d521c86-2059-4bee-bfd5-da03f85628c5"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1221), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1223), "migration", "Coffee machine" },
                    { new Guid("b77aefb0-2f61-4af4-8fb3-2612f8268e37"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1193), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1196), "migration", "Heating" },
                    { new Guid("b20eab81-6d69-4c2d-8ce4-d8c46999cfbe"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1146), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1148), "migration", "Iron" },
                    { new Guid("5c329ea1-d397-46d4-8ea5-e1981f77de0c"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1121), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1123), "migration", "Ironing facilities" },
                    { new Guid("bdc8f99e-00bb-49f6-bd8d-b9816f1939e8"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1094), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1097), "migration", "Safe" },
                    { new Guid("2944a569-258f-4800-8a6d-69128adbe73f"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1063), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1065), "migration", "Air conditioning" },
                    { new Guid("f143dc83-ceb0-4bd8-81cb-ff1f451fb35f"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1037), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1039), "migration", "Minibar" },
                    { new Guid("7d240db5-6b3f-48e8-ba4c-f3a253d1eac2"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1008), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1010), "migration", "Wardrobe or closet" },
                    { new Guid("24bea6a2-dd08-4146-87fe-2c54a4e60dec"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(936), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(953), "migration", "Linens" },
                    { new Guid("a6a77b5c-d9e7-4e1d-91d9-d9cfe6023e3b"), new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1246), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 395, DateTimeKind.Local).AddTicks(1248), "migration", "Electric kettle" },
                    { new Guid("25d3d13d-01c5-467f-922f-f9eb005fef8f"), new DateTime(2020, 11, 17, 16, 14, 45, 389, DateTimeKind.Local).AddTicks(4706), "migration", new DateTime(2020, 11, 17, 16, 14, 45, 394, DateTimeKind.Local).AddTicks(7928), "migration", "Upper floors accessible by elevator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("3ec1288a-c4d6-411a-91da-a60716fd327c"), new Guid("0e93e56a-9907-48da-8b0b-33536b9e60a0") });

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
                name: "IX_Extras_ReservationId",
                table: "Extras",
                column: "ReservationId");

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
                name: "Invoices");

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
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
