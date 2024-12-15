using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Vehicle_scripts
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField]
        public WheelCollider aboba;
        [SerializeField]
        public Rigidbody vehicle;
        public float Rotation => aboba.steerAngle;
        public float RotationSpeed => aboba.rotationSpeed;

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
            aboba.steerAngle += x;
        }
    }
}
