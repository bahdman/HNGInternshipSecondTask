using src.Models;
using src.ViewModels;

namespace src.Services{
    public interface IUser
    {
        Task<UserInfo> Create(string name);
        Task<UserInfo> View(string name);
        Task<bool> Update(string name, UserViewModel viewModel);
        Task<bool> Delete(string name);
    }
}