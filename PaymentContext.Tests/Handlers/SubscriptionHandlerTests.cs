using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnWhenDocumentExists()
        {
            var handler = new SubcriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Joao";
            command.LastName = "Nunes";
            command.Document = "99999999999";
            command.Email = "email@email.com";
            command.TransactionCode = "123456";
            command.PaymentNumber = "112354";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.PayerDocument = "123456789";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Payer = "email@email.com";
            command.Street = "Rua";
            command.Number = "123";
            command.Neighborhood = "Bairro";
            command.City = "Cidade";
            command.State = "ES";
            command.Country = "Brasil";
            command.ZipCode = "79845613";
            command.BoletoNumber = "1231246";
            command.BarCode = "557789785";
            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);

        }
    }
}
