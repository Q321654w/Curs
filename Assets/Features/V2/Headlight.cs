using UnityEngine;

namespace V2
{
    public class Headlight : MonoBehaviour
    {
        [SerializeField]
        public Light Light;

        public void Toggle()
        {
            Light.enabled = !Light.enabled;
        }
    }
}