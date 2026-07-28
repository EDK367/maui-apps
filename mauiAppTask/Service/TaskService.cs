using mauiAppTask.Models;
using mauiAppTask.Models.Entities;
using SQLite;

namespace mauiAppTask.Service
{
    public class TaskService
    {
        SQLiteAsyncConnection _database;

        public TaskService()
        {
            _database = new SQLiteAsyncConnection(TareaContex.DatabasePath, TareaContex.Flags);
            var result = _database.CreateTableAsync<Tarea>();
        }
        public async Task<int> CreateTask(Tarea newTarea)
        {
            return await _database.InsertAsync(newTarea);
        }

        public async Task<List<Tarea>> GetAllAsync()
        {
            return await _database.Table<Tarea>().ToListAsync();
        }

        public async Task<Tarea> GetTareaByID(int id)
        {   
            return await _database.GetAsync<Tarea>(id);
        }

        public async Task<int> UpdateTarea(Tarea tarea)
        {
            return await _database.UpdateAsync(tarea);
        }

        public async Task<int> DeleteTarea(int id)
        {
            return await _database.DeleteAsync(id);
        }

        public async Task<int> DeleteTarea(Tarea tarea)
        {
            return await _database.DeleteAsync(tarea);
        }
        
    }
}
