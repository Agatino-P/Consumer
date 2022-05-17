using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Agicap.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class PaymentPaidConsumer : IConsumer<IPaymentPaid>
{
    private readonly ILogger<PaymentPaidConsumer> _logger;

    public PaymentPaidConsumer(ILogger<PaymentPaidConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<IPaymentPaid> context)
    {
        IPaymentPaid payment = context.Message!;
        _logger.Log(LogLevel.Information,
            $"{payment.PaymentId.ToString()} received at {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff")}\n");

        DateTime failingDateTime= DateTime.Parse("2001-01-01");

        if (payment.PaidDate.Equals(failingDateTime))
            throw new NotImplementedException();
        return Task.CompletedTask;
    }
}