using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class PreviousSessionsViewModel : INotifyPropertyChanged
{
    private readonly DbRepository _repository;
    private int statsYear;
    private string yearForStats = null!;
    private bool chooseViewallSessions = true;

    public ObservableCollection<StudySession> AllStudySessions { get; set; } = new();
    public string YearForStats { 
        get => yearForStats; 
        set { 
            yearForStats = value; 
            OnPropertyChanged(nameof(YearForStats)); 
        } }
    public bool ChooseViewAllSessions { 
        get => chooseViewallSessions; 
        set { 
            chooseViewallSessions = value;
            OnPropertyChanged(nameof(ChooseViewAllSessions));
            OnPropertyChanged(nameof(ChooseViewStats));
        } }
    public bool ChooseViewStats => !ChooseViewAllSessions;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    public PreviousSessionsViewModel(DbRepository repository)
    {
        _repository = repository;
        InitializeAsync();
    }

    private void InitializeAsync()
    {
        _ = LoadSessions();
    }

    private async Task LoadSessions()
    {
        var retreivedSessions = await _repository.GetAllStudySessionsAsync();
        foreach (StudySession s in retreivedSessions)
        {
            AllStudySessions.Add(s);
        }
    }

    [RelayCommand]
    public void SubmitYearForStats()
    {
        if(int.TryParse(YearForStats, out statsYear))
        {
            //_repository.GetSessionsPerMonth(statsYear);
            //_repository.GetAverageScoresPerMonths(statsYear);
        }
    }

    [RelayCommand]
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
