using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using dotnetMAUI.Flashcards.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class ManageStacksViewModel : ObservableObject
{
    private readonly DbRepository _repository;
    public ObservableCollection<FlashcardStack> AllStacks { get; set; } = new();

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
        foreach (FlashcardStack s in stacks)
        {
            AllStacks.Add(s);
        }
    }




    [RelayCommand]
    Task ModifyStack(int stackId) => Shell.Current.GoToAsync(nameof(ManageFlashcardsPage));

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
