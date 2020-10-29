using Microsoft.EntityFrameworkCore.Migrations;

namespace JosiArchitecture.Data.Migrations
{
    public partial class TodoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Todos",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<long>(
                name: "TodoListId",
                table: "Todos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoListId",
                table: "Todos",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodoLists_TodoListId",
                table: "Todos",
                column: "TodoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodoLists_TodoListId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropIndex(
                name: "IX_Todos_TodoListId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "TodoListId",
                table: "Todos");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Todos",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }
    }
}
