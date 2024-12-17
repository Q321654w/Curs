using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using V2.spidmtr;

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
        [SerializeField] private Speedometr _speedometer;
        [SerializeField] private GameObject[] _carPrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Waypoint> _waypoints;
        
        [Header("Colors")]
        [SerializeField] private Color _redColor;
        [SerializeField] private Color _orangeColor;
        [SerializeField] private Color _yellowColor;
        [SerializeField] private Color _greenColor;

        private bool _raceStarted;
        private GameObject _playerGameObject;
        private readonly List<GameObject> _finishedCars = new();
        private readonly List<ICarController> _cars = new();
        private readonly Dictionary<int, int> _carLapCounts = new();
        private readonly Dictionary<int, int> _carCheckpointIndex = new();

        private void Start()
        {
            foreach (var waypoint in _waypoints)
            {
                waypoint.Reached += OnReached;
            }
            
            _resultWindow.gameObject.SetActive(false);
            _playerLapText.text = $"Lap: 0/{_targetLaps}";
            
            SpawnCars();

            _speedometer.target = _playerGameObject.GetComponent<Rigidbody>(); 

            StartCoroutine(StartRaceCountdown());
            StartCoroutine(TurnOffCountdown());
        }

        private void OnReached(GameObject obj, int index)
        {
            if(!_carCheckpointIndex.TryGetValue(obj.GetInstanceID(), out var currentIndex))
                return;
            
            if(index - currentIndex == 1)
                _carCheckpointIndex[obj.GetInstanceID()] = index;
        }

        private void Update()
        {
            if (!_raceStarted) return;

            UpdatePlayerLapUI();
            CheckRaceCompletion();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var car = other.transform.gameObject;
            if (car != null 
                && _carLapCounts.Any(x => x.Key == car.GetInstanceID()) 
                && _carCheckpointIndex[car.GetInstanceID()] == _waypoints.Count - 1)
            {
                _carCheckpointIndex[car.GetInstanceID()] = -1;
                _carLapCounts[car.GetInstanceID()]++;
                Debug.Log($"{car.GetInstanceID()} is on the lap {_carLapCounts[car.GetInstanceID()]}");

                if (_carLapCounts[car.GetInstanceID()] >= _targetLaps)
                {
                    car.GetComponent<ICarController>().Deactivate();
                    _finishedCars.Add(car);
                }
            }
        }

        private IEnumerator StartRaceCountdown()
        {
            _countDownText.text = "Race starts in 3...";
            _countDownText.color = _redColor;
            yield return new WaitForSeconds(1);
            _countDownText.text = "2...";
            _countDownText.color = _orangeColor;
            yield return new WaitForSeconds(1);
            _countDownText.text = "1...";
            _countDownText.color = _yellowColor;
            yield return new WaitForSeconds(1);
            _countDownText.text = "Go!";
            _countDownText.color = _greenColor;
            
            foreach (var car in _cars)
                car.Activate();
            

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
                
                _carLapCounts.Add(carObject.GetInstanceID(), 0);
                _carCheckpointIndex.Add(carObject.GetInstanceID(), -1);
                _cars.Add(carObject.GetComponent<ICarController>());
            }
        }
        
        private void UpdatePlayerLapUI()
        {
            if (_carLapCounts.TryGetValue(_playerGameObject.GetInstanceID(), out var playerLaps))
            {
                _playerLapText.text = $"Lap: {playerLaps}/{_targetLaps}";
            }
        }
        
        private void CheckRaceCompletion()
        {
            if (_carLapCounts[_playerGameObject.GetInstanceID()] >= _targetLaps)
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