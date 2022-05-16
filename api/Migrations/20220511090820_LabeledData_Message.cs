using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class LabeledData_Message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabeledData_Commits_GitCommitHash",
                table: "LabeledData");

            migrationBuilder.DropIndex(
                name: "IX_LabeledData_GitCommitHash",
                table: "LabeledData");

            migrationBuilder.RenameColumn(
                name: "GitCommitHash",
                table: "LabeledData",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "LabeledData",
                newName: "GitCommitHash");

            migrationBuilder.CreateIndex(
                name: "IX_LabeledData_GitCommitHash",
                table: "LabeledData",
                column: "GitCommitHash");

            migrationBuilder.AddForeignKey(
                name: "FK_LabeledData_Commits_GitCommitHash",
                table: "LabeledData",
                column: "GitCommitHash",
                principalTable: "Commits",
                principalColumn: "Hash");
        }
    }
}
