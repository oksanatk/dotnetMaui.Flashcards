using dotnetMAUI.Flashcards.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace dotnetMAUI.Flashcards.Data;

public class DbRepository 
{
    private readonly AppDbContext _context;

    public DbRepository(AppDbContext context)
    {
        _context = context;
    }

    // TODO -- methods here to... get/read entities, add new entities to database, update, delete (CRUD operations here?)
    
    public async Task<List<Stack>> GetAllStacksAsync()
    {
        return await _context.Set<Stack>().ToListAsync();
    }

    public async Task<List<StudySession>> GetAllStudySessionsAsync()
    {
        return await _context.Set<StudySession>().ToListAsync();
    }

    public List<FlashcardDTO> GetAllFlashcardsDisplay(int stackId)
    {
        return _context.Flashcards
            .Where(f => f.StackId == stackId)
            .Select(f => new FlashcardDTO
            {
                Id = f.Id,
                Front = f.Front,
                Back = f.Back
            })
            .ToList();
    }

    public Stack GetStackById(int stackId)
    {
        return _context.Stacks.Find(stackId);
    }

    public async Task CreateNewFlashcard(int stackId, string front, string back)
    {
        await _context.Flashcards.AddAsync(new Flashcard { StackId = stackId, Front = front, Back = back });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFlashcardAsync(FlashcardDTO flashcard)
    {
        var existingFlashcard = await _context.Flashcards.FindAsync(flashcard.Id);
        if (existingFlashcard != null)
        {
            existingFlashcard.Front = flashcard.Front;
            existingFlashcard.Back = flashcard.Back;
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFlashcardAsync(FlashcardDTO flashcard)
    {
        var toBeDeleted = await _context.Flashcards.FindAsync(flashcard.Id);
        if (toBeDeleted != null)
        {
            _context.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
        }
    }
}
