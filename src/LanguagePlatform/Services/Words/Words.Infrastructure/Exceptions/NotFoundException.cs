namespace Words.Infrastructure.Exceptions;

public class NotFoundException : KeyNotFoundException
{
    public NotFoundException(string tableName, int id)
        : base($"Not found entity in : {tableName} with id: {id}")
    {
    }
}
