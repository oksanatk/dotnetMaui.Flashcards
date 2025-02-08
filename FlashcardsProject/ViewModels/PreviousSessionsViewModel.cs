using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class PreviousSessionsViewModel : ObservableObject
{
    public PreviousSessionsViewModel()
    {
    }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
