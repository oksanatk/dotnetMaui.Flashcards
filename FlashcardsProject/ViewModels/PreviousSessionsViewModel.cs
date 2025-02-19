using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dotnetMAUI.Flashcards.Data;
using dotnetMAUI.Flashcards.Models;
using System.Collections.ObjectModel;

namespace dotnetMAUI.Flashcards.ViewModels;

public partial class PreviousSessionsViewModel : ObservableObject
{
    private readonly DbRepository _repository;

    public ObservableCollection<StudySession> AllStudySessions { get; set; } = new();
    
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
    Task GoBackHome() => Shell.Current.GoToAsync("..");
}
