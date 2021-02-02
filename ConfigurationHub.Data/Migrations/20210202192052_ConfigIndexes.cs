using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigurationHub.Data.Migrations
{
    public partial class ConfigIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs");

            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_Systems_Name",
                table: "Systems");

            migrationBuilder.DropIndex(
                name: "IX_MicroServices_Name",
                table: "MicroServices");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MicroserviceId",
                table: "Configs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Configs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ConfigContents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Email",
                table: "Users",
                columns: new[] { "Username", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Systems_Name",
                table: "Systems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MicroServices_Name",
                table: "MicroServices",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs",
                column: "MicroserviceId",
                principalTable: "MicroServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs");

            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Systems_Name",
                table: "Systems");

            migrationBuilder.DropIndex(
                name: "IX_MicroServices_Name",
                table: "MicroServices");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "MicroserviceId",
                table: "Configs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Configs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ConfigContents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_Name",
                table: "Systems",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MicroServices_Name",
                table: "MicroServices",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_MicroServices_MicroserviceId",
                table: "Configs",
                column: "MicroserviceId",
                principalTable: "MicroServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
