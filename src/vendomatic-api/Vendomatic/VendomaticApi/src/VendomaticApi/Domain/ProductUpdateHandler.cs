namespace VendomaticApi.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using VendomaticApi.Databases;

public sealed class ProductUpdateHandler : IConsumer<IIUpdateProduct>
{
    private readonly IMapper _mapper;
    private readonly MigrationHostedService<VendomaticDbContext> _db;

    public ProductUpdateHandler(MigrationHostedService<VendomaticDbContext> db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public Task Consume(ConsumeContext<IIUpdateProduct> context)
    {
        // do work here

        return Task.CompletedTask;
    }
}