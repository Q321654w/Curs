using UnityEngine;

namespace V2
{
    public class Vehicle : MonoBehaviour
    {
        public Wheel[] Wheels;
        public Motor Motor;

        public void UpdateWheels()
        {
            foreach (var wheel in Wheels)
            {
                wheel.UpdateWheelPose();
            }
        }

        public void ApplyControls(float motorInput, float steerInput)
        {
            for (int i = 0; i < Wheels.Length; i++)
            {
                var collider = Wheels[i].Collider;
                var steerAngle = collider.steerAngle;
                var motorTorque = collider.motorTorque;
                
                if (i < 2)
                {
                    collider.steerAngle = Mathf.Clamp(steerAngle + steerInput, -Motor.MaxSteerAngle, Motor.MaxSteerAngle) ;
                }

                collider.motorTorque = Mathf.Clamp(motorTorque + motorInput, -Motor.MaxTorque, Motor.MaxTorque);
            }
        }
    }
}