using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Core;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class StudyViewModel : INotifyPropertyChanged
{
    private readonly DbRepository _repository;
    private List<FlashcardDTO> studyFlashcards = new();
    private FlashcardDTO currentFlashcard = null!;
    private Random random = new();
    private int questionsLeft = 3;
    private int numberCorrect = 0;
    private string userAnswer = "";
    private int score;
    private bool hasNotChosenStack = true;
    private bool hasCompletedGame = false;

    public ObservableCollection<Stack> AllStacks { get; set; } = new();
    public Stack StudyStack { get; set; } = null!;
    public FlashcardDTO CurrentFlashcard
    {
        get => currentFlashcard;
        set
        {
            currentFlashcard = value;
            OnPropertyChanged(nameof(CurrentFlashcard));
        }
    }
    public string UserAnswer
    {
        get => userAnswer;
        set
        {
            userAnswer = value;
            OnPropertyChanged(nameof(UserAnswer));
        }
    }
    public bool UserAnsweredCorrectly { get; set; } = false;
    public bool IsPlayingGame => !(HasNotChosenStack || HasCompletedGame);
    public bool HasNotChosenStack
    {
        get => hasNotChosenStack;
        set
        {
            hasNotChosenStack = value;
            OnPropertyChanged(nameof(HasNotChosenStack));
            OnPropertyChanged(nameof(IsPlayingGame));
        }
    }
    public bool HasCompletedGame
    {
        get => hasCompletedGame;
        set
        {
            hasCompletedGame = value;
            OnPropertyChanged(nameof(HasCompletedGame));
            OnPropertyChanged(nameof(IsPlayingGame));
        }
    }
    public int Score
    {
        get => score;
        set
        {
            score = (int)(numberCorrect / 3.0 * 100);
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(ScoreText));
        }
    }                   
    public string ScoreText
    {
        get => $"You Got {Score.ToString()}% Correct!";
    }
    public event PropertyChangedEventHandler PropertyChanged = null!;

    public StudyViewModel(DbRepository repository)
    {
        _repository = repository;
        _ = InitializeAsync();
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        studyFlashcards = _repository.GetAllFlashcardsDisplay(studyStack.Id);
        
        DisplayFlashcard();
    }

    private void DisplayFlashcard()
    {
        CurrentFlashcard = studyFlashcards[random.Next(0, studyFlashcards.Count)];
    }

    [RelayCommand]
    public async Task SubmitAnswer()
    {
        if (UserAnswer == CurrentFlashcard.Back)
        {
            UserAnsweredCorrectly = true;
            //await Task.Delay(2500);
            numberCorrect+=1;
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

            await _repository.CreateNewStudySession(DateTime.Now, Score, StudyStack.Id);
        }
    }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
