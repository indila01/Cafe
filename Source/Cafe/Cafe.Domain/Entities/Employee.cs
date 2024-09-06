using Cafe.Domain.ValueObjects;
using Cafe.SharedKernel.Util;

namespace Cafe.Domain.Entities
{
    public class Employee : BaseEntity
    {

        private Employee(string id, string name, Email email, PhoneNumber phoneNumber, Gender gender)
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

        public Cafe? Cafe { get; set; }

        public static Employee Create(string id, string name, Email email, PhoneNumber phoneNumber, Gender gender)
        {
            return new Employee(id, name, email, phoneNumber,gender);
        }
    }
}
