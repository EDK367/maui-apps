namespace mauiAppTask.Models
{
    public static class TareaContex
    {
        public static string DatabaseFileName = "TareasSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

    }
}
