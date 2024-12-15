using UnityEngine;

namespace V2
{
    public class TrackManager : MonoBehaviour
    {
        public Transform[] Checkpoints;
        private int _playerCheckpointIndex = 0;
        private int _aiCheckpointIndex = 0;

        public void PlayerPassedCheckpoint(Transform checkpoint)
        {
            if (Checkpoints[_playerCheckpointIndex] == checkpoint)
            {
                _playerCheckpointIndex++;
                if (_playerCheckpointIndex >= Checkpoints.Length)
                {
                    Debug.Log("Player Wins!");
                    _playerCheckpointIndex = 0;
                }
            }
        }

        public void AIPassedCheckpoint(Transform checkpoint)
        {
            if (Checkpoints[_aiCheckpointIndex] == checkpoint)
            {
                _aiCheckpointIndex++;
                if (_aiCheckpointIndex >= Checkpoints.Length)
                {
                    Debug.Log("AI Wins!");
                    _aiCheckpointIndex = 0;
                }
            }
        }
    }
}