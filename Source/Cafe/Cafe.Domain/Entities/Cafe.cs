using Cafe.SharedKernel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Entities
{
    public class Cafe : BaseEntity
    {
        private Cafe(string name, string description, string location)
        {
            Ensure.NotEmpty(name, "The Name is required.", nameof(name));
            Ensure.NotEmpty(description, "The description is required.", nameof(description));
            Ensure.NotEmpty(location, "The Name is required.", nameof(location));

            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Location = location;
        }
        private Cafe()
        {
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public static Cafe Create(string name, string description, string location)
        {
            return new Cafe(name, description, location);
        }
    }
}
