using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootLibrary
{
    public class Match
    {
        private Club ClubAway;
        private Club ClubHome;
        private int GoalsAway;
        private int GoalsHome;
        private Boolean forfeitAway;
        private Boolean forfeitHome;

        public Match(Club _Away, Club _Home, Boolean _Awayforfeit, Boolean _Homeforfeit)
        {
            ClubAway = _Away;
            ClubHome = _Home;
            forfeitAway = _Awayforfeit;
            forfeitHome = _Homeforfeit;
        }

        public Match(Club _Away,Club _Home,int _AwayGoals,int _HomeGoals)
        {
            ClubAway = _Away;
            ClubHome = _Home;
            GoalsAway = _AwayGoals;
            GoalsHome = _HomeGoals;
        }

        public Club Away
        {
            get
            {
                return ClubAway;
            }
        }

        public int AwayGoals
        {
            get
            {
                return GoalsAway;
            }
        }

        public Club Home
        {
            get
            {
                return ClubHome;
            }
        }

        public int HomeGoals
        {
            get
            {
                return GoalsHome;
            }
        }

        public bool IsAwayForfeit
        {
            get
            {
                return forfeitAway;
            }
        }

        public bool IsDraw
        {
            get
            {
                if (HomeGoals == AwayGoals)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsHomeForfeit
        {
            get
            {
                return forfeitHome;
            }
        }
    }

    public class Club
    {
        private String _name;

        public Club(String name)
        {
            this._name = name;
        }

        public override String ToString()
        {
            return _name;
        }
    }

    public abstract class PointSystem
    {
        private bool IsHome;
        private ITotal Points;

        public ITotal InitialPoints
        {
            get;
        }

        public ITotal GetPointsFromMatch(Match m, bool isHome)
        {
                return Points;
        }

        public interface ITotal : IComparable
        {
            public void Increment(ITotal with)
            {
                
            }

            public override String ToString()
            {
                return " ";
            }
        }
    }

    public class Ranking
    {
        private class RankingEntry : IComparable
        {
            private Club club;
            private PointSystem.ITotal points;

            public RankingEntry(Club club, PointSystem.ITotal points)
            {
                this.club = club;
                this.points = points;
            }
            public PointSystem.ITotal Points
            {
                get { return this.points; }
            }
            public Club Club
            {
                get { return this.club; }
            }

            #region IComparable Membres

            public int CompareTo(object obj)
            {
                return -this.points.CompareTo(((RankingEntry)obj).Points);
            }

            #endregion
        }
        private PointSystem system;
        private RankingEntry[] entries;

        public Ranking(PointSystem system, Club[] clubs)
        {
            this.system = system;
            this.entries = new RankingEntry[clubs.Length];
            for (int i = 0; i < clubs.Length; i++)
                this.entries[i] = new RankingEntry(clubs[i], system.InitialPoints);
        }
        private RankingEntry EntryFromClub(Club c)
        {
            foreach (RankingEntry entry in entries)
                if (entry.Club == c)
                    return entry;
            return null;
        }
        public void Register(Match m)
        {
            EntryFromClub(m.Home).Points.Increment(system.GetPointsFromMatch(m, true));
            EntryFromClub(m.Away).Points.Increment(system.GetPointsFromMatch(m, false));
            Array.Sort(entries);
        }
        public Club GetClub(int index)
        {
            return entries[index].Club;
        }
        public PointSystem.ITotal GetPoints(int index)
        {
            return entries[index].Points;
        }
        public PointSystem.ITotal GetPoints(Club club)
        {
            return EntryFromClub(club).Points;
        }
    }

    public sealed class FrenchLeague1PointSystem
    {
        private static readonly Lazy<FrenchLeague1PointSystem> TheInstance = new Lazy<FrenchLeague1PointSystem>(() => new FrenchLeague1PointSystem());

        private FrenchLeague1PointSystem()
        {

        }


        public class PointTotal
        {
            private int goalaverage;
            private int points;

            public int CompareTo(object obj)
            {
                return points - ((PointTotal)obj).points;
            }

            public void Increment(PointSystem.ITotal with)
            {
                points = ((PointTotal)with).points;
            }

            public PointTotal()
            {
                points = 0;
            }
            public PointTotal(Match m, bool home)
            {
                points += m.HomeGoals - m.AwayGoals;
            }
            public string ToString()
            {
                return "";
            }
        }

        public override PointTotal GetPointsFromMatch(Match m, bool isHome)
        {
            return new PointTotal(m, isHome);
        }

        public override PointTotal InitialPoints
        {
            get { 
                return new PointTotal();
                }
        }
    }

   
}
