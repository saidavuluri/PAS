using PAS;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PAS.Interfaces;
using PAS.Models;
using PAS.CustomExceptions;
using System.Collections.Generic;

namespace PAS.Tests
{
    [TestClass()]
    public class PlayeAuctionSystemManagerUnitTests
    {
        private Mock<IGenericRepository<Player>> mockPlayerRepository;
        private Mock<IGenericRepository<Team>> mockTeamRepository;
        private Mock<IGenericRepository<Team_Player>> mockTeamPlayerRepository;
        private Mock<IPlayerAuctionSystemManager> mockPASManager;
        private Player player;

        [TestInitialize]
        public void SetUp()
        {
            mockPlayerRepository = new Mock<IGenericRepository<Player>>();
            mockTeamRepository = new Mock<IGenericRepository<Team>>();
            mockTeamPlayerRepository = new Mock<IGenericRepository<Team_Player>>();
            mockPASManager = new Mock<IPlayerAuctionSystemManager>();
            player = new Player();
            player.Category = "Batsman";
            player.BestFigure = string.Empty;
            player.HighestScore = 75;
            player.Player_Name = "MS Dhoni";
            player.Player_NO = 0;

            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            Player addedPlayer = new Player();
            addedPlayer.Player_NO = 1;
            mockPlayerRepository.Setup(m => m.GetById(It.IsAny<string>()));
            mockPlayerRepository.Setup(m => m.Insert(It.IsAny<Player>())).Returns(addedPlayer);
          
        }

        [TestMethod()]
        public void PlayerAuctionSystemManagerTest()
        {
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            var result = pasmanager.AddPlayer("CSk", player);
            Assert.IsTrue(result == 1);
        }

        [TestMethod()]
        public void AddPlayer_NotABowlerTest()
        {
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            player.Category = "Bowler";
            player.BestFigure = string.Empty;
            try
            {
                var result = pasmanager.AddPlayer("CSk", player);
                Assert.Fail("No Exception thrown");
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex is NotABowlerException);
            }
        }

        [TestMethod()]
        public void AddPlayer_NotABatsmanTest()
        {
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            player.Category = "Batsman";
            player.HighestScore = 0;
            try
            {
                var result = pasmanager.AddPlayer("CSk", player);
                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NotABatsmanException);
            }
        }


        [TestMethod()]
        public void AddPlayer_InvalidCategoryTest()
        {
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            player.Category = "xxxx";
            try
            {
                var result = pasmanager.AddPlayer("CSk", player);
                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidCategoryException);
            }
        }

        [TestMethod()]
        public void AddPlayer_InvalidTeamNameTest()
        {
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            player.Category = "Batsman";
            player.HighestScore = 75;
            try
            {
                var result = pasmanager.AddPlayer("sss", player);

                Team team = new Team();
                team.Team_Id = 1;
                team.Team_Name = "SSS";
                mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>()));
                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidTeamNameException);
            }
        }


        [TestMethod()]
        public void AddPlayer_DuplicateEntryTest()
        {
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            player.Category = "Batsman";
            player.HighestScore = 75;
            try
            {
                var result = pasmanager.AddPlayer("CSK", player);
                var result2 = pasmanager.AddPlayer("CSK", player);
                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DuplicateEntryException);
            }
        }

        [TestMethod()]
        public void DisplayPlayerTest()
        {
            List<Player> players = new List<Player>();
            player.Player_NO = 1;
            players.Add(player);
            player.Player_Name = "Raina";
            player.Player_NO = 2;
            players.Add(player);
            Team team = new Team();
            team.Team_Id = 1;
            team.Team_Name = "CSK";

            List<Team_Player> team_Players = new List<Team_Player>();
            Team_Player team_Player = new Team_Player();
            team_Player.Player_No = 1;
            team_Player.Team_Id = 1;
            team_Players.Add(team_Player);
            team_Player.Player_No = 2;
            team_Player.Team_Id = 1;
            team_Players.Add(team_Player);
            mockTeamRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(team);
            mockTeamPlayerRepository.Setup(m => m.GetAll()).Returns(team_Players);
            mockPlayerRepository.Setup(m => m.GetAll()).Returns(players);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            var result =pasmanager.DisplayPlayer("CSK");
            Assert.IsTrue(players.Count == result.Count);
        }
    }
}

