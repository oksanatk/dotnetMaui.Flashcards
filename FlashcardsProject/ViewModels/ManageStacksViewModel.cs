using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using dotnetMAUI.Flashcards.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class ManageStacksViewModel : ObservableObject, INotifyPropertyChanged
{
    private readonly DbRepository _repository;
    public ObservableCollection<Stack> AllStacks { get; set; } = new();

    public ManageStacksViewModel(DbRepository repository)
    {
        _repository = repository;
        _ = InitializeAsync();
    }
    
    private async Task InitializeAsync()
    {
        await LoadStacks();
    }

    public async Task LoadStacks()
    {
        AllStacks.Clear();
        var stacks = await _repository.GetAllStacksAsync();
        foreach (Stack s in stacks)
        {
            AllStacks.Add(s);
        }
    }




    [RelayCommand]
    Task ModifyStack(int stackId) => Shell.Current.GoToAsync($"{nameof(ManageFlashcardsPage)}?StackId={stackId}");

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
