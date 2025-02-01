using System;
using System.Collections.Generic;
using System.Linq;
namespace dotnetMAUI.Flashcards.Models;
internal class Flashcard
{
    public int Id { get; set; }
    public int StackId { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
}
