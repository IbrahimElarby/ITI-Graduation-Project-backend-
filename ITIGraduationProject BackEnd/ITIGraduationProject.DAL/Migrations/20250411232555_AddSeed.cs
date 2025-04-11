using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITIGraduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLogin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "15e72db2-6c1a-4634-9bf2-c50cf8b39cd4", "admin@example.com", true, null, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAECOkdyLoocLH+q5E1BlE47+1NQrDtza1+L2nftNj5IH6F4gczzpXtCx2Oscr6R8cYw==", null, false, null, null, false, "admin" },
                    { 2, 0, null, "d7b27583-546b-4243-a656-9d4980f33a82", "chef1@example.com", true, null, false, null, "CHEF1@EXAMPLE.COM", "CHEF1", "AQAAAAIAAYagAAAAEIaTUCsTgtpCxNJv9dDP9oSvt9MwzCc4hDeIyPm8lVeHVqWHxcja268fqn0+2bew4Q==", null, false, null, null, false, "chef1" },
                    { 3, 0, null, "b3dd7d7f-7a0e-47cb-b6f7-9a453914cf8d", "foodie@example.com", true, null, false, null, "FOODIE@EXAMPLE.COM", "FOODIE", "AQAAAAIAAYagAAAAENzxPSLbZKLjjaf5/11VTZVxpencAoQ+jt49Ox/GhZVBlQ5Th/Vue6ebP+7BY/80mw==", null, false, null, null, false, "foodie" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Appetizer" },
                    { 2, "Vegetarian" },
                    { 3, "Dessert" },
                    { 4, "Salad" },
                    { 5, "Soup" },
                    { 6, "Italian" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientID", "CaloriesPer100g", "Carbs", "Fats", "Name", "Protein" },
                values: new object[,]
                {
                    { 1, 18m, 3.9m, 0.2m, "Tomato", 0.9m },
                    { 2, 402m, 1.3m, 33m, "Cheese", 25m },
                    { 3, 131m, 25m, 1.1m, "Pasta", 5m }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "BlogPostID", "AuthorID", "Content", "CreatedAt", "FeaturedImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Start with boiling water, salt it properly, and don't forget to stir occasionally.", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "10 Tips for Cooking Perfect Pasta" },
                    { 2, 2, "Discover how olive oil, fresh veggies, and lean protein contribute to long-term health.", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Benefits of Mediterranean Diet" },
                    { 3, 3, "Explore the science behind the most important meal of the day.", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Why Breakfast Matters" },
                    { 4, 1, "Eating healthy is important for maintaining a balanced diet.", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Healthy Eating" },
                    { 5, 2, "Here are some tips for cooking delicious meals.", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cooking Tips" }
                });

            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "CategoryID", "RecipeID" },
                values: new object[] { 3, 2 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientID", "RecipeID", "Quantity", "Unit" },
                values: new object[] { 3, 2, 300m, "g" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeID", "CookingTime", "CreatedAt", "CreatedBy", "CuisineType", "Description", "Instructions", "PrepTime", "Title" },
                values: new object[] { 1, 15, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Italian", "A classic Italian dish.", "Boil pasta, add cheese.", 10, "Spaghetti with Cheese" });

            migrationBuilder.InsertData(
                table: "BlogPostCategories",
                columns: new[] { "BlogPostID", "CategoryID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentID", "BlogPostID", "CreatedAt", "RecipeID", "Text", "UserID" },
                values: new object[] { 1, null, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Great recipe!", 1 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingID", "CreatedAt", "RecipeID", "Score", "UserID" },
                values: new object[] { 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 1 });

            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "CategoryID", "RecipeID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientID", "RecipeID", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, 1, 200m, "g" },
                    { 2, 1, 50m, "grams" },
                    { 3, 1, 100m, "grams" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostID", "CategoryID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostID", "CategoryID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostID", "CategoryID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "BlogPostID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "BlogPostID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "BlogPostID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "RatingID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumns: new[] { "CategoryID", "RecipeID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumns: new[] { "CategoryID", "RecipeID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumns: new[] { "CategoryID", "RecipeID" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientID", "RecipeID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientID", "RecipeID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientID", "RecipeID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientID", "RecipeID" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "BlogPostID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "BlogPostID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "RecipeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
