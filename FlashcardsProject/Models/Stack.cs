namespace dotnetMAUI.Flashcards.Models;
internal class Stack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Flashcard> Flashcards { get; set; } = new();
}
