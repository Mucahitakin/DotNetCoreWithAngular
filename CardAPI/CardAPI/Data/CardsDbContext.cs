using Microsoft.EntityFrameworkCore;

namespace CardAPI.Data { }

public class CardDbContext:DbContext
{

    public CardDbContext(DbContextOptions options): base (options) 
    { }
    public DbSet<Card> Card{ get; set; }
}

