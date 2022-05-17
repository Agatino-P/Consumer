using System;

namespace Agicap.Contracts;

public interface IPaymentPaid : IIntegrationEvent
{
    Guid PaymentId { get; }

    DateTimeOffset PaidDate { get; }
}

public interface IIntegrationEvent
{
}