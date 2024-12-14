using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Vehicle_scripts
{
    internal class Engine : MonoBehaviour
    {
        public float Power;
        
        public Engine(float power) 
        {
            Power = power;
        }
    }
}
