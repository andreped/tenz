using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace tenz.Services
{
    public class ChatService
    {
        private readonly SQLiteAsyncConnection _database;

        public ChatService(string dbPath)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbPath);
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<ChatMessage>().Wait();
            _database.CreateTableAsync<Conversation>().Wait();
        }

        public Task<List<ChatMessage>> GetMessagesAsync(int conversationId)
        {
            return _database.Table<ChatMessage>().Where(m => m.ConversationId == conversationId).ToListAsync();
        }

        public Task<int> SaveMessageAsync(ChatMessage message)
        {
            return _database.InsertAsync(message);
        }

        public Task<List<Conversation>> GetConversationsAsync()
        {
            return _database.Table<Conversation>().ToListAsync();
        }

        public Task<int> SaveConversationAsync(Conversation conversation)
        {
            return _database.InsertAsync(conversation);
        }
    }

    public class ChatMessage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string? Message { get; set; }
        public bool IsUser { get; set; }
    }

    public class Conversation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
