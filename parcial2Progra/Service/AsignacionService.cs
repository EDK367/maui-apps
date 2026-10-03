using parcial2Progra.Data;
using parcial2Progra.Models;

namespace parcial2Progra.Service
{
    public class AsignacionService
    {

        private readonly DatabaseService _db;

        public AsignacionService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<Asignacion>> GetAllAsync()
        {
            return await _db.GetAsignacionsAsync();
        }
        public async Task<Asignacion> GetByIdAsync(int id)
        {
            return await _db.GetAsignacionByIdAsync(id);
        }
        public async Task<Asignacion> CreateAsync(Asignacion asignacion)
        {
            await _db.SaveAsignacionAsync(asignacion);
            return asignacion;
        }
        public async Task<Asignacion> UpdateAsync(Asignacion asignacion)
        {
            await _db.UpdateAsignacionAsync(asignacion);
            return asignacion;
        }
        public async Task<int> DeleteAsync(Asignacion asignacion)
        {
            asignacion.Activo = false;
            return await _db.UpdateAsignacionAsync(asignacion);
        }
    }
}
