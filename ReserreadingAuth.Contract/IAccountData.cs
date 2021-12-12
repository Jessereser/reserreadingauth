using System.Threading.Tasks;
using reserreadingauth.common;

namespace ReserreadingAuth.Contract
{
    public interface IAccountData
    {
        Task<Account> GetGoogleAuthDataAsync(string token);
        Task<Account> GoogleAuthInsertAccount(Account account);
        Task<Account> GoogleAuthSelectAData(string username, string email);
        Task<Account> InsertAccount(Account account);
        Task<Account> SelectAccount(string accountId);
    }
}