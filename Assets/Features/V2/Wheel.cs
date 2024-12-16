using UnityEngine;

namespace V2
{
    [System.Serializable]
    public class Wheel : MonoBehaviour
    {
        public WheelCollider Collider;
        public Transform Transform;

        public void UpdateWheelPose()
        {
            Vector3 position;
            Quaternion rotation;

            Collider.GetWorldPose(out position, out rotation);

            Transform.position = position;
            Transform.rotation = rotation;
        }
    }
}