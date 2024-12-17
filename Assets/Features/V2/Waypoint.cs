using System;
using UnityEngine;
using V2.Sources;


namespace V2
{
    public class Waypoint : MonoBehaviour
    {
        public int Index;
        public event Action<GameObject, int> Reached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ICarController>(out _))
                Reached?.Invoke(other.gameObject, Index);
        }
    }
}