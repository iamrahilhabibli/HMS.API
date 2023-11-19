using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Persistence.Migrations
{
    public partial class HotelManagerGuidNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelManagers_Hotels_HotelId",
                table: "HotelManagers");

            migrationBuilder.DropIndex(
                name: "IX_HotelManagers_HotelId",
                table: "HotelManagers");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "HotelManagers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_HotelManagers_HotelId",
                table: "HotelManagers",
                column: "HotelId",
                unique: true,
                filter: "[HotelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelManagers_Hotels_HotelId",
                table: "HotelManagers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelManagers_Hotels_HotelId",
                table: "HotelManagers");

            migrationBuilder.DropIndex(
                name: "IX_HotelManagers_HotelId",
                table: "HotelManagers");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "HotelManagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelManagers_HotelId",
                table: "HotelManagers",
                column: "HotelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelManagers_Hotels_HotelId",
                table: "HotelManagers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
