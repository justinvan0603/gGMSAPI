using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatBot.Data.Migrations
{
    public partial class addTableProductListPerformanceEcommerces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VERSION",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "OW_PRODUCTLIST_PERFORMANCE_ECOMMERCE",
                columns: table => new
                {
                    PRODUCTLIST_PERFORMANCE_ECOMMERCE_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPROVE_DT = table.Column<DateTime>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<string>(nullable: true),
                    CREATE_DT = table.Column<DateTime>(nullable: true),
                    DIMENSIONS = table.Column<string>(nullable: true),
                    DOMAIN = table.Column<string>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    ITEM_REVENUE = table.Column<string>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    PRODUCTLIST = table.Column<string>(nullable: true),
                    PRODUCT_DETAIL_VIEWS = table.Column<string>(nullable: true),
                    PROJECT_ID = table.Column<string>(nullable: true),
                    QUANTITY_ADDED_TO_CART = table.Column<string>(nullable: true),
                    QUANTITY_CHECKED_OUT = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    VERSION_INT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_PRODUCTLIST_PERFORMANCE_ECOMMERCE", x => x.PRODUCTLIST_PERFORMANCE_ECOMMERCE_ID);
                });

            migrationBuilder.CreateTable(
                name: "OW_SHOPPING_BEHAVIOR_ECOMMERCE",
                columns: table => new
                {
                    SHOPPING_BEHAVIOR_ECOMMERCE_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPROVE_DT = table.Column<DateTime>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<string>(nullable: true),
                    CREATE_DT = table.Column<DateTime>(nullable: true),
                    DIMENSIONS = table.Column<string>(nullable: true),
                    DOMAIN = table.Column<string>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    ITEM_REVENUE = table.Column<string>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    PRODUCT_DETAIL_VIEWS = table.Column<string>(nullable: true),
                    PROJECT_ID = table.Column<string>(nullable: true),
                    QUANTITY_ADDED_TO_CART = table.Column<string>(nullable: true),
                    QUANTITY_CHECKED_OUT = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    USER_TYPE = table.Column<string>(nullable: true),
                    VERSION_INT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_SHOPPING_BEHAVIOR_ECOMMERCE", x => x.SHOPPING_BEHAVIOR_ECOMMERCE_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OW_PRODUCTLIST_PERFORMANCE_ECOMMERCE");

            migrationBuilder.DropTable(
                name: "OW_SHOPPING_BEHAVIOR_ECOMMERCE");

            migrationBuilder.AlterColumn<int>(
                name: "VERSION",
                table: "OW_OVERVIEW_ECOMMERCE",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
