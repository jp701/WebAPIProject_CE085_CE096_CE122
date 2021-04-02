using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePortalWebAPI.Data;
using RecipePortalWebAPI.Models;
using RecipePortalWebAPI.ViewModels;

namespace RecipePortalWebAPI.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RecipeController(IHostingEnvironment hostingEnvironment, IRecipeRepository recipeRepository, IAccountRepository accountRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _recipeRepository = recipeRepository;
            _accountRepository = accountRepository;
        }

        [Route("getallrecipes")]
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            try
            {
                var recipes = _recipeRepository.GetAllRecipes();
                if (recipes != Enumerable.Empty<Recipe>())
                {
                    return Ok(recipes);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status404NotFound, "Recipes Not Found..!!");
        }

        [Route("getallrecipes/{type}/{id}")]
        [HttpGet]
        public IActionResult GetAllRecipes(string type, int id)
        {
            try
            {
                var recipes = _recipeRepository.GetRecipes(type, id);
                if (recipes != Enumerable.Empty<Recipe>())
                {
                    return Ok(recipes);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status404NotFound, "Recipes Not Found..!!");
        }

        [Route("getrecipe/{id}")]
        [HttpGet]
        public IActionResult GetRecipeById(int id)
        {
            try
            {
                var recipe = _recipeRepository.GetRecipeById(id);
                if (recipe != null)
                {
                    //recipe.User = _accountRepository.GetUser(recipe.UserId);
                    return Ok(recipe);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status404NotFound, "Recipes Not Found..!!");
        }

        [Route("addrecipe")]
        [HttpPost]
        public IActionResult AddRecipe(Recipe newrecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*string uniquefilename = null;
            if(recipeViewModel.image!=null)
            {
                string uploadsfolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniquefilename = Guid.NewGuid().ToString() + "_" + recipeViewModel.image.FileName;
                string filePath = Path.Combine(uploadsfolder, uniquefilename);
                recipeViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));
            }*/
            /*Recipe newrecipe = new Recipe
            {
                title = recipeViewModel.title,
                ingredients = recipeViewModel.ingredients,
                method = recipeViewModel.method,
                category = recipeViewModel.category,
                image = uniquefilename,
                otherdetails = recipeViewModel.otherdetails,
                likes = 0,
                dislikes = 0,
                Date = DateTime.Now,
                UserId = (int)recipeViewModel.UserId
            };*/
            string response = _recipeRepository.AddRecipe(newrecipe);
            if (response == "success")
                return Ok(response);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [Route("addlike")]
        [HttpPost]
        public IActionResult AddLike([FromBody] int id)
        {
            try
            {
                var recipe = _recipeRepository.GetRecipeById(id);
                recipe.likes += 1;
                var state = _recipeRepository.AddLikeOrDislike(recipe);
                if (state == true)
                {
                    Ok(state);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("adddislike")]
        [HttpPost]
        public IActionResult AddDislike([FromBody] int id)
        {
            try
            {
                var recipe = _recipeRepository.GetRecipeById(id);
                recipe.dislikes += 1;
                var state = _recipeRepository.AddLikeOrDislike(recipe);
                if (state == true)
                {
                    Ok(state);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("addcomment")]
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            try
            {
                var result = _recipeRepository.AddComment(comment);
                if (result == "success")
                {
                    return Ok(StatusCodes.Status200OK);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("viewcomment/{id}")]
        [HttpGet]
        public IActionResult ViewComment(int id)
        {
            try
            {
                var comments = _recipeRepository.GetComments(id);
                if (comments != null)
                {
                    return Ok(comments);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode(StatusCodes.Status404NotFound, "No comments yet..!!");
        }
    }
}