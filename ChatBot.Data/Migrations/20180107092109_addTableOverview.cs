using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatBot.Data.Migrations
{
    public partial class addTableOverview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OW_OVERVIEW_ECOMMERCE",
                table: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.RenameTable(
                name: "OW_OVERVIEW_ECOMMERCE",
                newName: "OW_PRODUCT_PERFORMACE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OW_PRODUCT_PERFORMACE",
                table: "OW_PRODUCT_PERFORMACE",
                column: "OVERVIEW_ECOMMERCE_ID");

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
                    DOMAIN = table.Column<string>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    PAGEVIEWS = table.Column<string>(nullable: true),
                    PRODUCTADDSTOCART = table.Column<string>(nullable: true),
                    PRODUCTCHECKOUTS = table.Column<string>(nullable: true),
                    PRODUCTDETAILVIEWS = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    SESSIONS = table.Column<string>(nullable: true),
                    TIMEONPAGE = table.Column<string>(nullable: true),
                    TRANSACTIONREVENUE = table.Column<string>(nullable: true),
                    VERSION = table.Column<string>(nullable: true),
                    VERSION_INT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_OVERVIEW_ECOMMERCE", x => x.OVERVIEW_ECOMMERCE_ID);
                });

            migrationBuilder.CreateTable(
                name: "OW_TRAFFIC_SOURCE_ECOMMERCE",
                columns: table => new
                {
                    TRAFFIC_SOURCE_ECOMMERCE_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPROVE_DT = table.Column<DateTime>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<string>(nullable: true),
                    CREATE_DT = table.Column<DateTime>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    EXITS = table.Column<string>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    MEDIUM = table.Column<string>(nullable: true),
                    PAGEVIEWS = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    SESSIONDURATION = table.Column<string>(nullable: true),
                    SESSIONS = table.Column<string>(nullable: true),
                    SOURCE = table.Column<string>(nullable: true),
                    TRANSACTIONREVENUE = table.Column<string>(nullable: true),
                    TRANSACTIONS = table.Column<string>(nullable: true),
                    VERSION = table.Column<string>(nullable: true),
                    VERSION_INT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_TRAFFIC_SOURCE_ECOMMERCE", x => x.TRAFFIC_SOURCE_ECOMMERCE_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.DropTable(
                name: "OW_TRAFFIC_SOURCE_ECOMMERCE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OW_PRODUCT_PERFORMACE",
                table: "OW_PRODUCT_PERFORMACE");

            migrationBuilder.RenameTable(
                name: "OW_PRODUCT_PERFORMACE",
                newName: "OW_OVERVIEW_ECOMMERCE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OW_OVERVIEW_ECOMMERCE",
                table: "OW_OVERVIEW_ECOMMERCE",
                column: "OVERVIEW_ECOMMERCE_ID");
        }
    }
}
