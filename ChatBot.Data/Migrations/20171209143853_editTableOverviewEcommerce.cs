using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Data.Migrations
{
    public partial class editTableOverviewEcommerce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PROJECT_ID",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PROJECT_ID",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
