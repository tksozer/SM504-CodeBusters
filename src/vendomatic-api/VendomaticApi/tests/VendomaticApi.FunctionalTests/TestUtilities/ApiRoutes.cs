namespace VendomaticApi.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class MachineOperators
    {
        public static string GetList => $"{Base}/machineOperators";
        public static string GetRecord(Guid id) => $"{Base}/machineOperators/{id}";
        public static string Delete(Guid id) => $"{Base}/machineOperators/{id}";
        public static string Put(Guid id) => $"{Base}/machineOperators/{id}";
        public static string Create => $"{Base}/machineOperators";
        public static string CreateBatch => $"{Base}/machineOperators/batch";
    }

    public static class Inventories
    {
        public static string GetList => $"{Base}/inventories";
        public static string GetRecord(Guid id) => $"{Base}/inventories/{id}";
        public static string Delete(Guid id) => $"{Base}/inventories/{id}";
        public static string Put(Guid id) => $"{Base}/inventories/{id}";
        public static string Create => $"{Base}/inventories";
        public static string CreateBatch => $"{Base}/inventories/batch";
    }

    public static class Products
    {
        public static string GetList => $"{Base}/products";
        public static string GetRecord(Guid id) => $"{Base}/products/{id}";
        public static string Delete(Guid id) => $"{Base}/products/{id}";
        public static string Put(Guid id) => $"{Base}/products/{id}";
        public static string Create => $"{Base}/products";
        public static string CreateBatch => $"{Base}/products/batch";
    }

    public static class VendingMachines
    {
        public static string GetList => $"{Base}/vendingMachines";
        public static string GetRecord(Guid id) => $"{Base}/vendingMachines/{id}";
        public static string Delete(Guid id) => $"{Base}/vendingMachines/{id}";
        public static string Put(Guid id) => $"{Base}/vendingMachines/{id}";
        public static string Create => $"{Base}/vendingMachines";
        public static string CreateBatch => $"{Base}/vendingMachines/batch";
    }

    public static class Users
    {
        public static string GetList => $"{Base}/users";
        public static string GetRecord(Guid id) => $"{Base}/users/{id}";
        public static string Delete(Guid id) => $"{Base}/users/{id}";
        public static string Put(Guid id) => $"{Base}/users/{id}";
        public static string Create => $"{Base}/users";
        public static string CreateBatch => $"{Base}/users/batch";
        public static string AddRole(Guid id) => $"{Base}/users/{id}/addRole";
        public static string RemoveRole(Guid id) => $"{Base}/users/{id}/removeRole";
    }

    public static class RolePermissions
    {
        public static string GetList => $"{Base}/rolePermissions";
        public static string GetRecord(Guid id) => $"{Base}/rolePermissions/{id}";
        public static string Delete(Guid id) => $"{Base}/rolePermissions/{id}";
        public static string Put(Guid id) => $"{Base}/rolePermissions/{id}";
        public static string Create => $"{Base}/rolePermissions";
        public static string CreateBatch => $"{Base}/rolePermissions/batch";
    }
}
