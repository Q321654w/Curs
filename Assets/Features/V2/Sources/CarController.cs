using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace V2.Sources
{
    public class CarController : MonoBehaviour, ICarController
    {
        [Header("Light")] 
        [SerializeField] private Headlight[] _headlights;
        [SerializeField] private Headlight[] _backlights;
        
        [Header("Wheel Colliders")] 
        [SerializeField]
        private WheelCollider _frontLeftWheel;

        [SerializeField] private WheelCollider _frontRightWheel;
        [SerializeField] private WheelCollider _rearLeftWheel;
        [SerializeField] private WheelCollider _rearRightWheel;

        [Header("Wheel Transforms")] [SerializeField]
        private Transform _frontLeftTransform;

        [SerializeField] private Transform _frontRightTransform;
        [SerializeField] private Transform _rearLeftTransform;
        [SerializeField] private Transform _rearRightTransform;

        [Header("Car Settings")] 
        [SerializeField]
        private float _inputMotorTorque = 1500f;

        [SerializeField] private float _maxMotorTorque = 15000f;
        [SerializeField] private float _inputSteeringAngle = 30f;
        [SerializeField] private float _brakeTorque = 3000f;

        [Header("Audio Settings")] [SerializeField]
        private AudioSource _engineAudioSource;

        private bool _canDrive;
        private bool _isBraking;
        private float _motorInput;
        private float _steeringInput;

        private void Update()
        {
            if (!_canDrive) return;

            _motorInput = Input.GetAxis("Vertical");
            _steeringInput = Input.GetAxis("Horizontal");
            _isBraking = Input.GetKey(KeyCode.Space);

            UpdateLights();
            UpdateWheelPositions();
            UpdateEngineSound();
        }

        private void UpdateLights()
        {
            if (Input.GetKeyDown(KeyCode.F))
                ToggleHeadLigts(_headlights);
            
            if (Input.GetKeyDown(KeyCode.G))
                ToggleHeadLigts(_backlights);
        }

        private void ToggleHeadLigts(Headlight[] headlights)
        {
            foreach (var headlight in headlights)
                headlight.Toggle();
        }

        private void FixedUpdate()
        {
            if (!_canDrive) return;

            HandleMotor();
            HandleSteering();
            HandleBraking();
        }

        public void Activate()
        {
            _canDrive = true;
        }

        public void Deactivate()
        {
            _canDrive = false;
        }

        public void SetUpWayPoints(List<Checkpoint> wayPoints)
        {
        }

        private void HandleMotor()
        {
            var motor = _motorInput * _inputMotorTorque;

            _rearLeftWheel.motorTorque = motor;
            _rearRightWheel.motorTorque = motor;
        }

        private void HandleSteering()
        {
            var targetSteeringAngle = _steeringInput * _inputSteeringAngle;
            var currentSteeringAngle =
                Mathf.Lerp(_frontLeftWheel.steerAngle, targetSteeringAngle, Time.fixedDeltaTime * 2f);

            _frontLeftWheel.steerAngle = currentSteeringAngle;
            _frontRightWheel.steerAngle = currentSteeringAngle;
        }

        private void HandleBraking()
        {
            var brakeForce = _isBraking
                ? Mathf.Lerp(_rearLeftWheel.brakeTorque, _brakeTorque, Time.fixedDeltaTime * 2f)
                : 0f;

            _rearLeftWheel.brakeTorque = brakeForce;
            _rearRightWheel.brakeTorque = brakeForce;
            _frontLeftWheel.brakeTorque = brakeForce;
            _frontRightWheel.brakeTorque = brakeForce;
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

            var speed = Mathf.Abs(1 * _inputMotorTorque);
            _engineAudioSource.pitch = 1 + (speed / _inputMotorTorque);
        }
    }
}