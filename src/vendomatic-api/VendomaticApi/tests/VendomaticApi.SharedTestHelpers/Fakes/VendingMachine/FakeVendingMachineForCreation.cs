namespace VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

using AutoBogus;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Models;

public sealed class FakeVendingMachineForCreation : AutoFaker<VendingMachineForCreation>
{
    public FakeVendingMachineForCreation()
    {
        RuleFor(v => v.MachineType, f => f.PickRandom<MachineTypeEnum>(MachineTypeEnum.List).Name);
        RuleFor(v => v.Status, f => f.PickRandom<StatusEnum>(StatusEnum.List).Name);
    }
}