namespace FinalProject.Services
{
    public class DbCharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _db;

        public DbCharacterRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
