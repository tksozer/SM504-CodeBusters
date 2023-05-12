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


public class VendingMachine : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public int? VendingMachineId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Alias { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Type { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int TotalIsleNumber { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Status { get; private set; }


    public static VendingMachine Create(VendingMachineForCreation vendingMachineForCreation)
    {
        var newVendingMachine = new VendingMachine();

        newVendingMachine.VendingMachineId = vendingMachineForCreation.VendingMachineId;
        newVendingMachine.Alias = vendingMachineForCreation.Alias;
        newVendingMachine.Latitude = vendingMachineForCreation.Latitude;
        newVendingMachine.Longitude = vendingMachineForCreation.Longitude;
        newVendingMachine.Type = vendingMachineForCreation.Type;
        newVendingMachine.TotalIsleNumber = vendingMachineForCreation.TotalIsleNumber;
        newVendingMachine.Status = vendingMachineForCreation.Status;

        newVendingMachine.QueueDomainEvent(new VendingMachineCreated(){ VendingMachine = newVendingMachine });
        
        return newVendingMachine;
    }

    public VendingMachine Update(VendingMachineForUpdate vendingMachineForUpdate)
    {
        VendingMachineId = vendingMachineForUpdate.VendingMachineId;
        Alias = vendingMachineForUpdate.Alias;
        Latitude = vendingMachineForUpdate.Latitude;
        Longitude = vendingMachineForUpdate.Longitude;
        Type = vendingMachineForUpdate.Type;
        TotalIsleNumber = vendingMachineForUpdate.TotalIsleNumber;
        Status = vendingMachineForUpdate.Status;

        QueueDomainEvent(new VendingMachineUpdated(){ Id = Id });
        return this;
    }
    
    protected VendingMachine() { } // For EF + Mocking
}