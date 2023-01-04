using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SampleIdentityServer.UI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "customUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "İstanbul", "cnrgrsc@gmail.com", "password", "cnrgrsc" },
                    { 2, "Erzurum", "ogzgrsc@gmail.com", "password", "cnrgrsc" },
                    { 3, "İzmir", "hsngrsc@gmail.com", "password", "cnrgrsc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customUsers");
        }
    }
}
