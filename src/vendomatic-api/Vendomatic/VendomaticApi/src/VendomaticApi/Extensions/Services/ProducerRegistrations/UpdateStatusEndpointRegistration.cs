namespace VendomaticApi.Extensions.Services.ProducerRegistrations;

using MassTransit;
using MassTransit.RabbitMqTransport;
using SharedKernel.Messages;
using RabbitMQ.Client;

public static class UpdateStatusEndpointRegistration
{
    public static void UpdateStatusEndpoint(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.Message<IUpdateStatus>(e => e.SetEntityName("status-updates-manager")); // name of the primary exchange
        cfg.Publish<IUpdateStatus>(e => e.ExchangeType = ExchangeType.Fanout); // primary exchange type
    }
}