using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEquipamentos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentHistoryForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_EquipmentId",
                table: "EquipmentHistories",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistories_Equipments_EquipmentId",
                table: "EquipmentHistories",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistories_Equipments_EquipmentId",
                table: "EquipmentHistories");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistories_EquipmentId",
                table: "EquipmentHistories");
        }
    }
}
