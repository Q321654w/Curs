using System.Collections.Generic;
using UnityEngine;

namespace V2.Sources
{
    public class AiCarController : MonoBehaviour, ICarController
    {
        [Header("Wheel Colliders")]
        [SerializeField]
        private WheelCollider _frontLeftWheel;

        [SerializeField] private WheelCollider _frontRightWheel;
        [SerializeField] private WheelCollider _rearLeftWheel;
        [SerializeField] private WheelCollider _rearRightWheel;

        [Header("Wheel Transforms")]
        [SerializeField]
        private Transform _frontLeftTransform;

        [SerializeField] private Transform _frontRightTransform;
        [SerializeField] private Transform _rearLeftTransform;
        [SerializeField] private Transform _rearRightTransform;

        [Header("Car Settings")]
        [SerializeField]
        private float _maxMotorTorque = 1500f;

        [SerializeField] private float _inputSteeringAngle = 30f;
        [SerializeField] private float _waypointThreshold = 3f; 
        [SerializeField] private float _maxCarSpeed = 20f;
        [SerializeField] private float _accelerationMagnitude = 30f;

        [Header("Audio Settings")] [SerializeField]
        private AudioSource _engineAudioSource;

        private bool _canDrive;
        private int _currentWaypointIndex;
        private List<Waypoint> _waypoints;

        private void Update()
        {
            if (!_canDrive) return;

            UpdateWheelPositions();
            UpdateEngineSound();
        }

        private void FixedUpdate()
        {
            if (!_canDrive || _waypoints == null || _waypoints.Count == 0) return;

            MoveTowardsWaypoint();
            ControlCarSpeed();
        }

        public void Activate()
        {
            _canDrive = true;
        }

        public void Deactivate()
        {
            _canDrive = false;
        }

        public void SetUpWayPoints(List<Waypoint> wayPoints)
        {
            _waypoints = wayPoints;
        }

        private void MoveTowardsWaypoint()
        {
            var targetWaypoint = _waypoints[_currentWaypointIndex];
            var directionToWaypoint = (targetWaypoint.transform.position - transform.position).normalized;
            
            var steering = CalculateSteeringAngle(directionToWaypoint);
            _frontLeftWheel.steerAngle = steering;
            _frontRightWheel.steerAngle = steering;

            var distance = SquaredDistanceXZ(transform.position, targetWaypoint.transform.position);
            if (distance < _waypointThreshold)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
            }
        }

        private double SquaredDistanceXZ(Vector3 transformPosition, Vector3 position)
        {
            var x = (transformPosition.x - position.x);
            var z = (transformPosition.z - position.z);
            return x * x + z * z;
        }

        private void ControlCarSpeed()
        {
            float currentSpeed = GetCarSpeed();

            if (currentSpeed < _maxCarSpeed && currentSpeed <= _accelerationMagnitude)
            {
                _rearLeftWheel.motorTorque = _maxMotorTorque;
                _rearRightWheel.motorTorque = _maxMotorTorque;
            }
            else
            {
                _rearLeftWheel.motorTorque = 0f;
                _rearRightWheel.motorTorque = 0f;
            }
        }

        private float CalculateSteeringAngle(Vector3 directionToWaypoint)
        {
            var localTarget = transform.InverseTransformPoint(directionToWaypoint + transform.position);
            var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            return Mathf.Clamp(angle, -_inputSteeringAngle, _inputSteeringAngle);
        }

        private void UpdateWheelPositions()
        {
            UpdateSingleWheel(_frontLeftWheel, _frontLeftTransform, false);
            UpdateSingleWheel(_frontRightWheel, _frontRightTransform, true);
            UpdateSingleWheel(_rearLeftWheel, _rearLeftTransform, false);
            UpdateSingleWheel(_rearRightWheel, _rearRightTransform, true);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform, bool isRightWheel)
        {
            wheelCollider.GetWorldPose(out var position, out var rotation);

            wheelTransform.position = position;

            if (isRightWheel)
            {
                rotation *= Quaternion.Euler(0, 180, 0);
            }

            wheelTransform.rotation = rotation;
        }

        private void UpdateEngineSound()
        {
            if (!_engineAudioSource) return;

            var speed = Mathf.Abs(GetCarSpeed());
            _engineAudioSource.pitch = 1 + (speed / _maxCarSpeed);
        }

        private float GetCarSpeed()
        {
            var frontLeftWheelSpeed = GetWheelSpeed(_frontLeftWheel);
            var frontRightWheelSpeed = GetWheelSpeed(_frontRightWheel);
            var rearLeftWheelSpeed = GetWheelSpeed(_rearLeftWheel);
            var rearRightWheelSpeed = GetWheelSpeed(_rearRightWheel);

            var averageSpeed = (frontLeftWheelSpeed + frontRightWheelSpeed + rearLeftWheelSpeed + rearRightWheelSpeed) /
                               4f;

            return averageSpeed;
        }

        private float GetWheelSpeed(WheelCollider wheelCollider)
        {
            var rpm = wheelCollider.rpm;
            var wheelCircumference = 2 * Mathf.PI * wheelCollider.radius;
            var wheelSpeed = rpm * wheelCircumference / 60f;

            return wheelSpeed;
        }
    }
}