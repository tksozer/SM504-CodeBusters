namespace VendomaticApi.IntegrationTests.FeatureTests.Users;

using VendomaticApi.SharedTestHelpers.Fakes.User;
using VendomaticApi.Domain.Users.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Users.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateUserCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_user_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        var updatedUserDto = new FakeUserForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var user = await testingServiceScope.ExecuteDbContextAsync(db => db.Users
            .FirstOrDefaultAsync(u => u.Id == fakeUserOne.Id));
        var id = user.Id;

        // Act
        var command = new UpdateUser.Command(id, updatedUserDto);
        await testingServiceScope.SendAsync(command);
        var updatedUser = await testingServiceScope.ExecuteDbContextAsync(db => db.Users.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUser?.FirstName.Should().Be(updatedUserDto.FirstName);
        updatedUser?.LastName.Should().Be(updatedUserDto.LastName);
        updatedUser?.Username.Should().Be(updatedUserDto.Username);
        updatedUser?.Identifier.Should().Be(updatedUserDto.Identifier);
        updatedUser?.Email.Value.Should().Be(updatedUserDto.Email);
    }
}