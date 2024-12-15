using UnityEngine;
using UnityEngine.UI;

namespace V2
{
    public class Speedometer : MonoBehaviour
    {
        public Vehicle Vehicle;
        public Text SpeedText;

        void Update()
        {
            if (Vehicle != null && Vehicle.GetComponent<Rigidbody>() != null)
            {
                float speed = Vehicle.GetComponent<Rigidbody>().velocity.magnitude * 3.6f; // Convert to km/h
                SpeedText.text = Mathf.RoundToInt(speed).ToString() + " km/h";
            }
        }
    }
}