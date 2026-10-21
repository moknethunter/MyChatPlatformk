using Microsoft.AspNetCore.SignalR.Client;
namespace MyChatPlatformk.Mobile;
public partial class MainPage : ContentPage
{
    HubConnection hub; Guid currentConvId = Guid.Parse("11111111-1111-1111-1111-111111111111"); HttpClient http = new HttpClient();
    public MainPage(ChatServer server) { InitializeComponent(); server.Start(); SetupChat(); }
    async void SetupChat()
    {
        await Task.Delay(2000);
        hub = new HubConnectionBuilder().WithUrl("http://127.0.0.1:5000/hubs/chat").Build();
        hub.On<Message>("NewMessage", (msg) => { MainThread.BeginInvokeOnMainThread(() => { MessagesStack.Add(new Label { Text = $"{msg.SenderId}: {msg.Content}", Padding = 5 }); }); });
        await hub.StartAsync(); await hub.InvokeAsync("JoinConversation", currentConvId.ToString());
        http.BaseAddress = new Uri("http://127.0.0.1:5000");
    }
    async void OnSendClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(MessageEntry.Text)) return;
        var req = new { ConversationId = currentConvId, Content = MessageEntry.Text, SenderId = "me" };
        await http.PostAsJsonAsync("/api/messages", req); MessageEntry.Text = "";
    }
}
