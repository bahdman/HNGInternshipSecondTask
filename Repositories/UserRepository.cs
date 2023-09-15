using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Services;
using src.ViewModels;

namespace src.Repositories{
    public class UserRepository : IUser
    {

        private readonly Data.AppContext _context;

        public UserRepository(Data.AppContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> Create(string name)
        {
            UserInfo instance = new () {
                Name = name
            };

            try{
                await _context.UserInfos.AddAsync(instance);
                await _context.SaveChangesAsync();
                return instance;
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var userInstance = await View(id);
            if(userInstance == null)
            {
                return false;
            }

            try{

                _context.UserInfos.Remove(userInstance);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public async Task<UserInfo> Update(Guid id, UserViewModel viewModel)
        {
            var userInstance = await View(id);
            if(userInstance == null)
            {
                return null;
            }

            userInstance.Name = viewModel.Name;

            try{
                 _context.UserInfos.Update(userInstance);
                 await _context.SaveChangesAsync();
                 return await View(id);
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public Task<UserInfo> View(Guid id)
        {
            try{
                var user = _context.UserInfos.Where(m => m.Id == id).FirstOrDefaultAsync();
                return user;
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
            
            
        }
    }
}