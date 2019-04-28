using System;
using System.Threading.Tasks;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDTO> GetAccountAsync(Guid userId);
        Task RegisterAsync(Guid userId, string email,
            string username, string password, string role = "user");

        Task<TokenDTO> LoginAsync(string email, string password);
    }
}