using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class collab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    CollaboratorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaboratorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Userid = table.Column<long>(type: "bigint", nullable: false),
                    Noteid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.CollaboratorId);
                    table.ForeignKey(
                        name: "FK_Collaborators_Notes_Noteid",
                        column: x => x.Noteid,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collaborators_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "UsertId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_Noteid",
                table: "Collaborators",
                column: "Noteid");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_Userid",
                table: "Collaborators",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");
        }
    }
}
