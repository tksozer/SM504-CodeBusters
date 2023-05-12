namespace VendomaticApi.Domain.Inventorys;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.Inventorys.Models;
using VendomaticApi.Domain.Inventorys.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Inventory : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public int? InventoryId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int VendingMachineId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int ProductId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int IsleNumber { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int Quantity { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public decimal UnitPrice { get; private set; }


    public static Inventory Create(InventoryForCreation inventoryForCreation)
    {
        var newInventory = new Inventory();

        newInventory.InventoryId = inventoryForCreation.InventoryId;
        newInventory.VendingMachineId = inventoryForCreation.VendingMachineId;
        newInventory.ProductId = inventoryForCreation.ProductId;
        newInventory.IsleNumber = inventoryForCreation.IsleNumber;
        newInventory.Quantity = inventoryForCreation.Quantity;
        newInventory.UnitPrice = inventoryForCreation.UnitPrice;

        newInventory.QueueDomainEvent(new InventoryCreated(){ Inventory = newInventory });
        
        return newInventory;
    }

    public Inventory Update(InventoryForUpdate inventoryForUpdate)
    {
        InventoryId = inventoryForUpdate.InventoryId;
        VendingMachineId = inventoryForUpdate.VendingMachineId;
        ProductId = inventoryForUpdate.ProductId;
        IsleNumber = inventoryForUpdate.IsleNumber;
        Quantity = inventoryForUpdate.Quantity;
        UnitPrice = inventoryForUpdate.UnitPrice;

        QueueDomainEvent(new InventoryUpdated(){ Id = Id });
        return this;
    }
    
    protected Inventory() { } // For EF + Mocking
}