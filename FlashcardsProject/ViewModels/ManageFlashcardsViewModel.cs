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

    public string NewFront { get; set; }
    public string NewBack { get; set; }
    public bool IsCreatingFlashcard { get; set; } 
    public bool IsNotCreatingFlashcard { get => !IsCreatingFlashcard; }

    public ManageFlashcardsViewModel(DbRepository repository)
    {
        _repository = repository;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("StackId", out var stackIdValue) && int.TryParse(stackIdValue.ToString(), out int stackId))
        {
            StackId = stackId;
            IsCreatingFlashcard = false;
            OnPropertyChanged(nameof(IsCreatingFlashcard));
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
    public void NewFlashcardButton()
    {
        // display entries for front, back, and submit. 
        // make the collectionView invisible
        // in an mvvm-friendly format

        // okay, okay - before I ask chatgpt, let me at least make a guess-attempt at some of the elements that would be required, 
        // so that at least I only need chatgpt for  the missing pieces, not everything. 

        // I'll need the input for. 2 entries and a submit button. 
        // okay, made!
        // the part I'm stuck at: how to adjust visibility if I'm not using x:name= formatting
        IsCreatingFlashcard = true;
    }

    [RelayCommand]
    public void SubmitNewFlashcardInfo()
    {
        if (string.IsNullOrWhiteSpace(NewFront) || string.IsNullOrWhiteSpace(NewBack)) return;

        _repository.CreateNewFlashcard(StackId, NewFront, NewBack);

        LoadFlashcards();
        NewFront = string.Empty;
        NewBack = string.Empty;
        IsCreatingFlashcard = false;
    }


    [RelayCommand]
    Task GoBackToStacks() => Shell.Current.GoToAsync("..");

}
