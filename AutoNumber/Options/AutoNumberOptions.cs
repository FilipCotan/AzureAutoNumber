using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace AutoNumber.Options;

public class AutoNumberOptions
{
    public int BatchSize { get; set; } = 100;
        
    public int MaxWriteAttempts { get; set; } = 20;

    public Dictionary<string, long> InitialScopesAvailableNumber { get; set; }

    public string DatabaseId { get; set; }

    public string ContainerName { get; set; } = "autoNumberStates";

    public string ConnectionString { get; set; }
        
    public CosmosClient Client { get; set; }
}
