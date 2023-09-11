using System;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace AutoNumber.Options;

public class AutoNumberOptionsBuilder
{
    private const string AutoNumber = "AutoNumber";
    private readonly IConfiguration _configuration;

    public AutoNumberOptionsBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
        configuration.GetSection(AutoNumber).Bind(Options);
    }

    public AutoNumberOptions Options { get; } = new();

    /// <summary>
    ///     Uses the default StorageAccount already defined in dependency injection
    /// </summary>
    public AutoNumberOptionsBuilder UseDefaultStorageAccount()
    {
        Options.ConnectionString = null;
        return this;
    }

    /// <summary>
    ///     Uses an Azure CosmosDB connection string to init the cosmos client
    /// </summary>
    /// <param name="connectionStringSection"></param>
    public AutoNumberOptionsBuilder UseConnectionStringSection(string connectionStringSection)
    {
        if (string.IsNullOrEmpty(connectionStringSection))
            throw new ArgumentNullException(nameof(connectionStringSection));

        Options.ConnectionString =
            _configuration.GetSection(connectionStringSection).Value;

        return this;
    }

    public AutoNumberOptionsBuilder UseCosmosServiceClient(CosmosClient cosmosClient)
    {
        Options.Client = cosmosClient
                         ?? throw new ArgumentNullException(nameof(cosmosClient));

        return this;
    }

    /// <summary>
    ///     Max retrying to generate unique id
    /// </summary>
    /// <param name="attempts"></param>
    public AutoNumberOptionsBuilder SetMaxWriteAttempts(int attempts = 100)
    {
        Options.MaxWriteAttempts = attempts;
        return this;
    }

    /// <summary>
    ///     BatchSize for id generation, higher the value more losing unused id
    /// </summary>
    /// <param name="batchSize"></param>
    public AutoNumberOptionsBuilder SetBatchSize(int batchSize = 100)
    {
        Options.BatchSize = batchSize;
        return this;
    }

    /// <summary>
    ///     Set Cosmos Container Name
    /// </summary>
    /// <param name="containerName">Container name to use for maning identifiers</param>
    public AutoNumberOptionsBuilder SetContainerName(string containerName)
    {
        Options.ContainerName = containerName;
        return this;
    }

    /// <summary>
    ///     Set Cosmos Database Id
    /// </summary>
    /// <param name="databaseId">Database identifier</param>
    public AutoNumberOptionsBuilder SetDatabaseId(string databaseId)
    {
        Options.DatabaseId = databaseId;
        return this;
    }
}
