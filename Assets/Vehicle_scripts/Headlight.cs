using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Vehicle_scripts
{
    public class Headlight : MonoBehaviour
    {
        [SerializeField]
        public Light headlight;
        public void ToggleMode() 
        {
            headlight.enabled = !headlight.enabled;
        }
    }
}
