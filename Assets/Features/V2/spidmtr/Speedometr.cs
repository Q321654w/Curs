﻿using TMPro;
using UnityEngine;

namespace V2.spidmtr
{
    public class Speedometr : MonoBehaviour
    {
        public Rigidbody target;
        public float maxSpeed;
        public float minSpeedArrowAngle;
        public float maxSpeedArrowAngle;

        [Header("UI")]
        public TMP_Text speedLabel;
        public RectTransform arrow;

        private float _speed;
    
        private void Update()
        {
            if (target is null) return;
        
            _speed = target.velocity.magnitude * 3.6f;

            if (speedLabel != null)
                speedLabel.text = ((int)_speed) + " km/h";
            if (arrow != null)
                arrow.localEulerAngles =
                    new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, _speed / maxSpeed));
        }
    }

}