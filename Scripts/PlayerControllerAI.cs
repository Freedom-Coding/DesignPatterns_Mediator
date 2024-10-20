using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MediatorPattern
{
    public class PlayerControllerAI : Player
    {
        [SerializeField] private float attackRange = 3;

        private void Start()
        {
            AssignFriends();
        }

        private void AssignFriends()
        {
            List<Player> players = GetPlayersInRange();

            foreach (Player player in players)
            {
                friends.Add(player);
                playerMediator.NotifyPlayer(player, $"Player {player.gameObject.name} is now a friend of {gameObject.name}");
            }
        }

        public async override void StartPlayerTurn()
        {
            int delayMS = Random.Range(350, 1000);
            await Task.Delay(delayMS);

            if (!Application.isPlaying) return;

            Move();
            NotifyEnemiesInRange();
            playerMediator.RequestNextTurn();
        }

        private void Move()
        {
            Vector3 direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
            transform.position += direction;
        }

        public override void ReceiveNotification(string message)
        {
            Debug.Log($"{gameObject.name} received message: {message}");
        }

        private List<Player> GetPlayersInRange()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero);
            List<Player> players = new();

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out Player player) && player != this)
                {
                    players.Add(player);
                }
            }

            return players;
        }

        private void NotifyEnemiesInRange()
        {
            playerMediator.NotifyPlayerEnemies(this, "You are now in the range of another player.", GetPlayersInRange());
        }
    }
}