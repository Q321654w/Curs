using System.Collections.Generic;

namespace V2.Sources
{
    public interface ICarController
    {
        public void Activate();
        public void Deactivate();
        public void SetUpWayPoints(List<Waypoint> wayPoints);
    }
}