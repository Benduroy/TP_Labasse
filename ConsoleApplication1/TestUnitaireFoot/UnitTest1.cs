using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootLibrary;

namespace TestUnitaireFoot
{
    [TestClass]
    public class TestMatch
    {
        private Club ClubExterieur;
        private Club ClubHome;

        [TestInitialize]
        public void Init()
        {
            ClubExterieur = new Club("EquipeExterieur");
            ClubHome = new Club("EquipeInterieur");
        }

        [TestMethod]
        public void TestAway()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, false,true);
            Assert.AreEqual(UnMatch.Away, ClubExterieur);
        }

        [TestMethod]
        public void TestHome()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, false,true);
            Assert.AreEqual(UnMatch.Home, ClubHome);
        }
        [TestMethod]
        public void AwayGoals()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, 1, 0);
            Assert.AreEqual(UnMatch.AwayGoals, 1);
        }

        [TestMethod]
        public void HomeGoals()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, 0, 1);
            Assert.AreEqual(UnMatch.HomeGoals, 1);
        }

        [TestMethod]
        public void IsAwayForfeit()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, true, false);
            Assert.AreEqual(UnMatch.IsAwayForfeit, true);
        }

        [TestMethod]
        public void IsDraw()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, 1, 1);
            Assert.AreEqual(UnMatch.IsDraw, true);
        }
        [TestMethod]
        public void IsHomeForfeit()
        {
            Match UnMatch = new Match(ClubExterieur, ClubHome, false, true);
            Assert.AreEqual(UnMatch.IsHomeForfeit, true);
        }

        [TestMethod]
        public void TestToString()
        {
            Club UnClub = new Club("UnSuperClubDeLaMortQuiTueDuTrololo");
            Assert.AreEqual(UnClub.ToString(), "UnSuperClubDeLaMortQuiTueDuTrololo");
        }

    }


}
