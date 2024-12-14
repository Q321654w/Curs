using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Vehicle_scripts
{
    internal class Wheel : MonoBehaviour
    {
        [SerializeField]
        public WheelCollider aboba;
        [SerializeField]
        public Rigidbody vehicle;
        public float Rotation => aboba.steerAngle;
        public float speed => vehicle.velocity.magnitude * 3.6f;

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
            var mltplr = 1 * MathF.Sqrt(aboba.rotationSpeed);
            if (Mathf.Abs(Rotation) < 50)
            {
                if((aboba.steerAngle + x / Mathf.Sqrt(speed) < 50) && (-50 < aboba.steerAngle + x / Mathf.Sqrt(speed)))
                {
                    aboba.steerAngle += x / Mathf.Sqrt(speed);
                }
            }
        }
        public void Start()
        {

        }
    }
}
