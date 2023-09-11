using System.ComponentModel.DataAnnotations;

namespace src.Models{
    public class UserInfo{
        public Guid Id{get; set;}
        [Required(ErrorMessage="Cannot be blank")]
        [RegularExpression(@"^[a-zA-Z- ]*$", ErrorMessage ="Cannot contain special character(s)")]
        public string? FirstName{get; set;}
        [Required(ErrorMessage="Cannot be blank")]
        [RegularExpression(@"^[a-zA-Z- ]*$", ErrorMessage ="Cannot contain special character(s)")]
        public string? LastName{get; set;}
        [Required(ErrorMessage="Cannot be blank")]
        [RegularExpression(@"^[a-zA-Z- ]*$", ErrorMessage ="Cannot contain special character(s)")]
        public string? SlackName{get; set;}
        [Required(ErrorMessage="Cannot be blank")]
        [RegularExpression(@"^[a-zA-Z- ]*$", ErrorMessage ="Cannot contain special character(s)")]
        public string? Track{get; set;}
    }
}