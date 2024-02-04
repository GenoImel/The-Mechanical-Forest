using UnityEngine;

namespace Akashic.Runtime.Actors.Player
{
    internal sealed class PlayerMovementHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 1.0f; 

        [SerializeField] private Transform playerActorTransform;

        private Vector3 playerMoveDirection;

        private void FixedUpdate()
        {
            MovePlayer();
        }

        public void UpdatePlayerMoveDirection(Vector3 absoluteMoveDirection)
        {
            playerMoveDirection = absoluteMoveDirection;
        }

        public void StopMovement()
        {
            playerMoveDirection = Vector3.zero;
        }

        private void MovePlayer()
        {
            playerActorTransform.Translate(playerMoveDirection * (moveSpeed * Time.deltaTime));
        }
    }
}