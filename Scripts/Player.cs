using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediatorPattern
{
    public abstract class Player : MonoBehaviour
    {
        public IPlayerMediator playerMediator;

        protected List<Player> friends = new();

        public abstract void StartPlayerTurn();
        public abstract void ReceiveNotification(string message);

        public bool IsFriend(Player otherPlayer)
        {
            return friends.Contains(otherPlayer);
        }

        public bool IsEnemy(Player otherPlayer)
        {
            return !IsFriend(otherPlayer);
        }

        protected virtual void OnDestroy()
        {
            playerMediator?.UnregisterPlayer(this);
        }
    }
}