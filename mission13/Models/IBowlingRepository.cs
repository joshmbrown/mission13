using System;
using System.Linq;

namespace mission13.Models
{
    public interface IBowlingRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        void SaveBowler(Bowler bowler);
        void AddBowler(Bowler bowler);
        void DeleteBowler(Bowler bowler);
    }
}
