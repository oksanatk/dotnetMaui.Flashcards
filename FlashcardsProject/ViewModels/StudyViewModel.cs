using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class StudyViewModel : ObservableObject
{
    public StudyViewModel() { }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
