using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp2001.Migrations
{
    public partial class UseCW2Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CW1");

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "CW1",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                schema: "CW1",
                columns: table => new
                {
                    UserActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.UserActivityId);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                schema: "CW1",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityTimePreference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    MarketingLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "CW1",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations",
                schema: "CW1");

            migrationBuilder.DropTable(
                name: "UserActivities",
                schema: "CW1");

            migrationBuilder.DropTable(
                name: "UserPreferences",
                schema: "CW1");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "CW1");
        }
    }
}
