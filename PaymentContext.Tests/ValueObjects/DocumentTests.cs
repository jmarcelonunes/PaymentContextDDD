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
    public class DocumentTests
    {
        //red, green, refactor
        [TestMethod]
        public void ShouldReturnWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);    
        }

        [TestMethod]
        public void ShouldReturnWhenCNPJIsValid()
        {
            var doc = new Document("12345678910111", EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnWhenCPFIsValid()
        {
            var doc = new Document("12", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnWhenCPFIsInValid()
        {
            var doc = new Document("15532728038", EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}
