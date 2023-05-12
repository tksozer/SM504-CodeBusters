namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines.Services;
using VendomaticApi.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetVendingMachineList
{
    public sealed class Query : IRequest<PagedList<VendingMachineDto>>
    {
        public readonly VendingMachineParametersDto QueryParameters;

        public Query(VendingMachineParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<VendingMachineDto>>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IVendingMachineRepository vendingMachineRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _vendingMachineRepository = vendingMachineRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<VendingMachineDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _vendingMachineRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<VendingMachineDto>();

            return await PagedList<VendingMachineDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}