using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.boeing_737
{
    internal class Wheel : MonoBehaviour
    {
        [SerializeField]
        public WheelCollider aboba;

        public float Rotation => aboba.steerAngle;

        public void IncreaseWheelVelocity(float x) 
        {
            aboba.rotationSpeed += x;
            Debug.Log($"sped: {aboba.rotationSpeed}");
        }
        public void DecreaseWheelVelocity(float x)
        {
            aboba.rotationSpeed -= x;
            Debug.Log($"sped: {aboba.rotationSpeed}");
        }
        public void Rotate(float x)
        {
            if (Mathf.Abs(Rotation) < 50)
            {
                if((aboba.steerAngle + x < 50) && (-50 < aboba.steerAngle + x))
                {
                    aboba.steerAngle += x;
                }
            }
        }
        public void Start()
        {

        }
    }
}
