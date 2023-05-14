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
                name: "inventories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_inventories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "machine_operators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    correlationid = table.Column<Guid>(name: "correlation_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_machine_operators", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    alias = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    machinetype = table.Column<string>(name: "machine_type", type: "text", nullable: true),
                    totalislenumber = table.Column<int>(name: "total_isle_number", type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    machineoperatorid = table.Column<Guid>(name: "machine_operator_id", type: "uuid", nullable: true),
                    createdon = table.Column<DateTime>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: true),
                    lastmodifiedon = table.Column<DateTime>(name: "last_modified_on", type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vending_machines", x => x.id);
                    table.ForeignKey(
                        name: "fk_vending_machines_machine_operators_machine_operator_id",
                        column: x => x.machineoperatorid,
                        principalTable: "machine_operators",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inventory_product",
                columns: table => new
                {
                    inventoriesid = table.Column<Guid>(name: "inventories_id", type: "uuid", nullable: false),
                    productsid = table.Column<Guid>(name: "products_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_product", x => new { x.inventoriesid, x.productsid });
                    table.ForeignKey(
                        name: "fk_inventory_product_inventories_inventories_id",
                        column: x => x.inventoriesid,
                        principalTable: "inventories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inventory_product_products_products_id",
                        column: x => x.productsid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "inventory_vending_machine",
                columns: table => new
                {
                    inventoriesid = table.Column<Guid>(name: "inventories_id", type: "uuid", nullable: false),
                    vendingmachinesid = table.Column<Guid>(name: "vending_machines_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_vending_machine", x => new { x.inventoriesid, x.vendingmachinesid });
                    table.ForeignKey(
                        name: "fk_inventory_vending_machine_inventories_inventories_id",
                        column: x => x.inventoriesid,
                        principalTable: "inventories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inventory_vending_machine_vending_machines_vending_machines",
                        column: x => x.vendingmachinesid,
                        principalTable: "vending_machines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_product_products_id",
                table: "inventory_product",
                column: "products_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_vending_machine_vending_machines_id",
                table: "inventory_vending_machine",
                column: "vending_machines_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_vending_machines_machine_operator_id",
                table: "vending_machines",
                column: "machine_operator_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_product");

            migrationBuilder.DropTable(
                name: "inventory_vending_machine");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "vending_machines");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "machine_operators");
        }
    }
}
