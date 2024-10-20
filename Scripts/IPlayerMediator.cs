using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediatorPattern
{
    public interface IPlayerMediator
    {
        void UnregisterPlayer(Player player);
        void RequestNextTurn();
        void NotifyPlayer(Player player, string message);
        void NotifyAllPlayers(string message);
        void NotifyPlayerEnemies(Player relatedPlayer, string message, List<Player> playersToChooseFrom = null);
    }
}