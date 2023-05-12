namespace VendomaticApi.Extensions.Services.ProducerRegistrations;

using MassTransit;
using MassTransit.RabbitMqTransport;
using SharedKernel.Messages;
using RabbitMQ.Client;

public static class UpdateProductEndpointRegistration
{
    public static void UpdateProductEndpoint(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.Message<IUpdateProduct>(e => e.SetEntityName("product-updates-manager")); // name of the primary exchange
        cfg.Publish<IUpdateProduct>(e => e.ExchangeType = ExchangeType.Fanout); // primary exchange type
    }
}