using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NastyaKupcovakt_42_21.Helpers;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "Groups";
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.GroupId)
                                .HasName($"pk_{TableName}_Id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GroupId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GroupId)
                                .HasColumnName("Id")
                .HasComment("Идентификатор записи группы");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
            builder.Property(p => p.GroupName)
                .IsRequired()
                                .HasColumnName("GroupName")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название группы");

            builder.Property(p => p.GroupJob)
                .IsRequired()
                .HasColumnName("GroupJob")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Специальность группы");
            builder.Property(p => p.GroupYear)
                .IsRequired()
                .HasColumnName("GroupYear")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Год поступления");
            builder.Property(p => p.StudentQuantity)
                .IsRequired()
                .HasColumnName("StudentQuantity")
                .HasColumnType(ColumnType.Int)
                .HasComment("Количество студентов в группе");
            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("bit") // Используйте "bit" вместо ColumnType.Bool
                .HasComment("Статус удаления");

            

            builder.ToTable(TableName);
        }

    }
}
