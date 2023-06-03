namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.VendingMachines.Models;
using MapsterMapper;
using MediatR;

public static class RateVendingMachine
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly VendingMachineForRatingDto RatedVendingMachineData;

        public Command(Guid id, VendingMachineForRatingDto ratedVendingMachineData)
        {
            Id = id;
            RatedVendingMachineData = ratedVendingMachineData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IVendingMachineRepository vendingMachineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _vendingMachineRepository = vendingMachineRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var vendingMachineToUpdate = await _vendingMachineRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var UpdatedVendingMachineData = new VendingMachineForUpdateDto
            {
                Alias = vendingMachineToUpdate.Alias,
                Latitude = vendingMachineToUpdate.Latitude,
                Longitude = vendingMachineToUpdate.Longitude,
                MachineType = vendingMachineToUpdate.MachineType,
                Status = vendingMachineToUpdate.Status,
                TotalIsleNumber = vendingMachineToUpdate.TotalIsleNumber,
                MachineOperatorId = vendingMachineToUpdate.MachineOperatorId
            };

            if (request.RatedVendingMachineData.Rating > 0)
            {
                var prevRating = vendingMachineToUpdate.Rating;
                var prevRatingCount = vendingMachineToUpdate.RatingCount;

                //var newRating = (int)Math.Ceiling((double)((prevRating * prevRatingCount) + request.RatedVendingMachineData.Rating) / (prevRatingCount+1));
                //int totalRating = (prevRating * prevRatingCount) + request.RatedVendingMachineData.Rating;
                //int newRatingCount = prevRatingCount + 1;

                //double averageRating = (double)totalRating / newRatingCount;
                //var newRating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);
                //var newRating = (int)Math.Ceiling(averageRating);
                double decayFactor = 0.7;
                double totalWeightedRating = (prevRating * decayFactor * prevRatingCount) + (request.RatedVendingMachineData.Rating);
                int newRatingCount = prevRatingCount + 1;

                double averageRating = totalWeightedRating / newRatingCount;
                var newRating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);

                UpdatedVendingMachineData.RatingCount = prevRatingCount + 1;
                UpdatedVendingMachineData.Rating = newRating;
            }

            var vendingMachineToAdd = _mapper.Map<VendingMachineForUpdate>(UpdatedVendingMachineData);
            vendingMachineToUpdate.Update(vendingMachineToAdd);

            _vendingMachineRepository.Update(vendingMachineToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}