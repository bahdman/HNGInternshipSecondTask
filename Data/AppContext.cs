using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options){}

        public DbSet<UserInfo> UserInfos{get; set;}
    }
}