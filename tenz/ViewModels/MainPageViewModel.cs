using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using tenz.Services;

namespace tenz.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ChatService _chatService;
        private int _currentConversationId;
        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Conversation> Conversations { get; set; } = new ObservableCollection<Conversation>();

        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged();
            }
        }

        private string _newConversationTitle;
        public string NewConversationTitle
        {
            get => _newConversationTitle;
            set
            {
                _newConversationTitle = value;
                OnPropertyChanged();
            }
        }

        private Conversation _selectedConversation;
        public Conversation SelectedConversation
        {
            get => _selectedConversation;
            set
            {
                _selectedConversation = value;
                OnPropertyChanged();
                if (value != null)
                {
                    LoadMessages(value.Id);
                }
            }
        }

        public ICommand SendMessageCommand { get; }
        public ICommand AddConversationCommand { get; }

        public MainPageViewModel()
        {
            _userInput = string.Empty;
            _newConversationTitle = string.Empty;
            PropertyChanged = delegate { };
            _chatService = new ChatService("chat.db");

            SendMessageCommand = new Command(async () => await SendMessage());
            AddConversationCommand = new Command(async () => await AddConversation());

            LoadConversations();
        }

        private async void LoadConversations()
        {
            var conversations = await _chatService.GetConversationsAsync();
            foreach (var conversation in conversations)
            {
                Conversations.Add(conversation);
            }
        }

        private async void LoadMessages(int conversationId)
        {
            _currentConversationId = conversationId;
            Messages.Clear();
            var messages = await _chatService.GetMessagesAsync(conversationId);
            foreach (var message in messages)
            {
                Messages.Add(message.IsUser ? $"You: {message.Message}" : $"Tenz: {message.Message}");
            }
        }

        public async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(UserInput))
            {
                Messages.Add($"You: {UserInput}");
                await _chatService.SaveMessageAsync(new ChatMessage { Message = UserInput, IsUser = true, ConversationId = _currentConversationId });
                var response = await GetChatResponse(UserInput);
                Messages.Add($"Tenz: {response}");
                await _chatService.SaveMessageAsync(new ChatMessage { Message = response, IsUser = false, ConversationId = _currentConversationId });
                UserInput = string.Empty;
            }
        }

        public async Task AddConversation()
        {
            if (!string.IsNullOrEmpty(NewConversationTitle))
            {
                var conversation = new Conversation { Title = NewConversationTitle };
                await _chatService.SaveConversationAsync(conversation);
                Conversations.Add(conversation);
                NewConversationTitle = string.Empty;
            }
        }

        private async Task<string> GetChatResponse(string input)
        {
            // Placeholder response instead of ONNX inference
            await Task.Delay(500); // Simulate some processing delay
            var response = "This is a placeholder response."; // Placeholder response

            return response;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}