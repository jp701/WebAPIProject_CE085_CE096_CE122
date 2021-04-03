using Microsoft.AspNetCore.Http;
using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.ViewModels
{
    public class RecipeViewModel
    {
        public int id { get; set; }

        public string title { get; set; }

        [DataType(DataType.Text)]
        public string ingredients { get; set; }

        [DataType(DataType.MultilineText)]
        public string method { get; set; }

        public IFormFile image { get; set; }

        public string category { get; set; }

        [DataType(DataType.Text)]
        public string otherdetails { get; set; }
        public int likes { get; set; }

        public int dislikes { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public User User { get; set; } //ref navigation property
        public int UserId { get; set; } //foreign key
    }
}
