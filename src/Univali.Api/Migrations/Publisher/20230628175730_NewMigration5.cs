using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Univali.Api.Migrations.Publisher
{
    /// <inheritdoc />
    public partial class NewMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorCourse");

            migrationBuilder.CreateTable(
                name: "AuthorsCourses",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsCourses", x => new { x.AuthorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_AuthorsCourses_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorsCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Autor", "1" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "BasePrice", "Title" },
                values: new object[] { 1, "Legal", 110.98999999999999, "Curso" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsCourses_CourseId",
                table: "AuthorsCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsCourses");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "AuthorCourse",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorCourse", x => new { x.AuthorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_AuthorCourse_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorCourse_CourseId",
                table: "AuthorCourse",
                column: "CourseId");
        }
    }
}
