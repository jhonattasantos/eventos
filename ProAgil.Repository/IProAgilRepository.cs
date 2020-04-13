using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //GERAl
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetAllEventoAsyncById(int eventoId, bool includePalestrantes);

         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos);
         Task<Palestrante> GetAllPalestranteAsyncById(int palestranteId, bool includeEventos);
    }
}