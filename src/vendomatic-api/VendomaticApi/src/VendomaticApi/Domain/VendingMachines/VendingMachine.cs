namespace VendomaticApi.Domain.VendingMachines;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.VendingMachines.Models;
using VendomaticApi.Domain.VendingMachines.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Operators;


public class VendingMachine : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Alias { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    private MachineTypeEnum _machineType;
    [Sieve(CanFilter = true, CanSort = true)]
    public string MachineType
    {
        get => _machineType.Name;
        private set
        {
            if (!MachineTypeEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(MachineType), value);

            _machineType = parsed;
        }
    }

    [Sieve(CanFilter = true, CanSort = true)]
    public int TotalIsleNumber { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Status { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    public ICollection<Inventory> Inventories { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("Operator")]
    public Guid? OperatorId { get; private set; }
    public Operator Operator { get; private set; }


    public static VendingMachine Create(VendingMachineForCreation vendingMachineForCreation)
    {
        var newVendingMachine = new VendingMachine();

        newVendingMachine.Alias = vendingMachineForCreation.Alias;
        newVendingMachine.Latitude = vendingMachineForCreation.Latitude;
        newVendingMachine.Longitude = vendingMachineForCreation.Longitude;
        newVendingMachine.MachineType = vendingMachineForCreation.MachineType;
        newVendingMachine.TotalIsleNumber = vendingMachineForCreation.TotalIsleNumber;
        newVendingMachine.Status = vendingMachineForCreation.Status;
        newVendingMachine.OperatorId = vendingMachineForCreation.OperatorId;

        newVendingMachine.QueueDomainEvent(new VendingMachineCreated(){ VendingMachine = newVendingMachine });
        
        return newVendingMachine;
    }

    public VendingMachine Update(VendingMachineForUpdate vendingMachineForUpdate)
    {
        Alias = vendingMachineForUpdate.Alias;
        Latitude = vendingMachineForUpdate.Latitude;
        Longitude = vendingMachineForUpdate.Longitude;
        MachineType = vendingMachineForUpdate.MachineType;
        TotalIsleNumber = vendingMachineForUpdate.TotalIsleNumber;
        Status = vendingMachineForUpdate.Status;
        OperatorId = vendingMachineForUpdate.OperatorId;

        QueueDomainEvent(new VendingMachineUpdated(){ Id = Id });
        return this;
    }
    
    protected VendingMachine() { } // For EF + Mocking
}