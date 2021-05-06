using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(
            DateTime paidDate, 
            DateTime expireDate,
            decimal total, 
            decimal totalPaid,
            string payer, 
            Document document,
            Address address,
            Email email, 
            string transactionCode)
            : base(
                  paidDate, 
                  expireDate, 
                  total, 
                  totalPaid,
                  document,
                  payer, 
                  address, 
                  email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}
