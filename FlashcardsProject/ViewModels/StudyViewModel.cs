using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using System.Collections.ObjectModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class StudyViewModel : ObservableObject
{
    private readonly DbRepository _repository;
    private List<FlashcardDTO> studyFlashcards = new();
    private Random random = new();
    private int questionsLeft = 3;
    private int numberCorrect = 0;

    public ObservableCollection<Stack> AllStacks { get; set; } = new();
    public Stack StudyStack { get; set; } = null!;
    public FlashcardDTO CurrentFlashcard { get; private set; } = null!;
    public string UserAnswer { get; set; } = null!;
    public bool UserAnsweredCorrectly { get; set; } = false;
    public bool IsPlayingGame { get => !(HasNotChosenStack || HasCompletedGame); }
    public bool HasNotChosenStack { get; set; } = true;
    public bool HasCompletedGame { get; set; } = false;
    public int Score { get => (int)(numberCorrect / 3.0 * 100); }

    public StudyViewModel(DbRepository repository)
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
        var stacks = await _repository.GetAllStacksAsync();
        foreach(Stack s in stacks)
        {
            AllStacks.Add(s);
        }
    }

    [RelayCommand]
    public void ChooseStack(Stack studyStack)
    {
        StudyStack = studyStack;

        HasNotChosenStack = false;
        OnPropertyChanged(nameof(IsPlayingGame));
        OnPropertyChanged(nameof(HasNotChosenStack));

        studyFlashcards = _repository.GetAllFlashcardsDisplay(studyStack.Id);
        
        DisplayFlashcard();
    }

    private void DisplayFlashcard()
    {
        CurrentFlashcard = studyFlashcards[random.Next(0, studyFlashcards.Count)];
        OnPropertyChanged(nameof(CurrentFlashcard));
    }

    [RelayCommand]
    public async Task SubmitAnswer()
    {
        if (UserAnswer == CurrentFlashcard.Back)
        {
            numberCorrect++;
        }
        questionsLeft--;
        if (questionsLeft > 0)
        {
            UserAnswer = string.Empty;
            OnPropertyChanged(nameof(UserAnswer));
            DisplayFlashcard();
        }else
        {
            HasCompletedGame = true;
            OnPropertyChanged(nameof(IsPlayingGame));
            OnPropertyChanged(nameof(HasCompletedGame));
            OnPropertyChanged(nameof(Score));

            await _repository.CreateNewStudySession(DateTime.Now, Score, StudyStack.Id);
        }
    }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
