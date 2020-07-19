using Assets.Scripts.Player.Enums;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateController : MonoBehaviour
    {
		public PlayerState currentState = PlayerState.Idle;
		public void SetState(PlayerState state)
        {
			currentState = state;
        }
    }
}
