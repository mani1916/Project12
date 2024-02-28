using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project1.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(250);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
            builder.HasData(new List<Student>()
            {
            new Student { Id = 1, StudentName = "Mani", Address = "India", DOB = new DateTime(2022, 12,12),Email="abc@gmail.com"} ,
            new Student { Id = 2, StudentName = "Manikandan", Address = "India", DOB = new DateTime(2022, 12,12),Email="efg@gmail.com"}
            });
        }
    }
}