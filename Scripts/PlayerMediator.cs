using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediatorPattern
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        [SerializeField] private bool notifyOnNextTurn;
        [SerializeField] private List<Player> players = new();

        private Player currentPlayer;

        private void Awake()
        {
            foreach (Player player in players)
            {
                player.playerMediator = this;
            }

            if (players.Count > 0)
            {
                currentPlayer = players[0];
                currentPlayer.StartPlayerTurn();
            }
        }

        public void RequestNextTurn()
        {
            if (players.Count == 0) return;

            int nextPlayerIndex = (players.IndexOf(currentPlayer) + 1) % players.Count;
            currentPlayer = players[nextPlayerIndex];
            currentPlayer.StartPlayerTurn();

            if (notifyOnNextTurn)
            {
                NotifyAllPlayers($"{currentPlayer.name}'s turn just started!");
            }
        }

        public void UnregisterPlayer(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }

        public void NotifyPlayer(Player player, string message)
        {
            player.ReceiveNotification(message);
        }

        public void NotifyAllPlayers(string message)
        {
            NotifyPlayersInList(players, message);
        }

        private void NotifyPlayersInList(List<Player> playerList, string message)
        {
            foreach (Player player in playerList)
            {
                player.ReceiveNotification(message);
            }
        }

        public void NotifyPlayerEnemies(Player relatedPlayer, string message, List<Player> playersToChooseFrom = null)
        {
            foreach (Player player in playersToChooseFrom ?? players)
            {
                if (player.IsEnemy(relatedPlayer))
                {
                    player.ReceiveNotification(message);
                }
            }
        }
    }
}