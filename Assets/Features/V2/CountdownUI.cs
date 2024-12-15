using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace V2
{
    public class CountdownUI : MonoBehaviour
    {
        public Text CountdownText;
        public Color RedColor;
        public Color YellowColor;
        public Color GreenColor;

        public void StartCountdown(float duration)
        {
            StartCoroutine(CountdownRoutine(duration));
        }

        IEnumerator CountdownRoutine(float duration)
        {
            float interval = duration / 3f;

            CountdownText.color = RedColor;
            CountdownText.text = "3";
            yield return new WaitForSeconds(interval);

            CountdownText.color = YellowColor;
            CountdownText.text = "2";
            yield return new WaitForSeconds(interval);

            CountdownText.color = GreenColor;
            CountdownText.text = "1";
            yield return new WaitForSeconds(interval);

            CountdownText.text = "GO!";
            yield return new WaitForSeconds(1f);

            Destroy(gameObject);
        }
    }
}