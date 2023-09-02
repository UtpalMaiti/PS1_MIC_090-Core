using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NameSuffix",
                columns: table => new
                {
                    SuffixId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameSuffix", x => x.SuffixId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameSuffix");
        }
    }
}
