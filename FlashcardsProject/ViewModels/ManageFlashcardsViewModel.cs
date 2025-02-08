using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class ManageFlashcardsViewModel : ObservableObject
{
    public ManageFlashcardsViewModel()
    {
    }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");

}
