using dotnetMAUI.Flashcards.Models;
using Microsoft.EntityFrameworkCore;

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

    internal Stack GetStackById(int stackId)
    {
        return _context.Stacks.Find(stackId);
    }
}
