namespace VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

using AutoBogus;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Dtos;

public sealed class FakeVendingMachineForUpdateDto : AutoFaker<VendingMachineForUpdateDto>
{
    public FakeVendingMachineForUpdateDto()
    {
    }
}