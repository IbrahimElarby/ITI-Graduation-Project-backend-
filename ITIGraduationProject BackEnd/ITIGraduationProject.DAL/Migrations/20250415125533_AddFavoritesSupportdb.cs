using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIGraduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoritesSupportdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserID",
                table: "FavoriteRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeID",
                table: "FavoriteRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipe",
                newName: "FavoriteRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipe_UserID",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes",
                columns: new[] { "RecipeID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserID",
                table: "FavoriteRecipes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserID",
                table: "FavoriteRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipes",
                newName: "FavoriteRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserID",
                table: "FavoriteRecipe",
                newName: "IX_FavoriteRecipe_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe",
                columns: new[] { "RecipeID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserID",
                table: "FavoriteRecipe",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeID",
                table: "FavoriteRecipe",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
