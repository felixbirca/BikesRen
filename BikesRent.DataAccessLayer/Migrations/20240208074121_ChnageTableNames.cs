using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikesRent.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChnageTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_Bikes_BikeId",
                table: "RentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_User_UserId",
                table: "RentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Subscription_SubscriptionId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentHistory",
                table: "RentHistory");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "RentHistory",
                newName: "RentHistories");

            migrationBuilder.RenameIndex(
                name: "IX_User_SubscriptionId",
                table: "Users",
                newName: "IX_Users_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistory_UserId",
                table: "RentHistories",
                newName: "IX_RentHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistory_BikeId",
                table: "RentHistories",
                newName: "IX_RentHistories_BikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentHistories",
                table: "RentHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Bikes_BikeId",
                table: "RentHistories",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Users_UserId",
                table: "RentHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
        
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Bikes_BikeId",
                table: "RentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Users_UserId",
                table: "RentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentHistories",
                table: "RentHistories");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "RentHistories",
                newName: "RentHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SubscriptionId",
                table: "User",
                newName: "IX_User_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistories_UserId",
                table: "RentHistory",
                newName: "IX_RentHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistories_BikeId",
                table: "RentHistory",
                newName: "IX_RentHistory_BikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentHistory",
                table: "RentHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_Bikes_BikeId",
                table: "RentHistory",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_User_UserId",
                table: "RentHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Subscription_SubscriptionId",
                table: "User",
                column: "SubscriptionId",
                principalTable: "Subscription",
                principalColumn: "Id");
        }
    }
}
