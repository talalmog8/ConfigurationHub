using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigurationHub.Data.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_ConfigAuthors_AuthorId",
                table: "Configs");

            migrationBuilder.DropTable(
                name: "ConfigAuthors");

            migrationBuilder.AlterColumn<string>(
                name: "MicroserviceName",
                table: "ConfigSystems",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Username", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigSystems_MicroserviceName",
                table: "ConfigSystems",
                column: "MicroserviceName");

            migrationBuilder.CreateIndex(
                name: "IX_Configs_LastModified",
                table: "Configs",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_AuthorId",
                table: "Configs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ConfigSystems_MicroserviceName",
                table: "ConfigSystems");

            migrationBuilder.DropIndex(
                name: "IX_Configs_LastModified",
                table: "Configs");

            migrationBuilder.AlterColumn<string>(
                name: "MicroserviceName",
                table: "ConfigSystems",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "ConfigAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigAuthors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_ConfigAuthors_AuthorId",
                table: "Configs",
                column: "AuthorId",
                principalTable: "ConfigAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
