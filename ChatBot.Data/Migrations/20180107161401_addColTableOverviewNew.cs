using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Data.Migrations
{
    public partial class addColTableOverviewNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NEWS_USERS",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "USERS",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NEWS_USERS",
                table: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.DropColumn(
                name: "USERS",
                table: "OW_OVERVIEW_ECOMMERCE");
        }
    }
}
