using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Views;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class ManageStacksViewModel : ObservableObject
{
    internal List<Models.Stack>? AllStacks;

    public ManageStacksViewModel()
    {
    }

    [RelayCommand]
    Task ModifyStack(int stackId) => Shell.Current.GoToAsync(nameof(ManageFlashcardsPage));

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
