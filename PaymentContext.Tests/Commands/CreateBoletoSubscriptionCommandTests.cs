using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestClass]
        public class DocumentTests
        {
            [TestMethod]
            public void ShouldReturnErrorWhenNameIsInvalid()
            {
                var command = new CreateBoletoSubscriptionCommand();
                command.FirstName = "";
                command.Validate();
                Assert.AreEqual(false, command.IsValid);
            }
        }
    }
}
