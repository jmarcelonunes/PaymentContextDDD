using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Services
{
    public interface IEmailService
    {
        public void Send(string to, string email, string subject, string body);
    }
}
