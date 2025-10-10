using APICuidadosCapilar.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : class
    {
        public DBRotinaCapilarContext _context;
        public bool _saveChanges = true;

        public RepositoryBase(DBRotinaCapilarContext context, bool _saveChanges = true)
        {
            _context = context;
            _saveChanges = _saveChanges;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public T Alterar(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            if (_saveChanges) 
            {
                _context.SaveChanges();
            }
            return obj;
        }

        public async Task<T> AlterarAsync(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            if (_saveChanges)
            {
                await _context.SaveChangesAsync();
            }
            return obj;
        }

        public void Excluir(T obj)
        {
            _context.Set<T>().Remove(obj);
            if (_saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Excluir(params object[] variavel)
        {
            var obj = SelecionarPk(variavel);
            Excluir(obj);
        }

        public async Task ExcluirAsync(T obj)
        {
            _context.Set<T>().Remove(obj);
            if (_saveChanges)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task ExcluirAsync(params object[] variavel)
        {
            var obj = await SelecionarPkAsync(variavel);
            ExcluirAsync(obj);
        }

        public T Incluir(T obj)
        {
            _context.Set<T>().Add(obj);
            if (_saveChanges)
            {
                _context.SaveChanges();
            }
            return obj;
        }

        public async Task<T> IncluirAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            if (_saveChanges)
            {
                await _context.SaveChangesAsync();
            }
            return obj;
        }

        public T SelecionarPk(params object[] variavel)
        {
            var obj = _context.Set<T>().Find(variavel);
            return obj;
        }

        public async Task<T> SelecionarPkAsync(params object[] variavel)
        {
            var obj = await _context.Set<T>().FindAsync(variavel);
            return obj;
        }

        public List<T> SelecionarTodos()
        {
            var obj = _context.Set<T>().ToList();
            return obj;
        }

        public async Task<List<T>> SelecionarTodosAsync()
        {
            var obj = await _context.Set<T>().ToListAsync();
            return obj;
        }
    }
}
