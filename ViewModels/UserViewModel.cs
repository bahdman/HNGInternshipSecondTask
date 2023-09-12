using System.ComponentModel.DataAnnotations;

namespace src.ViewModels{
    public class UserViewModel{
        
        [Required(ErrorMessage="Cannot be blank")]
        [RegularExpression(@"^[a-zA-Z- ]*$", ErrorMessage ="Cannot contain special character(s)")]
        public string? Name{get; set;}
    }
}