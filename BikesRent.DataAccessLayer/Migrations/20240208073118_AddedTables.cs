using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikesRent.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    RentLimit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    SubscriptionId = table.Column<string>(type: "TEXT", nullable: true),
                    SubscriptionExpiration = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RentHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    BikeId = table.Column<string>(type: "TEXT", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentHistory_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_BikeId",
                table: "RentHistory",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_UserId",
                table: "RentHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SubscriptionId",
                table: "User",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentHistory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Subscription");
        }
    }
}
