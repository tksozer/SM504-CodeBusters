namespace VendomaticApi.IntegrationTests.FeatureTests.Operators;

using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Operators.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class OperatorListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_operator_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        var fakeOperatorTwo = new FakeOperatorBuilder().Build();
        var queryParameters = new OperatorParametersDto();

        await testingServiceScope.InsertAsync(fakeOperatorOne, fakeOperatorTwo);

        // Act
        var query = new GetOperatorList.Query(queryParameters);
        var operators = await testingServiceScope.SendAsync(query);

        // Assert
        operators.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}