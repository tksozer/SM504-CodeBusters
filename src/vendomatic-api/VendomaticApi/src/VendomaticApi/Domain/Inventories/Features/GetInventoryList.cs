namespace VendomaticApi.Domain.Inventories.Features;

using VendomaticApi.Domain.Inventories.Dtos;
using VendomaticApi.Domain.Inventories.Services;
using VendomaticApi.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetInventoryList
{
    public sealed class Query : IRequest<PagedList<InventoryDto>>
    {
        public readonly InventoryParametersDto QueryParameters;

        public Query(InventoryParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<InventoryDto>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IInventoryRepository inventoryRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<InventoryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _inventoryRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<InventoryDto>();

            return await PagedList<InventoryDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}