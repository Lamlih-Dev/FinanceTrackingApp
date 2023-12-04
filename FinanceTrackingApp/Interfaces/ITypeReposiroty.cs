using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface ITypeReposiroty
    {
        ICollection<Models.Type> GetTypes();
        Models.Type GetType(int id);
        ICollection<Transaction> GetTypeTransations(int id);
        bool TypeExists(string name);
        bool TypeExists(int id);
        bool CreateType(Models.Type newType);
        bool UpdateType(Models.Type newType);
        bool DeleteType(Models.Type Type);
        bool Save();
    }
}
