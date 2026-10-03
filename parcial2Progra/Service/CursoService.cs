using parcial2Progra.Models;
using parcial2Progra.Data;

namespace parcial2Progra.Service
{
    public class CursoService
    {
        private readonly DatabaseService _db;

        public CursoService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            return await _db.GetCursosAsync();
        }
        public async Task<Curso> GetByIdAsync(int id)
        {
            return await _db.GetCursoByIdAsync(id);
        }
        public async Task<Curso> CreateAsync(Curso curso)
        {
            await _db.SaveCursoAsync(curso);
            return curso;
        }
        public async Task<Curso> UpdateAsync(Curso curso)
        {
            await _db.UpdateCursoAsync(curso);
            return curso;
        }
        public async Task<int> DeleteAsync(Curso curso)
        {
            curso.Activo = false;
            return await _db.UpdateCursoAsync(curso);
        }
    }
}
