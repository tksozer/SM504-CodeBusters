namespace VendomaticApi.Domain.Products.Features;

using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Domain.Products.Services;
using VendomaticApi.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetProductList
{
    public sealed class Query : IRequest<PagedList<ProductDto>>
    {
        public readonly ProductParametersDto QueryParameters;

        public Query(ProductParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IProductRepository productRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _productRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ProductDto>();

            return await PagedList<ProductDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}