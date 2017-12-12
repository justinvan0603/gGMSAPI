using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Data.Migrations
{
    public partial class addColToTableOverviewEcommerce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DIMENSIONS",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DOMAIN",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VERSION",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DIMENSIONS",
                table: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.DropColumn(
                name: "DOMAIN",
                table: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.DropColumn(
                name: "VERSION",
                table: "OW_OVERVIEW_ECOMMERCE");
        }
    }
}
