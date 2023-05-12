namespace VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Models;

public class FakeVendingMachineBuilder
{
    private VendingMachineForCreation _creationData = new FakeVendingMachineForCreation().Generate();

    public FakeVendingMachineBuilder WithModel(VendingMachineForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeVendingMachineBuilder WithVendingMachineId(int? vendingMachineId)
    {
        _creationData.VendingMachineId = vendingMachineId;
        return this;
    }
    
    public FakeVendingMachineBuilder WithAlias(string alias)
    {
        _creationData.Alias = alias;
        return this;
    }
    
    public FakeVendingMachineBuilder WithLatitude(double? latitude)
    {
        _creationData.Latitude = latitude;
        return this;
    }
    
    public FakeVendingMachineBuilder WithLongitude(double? longitude)
    {
        _creationData.Longitude = longitude;
        return this;
    }
    
    public FakeVendingMachineBuilder WithType(string type)
    {
        _creationData.Type = type;
        return this;
    }
    
    public FakeVendingMachineBuilder WithTotalIsleNumber(int totalIsleNumber)
    {
        _creationData.TotalIsleNumber = totalIsleNumber;
        return this;
    }
    
    public FakeVendingMachineBuilder WithStatus(string status)
    {
        _creationData.Status = status;
        return this;
    }
    
    public VendingMachine Build()
    {
        var result = VendingMachine.Create(_creationData);
        return result;
    }
}