using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace V2.Sources
{
    public class MenuWindow : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
    }
}