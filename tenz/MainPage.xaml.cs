using Microsoft.Maui.Controls;
using tenz.ViewModels;

namespace tenz;

public partial class MainPage : ContentPage
{
    private MainPageViewModel ViewModel;

    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainPageViewModel();
        BindingContext = ViewModel;
    }

    private async void OnSendClicked(object sender, EventArgs e)
    {
        await ViewModel.SendMessage();
    }
}
