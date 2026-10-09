using LiteDB;

namespace CrispyApp.Infrastructure.Data;

public class LiteDbContext
{
    public ILiteDatabase Database { get; }

    static LiteDbContext()
    {
        // Global configurations for LiteDB mappings
        // Save enums as string instead of int for better readability
        BsonMapper.Global.EnumAsInteger = false;
    }

    public LiteDbContext(string connectionString)
    {
        Database = new LiteDatabase(connectionString);
    }
}
