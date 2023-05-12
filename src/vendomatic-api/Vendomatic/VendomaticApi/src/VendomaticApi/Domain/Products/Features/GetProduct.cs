namespace VendomaticApi.Domain.Products.Features;

using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Domain.Products.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetProduct
{
    public sealed class Query : IRequest<ProductDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ProductDto>(result);
        }
    }
}