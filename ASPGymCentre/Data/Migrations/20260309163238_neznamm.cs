using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPGymCentre.Data.Migrations
{
    /// <inheritdoc />
    public partial class neznamm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanCategory_PlansCategoriesId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientsId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClientsId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ClientsId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "PlansCategoriesId",
                table: "Plans",
                newName: "PlanCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_PlansCategoriesId",
                table: "Plans",
                newName: "IX_Plans_PlanCategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanCategory_PlanCategoryID",
                table: "Plans",
                column: "PlanCategoryID",
                principalTable: "PlanCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientId",
                table: "Reservations",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanCategory_PlanCategoryID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "PlanCategoryID",
                table: "Plans",
                newName: "PlansCategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_PlanCategoryID",
                table: "Plans",
                newName: "IX_Plans_PlansCategoriesId");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ClientsId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientsId",
                table: "Reservations",
                column: "ClientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanCategory_PlansCategoriesId",
                table: "Plans",
                column: "PlansCategoriesId",
                principalTable: "PlanCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientsId",
                table: "Reservations",
                column: "ClientsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
