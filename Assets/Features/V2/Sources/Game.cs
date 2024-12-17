using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using TMPro;

namespace V2.Sources
{
    [RequireComponent(typeof(Collider))]
    public class Game : MonoBehaviour
    {
        [Header("Race Settings")]
        [SerializeField] private int _targetLaps = 3;
        [SerializeField] private TMP_Text _playerLapText;
        [SerializeField] private TMP_Text _countDownText;
        [SerializeField] private ResultWindow _resultWindow;
        [SerializeField] private Speedometer _speedometer;
        [SerializeField] private GameObject[] _carPrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Transform> _waypoints;

        private bool _raceStarted;
        private GameObject _playerGameObject;
        private readonly List<GameObject> _finishedCars = new();
        private readonly Dictionary<GameObject, int> _carLapCounts = new();

        private void Start()
        {
            _resultWindow.gameObject.SetActive(false);
            _playerLapText.text = $"Lap: 0/{_targetLaps}";
            
            SpawnCars();

            _speedometer.target = _playerGameObject.GetComponent<Rigidbody>(); 

            StartCoroutine(StartRaceCountdown());
            StartCoroutine(TurnOffCountdown());
        }
        private void Update()
        {
            if (!_raceStarted) return;

            UpdatePlayerLapUI();
            CheckRaceCompletion();
        }
        private void OnTriggerEnter(Collider other)
        {
            var car = other.transform.parent.gameObject;
            if (car != null && _carLapCounts.Any(x => x.Key.GetInstanceID() == car.GetInstanceID()))
            {
                _carLapCounts[car]++;
                Debug.Log($"{car.GetInstanceID()} is on the lap {_carLapCounts[car]}");

                if (_carLapCounts[car] >= _targetLaps)
                {
                    car.GetComponent<ICarController>().Deactivate();
                    _finishedCars.Add(car);
                }
            }
        }

        private IEnumerator StartRaceCountdown()
        {
            _countDownText.text = "Race starts in 3...";
            yield return new WaitForSeconds(1);
            _countDownText.text = "2...";
            yield return new WaitForSeconds(1);
            _countDownText.text = "1...";
            yield return new WaitForSeconds(1);
            _countDownText.text = "Go!";
            
            foreach (var car in _carLapCounts.Keys)
            {
                car.GetComponent<ICarController>().Activate();
            }

            _raceStarted = true;
        }
        private IEnumerator TurnOffCountdown()
        {
            yield return new WaitForSeconds(4);
            _countDownText.gameObject.SetActive(false);
        }

        private void SpawnCars()
        {
            for (var i = 0; i < _carPrefabs.Length; i++)
            {
                var carObject = Instantiate(_carPrefabs[i], _spawnPoints[i].position, _spawnPoints[i].rotation);

                if (i == 0)
                {
                    _playerGameObject = carObject;
                }

                carObject.GetComponent<ICarController>().SetUpWayPoints(_waypoints);
                
                _carLapCounts.Add(carObject, 0);
            }
        }
        private void UpdatePlayerLapUI()
        {
            if (_carLapCounts.TryGetValue(_playerGameObject, out var playerLaps))
            {
                _playerLapText.text = $"Lap: {playerLaps}/{_targetLaps}";
            }
        }
        private void CheckRaceCompletion()
        {
            if (_carLapCounts[_playerGameObject] >= _targetLaps)
            {
                Debug.Log("Race finished!");
                Time.timeScale = 0f;
                _raceStarted = false;
                ShowResultWindow();
            }
        }
        private void ShowResultWindow()
        {
            _resultWindow.gameObject.SetActive(true);

            var playerIndex = _finishedCars.IndexOf(_playerGameObject) + 1;
            _resultWindow.SetPlace(playerIndex);
        }
    }
}