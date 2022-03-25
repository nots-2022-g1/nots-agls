using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class KeywordCreatedModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabeledData_DataSets_DataSetId",
                table: "LabeledData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataSets",
                table: "DataSets");

            migrationBuilder.RenameTable(
                name: "DataSets",
                newName: "Datasets");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Keywords",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Keywords",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datasets",
                table: "Datasets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabeledData_Datasets_DataSetId",
                table: "LabeledData",
                column: "DataSetId",
                principalTable: "Datasets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabeledData_Datasets_DataSetId",
                table: "LabeledData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Datasets",
                table: "Datasets");

            migrationBuilder.RenameTable(
                name: "Datasets",
                newName: "DataSets");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Keywords",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Keywords",
                newName: "CreatedDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataSets",
                table: "DataSets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabeledData_DataSets_DataSetId",
                table: "LabeledData",
                column: "DataSetId",
                principalTable: "DataSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
