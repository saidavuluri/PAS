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
        }

        [TestMethod()]
        public void PlayerAuctionSystemManagerTest()
        {
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            var result = pasmanager.AddPlayer("CSk", player);
            Assert.IsTrue(result == 1);
        }

        [TestMethod()]
        public void AddPlayer_NotABowlerTest()
        {
            mockPASManager.Setup(m => m.AddPlayer(It.IsAny<string>(), It.IsAny<Player>())).Returns(1);
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
            players.Add(player);
            player.Player_Name = "Raina";
            players.Add(player);

            mockPASManager.Setup(m => m.DisplayPlayer(It.IsAny<string>())).Returns(players);
            var pasmanager = new PlayerAuctionSystemManager(mockPlayerRepository.Object, mockTeamRepository.Object, mockTeamPlayerRepository.Object);
            var result =pasmanager.DisplayPlayer("CSK");
            Assert.IsTrue(players.Count == result.Count);
        }
    }
}

