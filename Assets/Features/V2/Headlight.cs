using UnityEngine;

namespace V2
{
    public class Headlight : MonoBehaviour
    {
        public Light Light;

        public void Toggle(bool state)
        {
            Light.enabled = state;
        }
    }
}