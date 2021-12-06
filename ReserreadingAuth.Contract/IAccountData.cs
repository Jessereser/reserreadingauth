using System.Threading.Tasks;
using reserreadingauth.common;

namespace ReserreadingAuth.Contract
{
    public interface IAccountData
    {
        Task<Account> GetGoogleAuthDataAsync(string token);
        Task<bool> GoogleAuthInsertAccount(Account account);
        Task<Account> GoogleAuthSelectAData(string username, string email);
    }
}