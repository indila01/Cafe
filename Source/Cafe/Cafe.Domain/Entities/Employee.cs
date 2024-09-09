using Cafe.Domain.ValueObjects;
using Cafe.SharedKernel.Util;

namespace Cafe.Domain.Entities
{
    public class Employee : BaseEntity
    {

        private Employee(string id, string name, Email email, PhoneNumber phoneNumber, Gender gender, Cafe cafe)
        {
            Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
            Ensure.NotEmpty(name, "The name is required.", nameof(name));
            Ensure.NotEmpty(email, "The email is required.", nameof(email));
            Ensure.NotNull(phoneNumber, "The phone number is required.", nameof(phoneNumber));
            Ensure.NotEmpty(gender, "The gender is required.", nameof(gender));

            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            if (cafe != null)
            {
                this.StartDate = DateTime.Now;
                this.CafeId = cafe.Id;
            }
        }
        private Employee()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime? StartDate { get; set; }

        public Guid? CafeId { get; set; }
        public Cafe? Cafe { get; set; }

        public static Employee CreateEmployee(string id, string name, Email email, PhoneNumber phoneNumber, Gender gender, Cafe? cafe = null)
        {
            return new Employee(id, name, email, phoneNumber, gender, cafe);
        }
        public void UpdateEmployee(
            string name,
            Email email,
            PhoneNumber phoneNumber,
            Gender gender,
            Cafe cafe)
        {
            Ensure.NotEmpty(name, "The name is required.", nameof(name));
            Ensure.NotEmpty(email, "The email is required.", nameof(email));
            Ensure.NotNull(phoneNumber, "The phone number is required.", nameof(phoneNumber));
            Ensure.NotNull(Gender, "The phone number is required.", nameof(phoneNumber));
            //Ensure.NotEmpty(cafeId, "The cafe identifier is required.", nameof(cafeId));

            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            if (cafe != null)
            {
                this.StartDate = DateTime.Now;
                this.CafeId = cafe.Id;
            }
        }
    }
}
