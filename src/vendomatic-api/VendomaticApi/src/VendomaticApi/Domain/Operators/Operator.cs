namespace VendomaticApi.Domain.Operators;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.Operators.Models;
using VendomaticApi.Domain.Operators.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using VendomaticApi.Domain.VendingMachines;


public class Operator : BaseEntity
{
    public Guid CorrelationId { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    public ICollection<VendingMachine> VendingMachines { get; private set; }


    public static Operator Create(OperatorForCreation operatorForCreation)
    {
        var newOperator = new Operator();

        newOperator.CorrelationId = operatorForCreation.CorrelationId;

        newOperator.QueueDomainEvent(new OperatorCreated(){ Operator = newOperator });
        
        return newOperator;
    }

    public Operator Update(OperatorForUpdate operatorForUpdate)
    {
        CorrelationId = operatorForUpdate.CorrelationId;

        QueueDomainEvent(new OperatorUpdated(){ Id = Id });
        return this;
    }
    
    protected Operator() { } // For EF + Mocking
}