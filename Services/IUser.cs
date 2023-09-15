using src.Models;
using src.ViewModels;

namespace src.Services{
    public interface IUser
    {
        Task<UserInfo> Create(string name);
        Task<UserInfo> View(Guid id);
        Task<UserInfo> Update(Guid id, UserViewModel viewModel);
        Task<bool> Delete(Guid id);
    }
}