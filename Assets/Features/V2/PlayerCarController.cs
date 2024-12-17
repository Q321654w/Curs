using UnityEngine;

namespace V2
{
    public class PlayerCarController : MonoBehaviour
    {
        public Vehicle Vehicle;

        void Update()
        {
            float motorInput = Vehicle.Motor.MaxTorque * Input.GetAxis("Vertical");
            float steerInput = Vehicle.Motor.MaxSteerAngle * Input.GetAxis("Horizontal");

            Vehicle.ApplyControls(motorInput, steerInput);
            Vehicle.UpdateWheels();
            if (Input.GetKeyDown(KeyCode.F))
                Vehicle.ToggleLights();
        }
    }
}