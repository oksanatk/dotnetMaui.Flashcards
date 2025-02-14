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
    
    public async Task<List<FlashcardStack>> GetAllStacksAsync()
    {
        return await _context.Set<FlashcardStack>().ToListAsync();
    }


}
