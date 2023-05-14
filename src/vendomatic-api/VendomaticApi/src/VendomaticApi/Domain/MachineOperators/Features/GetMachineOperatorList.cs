namespace VendomaticApi.Domain.MachineOperators.Features;

using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Domain.MachineOperators.Services;
using VendomaticApi.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetMachineOperatorList
{
    public sealed class Query : IRequest<PagedList<MachineOperatorDto>>
    {
        public readonly MachineOperatorParametersDto QueryParameters;

        public Query(MachineOperatorParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<MachineOperatorDto>>
    {
        private readonly IMachineOperatorRepository _machineOperatorRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IMachineOperatorRepository machineOperatorRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _machineOperatorRepository = machineOperatorRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<MachineOperatorDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _machineOperatorRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<MachineOperatorDto>();

            return await PagedList<MachineOperatorDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}