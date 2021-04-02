using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Models
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string name { get; set; }

        public ICollection<Recipe> Recipes { get; set; } //collection navigation property

        public ICollection<Comment> Comments { get; set; } //collection navigation property
    }
}
