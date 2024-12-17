using UnityEngine;

namespace V2
{
    public class Headlight : MonoBehaviour
    {
        public Light Light;

        public void Toggle()
        {
            Light.enabled = !Light.enabled;
        }
    }
}