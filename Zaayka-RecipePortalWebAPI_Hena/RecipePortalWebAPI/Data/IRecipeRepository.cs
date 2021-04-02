using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Data
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipes();

        IEnumerable<Recipe> GetRecipes(string type, int id);

        Recipe GetRecipeById(int id);

        string AddRecipe(Recipe recipe);

        bool AddLikeOrDislike(Recipe recipe);

        string AddComment(Comment comment);

        IEnumerable<Comment> GetComments(int id);

    }
}
