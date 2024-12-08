using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.boeing_737
{
    internal class Engine : MonoBehaviour
    {
        public float Power;
        public float CurrentRounds;
        
        public Engine(float power) 
        {
            Power = power;
            CurrentRounds = 0;
        }
    }
}
