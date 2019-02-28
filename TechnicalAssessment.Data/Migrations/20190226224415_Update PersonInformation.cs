using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TechnicalAssessment.Data.Migrations
{
    public partial class UpdatePersonInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesInformation_AspNetUsers_UserId",
                table: "PeoplesInformation");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PeoplesInformation",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesInformation_UserId",
                table: "PeoplesInformation",
                newName: "IX_PeoplesInformation_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesInformation_AspNetUsers_AppUserId",
                table: "PeoplesInformation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesInformation_AspNetUsers_AppUserId",
                table: "PeoplesInformation");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "PeoplesInformation",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesInformation_AppUserId",
                table: "PeoplesInformation",
                newName: "IX_PeoplesInformation_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesInformation_AspNetUsers_UserId",
                table: "PeoplesInformation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
