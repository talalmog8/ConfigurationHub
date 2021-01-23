using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigurationHub.Data.Migrations
{
    public partial class SystemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_ConfigSystems_SystemId",
                table: "Configs");

            migrationBuilder.RenameColumn(
                name: "MicroserviceName",
                table: "ConfigSystems",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigSystems_MicroserviceName",
                table: "ConfigSystems",
                newName: "IX_ConfigSystems_Name");

            migrationBuilder.RenameColumn(
                name: "SystemId",
                table: "Configs",
                newName: "MicroserviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_SystemId",
                table: "Configs",
                newName: "IX_Configs_MicroserviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_ConfigSystems_MicroserviceId",
                table: "Configs",
                column: "MicroserviceId",
                principalTable: "ConfigSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_ConfigSystems_MicroserviceId",
                table: "Configs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ConfigSystems",
                newName: "MicroserviceName");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigSystems_Name",
                table: "ConfigSystems",
                newName: "IX_ConfigSystems_MicroserviceName");

            migrationBuilder.RenameColumn(
                name: "MicroserviceId",
                table: "Configs",
                newName: "SystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_MicroserviceId",
                table: "Configs",
                newName: "IX_Configs_SystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_ConfigSystems_SystemId",
                table: "Configs",
                column: "SystemId",
                principalTable: "ConfigSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
