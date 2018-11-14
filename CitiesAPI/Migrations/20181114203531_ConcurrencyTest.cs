using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CitiesAPI.Migrations
{
    public partial class ConcurrencyTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Cities",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Cities");
        }
    }
}
