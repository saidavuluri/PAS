using PAS.Interfaces;
using PAS.Models;
using System;
using System.Collections.Generic;

public class PlayerAuctionSystemClient : IPlayerAuctionSystemClient
{
    private IPlayerAuctionSystemManager _playerAuctionSystemManager;
    public PlayerAuctionSystemClient(IPlayerAuctionSystemManager playerAuctionSystemManager)
    {
        this._playerAuctionSystemManager = playerAuctionSystemManager;
    }

    public int AddPlayer(TeamPlayerObject teamPlayerObject)
    {
        Player player = new Player();
        player.Category = teamPlayerObject.Category;
        player.HighestScore = teamPlayerObject.HighestScore;
        player.Player_Name = teamPlayerObject.Player_Name;
        player.BestFigure = teamPlayerObject.BestFigure;
        return this._playerAuctionSystemManager.AddPlayer(teamPlayerObject.TeamName,player);
    }

    public List<Player> DisplayPlayer(string teamName)
    {
        return this._playerAuctionSystemManager.DisplayPlayer(teamName);
    }
}
