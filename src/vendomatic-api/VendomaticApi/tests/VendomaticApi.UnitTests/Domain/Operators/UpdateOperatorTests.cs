namespace VendomaticApi.UnitTests.Domain.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateOperatorTests
{
    private readonly Faker _faker;

    public UpdateOperatorTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_operator()
    {
        // Arrange
        var fakeOperator = new FakeOperatorBuilder().Build();
        var updatedOperator = new FakeOperatorForUpdate().Generate();
        
        // Act
        fakeOperator.Update(updatedOperator);

        // Assert
        fakeOperator.CorrelationId.Should().Be(updatedOperator.CorrelationId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeOperator = new FakeOperatorBuilder().Build();
        var updatedOperator = new FakeOperatorForUpdate().Generate();
        fakeOperator.DomainEvents.Clear();
        
        // Act
        fakeOperator.Update(updatedOperator);

        // Assert
        fakeOperator.DomainEvents.Count.Should().Be(1);
        fakeOperator.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(OperatorUpdated));
    }
}