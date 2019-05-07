using PAS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.DAO
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _context = null;
        private DbSet<T> table = null;
        private string connectionstring = string.Empty;
        public GenericRepository()
        {
            this._context = new DbContext(connectionstring);
            table = _context.Set<T>();
        }
        public GenericRepository(DbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public T Insert(T obj)
        {
            table.Add(obj);
            obj = table.Find(obj);
            return obj;
        }
        public void Update(T obj)
        {
            table.Attach(obj);
           _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
