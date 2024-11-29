using System.Linq;
using DefaultNamespace.Features;
using Features.Cars;
using UnityEngine;

namespace Features
{
    public class Player : Driver
    {
        private readonly Wheel _wheel;

        public Player(Car car) : base(car)
        {
            _wheel = Car.Wheels.First(s => s.IsRotate);
        }

        protected override float GetXDirection()
        {
            var horizontalDirection = (Input.GetKey(KeyCode.D) ? 1f : 0f) + (Input.GetKey(KeyCode.A) ? -1f : 0f);

            if (horizontalDirection == 0)
                return CorectionAngle();
            
            return horizontalDirection;
        }

        private float CorectionAngle()
        {
            Debug.Log("Correcting");
            var transform = Car.transform;

            var steerAngleInRadians = _wheel.Angle * Mathf.Deg2Rad;

            var localDirection = new Vector3(Mathf.Sin(steerAngleInRadians), 0, Mathf.Cos(steerAngleInRadians));
            var globalDirection = transform.TransformDirection(localDirection);

            return Vector3.SignedAngle(globalDirection, transform.forward, transform.up);
        }

        protected override float GetZDirection()
        {
            return (Input.GetKey(KeyCode.W) ? 1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f);
        }
    }
}