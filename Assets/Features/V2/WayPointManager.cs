using UnityEngine;

namespace V2
{
    public class WayPointManager : MonoBehaviour
    {
        [SerializeField] private Waypoint[] Waypoints;
        [SerializeField] private string _name;

        [SerializeField] private Vector3 _adjustment;

        [ContextMenu("Adjust")]
        public void Adjust()
        {
            foreach (var waypoint in Waypoints)
            {
                waypoint.transform.position += _adjustment;
            }
        }

        private void OnValidate()
        {
            Waypoints = new Waypoint[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.name = _name + i;

                var waypoint = child.GetComponent<Waypoint>();
                waypoint.Index = i;
                Waypoints[i] = waypoint;
            }
        }
    }
}