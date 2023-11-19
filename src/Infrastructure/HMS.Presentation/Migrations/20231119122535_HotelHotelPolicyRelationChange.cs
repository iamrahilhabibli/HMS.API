using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Persistence.Migrations
{
    public partial class HotelHotelPolicyRelationChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelPolicies_PoliciesId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_PoliciesId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PoliciesId",
                table: "Hotels");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId",
                table: "HotelPolicies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HotelPolicies_HotelId",
                table: "HotelPolicies",
                column: "HotelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPolicies_Hotels_HotelId",
                table: "HotelPolicies",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPolicies_Hotels_HotelId",
                table: "HotelPolicies");

            migrationBuilder.DropIndex(
                name: "IX_HotelPolicies_HotelId",
                table: "HotelPolicies");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelPolicies");

            migrationBuilder.AddColumn<Guid>(
                name: "PoliciesId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_PoliciesId",
                table: "Hotels",
                column: "PoliciesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelPolicies_PoliciesId",
                table: "Hotels",
                column: "PoliciesId",
                principalTable: "HotelPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
