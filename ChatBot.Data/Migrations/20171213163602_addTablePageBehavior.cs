using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatBot.Data.Migrations
{
    public partial class addTablePageBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OW_PAGE_BEHAVIOR_ECOMMERCE",
                columns: table => new
                {
                    PAGE_BEHAVIOR_ECOMMERCE_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPROVE_DT = table.Column<DateTime>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<string>(nullable: true),
                    CREATE_DT = table.Column<DateTime>(nullable: true),
                    DIMENSIONS = table.Column<string>(nullable: true),
                    DOMAIN = table.Column<string>(nullable: true),
                    EDITOR_ID = table.Column<string>(nullable: true),
                    EDIT_DT = table.Column<DateTime>(nullable: true),
                    EXIT_RATE = table.Column<string>(nullable: true),
                    MAKER_ID = table.Column<string>(nullable: true),
                    PAGE_PATH = table.Column<string>(nullable: true),
                    PAGE_VALUE = table.Column<string>(nullable: true),
                    PAGE_VIEW = table.Column<string>(nullable: true),
                    PROJECT_ID = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    TIME_ON_PAGE = table.Column<string>(nullable: true),
                    VERSION_INT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OW_PAGE_BEHAVIOR_ECOMMERCE", x => x.PAGE_BEHAVIOR_ECOMMERCE_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OW_PAGE_BEHAVIOR_ECOMMERCE");
        }
    }
}
