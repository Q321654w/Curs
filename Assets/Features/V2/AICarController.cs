using UnityEngine;

namespace V2
{
    public class AICarController : MonoBehaviour
    {
        public Vehicle Vehicle;
        public Transform[] Waypoints;
        public float WaypointThreshold = 5f;

        private int _currentWaypoint = 0;

        void FixedUpdate()
        {
            if (Waypoints.Length == 0) return;

            Vector3 target = Waypoints[_currentWaypoint].position;
            Vector3 localTarget = Vehicle.transform.InverseTransformPoint(target);

            float steering = Vehicle.Motor.MaxSteerAngle * (localTarget.x / localTarget.magnitude);
            float motorInput = Vehicle.Motor.MaxTorque;

            Vehicle.ApplyControls(motorInput, steering);
            Vehicle.UpdateWheels();

            if (localTarget.magnitude < WaypointThreshold)
            {
                _currentWaypoint = (_currentWaypoint + 1) % Waypoints.Length;
            }
        }
    }
}