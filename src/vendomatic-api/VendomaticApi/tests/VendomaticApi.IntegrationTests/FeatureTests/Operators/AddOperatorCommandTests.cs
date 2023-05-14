namespace VendomaticApi.IntegrationTests.FeatureTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.Operators.Features;
using SharedKernel.Exceptions;

public class AddOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_operator_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorForCreationDto().Generate();

        // Act
        var command = new AddOperator.Command(fakeOperatorOne);
        var operatorReturned = await testingServiceScope.SendAsync(command);
        var operatorCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators
            .FirstOrDefaultAsync(o => o.Id == operatorReturned.Id));

        // Assert
        operatorReturned.CorrelationId.Should().Be(fakeOperatorOne.CorrelationId);

        operatorCreated.CorrelationId.Should().Be(fakeOperatorOne.CorrelationId);
    }
}