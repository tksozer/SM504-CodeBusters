namespace VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

using AutoBogus;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Dtos;

public sealed class FakeVendingMachineForUpdateDto : AutoFaker<VendingMachineForUpdateDto>
{
    public FakeVendingMachineForUpdateDto()
    {
        RuleFor(v => v.MachineType, f => f.PickRandom<MachineTypeEnum>(MachineTypeEnum.List).Name);
        RuleFor(v => v.Status, f => f.PickRandom<StatusEnum>(StatusEnum.List).Name);
    }
}