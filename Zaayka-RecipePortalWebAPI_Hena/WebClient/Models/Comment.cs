using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Comment
    {
        public int id { get; set; }

        [DataType(DataType.MultilineText)]
        public string commentText { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime datetime { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
    }
}
