namespace VendomaticApi.FunctionalTests.FunctionalTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetOperatorListTests : TestBase
{
    [Fact]
    public async Task get_operator_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Operators.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}