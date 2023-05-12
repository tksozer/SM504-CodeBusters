namespace VendomaticApi.Domain;

using SharedKernel.Messages;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VendomaticApi.Databases;

public static class StatusUpdated
{
    public sealed class StatusUpdatedCommand : IRequest<bool>
    {
        public StatusUpdatedCommand()
        {
        }
    }

    public sealed class Handler : IRequestHandler<StatusUpdatedCommand, bool>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly MigrationHostedService<VendomaticDbContext> _db;

        public Handler(MigrationHostedService<VendomaticDbContext> db, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(StatusUpdatedCommand request, CancellationToken cancellationToken)
        {
            var message = new IUpdateStatus
            {
                // map content to message here or with mapster
            };
            await _publishEndpoint.Publish<IIUpdateStatus>(message, cancellationToken);

            return true;
        }
    }
}