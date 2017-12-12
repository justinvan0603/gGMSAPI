using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatBot.Data.Migrations
{
    public partial class addTable_OW_OVERVIEW_ECOMMERCE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OW_OVERVIEW_ECOMMERCE",
                columns: table => new
                {
                    OVERVIEW_ECOMMERCE_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPROVE_DT = table.Column<DateTime>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<string>(nullable: true),
                    CREATE_DT = table.Column<DateTime>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    ITEM_REVENUE = table.Column<string>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    PRODUCT_DETAIL_VIEWS = table.Column<string>(nullable: true),
                    PRODUCT_NAME = table.Column<string>(nullable: true),
                    PROJECT_ID = table.Column<int>(nullable: false),
                    QUANTITY_ADDED_TO_CART = table.Column<string>(nullable: true),
                    QUANTITY_CHECKED_OUT = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_OVERVIEW_ECOMMERCE", x => x.OVERVIEW_ECOMMERCE_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OW_OVERVIEW_ECOMMERCE");
        }
    }
}
