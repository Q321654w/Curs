using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace V2.Sources
{
    public interface ICarController
    {
        public void Activate();
        public void Deactivate();
        public void SetUpWayPoints(List<Transform> wayPoints);
    }
}