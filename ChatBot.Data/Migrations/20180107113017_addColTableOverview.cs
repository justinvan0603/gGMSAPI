using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Data.Migrations
{
    public partial class addColTableOverview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DOMAIN",
                table: "OW_TRAFFIC_SOURCE_ECOMMERCE",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PROJECT_ID",
                table: "OW_TRAFFIC_SOURCE_ECOMMERCE",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PROJECT_ID",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOMAIN",
                table: "OW_TRAFFIC_SOURCE_ECOMMERCE");

            migrationBuilder.DropColumn(
                name: "PROJECT_ID",
                table: "OW_TRAFFIC_SOURCE_ECOMMERCE");

            migrationBuilder.DropColumn(
                name: "PROJECT_ID",
                table: "OW_OVERVIEW_ECOMMERCE");
        }
    }
}
