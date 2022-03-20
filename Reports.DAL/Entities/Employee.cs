using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public Guid BossId { get; set; }

        private Employee()
        {
        }

        public Employee(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            Id = id;
            Name = name;
        }
    }
}