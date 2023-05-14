namespace VendomaticApi.IntegrationTests.FeatureTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.Domain.Operators.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class OperatorQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_operator_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne);

        // Act
        var query = new GetOperator.Query(fakeOperatorOne.Id);
        var operator = await testingServiceScope.SendAsync(query);

        // Assert
        operator.CorrelationId.Should().Be(fakeOperatorOne.CorrelationId);
    }

    [Fact]
    public async Task get_operator_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetOperator.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}