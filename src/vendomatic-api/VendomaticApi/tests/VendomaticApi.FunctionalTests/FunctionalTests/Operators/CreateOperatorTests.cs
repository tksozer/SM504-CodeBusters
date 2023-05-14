namespace VendomaticApi.FunctionalTests.FunctionalTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateOperatorTests : TestBase
{
    [Fact]
    public async Task create_operator_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeOperator = new FakeOperatorForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Operators.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeOperator);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}