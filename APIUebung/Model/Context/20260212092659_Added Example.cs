using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Model.Context
{
    /// <inheritdoc />
    public partial class AddedExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

        /*        migrationBuilder.CreateTable(
                    name: "demo",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        value = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PRIMARY", x => x.id);
                    })
                    .Annotation("MySQL:Charset", "utf8mb4");
*/
                migrationBuilder.CreateTable(
                    name: "Examples",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        Value1 = table.Column<int>(type: "int", nullable: false),
                        Value2 = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Examples", x => x.Id);
                    })
                    .Annotation("MySQL:Charset", "utf8mb4");
            }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "demo");

            migrationBuilder.DropTable(
                name: "Examples");
        }
    }
}
