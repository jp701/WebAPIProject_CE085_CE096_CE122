using RecipePortalWebAPI.Models;
using RecipePortalWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Data
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext context;

        public SQLRecipeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public string AddRecipe(Recipe recipe)
        {
            //throw new NotImplementedException();
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return "success";
        }
        public string UpdateRecipe(Recipe recipe)
        {
            var modifed_recipe = context.Recipes.Attach(recipe);
            modifed_recipe.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return "success";
        }
        
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return context.Recipes.ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            var recipe = context.Recipes.Where(r => r.id == id).FirstOrDefault();
            //recipe.User = context.Users.Where(u => u.id == recipe.UserId).FirstOrDefault();
            return recipe;
        }
        public IEnumerable<Recipe> GetRecipes(string type, int id)
        {
            if (type == "all") //return all recipes except loggedin user's recipes
                return context.Recipes.Where(r => r.UserId != id);
            else if (type == "my")  //return my recipes
                return context.Recipes.Where(r => r.UserId == id);
            else
                return null;
        }
        public string DeleteRecipe(Recipe recipe)
        {
            context.Recipes.Remove(recipe);
            context.SaveChanges();
            return "success";
        }
    }
}
