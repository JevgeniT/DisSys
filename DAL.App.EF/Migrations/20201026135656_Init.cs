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
                    FacilityId = table.Column<Guid>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extras_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
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
                    Name = table.Column<string>(nullable: true),
                    AdultsOccupancy = table.Column<int>(nullable: false),
                    ChildOccupancy = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AllowSmoking = table.Column<bool>(nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
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
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
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
                    { new Guid("37776cf1-aa12-4d36-a96c-44968b4c99d2"), "74a6433e-3526-45f9-8bdb-54e978910c63", "gust", "Guest" },
                    { new Guid("8084c905-6aa5-4054-9bf9-75083bf4d7db"), "4bddd23d-2ea2-4cfd-8d0e-bd32ce076b23", "host", "Host" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0d755c28-0424-4925-bff5-b4c2d5dcab9c"), 0, "4fb94b12-49b2-4719-afa7-80ba141c84a3", "host@host.com", true, "host", "host", false, null, "HOST@HOST.COM", "HOST@HOST.COM", "AQAAAAEAACcQAAAAEEERAiWN2QFN19QTciy+57I1tyLGt1zDcJ5OgzphisCueRc0y5qoKqo5NtYPFQwNEA==", null, false, "", false, "host@host.com" },
                    { new Guid("bda316be-f6fd-4ba6-bd2f-1c7fe3f8fa3c"), 0, "25b617db-503f-4209-944b-d661dd4560ee", "user@user.com", true, "user", "user", false, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAEAACcQAAAAEPf+tPpfiH+xcvsuWmVUMlrFE+dk5YBziis3nRUVg7821vAYm3KAXN5rj5kXNzXGpQ==", null, false, "", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "ChangedAt", "ChangedBy", "CreatedAt", "CreatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("e2e19eab-190f-46d7-b668-653086b02d57"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4421), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4423), null, "Free Wi-Fi" },
                    { new Guid("801a7825-b51d-4732-9e39-845cafb5ca77"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4396), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4398), null, "Wake-up service" },
                    { new Guid("fa63ac8b-18ff-4f71-b872-ba0bca876077"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4369), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4371), null, "Outdoor furniture" },
                    { new Guid("825d1e60-0004-4789-991b-f600265db853"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4343), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4346), null, "Balcony" },
                    { new Guid("4df67468-fbeb-4d37-9368-b0c3feb29911"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4319), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4321), null, "Flat-screen TV" },
                    { new Guid("b6704d84-6b36-494c-ba26-4336a98a0b16"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4294), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4296), null, "Satellite channels" },
                    { new Guid("c96117ce-50f3-45cf-9abb-f5fdfc848edf"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4268), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4270), null, "Desk" },
                    { new Guid("3625543a-5738-47fc-9efc-348be88ab296"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4243), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4245), null, "Sofa" },
                    { new Guid("1bcc091b-0405-4be4-afb1-d320429c0ff4"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4192), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4194), null, "Coffee machine" },
                    { new Guid("76876efc-0f34-4ab6-a966-29e19f433fed"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4165), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4167), null, "Heating" },
                    { new Guid("af79445f-48d3-4537-8f0d-bf2d1c063a4d"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4140), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4142), null, "Iron" },
                    { new Guid("a6722d67-4c0b-491d-8d5e-49fd497329e1"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4115), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4117), null, "Ironing facilities" },
                    { new Guid("59fefab2-c7e8-4e29-ba13-6a6c0ccbffff"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4088), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4091), null, "Safe" },
                    { new Guid("3da34d6c-dbbd-49c5-89d7-7d0bcceebd1d"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4057), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4059), null, "Air conditioning" },
                    { new Guid("6c9fe5a4-5dde-48ad-a03a-cf567466446f"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4032), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4034), null, "Minibar" },
                    { new Guid("b49c6156-9c37-499c-b07c-7c3502b42010"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4004), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4007), null, "Wardrobe or closet" },
                    { new Guid("28b20acd-fa86-4fde-9ac4-fabc9a4f0600"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(3945), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(3957), null, "Linens" },
                    { new Guid("d61218dc-b6ad-4eff-ba2e-294c97ad8073"), new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4217), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(4220), null, "Electric kettle" },
                    { new Guid("0dcc25e5-32fd-4c0d-9024-714836813240"), new DateTime(2020, 10, 26, 15, 56, 55, 748, DateTimeKind.Local).AddTicks(554), null, new DateTime(2020, 10, 26, 15, 56, 55, 753, DateTimeKind.Local).AddTicks(1157), null, "Upper floors accessible by elevator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("0d755c28-0424-4925-bff5-b4c2d5dcab9c"), new Guid("8084c905-6aa5-4054-9bf9-75083bf4d7db") });

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
                name: "IX_Extras_FacilityId",
                table: "Extras",
                column: "FacilityId");

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
