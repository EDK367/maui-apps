using parcial2Progra.Models;
using parcial2Progra.Data;

namespace parcial2Progra.Service
{
    public class AlumnoService
    {
        private readonly DatabaseService _db;

        public AlumnoService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<Alumno>> GetAllAsync()
        {
            return await _db.GetAlumnosAsync();
        }
        public async Task<List<Alumno>> GetUltimos10Async()
        {
            return await _db.GetUltimos10AlumnosAsync();
        }
        public async Task<Alumno> GetByIdAsync(int id)
        {
            return await _db.GetAlumnoByIdAsync(id);
        }
        public async Task<Alumno> CreateAsync(Alumno alumno)
        {
            await _db.SaveAlumnoAsync(alumno);
            return alumno;
        }
        public async Task<Alumno> UpdateAsync(Alumno alumno)
        {
            await _db.UpdateAlumnoAsync(alumno);
            return alumno;
        }
        public async Task<int> DeleteAsync(Alumno alumno)
        {
            alumno.Activo = false;
            return await _db.UpdateAlumnoAsync(alumno);
        }
    }
}
