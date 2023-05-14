namespace VendomaticApi.Domain.MachineOperators;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.MachineOperators.Models;
using VendomaticApi.Domain.MachineOperators.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using VendomaticApi.Domain.VendingMachines;


public class MachineOperator : BaseEntity
{
    public Guid CorrelationId { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    public ICollection<VendingMachine> VendingMachines { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; private set; }


    public static MachineOperator Create(MachineOperatorForCreation machineOperatorForCreation)
    {
        var newMachineOperator = new MachineOperator();

        newMachineOperator.CorrelationId = machineOperatorForCreation.CorrelationId;
        newMachineOperator.Name = machineOperatorForCreation.Name;

        newMachineOperator.QueueDomainEvent(new MachineOperatorCreated(){ MachineOperator = newMachineOperator });
        
        return newMachineOperator;
    }

    public MachineOperator Update(MachineOperatorForUpdate machineOperatorForUpdate)
    {
        CorrelationId = machineOperatorForUpdate.CorrelationId;
        Name = machineOperatorForUpdate.Name;

        QueueDomainEvent(new MachineOperatorUpdated(){ Id = Id });
        return this;
    }
    
    protected MachineOperator() { } // For EF + Mocking
}