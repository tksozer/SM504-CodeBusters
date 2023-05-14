namespace VendomaticApi.UnitTests.Domain.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateOperatorTests
{
    private readonly Faker _faker;

    public CreateOperatorTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_operator()
    {
        // Arrange
        var operatorToCreate = new FakeOperatorForCreation().Generate();
        
        // Act
        var fakeOperator = Operator.Create(operatorToCreate);

        // Assert
        fakeOperator.CorrelationId.Should().Be(operatorToCreate.CorrelationId);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var operatorToCreate = new FakeOperatorForCreation().Generate();
        
        // Act
        var fakeOperator = Operator.Create(operatorToCreate);

        // Assert
        fakeOperator.DomainEvents.Count.Should().Be(1);
        fakeOperator.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(OperatorCreated));
    }
}