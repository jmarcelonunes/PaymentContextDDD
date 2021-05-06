using Flunt.Validations;
using PaymentContext.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateTime = DateTime.Now;
            ExpireDate = expireDate;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateTime { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }
    
        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract<Payment>()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento não pode ser no passado"));
                
            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdateTime = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = false;
            LastUpdateTime = DateTime.Now;
        }
    }
}
