using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Vehicle_scripts
{
    internal class Player : MonoBehaviour 
    {
        [SerializeField]
        public Vehicle Vehicle;

        public void Update()
        {
            var x = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
            var y = (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0);
            var rotation = Vehicle.Wheels[0].Rotation;

            if (x != 0) 
            {
                Vehicle.Rotate(x);
            }
            else
            {
                if (rotation != 0)
                {
                    Vehicle.AlignWheels();
                }
            }
            
            if (y > 0) 
            {
                Vehicle.IncreaseVelocity();
            }
            if(y < 0)
            {
                Vehicle.DecreaseVelocity();
            }

            if (y == 0)
            {
                if(Vehicle.Wheels[0].RotationSpeed != 0)
                    Vehicle.Decelerate();
            }


            if (Input.GetKeyDown(KeyCode.F))
                Vehicle.ToggleHeadlights();

        }
    }
}
