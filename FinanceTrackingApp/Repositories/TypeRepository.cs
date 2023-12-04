using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class TypeRepository : ITypeReposiroty
    {
        private readonly DataContext _context;

        public TypeRepository(DataContext context)
        {
            _context = context;
        }

        public bool TypeExists(string name)
        {
            return _context.Types.Any(t => t.TypeName.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool TypeExists(int id)
        {
            return _context.Types.Any(t => t.TypeID == id);
        }

        public ICollection<Models.Type> GetTypes()
        {
            return _context.Types.ToList();
        }
        public Models.Type GetType(int id)
        {
            return _context.Types.Where(t => t.TypeID == id).FirstOrDefault();
        }

        public ICollection<Transaction> GetTypeTransations(int id)
        {
            return _context.Transactions.Where(t => t.Type.TypeID == id).ToList();
        }

        public bool CreateType(Models.Type newType)
        {
            _context.Add(newType);
            return Save();
        }

        public bool UpdateType(Models.Type newType)
        {
            _context.Update(newType);
            return Save();
        }

        public bool DeleteType(Models.Type Type)
        {
            _context.Remove(Type);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
