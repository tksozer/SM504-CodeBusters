namespace VendomaticApi.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using VendomaticApi.Databases;

public sealed class StatusUpdateHandler : IConsumer<IIUpdateStatus>
{
    private readonly IMapper _mapper;
    private readonly MigrationHostedService<VendomaticDbContext> _db;

    public StatusUpdateHandler(MigrationHostedService<VendomaticDbContext> db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public Task Consume(ConsumeContext<IIUpdateStatus> context)
    {
        // do work here

        return Task.CompletedTask;
    }
}