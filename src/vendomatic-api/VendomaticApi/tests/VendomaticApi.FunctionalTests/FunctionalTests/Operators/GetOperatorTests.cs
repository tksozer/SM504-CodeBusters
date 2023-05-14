namespace VendomaticApi.FunctionalTests.FunctionalTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetOperatorTests : TestBase
{
    [Fact]
    public async Task get_operator_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeOperator = new FakeOperatorBuilder().Build();
        await InsertAsync(fakeOperator);

        // Act
        var route = ApiRoutes.Operators.GetRecord(fakeOperator.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}