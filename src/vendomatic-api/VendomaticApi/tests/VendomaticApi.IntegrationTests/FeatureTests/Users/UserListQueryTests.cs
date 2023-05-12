namespace VendomaticApi.IntegrationTests.FeatureTests.Users;

using VendomaticApi.Domain.Users.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.User;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Users.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class UserListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_user_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        var fakeUserTwo = new FakeUserBuilder().Build();
        var queryParameters = new UserParametersDto();

        await testingServiceScope.InsertAsync(fakeUserOne, fakeUserTwo);

        // Act
        var query = new GetUserList.Query(queryParameters);
        var users = await testingServiceScope.SendAsync(query);

        // Assert
        users.Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task must_be_permitted()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        testingServiceScope.SetUserNotPermitted(Permissions.CanReadUsers);
        var queryParameters = new UserParametersDto();

        // Act
        var command = new GetUserList.Query(queryParameters);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }
}