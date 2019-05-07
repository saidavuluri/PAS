using PAS.CustomExceptions;
using PAS.Interfaces;
using PAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS
{
    public class PlayerAuctionSystemManager : IPlayerAuctionSystemManager
    {
        private IGenericRepository<Player> _playerGenericRepository;
        private IGenericRepository<Team> _teamGenericRepository;
        private IGenericRepository<Team_Player> _teamPlayerGenericRepository;
        public PlayerAuctionSystemManager(IGenericRepository<Player> playerGenericRepository, IGenericRepository<Team> teamGenericRepository, IGenericRepository<Team_Player> teamPlayerGenericRepository)
        {
            this._playerGenericRepository = playerGenericRepository;
            this._teamGenericRepository = teamGenericRepository;
            this._teamPlayerGenericRepository = teamPlayerGenericRepository;
        }

        public int AddPlayer(string teamName,Player player)
        {

            if (player.Category != "Batsman" || player.Category != "Bowler" || player.Category != "Allrounder")
            {
                throw new InvalidCategoryException("Invalid category name please check your input");
            }

            if(player.Category == "Batsman" && player.HighestScore == 0)
            {
                throw new NotABatsmanException("Invalid Batsman, please check your input");
            }

            if (player.Category == "Bowler" && string.IsNullOrEmpty(player.BestFigure))
            {
                throw new NotABowlerException("Invalid Bowler, please check your input");
            }

            if(_playerGenericRepository.GetById(player.Player_Name) != null)
            {
                throw new DuplicateEntryException("Player details alreadt exist in the database");
            }

            _playerGenericRepository.Insert(player);

            Player addedPlayer = _playerGenericRepository.GetById(player.Player_Name);
            if(addedPlayer.Player_NO != 0)
            {
                AddTeamPlayer(teamName, addedPlayer.Player_NO);
            }


            return addedPlayer.Player_NO;
        }

        private void AddTeamPlayer(string teamName, int playerNo)
        {
            var team = _teamGenericRepository.GetById(teamName);
            if (team == null)
            {
                throw new InvalidTeamNameException("Invalid team name, please check your input");
            }

            Team_Player team_Player = new Team_Player();
            team_Player.Team_Id = team.Team_Id;
            team_Player.Player_No = playerNo;
            _teamPlayerGenericRepository.Insert(team_Player);
        }

        public List<Player> DisplayPlayer(string teamName)
        {
            Team team = _teamGenericRepository.GetById(teamName);

            IEnumerable<Team_Player> team_Players = _teamPlayerGenericRepository.GetAll().Where(x => x.Team_Id == team.Team_Id);

            return _playerGenericRepository.GetAll().Where(p=> team_Players.Any(tp=>tp.Player_No == p.Player_NO)).ToList();
        }
    }
}
