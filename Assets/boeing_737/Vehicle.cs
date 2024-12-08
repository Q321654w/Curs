using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.boeing_737
{
    internal class Vehicle : MonoBehaviour
    {
        [SerializeField]
        public Headlight[] FrontLights;
        public Headlight[] BackLights;
        public Wheel[] Wheels;
        public Engine CurrentEngine;

        public void ToggleHeadlights()
        {
            foreach (var headlight in FrontLights)
            {
                headlight.ToggleMode();
            }
        }
        public void IncreaseVelocity()
        {
            foreach (var wheel in Wheels)
            {
                wheel.IncreaseWheelVelocity(CurrentEngine.Power);
            }
        }
        public void DecreaseVelocity()
        {
            foreach (var wheel in Wheels)
            {
                wheel.DecreaseWheelVelocity(CurrentEngine.Power);
            }
        }
        public void Rotate(float x)
        {
            for (int i = 0; i < 2; i++)
            {
                Wheels[i].Rotate(x);
            }
        }
    }
}
