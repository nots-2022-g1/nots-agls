using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    public partial class LabeledData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabeledData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UseFul = table.Column<bool>(type: "boolean", nullable: false),
                    GitCommitHash = table.Column<string>(type: "text", nullable: false),
                    DataSetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabeledData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabeledData_Commits_GitCommitHash",
                        column: x => x.GitCommitHash,
                        principalTable: "Commits",
                        principalColumn: "Hash");
                    table.ForeignKey(
                        name: "FK_LabeledData_DataSets_DataSetId",
                        column: x => x.DataSetId,
                        principalTable: "DataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabeledData_DataSetId",
                table: "LabeledData",
                column: "DataSetId");

            migrationBuilder.CreateIndex(
                name: "IX_LabeledData_GitCommitHash",
                table: "LabeledData",
                column: "GitCommitHash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabeledData");

            migrationBuilder.DropTable(
                name: "DataSets");
        }
    }
}
