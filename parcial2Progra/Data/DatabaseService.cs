using SQLite;
using parcial2Progra.Models;

namespace parcial2Progra.Data;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "parcial2.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<User>().Wait();
        _database.CreateTableAsync<Alumno>().Wait();
        _database.CreateTableAsync<Curso>().Wait();
        _database.CreateTableAsync<Asignacion>().Wait();
    }

    public Task<List<User>> GetUsersAsync() =>
        _database.Table<User>().ToListAsync();

    public Task<int> SaveUserAsync(User user) =>
        _database.InsertAsync(user);


    public Task<List<Alumno>> GetAlumnosAsync() =>
        _database.Table<Alumno>().Where(a => a.Activo).ToListAsync();

    public async Task<List<Alumno>> GetUltimos10AlumnosAsync()
    {
        var todos = await _database.Table<Alumno>().Where(a => a.Activo).ToListAsync();
        return todos.OrderByDescending(a => a.FechaMod).Take(10).ToList();
    }

    public Task<Alumno> GetAlumnoByIdAsync(int id) =>
        _database.Table<Alumno>().Where(x => x.AlumnoId == id).FirstOrDefaultAsync();

    public Task<int> SaveAlumnoAsync(Alumno alumno) =>
        _database.InsertAsync(alumno);

    public Task<int> UpdateAlumnoAsync(Alumno alumno) =>
        _database.UpdateAsync(alumno);


    public Task<List<Curso>> GetCursosAsync() =>
        _database.Table<Curso>().Where(c => c.Activo).ToListAsync();

    public Task<Curso> GetCursoByIdAsync(int id) =>
        _database.Table<Curso>().Where(x => x.CursoId == id).FirstOrDefaultAsync();

    public Task<int> SaveCursoAsync(Curso curso) =>
        _database.InsertAsync(curso);

    public Task<int> UpdateCursoAsync(Curso curso) =>
        _database.UpdateAsync(curso);


    public Task<List<Asignacion>> GetAsignacionsAsync() =>
        _database.Table<Asignacion>().Where(a => a.Activo).ToListAsync();

    public Task<Asignacion> GetAsignacionByIdAsync(int id) =>
        _database.Table<Asignacion>().Where(x => x.AsignacionId == id).FirstOrDefaultAsync();

    public Task<int> SaveAsignacionAsync(Asignacion asignacion) =>
        _database.InsertAsync(asignacion);

    public Task<int> UpdateAsignacionAsync(Asignacion asignacion) =>
        _database.UpdateAsync(asignacion);
}
