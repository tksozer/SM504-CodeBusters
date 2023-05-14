namespace VendomaticApi.Domain.Inventories;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.Inventories.Models;
using VendomaticApi.Domain.Inventories.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.VendingMachines;


public class Inventory : BaseEntity
{
    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("Product")]
    public Guid? ProductId { get; private set; }
    public Product Product { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("VendingMachine")]
    public Guid? VendingMachineId { get; private set; }
    public VendingMachine VendingMachine { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int IsleNumber { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int Quantity { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public decimal UnitPrice { get; private set; }


    public static Inventory Create(InventoryForCreation inventoryForCreation)
    {
        var newInventory = new Inventory();

        newInventory.ProductId = inventoryForCreation.ProductId;
        newInventory.VendingMachineId = inventoryForCreation.VendingMachineId;
        newInventory.IsleNumber = inventoryForCreation.IsleNumber;
        newInventory.Quantity = inventoryForCreation.Quantity;
        newInventory.UnitPrice = inventoryForCreation.UnitPrice;

        newInventory.QueueDomainEvent(new InventoryCreated(){ Inventory = newInventory });
        
        return newInventory;
    }

    public Inventory Update(InventoryForUpdate inventoryForUpdate)
    {
        ProductId = inventoryForUpdate.ProductId;
        VendingMachineId = inventoryForUpdate.VendingMachineId;
        IsleNumber = inventoryForUpdate.IsleNumber;
        Quantity = inventoryForUpdate.Quantity;
        UnitPrice = inventoryForUpdate.UnitPrice;

        QueueDomainEvent(new InventoryUpdated(){ Id = Id });
        return this;
    }
    
    protected Inventory() { } // For EF + Mocking
}