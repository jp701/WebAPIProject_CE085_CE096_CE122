using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipePortalWebAPI.Models;

namespace RecipePortalWebAPI.Data
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext context;

        public SQLRecipeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return context.Recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            var recipe = context.Recipes.Where(r => r.id == id).FirstOrDefault();
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

        public string AddRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return "success";
        }

        public bool AddLikeOrDislike(Recipe recipe)
        {
            //var recipe = context.Recipes.Where(r => r.id == id).FirstOrDefault();
            if(recipe!=null)
            {
                try
                {
                    var modifed_recipe = context.Recipes.Attach(recipe);
                    modifed_recipe.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    throw; 
                }
            }
            else
            {
                return false;
            }
        }

    }
}
