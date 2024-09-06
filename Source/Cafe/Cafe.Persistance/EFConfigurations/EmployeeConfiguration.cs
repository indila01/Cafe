using Cafe.Domain.Entities;
using Cafe.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Persistance.EFConfigurations
{
    internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.OwnsOne(c => c.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(email => email.Value)
                    .HasColumnName(nameof(Employee.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.PhoneNumber, phoneNumberBuilder =>
            {
                phoneNumberBuilder.WithOwner();

                phoneNumberBuilder.Property(phoneNumber => phoneNumber.Value)
                    .HasColumnName(nameof(Employee.PhoneNumber))
                    .HasMaxLength(PhoneNumber.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Gender, genderBuilder =>
            {
                genderBuilder.WithOwner();

                genderBuilder.Property(gender => gender.Value)
                    .HasColumnName(nameof(Employee.Gender))
                    .HasMaxLength(Gender.MaxLength)
                    .IsRequired();
            });
        }
    }
}