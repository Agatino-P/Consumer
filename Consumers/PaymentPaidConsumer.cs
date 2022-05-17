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
        _logger.Log(LogLevel.Information, $"{context.Message.PaymentId} {context.Message.PaidDate}");
        return Task.CompletedTask;
    }
}