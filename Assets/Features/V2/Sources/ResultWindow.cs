using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace V2.Sources
{
    public class ResultWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _placeText;
        [SerializeField] private Button _exitButton;

        private const string PlaceFormat = "Your place is\n{0}";

        private void Start()
        {
            if (_exitButton != null)
            {
                _exitButton.onClick.AddListener(OnExitButtonClicked);
            }
        }
        
        public void SetPlace(int place)
        {
            if (_placeText != null)
            {
                _placeText.text = string.Format(PlaceFormat, place);
            }
        }
        
        private void OnExitButtonClicked()
        {
            SceneManager.LoadScene(0);
        }
    }

}