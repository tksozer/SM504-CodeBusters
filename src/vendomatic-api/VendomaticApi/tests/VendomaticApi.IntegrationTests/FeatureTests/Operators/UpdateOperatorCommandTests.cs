namespace VendomaticApi.IntegrationTests.FeatureTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.Domain.Operators.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Operators.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_operator_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        var updatedOperatorDto = new FakeOperatorForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeOperatorOne);

        var operator = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators
            .FirstOrDefaultAsync(o => o.Id == fakeOperatorOne.Id));
        var id = operator.Id;

        // Act
        var command = new UpdateOperator.Command(id, updatedOperatorDto);
        await testingServiceScope.SendAsync(command);
        var updatedOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators.FirstOrDefaultAsync(o => o.Id == id));

        // Assert
        updatedOperator.CorrelationId.Should().Be(updatedOperatorDto.CorrelationId);
    }
}