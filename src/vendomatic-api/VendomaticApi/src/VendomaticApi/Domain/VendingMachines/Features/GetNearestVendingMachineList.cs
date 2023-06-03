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
using Geohash;

public static class GetNearestVendingMachineList
{
    public sealed class Query : IRequest<PagedList<VendingMachineDto>>
    {
        public readonly FindNearestVendingMachinesParametersDto QueryParameters;

        public Query(FindNearestVendingMachinesParametersDto queryParameters)
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

        private class VendingMachineGeo
        {
            public Guid Id { get; set; }
            public string Parent { get; set; }
        }

        public Task<PagedList<VendingMachineDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var geohasher = new Geohasher();

            var collection = _vendingMachineRepository.Query().AsNoTracking();
            var hashedCollection = collection.Select(v => new VendingMachineGeo
            {
                Id = v.Id,
                Parent = geohasher.GetParent(geohasher.Encode(v.Latitude.Value, v.Longitude.Value, 7))
            }).ToList();

            var location = geohasher.Encode(request.QueryParameters.Latitude, request.QueryParameters.Longitude, 7);
            var parent = geohasher.GetParent(location);

            var neighborIds = hashedCollection.Where(v => parent == v.Parent).Select(n => n.Id).ToList();
            
            var neighbors = collection.Where(v => neighborIds.Contains(v.Id));

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, neighbors);
            var dtoCollection = appliedCollection
                .ProjectToType<VendingMachineDto>();

            return PagedList<VendingMachineDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}