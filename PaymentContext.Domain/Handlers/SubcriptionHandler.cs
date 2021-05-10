using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Handlers
{
    public class SubcriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubcriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "O número do documento já está em uso");

            if (_repository.EmailExists(command.Document))
                AddNotification("Document", "Este email já está em uso");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email, 
                command.BarCode, 
                command.BoletoNumber);

            subscription.AddPayment(payment);
            student.AddSubscriptions(subscription);

            AddNotifications(name, document, email, address, student, payment, subscription);

            if (!IsValid)
                return new CommandResult(false, "Erro ao criar assinatura");

            _repository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo!", "Sua assinatura foi criada!");
            
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
