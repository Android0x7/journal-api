using JournalAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using JournalAPI.Common;

namespace JournalAPI.Services;

public class MongoDBService
{

    private IMongoCollection<JournalEntry> JournalEntriesCollection;
    private MongoClient Client { get; set; }
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {

        MongoClientSettings settings = MongoClientSettings.FromConnectionString(
            mongoDBSettings.Value.ConnectionURI
        );
        settings.LinqProvider = LinqProvider.V3;
        this.Client = new MongoClient(settings);
        IMongoDatabase database = this.Client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        this.JournalEntriesCollection = database.GetCollection<JournalEntry>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<JournalEntry>?> GetJournalsAsync()
    {
        this.JournalEntriesCollection = this.Client
                                        .GetDatabase("journal-db")
                                        .GetCollection<JournalEntry>("journalEntries");

        var results = from journal in JournalEntriesCollection.AsQueryable()
                          //where journal.TimeStamp.AsDateTime.Day == System.DateTime.Today.Day
                      select journal;

        if (await results.AnyAsync())
            return await results.ToListAsync<JournalEntry>();
        else
            return null;
    }

    public async Task AddJournalsEntryAsync(JournalEntry je)
    {
        await this.JournalEntriesCollection.InsertOneAsync(je);
    }
}