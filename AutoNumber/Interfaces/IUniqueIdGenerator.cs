namespace AutoNumber.Interfaces;

public interface IUniqueIdGenerator
{
    int BatchSize { get; set; }
    
    int MaxWriteAttempts { get; set; }

    /// <summary>
    ///     Generate a new incremental id regards the scope name
    /// </summary>
    /// <param name="scopeName">Generator use this scope name to generate different ids for different scopes</param>
    /// <returns></returns>
    long NextId(string scopeName);
}