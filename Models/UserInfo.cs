using System.ComponentModel.DataAnnotations;

namespace src.Models{
    public class UserInfo{
        public Guid Id{get; set;}
        public string? Name{get; set;}
    }
}