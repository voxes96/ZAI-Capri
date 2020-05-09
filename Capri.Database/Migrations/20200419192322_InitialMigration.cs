using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capri.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.UniqueConstraint("AK_UserRoles_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promoters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TitlePrefix = table.Column<string>(nullable: false),
                    TitlePostfix = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ExpectedNumberOfBachelorProposals = table.Column<int>(nullable: false),
                    ExpectedNumberOfMasterProposals = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    InstituteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promoters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promoters_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promoters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TopicPolish = table.Column<string>(nullable: false),
                    TopicEnglish = table.Column<string>(nullable: false),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Specialization = table.Column<string>(nullable: true),
                    OutputData = table.Column<string>(nullable: true),
                    MaxNumberOfStudents = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StudyProfile = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    PromoterId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_Promoters_PromoterId",
                        column: x => x.PromoterId,
                        principalTable: "Promoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexNumber = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    StudyLevel = table.Column<int>(nullable: false),
                    StudyMode = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProposalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wydział Architektury" },
                    { 9, "Wydział Inżynierii Zarządzania" },
                    { 8, "Wydział Inżynierii Transportu" },
                    { 7, "Wydział Informatyki" },
                    { 6, "Wydział Fizyki Technicznej" },
                    { 10, "Wydział Technologii Chemicznej" },
                    { 4, "Wydział Elektroniki i Telekomunikacji" },
                    { 3, "Wydział Budowy Maszyn i Zarządzania" },
                    { 2, "Wydział Budownictwa i Inżynierii Środowiska" },
                    { 5, "Wydział Elektryczny" }
                });

            migrationBuilder.InsertData(
                table: "Institutes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Instytut Informatyki" },
                    { 9, "Instytut Mechaniki Stosowanej" },
                    { 8, "Instytut Elektrotechniki i Elektroniki Przemysłowej" },
                    { 7, "Instytut Chemii i Elektrochemii Technicznej" },
                    { 6, "Instytut Inżynierii Środowiska" },
                    { 5, "Instytut Inżynierii Lądowej" },
                    { 4, "Instytut Technologii Materiałów" },
                    { 3, "Instytut Matematyki" },
                    { 2, "Instytut Technologii Mechanicznej" },
                    { 10, "Instytut Technologii i Inżynierii Chemicznej" },
                    { 11, "Instytut Architektury i Planowania Przestrzennego" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "41d0add9-0194-405b-8e1a-4341afed0a6d", "Dean", "Dean" },
                    { 3, "8df094d5-036c-4107-8376-f2b0ca6c3d4a", "Student", "Student" },
                    { 2, "b92042d8-ee84-4917-8758-b693869d862c", "Promoter", "Promoter" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId", "Id" },
                values: new object[,]
                {
                    { 9, 3, 9 },
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 3, 6 },
                    { 8, 3, 8 },
                    { 27, 2, 27 },
                    { 7, 3, 7 },
                    { 25, 2, 25 },
                    { 24, 2, 24 },
                    { 26, 2, 26 },
                    { 22, 2, 22 },
                    { 19, 2, 19 },
                    { 18, 2, 18 },
                    { 23, 2, 23 },
                    { 16, 2, 16 },
                    { 15, 2, 15 },
                    { 14, 2, 14 },
                    { 17, 2, 17 },
                    { 12, 2, 12 },
                    { 11, 2, 11 },
                    { 10, 3, 10 },
                    { 20, 2, 20 },
                    { 21, 2, 21 },
                    { 13, 2, 13 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 27, 0, "b27fbf5f-bede-4f80-b72b-7bd61776e8ac", "promoter10@gmail.com", true, false, null, "PROMOTER10@GMAIL.COM", "PROMOTER10@GMAIL.COM", "AQAAAAEAACcQAAAAELxAsKkrw0M0/66fPnnMbG0JQXhmYm4DBPdkSSav012zXzSXLGlCVVBE13SvYWRvjA==", null, false, "", false, "promoter10@gmail.com" },
                    { 26, 0, "9d38dff1-c838-43bc-bd36-c8b2ae9d7277", "krzysztof.alejski@put.poznan.pl", true, false, null, "KRZYSZTOF.ALEJSKI@PUT.POZNAN.PL", "KRZYSZTOF.ALEJSKI@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEJRBr0S8Tc1qBqmQCKcHOCy5j4J9rGdjptRkmFqAXvQej7UekTz/an9fowBECwVMbQ==", null, false, "", false, "krzysztof.alejski@put.poznan.pl" },
                    { 25, 0, "c32b55ad-5c24-448b-b478-a75e0a62c4e4", "katarzyna.adamska@put.poznan.pl", true, false, null, "KATARZYNA.ADAMSKA@PUT.POZNAN.PL", "KATARZYNA.ADAMSKA@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEAUlOeyCbeXvQD9LEpM8AXhBGRWx/pHeHi/5gsRoRyfc518Wjt52D8h2OKCvGgayGQ==", null, false, "", false, "katarzyna.adamska@put.poznan.pl" },
                    { 24, 0, "76bbc418-e788-4dc9-bd2f-50941337014b", "promoter9@gmail.com", true, false, null, "PROMOTER9@GMAIL.COM", "PROMOTER9@GMAIL.COM", "AQAAAAEAACcQAAAAEAXe38QabvJO1r6inTlexj/GidJCRwegxjH6dqvj/lescizec+hSqKqVeYrRq/B5rQ==", null, false, "", false, "promoter9@gmail.com" },
                    { 23, 0, "96f07ae2-7f3f-41dc-b01f-6d5a9514bb39", "promoter8@gmail.com", true, false, null, "PROMOTER8@GMAIL.COM", "PROMOTER8@GMAIL.COM", "AQAAAAEAACcQAAAAEPhPBCutkgCxrD17na0LSyEpqSCzUmjQgyUS64ka594u0XjWvHHbR8pLqJPi39vPsg==", null, false, "", false, "promoter8@gmail.com" },
                    { 22, 0, "9b7283f1-dc90-4e9e-b7d6-f3bd9359b3a6", "promoter7@gmail.com", true, false, null, "PROMOTER7@GMAIL.COM", "PROMOTER7@GMAIL.COM", "AQAAAAEAACcQAAAAEPE2T7eD9yu1iE7phpjoCCAGv3N7teCfoSj8JyZZQMR+CzcyZ31om9arwqf0pcoIqA==", null, false, "", false, "promoter7@gmail.com" },
                    { 21, 0, "4145611f-6124-4a0d-bd3e-1840bb255201", "promoter6@gmail.com", true, false, null, "PROMOTER6@GMAIL.COM", "PROMOTER6@GMAIL.COM", "AQAAAAEAACcQAAAAEO3lv2iRTDw/ep0nGtR6YOTU6IEdB7WpV7+ArJX+3zZV1bl9R24tI64NE4s1hwcIrQ==", null, false, "", false, "promoter6@gmail.com" },
                    { 20, 0, "410f3886-3c38-4462-a531-ddbb7e98d796", "promoter5@gmail.com", true, false, null, "PROMOTER5@GMAIL.COM", "PROMOTER5@GMAIL.COM", "AQAAAAEAACcQAAAAEIp5/DGct9cmc1OZW7jxEjlKXOADcFYD5BwTsLLKtCcSxB5rjm8LXeywwFFA9CFXmw==", null, false, "", false, "promoter5@gmail.com" },
                    { 18, 0, "9ef8f764-3aac-4750-b773-6916c520c511", "promoter3@gmail.com", true, false, null, "PROMOTER3@GMAIL.COM", "PROMOTER3@GMAIL.COM", "AQAAAAEAACcQAAAAENB4L9yArr6kOKU9+aAe+8WoaN8F24tWNuywE43tH0PXOXccQwgu1nNpiJeajYitxA==", null, false, "", false, "promoter3@gmail.com" },
                    { 17, 0, "674c06a5-f630-49ab-a381-b8c3e955b2cc", "promoter2@gmail.com", true, false, null, "PROMOTER2@GMAIL.COM", "PROMOTER2@GMAIL.COM", "AQAAAAEAACcQAAAAELoYrYuzSEnhgUjlED8u1woQ9uwg3Y+oQ4C1MzlwKSNj1cblOf71AMuYBpF18+YLOw==", null, false, "", false, "promoter2@gmail.com" },
                    { 1, 0, "7ca4a632-c6a3-40c3-9629-f0373a520ab0", "dean1@gmail.com", true, false, null, "DEAN1@GMAIL.COM", "DEAN1@GMAIL.COM", "AQAAAAEAACcQAAAAEN05YFr0RVhgghahgWFbnMwDWtALatuA1yKXPll2ncihG8Ucn049uxB5QxEtCeBexw==", null, false, "", false, "dean1@gmail.com" },
                    { 2, 0, "5b2eb0cc-51e7-4d79-b402-8e7b8af252fc", "dean2@gmail.com", true, false, null, "DEAN2@GMAIL.COM", "DEAN2@GMAIL.COM", "AQAAAAEAACcQAAAAEEw5ttGdVcZVI4+IsBDxa9t4/eXCuLjB1rF1w1mKH8H5cBms9gEdnuXVQcflB/Q7mg==", null, false, "", false, "dean2@gmail.com" },
                    { 3, 0, "1c2500a1-2960-4bb9-a69f-76461e7f065e", "dean3@gmail.com", true, false, null, "DEAN3@GMAIL.COM", "DEAN3@GMAIL.COM", "AQAAAAEAACcQAAAAEL/bSsP+RqoSI/TNwXveBNx8tlCHojgk+q90v8xb2FAkWnsymBaebAKRr8T9v2vj5A==", null, false, "", false, "dean3@gmail.com" },
                    { 4, 0, "89e3d072-dcaa-45e8-8d61-5542c1be32d9", "dean4@gmail.com", true, false, null, "DEAN4@GMAIL.COM", "DEAN4@GMAIL.COM", "AQAAAAEAACcQAAAAEK7fLkm6fBLfK1RBfuUGTzzWgHM040yTVnrDf3+xwC1QnYjY/VI+LqeiVAW13f6+yg==", null, false, "", false, "dean4@gmail.com" },
                    { 5, 0, "198ec1f1-c9e6-4de7-be66-8866194db9fe", "dean5@gmail.com", true, false, null, "DEAN5@GMAIL.COM", "DEAN5@GMAIL.COM", "AQAAAAEAACcQAAAAEINtnZA0JCfVPnm4ZhldP9F2Hj8VJvtejSXGILaTz25hL79nx3oaye0aHj6RUabMqA==", null, false, "", false, "dean5@gmail.com" },
                    { 6, 0, "09b7ced5-1578-4754-9b42-40e837805992", "student1@gmail.com", true, false, null, "STUDENT1@GMAIL.COM", "STUDENT1@GMAIL.COM", "AQAAAAEAACcQAAAAECHPtNIJRn70x4qcbCR8lr4qHm2avAkH+DRY5OyY6JVMduhxXHg8LtMrAkI2YUO+JQ==", null, false, "", false, "student1@gmail.com" },
                    { 7, 0, "4f06fcab-67db-4d56-bcd8-29a140a216b6", "student2@gmail.com", true, false, null, "STUDENT2@GMAIL.COM", "STUDENT2@GMAIL.COM", "AQAAAAEAACcQAAAAENomWq+TZGBtoK/orXtHnuFIvcJMzABSN09Mwf3buK3ugDHLKJOJAumd2ocDgnWEdQ==", null, false, "", false, "student2@gmail.com" },
                    { 8, 0, "6d49706e-71ad-4c27-9b1d-78bd2540fc83", "student3@gmail.com", true, false, null, "STUDENT3@GMAIL.COM", "STUDENT3@GMAIL.COM", "AQAAAAEAACcQAAAAEMXW9sDAmnE9xcHW7FbWR3sRJy2FYoBfRSuosbNYZ8/bQLf+RaCkURwi9hraiI2ncQ==", null, false, "", false, "student3@gmail.com" },
                    { 9, 0, "0e41fb98-c2ef-406b-8e97-7847861228da", "student4@gmail.com", true, false, null, "STUDENT4@GMAIL.COM", "STUDENT4@GMAIL.COM", "AQAAAAEAACcQAAAAEDyr83kWdFXB3xt/oH1YYi/3c5g0IOot4N0eSiaN2RazVKjNU0t3Njfw7/bPoRzLvw==", null, false, "", false, "student4@gmail.com" },
                    { 11, 0, "14ec6e61-c578-4944-a77f-ff6cef5f158a", "irmina.maslowska@put.poznan.pl", true, false, null, "IRMINA.MASLOWSKA@PUT.POZNAN.PL", "IRMINA.MASLOWSKA@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEBHujWnSOkufwhBZGDRJGIzHAvhLFsLVfLPyZTc1VWqewMOQheFtLiWZNiBOUSI+JA==", null, false, "", false, "irmina.maslowska@put.poznan.pl" },
                    { 12, 0, "fdcca6b7-d398-491d-9133-417e5d7bc78b", "bartlomiej.predki@put.poznan.pl", true, false, null, "BARTLOMIEJ.PREDKI@PUT.POZNAN.PL", "BARTLOMIEJ.PREDKI@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEJPKufbtkymoZ2KUBtTbeVOAeXA3o7PPvdWgklbrEpio47wMBGYOhiJsSuko8WGkog==", null, false, "", false, "bartlomiej.predki@put.poznan.pl" },
                    { 13, 0, "fb1ae0a9-ef97-4b99-93e5-cf84db36d8fd", "milosz.kadzinski@put.poznan.pl", true, false, null, "MILOSZ.KADZINSKI@PUT.POZNAN.PL", "MILOSZ.KADZINSKI@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEBszOXb3hn+vdj7x2oYjVb0jnQaKTIqwWq0i38jhdBSke+EzDcQPbGu8iksWAgHOlQ==", null, false, "", false, "milosz.kadzinski@put.poznan.pl" },
                    { 14, 0, "a77629ad-00d3-4b7d-83c3-1d01372a3214", "wojciech.kotlowski@put.poznan.pl", true, false, null, "WOJCIECH.KOTLOWSKI@PUT.POZNAN.PL", "WOJCIECH.KOTLOWSKI@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEIygijbGeK3QhEWpMICTUmA3K3V5a2BxZLRO9GYDwJ6ifQGMgvotNlBYW/8xT/ybNA==", null, false, "", false, "wojciech.kotlowski@put.poznan.pl" },
                    { 15, 0, "3c1854c8-024c-43f8-962c-544de60ed930", "jerzy.nawrocki@put.poznan.pl", true, false, null, "JERZY.NAWROCKI@PUT.POZNAN.PL", "JERZY.NAWROCKI@PUT.POZNAN.PL", "AQAAAAEAACcQAAAAEDSVbGOQuWdLLm5NVhxAbkhSxLEnYoeuC209rlWUd4iNPxZWjyG9iPHhJRPMoL+STQ==", null, false, "", false, "jerzy.nawrocki@put.poznan.pl" },
                    { 16, 0, "69ec60cd-c215-4fa2-8abd-c936bed3984e", "promoter1@gmail.com", true, false, null, "PROMOTER1@GMAIL.COM", "PROMOTER1@GMAIL.COM", "AQAAAAEAACcQAAAAEK7+9PovBm+SCwKU16An3O1+lD3QL5MoEE43MaJ1U+P3aHhhK5I+Tr9KVRwXG8BxMA==", null, false, "", false, "promoter1@gmail.com" },
                    { 19, 0, "1bcaf670-1373-472e-8d7a-5dedb4e763e4", "promoter4@gmail.com", true, false, null, "PROMOTER4@GMAIL.COM", "PROMOTER4@GMAIL.COM", "AQAAAAEAACcQAAAAEEhDNuPEmxk+AAZ1WwoY6kS5ttSm3PhAZfY54cv3NkSvqQ4Evt/N0clera1tVrcbaA==", null, false, "", false, "promoter4@gmail.com" },
                    { 10, 0, "1e8682b9-1050-4e60-80a7-728571002859", "student5@gmail.com", true, false, null, "STUDENT5@GMAIL.COM", "STUDENT5@GMAIL.COM", "AQAAAAEAACcQAAAAELd2H6oLX1NZWgr+9RLJ6DYt+phGkVwrH6lAoGoM18HKPwBKzDHsSZjkaXI913cDVA==", null, false, "", false, "student5@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "FacultyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Architektura" },
                    { 30, 10, "Inżynieria Farmaceutyczna" },
                    { 29, 10, "Technologie Ochrony Środowiska" },
                    { 28, 10, "Technologia Chemiczna / Chemical Technology" },
                    { 27, 10, "Inżynieria Chemiczna i Procesowa" },
                    { 25, 9, "Inżynieria Zarządzania" },
                    { 24, 9, "Logistyka" },
                    { 23, 8, "Lotnictwo i Kosmonautyka" },
                    { 22, 8, "Transport" },
                    { 21, 8, "Konstrukcja i Eksploatacja Środków Transportu" },
                    { 20, 7, "Sztuczna Inteligencja / Artificial Intelligence" },
                    { 19, 7, "Bioinformatyka" },
                    { 18, 7, "Informatyka" },
                    { 17, 6, "Fizyka Techniczna" },
                    { 16, 6, "Edukacja Techniczno-Informatyczna" },
                    { 26, 9, "Inżynieria Bezpieczeństwa" },
                    { 14, 5, "Energetyka" },
                    { 15, 5, "Matematyka w Technice" },
                    { 2, 1, "Architektura Wnętrz" },
                    { 3, 2, "Budownictwo" },
                    { 5, 3, "Inżynieria Biomedyczna" },
                    { 6, 3, "Inżynieria Materiałowa" },
                    { 7, 3, "Mechanika i Budowa Maszyn" },
                    { 4, 2, "Inżynieria Środowiska" },
                    { 9, 3, "Zarządzanie i Inżynieria Produkcji" },
                    { 10, 4, "Elektronika i Telekomunikacja" },
                    { 11, 4, "Teleinformatyka" },
                    { 12, 5, "Automatyka i Robotyka" },
                    { 13, 5, "Elektrotechnika" },
                    { 8, 3, "Mechatronika" }
                });

            migrationBuilder.InsertData(
                table: "Promoters",
                columns: new[] { "Id", "ExpectedNumberOfBachelorProposals", "ExpectedNumberOfMasterProposals", "FirstName", "InstituteId", "LastName", "TitlePostfix", "TitlePrefix", "UserId" },
                values: new object[,]
                {
                    { 16, 1, 1, "Katarzyna", 10, "Adamska", "", "dr inż.", 25 },
                    { 2, 1000, 1000, "PromoterN2", 2, "PromoterS2", "", "dr inż.", 17 },
                    { 9, 1000, 1000, "PromoterN9", 9, "PromoterS9", "", "dr inż.", 24 },
                    { 8, 1000, 1000, "PromoterN8", 8, "PromoterS8", "", "dr inż.", 23 },
                    { 7, 1000, 1000, "PromoterN7", 7, "PromoterS7", "", "dr inż.", 22 },
                    { 6, 1000, 1000, "PromoterN6", 6, "PromoterS6", "", "dr inż.", 21 },
                    { 5, 1000, 1000, "PromoterN5", 5, "PromoterS5", "", "dr inż.", 20 },
                    { 4, 1000, 1000, "PromoterN4", 4, "PromoterS4", "", "dr inż.", 19 },
                    { 3, 1000, 1000, "PromoterN3", 3, "PromoterS3", "", "dr inż.", 18 },
                    { 1, 1000, 1000, "PromoterN1", 1, "PromoterS1", "", "dr inż.", 16 },
                    { 10, 1000, 1000, "PromoterN10", 10, "PromoterS10", "", "dr inż.", 27 },
                    { 14, 2, 1, "Wojciech", 1, "Kotłowski", "", "dr hab. inż.", 14 },
                    { 13, 2, 1, "Miłosz", 1, "Kadziński", "", "dr hab. inż.", 13 },
                    { 12, 2, 1, "Bartłomiej", 1, "Prędki", "", "dr inż.", 12 },
                    { 11, 2, 1, "Irmina", 1, "Masłowska", "", "dr inż.", 11 },
                    { 17, 3, 2, "Krzysztof", 10, "Alejski", "prof. PP", "dr hab. inż.", 26 },
                    { 15, 2, 1, "Jerzy", 1, "Nawrocki", "prof. PP", "dr hab inż.", 15 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "IndexNumber", "LastName", "ProposalId", "Semester", "StudyLevel", "StudyMode", "UserId" },
                values: new object[,]
                {
                    { 5, "Jan", 132205, "Nowak", null, 6, 0, 0, 10 },
                    { 3, "Szymon", 132203, "Wójcik", null, 6, 0, 0, 8 },
                    { 2, "Marcin", 132202, "Zawadzki", null, 6, 0, 0, 7 },
                    { 1, "Filip", 132201, "Cegielski", null, 6, 0, 0, 6 },
                    { 4, "Andrzej", 132204, "Król", null, 6, 0, 0, 9 }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "CourseId", "Description", "Level", "MaxNumberOfStudents", "Mode", "OutputData", "PromoterId", "Specialization", "StartingDate", "Status", "StudyProfile", "TopicEnglish", "TopicPolish" },
                values: new object[,]
                {
                    { 2, 18, "Celem pracy jest zaimplementowanie metod Electre 1s i Electre TRI jako aplikacji desktopowych.....", 0, 4, 0, "Brak danych", 11, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Implementation of chosen methods from Electre family", "Implementacja wybranych metod z rodziny Electre" },
                    { 3, 18, "Opis.....", 0, 4, 0, "Brak danych", 12, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Implementation of decision support methods based on utility function (UTA, Assess)", "Implementacja metod wspomagania decyzji opartych na funkcji użyteczności (UTA, Assess)" },
                    { 4, 18, "Opis.....", 0, 4, 0, "Brak danych", 13, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Extension of diviz platform", "Rozszerzenie platformy diviz o metody wielokryterialnego wspomagania decyzji oparte na różnych modelach preferencji" },
                    { 5, 18, "Opis.....", 0, 4, 0, "Brak danych", 14, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Style transfering using a neural network", "Transfer stylu przy użyciu sieci neuronowej" },
                    { 1, 18, "Opis.....", 0, 4, 0, "Brak danych", 15, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Capri 2 - system for managing diploma topic cards", "Capri 2 - system do zarządzania kartami tematów prac dyplomowych" },
                    { 6, 27, "Praca będzie polegać na własnoręcznym upakowaniu kolumny chromatograficznej, doborze odpowiednich warunków pomiaru w celu wyznaczenia podstawowych parametrów dla niestandardowych kolumn do HPLC.", 0, 4, 0, "Brak danych", 16, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "No title", "Wyznaczenie parametrów kolumny chromatograficznej za pomocą odwróconej chromatografii cieczowej" },
                    { 7, 27, "Brak opisu", 1, 4, 0, "Brak danych", 17, "-", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "No title", "Analiza i projektowanie procesów wydzielania produktów otrzymywanych w procesie biokonwersji surowców odnawialnych" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyId",
                table: "Courses",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Name",
                table: "Faculties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_Name",
                table: "Institutes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promoters_InstituteId",
                table: "Promoters",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Promoters_UserId",
                table: "Promoters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_CourseId",
                table: "Proposals",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_PromoterId",
                table: "Proposals",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IndexNumber",
                table: "Students",
                column: "IndexNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProposalId",
                table: "Students",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Promoters");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Institutes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
