namespace VendomaticApi.Domain.Operators.Features;

using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Domain.Operators.Services;
using VendomaticApi.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetOperatorList
{
    public sealed class Query : IRequest<PagedList<OperatorDto>>
    {
        public readonly OperatorParametersDto QueryParameters;

        public Query(OperatorParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<OperatorDto>>
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IOperatorRepository operatorRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _operatorRepository = operatorRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<OperatorDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _operatorRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<OperatorDto>();

            return await PagedList<OperatorDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}