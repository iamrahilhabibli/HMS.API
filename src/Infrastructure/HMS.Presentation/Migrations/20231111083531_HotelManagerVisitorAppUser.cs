using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Persistence.Migrations
{
    public partial class HotelManagerVisitorAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HotelManagers_ManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Visitors_VisitorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VisitorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Visitors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "HotelManagers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_AppUserId",
                table: "Visitors",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelManagers_AppUserId",
                table: "HotelManagers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelManagers_AspNetUsers_AppUserId",
                table: "HotelManagers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_AspNetUsers_AppUserId",
                table: "Visitors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelManagers_AspNetUsers_AppUserId",
                table: "HotelManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_AspNetUsers_AppUserId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_AppUserId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_HotelManagers_AppUserId",
                table: "HotelManagers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "HotelManagers");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitorId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ManagerId",
                table: "AspNetUsers",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VisitorId",
                table: "AspNetUsers",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HotelManagers_ManagerId",
                table: "AspNetUsers",
                column: "ManagerId",
                principalTable: "HotelManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Visitors_VisitorId",
                table: "AspNetUsers",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }
    }
}
