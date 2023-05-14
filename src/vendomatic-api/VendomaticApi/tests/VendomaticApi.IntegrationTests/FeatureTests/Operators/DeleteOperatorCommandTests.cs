namespace VendomaticApi.IntegrationTests.FeatureTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.Domain.Operators.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_operator_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne);
        var operator = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators
            .FirstOrDefaultAsync(o => o.Id == fakeOperatorOne.Id));

        // Act
        var command = new DeleteOperator.Command(operator.Id);
        await testingServiceScope.SendAsync(command);
        var operatorResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators.CountAsync(o => o.Id == operator.Id));

        // Assert
        operatorResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_operator_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteOperator.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_operator_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne);
        var operator = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators
            .FirstOrDefaultAsync(o => o.Id == fakeOperatorOne.Id));

        // Act
        var command = new DeleteOperator.Command(operator.Id);
        await testingServiceScope.SendAsync(command);
        var deletedOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.Operators
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == operator.Id));

        // Assert
        deletedOperator?.IsDeleted.Should().BeTrue();
    }
}