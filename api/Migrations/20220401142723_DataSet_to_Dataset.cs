using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class DataSet_to_Dataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabeledData_Datasets_DataSetId",
                table: "LabeledData");

            migrationBuilder.RenameColumn(
                name: "DataSetId",
                table: "LabeledData",
                newName: "DatasetId");

            migrationBuilder.RenameIndex(
                name: "IX_LabeledData_DataSetId",
                table: "LabeledData",
                newName: "IX_LabeledData_DatasetId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabeledData_Datasets_DatasetId",
                table: "LabeledData",
                column: "DatasetId",
                principalTable: "Datasets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabeledData_Datasets_DatasetId",
                table: "LabeledData");

            migrationBuilder.RenameColumn(
                name: "DatasetId",
                table: "LabeledData",
                newName: "DataSetId");

            migrationBuilder.RenameIndex(
                name: "IX_LabeledData_DatasetId",
                table: "LabeledData",
                newName: "IX_LabeledData_DataSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabeledData_Datasets_DataSetId",
                table: "LabeledData",
                column: "DataSetId",
                principalTable: "Datasets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
