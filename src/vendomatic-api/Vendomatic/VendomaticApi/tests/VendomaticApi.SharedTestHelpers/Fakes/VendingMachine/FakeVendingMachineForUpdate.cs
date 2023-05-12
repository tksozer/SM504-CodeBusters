namespace VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

using AutoBogus;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Models;

public sealed class FakeVendingMachineForUpdate : AutoFaker<VendingMachineForUpdate>
{
    public FakeVendingMachineForUpdate()
    {
    }
}