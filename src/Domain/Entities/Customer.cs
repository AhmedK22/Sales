using System;
namespace Domain.Entities
{
    public class Customer
    {
        private Customer() { }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}
