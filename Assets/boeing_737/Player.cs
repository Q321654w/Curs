using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.boeing_737
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
                    Vehicle.Rotate(-Mathf.Sign(rotation));
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

            if (Input.GetKeyDown(KeyCode.F))
                Vehicle.ToggleHeadlights();

        }
    }
}
