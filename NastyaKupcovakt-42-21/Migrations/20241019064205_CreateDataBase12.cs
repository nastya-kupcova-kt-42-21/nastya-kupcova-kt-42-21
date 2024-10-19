using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NastyaKupcovakt_42_21.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataBase12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_GroupId",
                table: "GroupSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_SubjectId",
                table: "GroupSubject");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "GroupSubject",
                newName: "SubjectsSubjectId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupSubject",
                newName: "GroupsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubject_SubjectId",
                table: "GroupSubject",
                newName: "IX_GroupSubject_SubjectsSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_Groups_GroupsGroupId",
                table: "GroupSubject",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_Subject_SubjectsSubjectId",
                table: "GroupSubject",
                column: "SubjectsSubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_Groups_GroupsGroupId",
                table: "GroupSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_Subject_SubjectsSubjectId",
                table: "GroupSubject");

            migrationBuilder.RenameColumn(
                name: "SubjectsSubjectId",
                table: "GroupSubject",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupId",
                table: "GroupSubject",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubject_SubjectsSubjectId",
                table: "GroupSubject",
                newName: "IX_GroupSubject_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_GroupId",
                table: "GroupSubject",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_SubjectId",
                table: "GroupSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
