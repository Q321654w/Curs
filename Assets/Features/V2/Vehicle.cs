using UnityEngine;

namespace V2
{
    public class Vehicle : MonoBehaviour
    {
        public Wheel[] Wheels;
        public Motor Motor;

        public Rigidbody Rigidbody;


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
                if (i < 2)
                {
                    Wheels[i].Collider.steerAngle = steerInput;
                }

                Wheels[i].Collider.motorTorque = motorInput;
            }
        }
    }
}