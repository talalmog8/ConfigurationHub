using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigurationHub.Data.Migrations
{
    public partial class System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_ConfigSystems_MicroserviceId",
                table: "Configs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigSystems",
                table: "ConfigSystems");

            migrationBuilder.RenameTable(
                name: "ConfigSystems",
                newName: "MicroServices");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigSystems_Name",
                table: "MicroServices",
                newName: "IX_MicroServices_Name");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "MicroServices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicroServices",
                table: "MicroServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicroServices_SystemId",
                table: "MicroServices",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_Name",
                table: "Systems",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs",
                column: "MicroserviceId",
                principalTable: "MicroServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MicroServices_Systems_SystemId",
                table: "MicroServices",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs");

            migrationBuilder.DropForeignKey(
                name: "FK_MicroServices_Systems_SystemId",
                table: "MicroServices");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MicroServices",
                table: "MicroServices");

            migrationBuilder.DropIndex(
                name: "IX_MicroServices_SystemId",
                table: "MicroServices");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "MicroServices");

            migrationBuilder.RenameTable(
                name: "MicroServices",
                newName: "ConfigSystems");

            migrationBuilder.RenameIndex(
                name: "IX_MicroServices_Name",
                table: "ConfigSystems",
                newName: "IX_ConfigSystems_Name");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigSystems",
                table: "ConfigSystems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_ConfigSystems_MicroserviceId",
                table: "Configs",
                column: "MicroserviceId",
                principalTable: "ConfigSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
