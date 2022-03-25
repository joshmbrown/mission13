using System;
using System.Linq;

namespace mission13.Models
{
    public class EFBowlingRepository : IBowlingRepository
    {
        private BowlingDbContext _context { get; set; }

        public EFBowlingRepository(BowlingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Bowler> Bowlers => _context.Bowlers;
        public IQueryable<Team> Teams => _context.Teams;

        public void SaveBowler(Bowler bowler)
        {
            _context.Update(bowler);
            _context.SaveChanges();
        }

        public void AddBowler(Bowler bowler)
        {
            _context.Bowlers.Add(bowler);
            _context.SaveChanges();
        }

        public void DeleteBowler(Bowler bowler)
        {
            _context.Bowlers.Remove(bowler);
            _context.SaveChanges();
        }
    }
}
