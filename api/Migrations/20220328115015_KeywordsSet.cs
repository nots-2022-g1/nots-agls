using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    public partial class KeywordsSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeywordSetId",
                table: "Keywords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KeywordSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordSets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_KeywordSetId",
                table: "Keywords",
                column: "KeywordSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_KeywordSets_KeywordSetId",
                table: "Keywords",
                column: "KeywordSetId",
                principalTable: "KeywordSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_KeywordSets_KeywordSetId",
                table: "Keywords");

            migrationBuilder.DropTable(
                name: "KeywordSets");

            migrationBuilder.DropIndex(
                name: "IX_Keywords_KeywordSetId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "KeywordSetId",
                table: "Keywords");
        }
    }
}
