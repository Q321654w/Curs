using UnityEngine;


namespace V2
{
    public class Checkpoint : MonoBehaviour
    {
        public TrackManager TrackManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TrackManager.PlayerPassedCheckpoint(transform);
            }
            else if (other.CompareTag("AI"))
            {
                TrackManager.AIPassedCheckpoint(transform);
            }
        }
    }
}