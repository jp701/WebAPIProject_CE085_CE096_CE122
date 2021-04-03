using RecipePortalWebAPI.Models;
using RecipePortalWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Data
{
    public interface IRecipeRepository
    {
        string AddRecipe(Recipe recipe);
        string UpdateRecipe(Recipe recipe);
        Recipe GetRecipeById(int id);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> GetRecipes(string type, int id);
        string DeleteRecipe(Recipe recipe);
        bool AddLikeOrDislike(Recipe recipe);
        string AddComment(Comment comment);

        IEnumerable<Comment> GetComments(int id);
    }
}
