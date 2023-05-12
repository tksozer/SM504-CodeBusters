namespace VendomaticApi.Domain.Products.Features;

using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Domain.Products.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.Products.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateProduct
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ProductForUpdateDto UpdatedProductData;

        public Command(Guid id, ProductForUpdateDto updatedProductData)
        {
            Id = id;
            UpdatedProductData = updatedProductData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var productToAdd = _mapper.Map<ProductForUpdate>(request.UpdatedProductData);
            productToUpdate.Update(productToAdd);

            _productRepository.Update(productToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}