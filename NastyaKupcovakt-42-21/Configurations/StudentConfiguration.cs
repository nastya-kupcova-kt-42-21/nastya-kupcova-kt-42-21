
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NastyaKupcovakt_42_21.Helpers;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "Students";
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.StudentId)
                               .HasName($"pk_{TableName}_Id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.StudentId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.StudentId)
                                .HasColumnName("Id")
                .HasComment("Идентификатор записи студента");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
            builder.Property(p => p.Surname)
    .IsRequired()
                    .HasColumnName("Surname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя студента");

            builder.Property(p => p.Name)
    .IsRequired()
                    .HasColumnName("Name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Фамилия студента");

            builder.Property(p => p.Midname)
    .IsRequired()
    .HasColumnName("Midname")
    .HasColumnType(ColumnType.String).HasMaxLength(100)
    .HasComment("Отчество студента");

            builder.Property(p => p.GroupId)
                .IsRequired()
                                .HasColumnName("GroupId")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор группы");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("bit") // Используйте "bit" вместо ColumnType.Bool
                .HasComment("Статус удаления");
            builder.ToTable(TableName)
                .HasOne(p => p.Group)
                                .WithMany(t => t.Students)
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Group)
                .AutoInclude();
        }
    }
    }
