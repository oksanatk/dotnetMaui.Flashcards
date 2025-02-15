using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using System.Collections.ObjectModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class ManageFlashcardsViewModel : ObservableObject, IQueryAttributable
{
    private readonly DbRepository _repository;
    public int StackId { get; private set; }
    public string StackTitle { get; private set; } = null!;
    public ObservableCollection<FlashcardDTO> AllFlashcards { get; private set; } = new();

    public ManageFlashcardsViewModel(DbRepository repository)
    {
        _repository = repository;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("StackId", out var stackIdValue) && int.TryParse(stackIdValue.ToString(), out int stackId))
        {
            StackId = stackId;
            LoadStackName();
            LoadFlashcards();
        }
    }

    private void LoadStackName()
    {
        Stack currentStack = _repository.GetStackById(StackId);
        StackTitle = $"Manage {currentStack.Name} Stack";
        OnPropertyChanged(nameof(StackTitle));
    }

    private void LoadFlashcards()
    {
        AllFlashcards.Clear();
        List<FlashcardDTO> flashcards = _repository.GetAllFlashcardsDisplay(StackId);
        foreach (FlashcardDTO f in flashcards)
        {
            AllFlashcards.Add(f);
        }
    }




    [RelayCommand]
    Task GoBackToStacks() => Shell.Current.GoToAsync("..");

}
