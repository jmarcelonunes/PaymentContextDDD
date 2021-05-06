using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsLowerThan(FirstName, 3, "Name.FirstName", "Primeiro nome deve conter pelo menos 3 caracteres")
                .IsLowerThan(LastName, 3, "Name.LastName", "Ultimo nome deve conter pelo menos 3 caracteres")
                .IsGreaterThan(FirstName, 100, "Name.FistName", "Primeiro nome deve conter no máximo 100 caracteres")
                .IsGreaterThan(LastName, 100, "Name.LastName", "Ultimo nome deve conter no máximo 100 caracteres")
                );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
