using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Univali.Api.Migrations.Publisher
{
    /// <inheritdoc />
    public partial class NewNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourse_Authors_AuthorsId",
                table: "AuthorCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourse_Courses_CoursesId",
                table: "AuthorCourse");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "AuthorCourse",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AuthorCourse",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorCourse_CoursesId",
                table: "AuthorCourse",
                newName: "IX_AuthorCourse_CourseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AuthorCourse",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourse_Authors_AuthorId",
                table: "AuthorCourse",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourse_Courses_CourseId",
                table: "AuthorCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourse_Authors_AuthorId",
                table: "AuthorCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourse_Courses_CourseId",
                table: "AuthorCourse");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AuthorCourse");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "AuthorCourse",
                newName: "CoursesId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AuthorCourse",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorCourse_CourseId",
                table: "AuthorCourse",
                newName: "IX_AuthorCourse_CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourse_Authors_AuthorsId",
                table: "AuthorCourse",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourse_Courses_CoursesId",
                table: "AuthorCourse",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
