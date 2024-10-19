using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NastyaKupcovakt_42_21.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Название группы"),
                    GroupJob = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Специальность группы"),
                    GroupYear = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Год поступления"),
                    StudentQuantity = table.Column<int>(type: "int", nullable: false, comment: "Количество студентов в группе"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Groups_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи дисциплины")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Название дисциплины"),
                    SubjectDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Направление дисциплины"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Subject_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Имя студента"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    Midname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Отчество студента"),
                    GroupId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор группы"),
                    Exams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grades = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Students_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupSubject",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubject", x => new { x.GroupId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_GroupSubject_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSubject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubject_SubjectId",
                table: "GroupSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "idx_Students_fk_f_group_id",
                table: "Students",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubject");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
