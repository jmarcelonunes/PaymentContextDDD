using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;

        public StudentTests()
        {
            _name = new Name("Joao", "Nunes");
            _document = new Document("02271605091", EDocumentType.CPF);
            _email = new Email("joao@joao.com");
            _student = new Student(_name, _document, _email);
            _address = new Address("Rua 1", "1213", "Bairro", "Brasilia", "DF", "Brasil", "789456123");
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription() 
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment(DateTime.Now,
                DateTime.Now.AddDays(5),
                10,
                10,
                "joaonunes",
                _document,
                _address,
                _email,
                "12345679");
            subscription.AddPayment(payment);
            _student.AddSubscriptions(subscription);
            _student.AddSubscriptions(subscription);
            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscriptions(subscription);
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment(DateTime.Now,
                DateTime.Now.AddDays(5),
                10,
                10,
                "joaonunes",
                _document,
                _address,
                _email,
                "12345679");
            subscription.AddPayment(payment);
            _student.AddSubscriptions(subscription);
            Assert.IsTrue(_student.IsValid);
        }
    }
}
