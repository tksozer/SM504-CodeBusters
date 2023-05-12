namespace VendomaticApi.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using VendomaticApi.Databases;

public sealed class ProductpurchaseHandler : IConsumer<IIPurchaseProduct>
{
    private readonly IMapper _mapper;
    private readonly MigrationHostedService<VendomaticDbContext> _db;

    public ProductpurchaseHandler(MigrationHostedService<VendomaticDbContext> db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public Task Consume(ConsumeContext<IIPurchaseProduct> context)
    {
        // do work here

        return Task.CompletedTask;
    }
}