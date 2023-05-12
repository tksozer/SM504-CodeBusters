namespace VendomaticApi.SharedTestHelpers.Fakes.User;

using AutoBogus;
using VendomaticApi.Domain;
using VendomaticApi.Domain.Users.Dtos;
using VendomaticApi.Domain.Roles;
using VendomaticApi.Domain.Users.Models;

public sealed class FakeUserForCreation : AutoFaker<UserForCreation>
{
    public FakeUserForCreation()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}