namespace dotnetMAUI.Flashcards.Models;
internal class StudySession
{
    public int Id { get; }
    public DateTime DateStudied { get; set; }
    public int Score { get; set; }
    public int StackId { get; set; }
}

