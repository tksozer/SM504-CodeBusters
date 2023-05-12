using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendomaticApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventorys",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventoryid = table.Column<int>(name: "inventory_id", type: "integer", nullable: true),
                    vendingmachineid = table.Column<int>(name: "vending_machine_id", type: "integer", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false),
                    islenumber = table.Column<int>(name: "isle_number", type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unitprice = table.Column<decimal>(name: "unit_price", type: "numeric", nullable: false),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventorys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unitprice = table.Column<decimal>(name: "unit_price", type: "numeric", nullable: false),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true),
                    permission = table.Column<string>(type: "text", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identifier = table.Column<string>(type: "text", nullable: true),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: true),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vending_machines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vendingmachineid = table.Column<int>(name: "vending_machine_id", type: "integer", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    totalislenumber = table.Column<int>(name: "total_isle_number", type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vending_machines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                table: "user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventorys");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "vending_machines");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
