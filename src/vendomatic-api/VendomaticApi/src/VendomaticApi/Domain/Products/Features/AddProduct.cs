namespace VendomaticApi.Domain.Products.Features;

using VendomaticApi.Domain.Products.Services;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Domain.Products.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddProduct
{
    public sealed class Command : IRequest<ProductDto>
    {
        public readonly ProductForCreationDto ProductToAdd;

        public Command(ProductForCreationDto productToAdd)
        {
            ProductToAdd = productToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var productToAdd = _mapper.Map<ProductForCreation>(request.ProductToAdd);
            var product = Product.Create(productToAdd);

            await _productRepository.Add(product, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}