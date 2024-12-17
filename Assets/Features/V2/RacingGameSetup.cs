using System.Collections;
using UnityEngine;

namespace V2
{
    public class RacingGameSetup : MonoBehaviour
    {
        public Transform[] TrackCheckpoints;
        public Transform PlayerStartPosition;
        public Transform AIStartPosition;
        public Transform[] AIWaypoints;

        public GameObject VehiclePrefab;
        public PlayerCarController PlayerControllerPrefab;
        public AICarController AIControllerPrefab;
        public GameObject SpeedometerPrefab;
        public GameObject CountdownUIPrefab;
        public Camera Camera;

        public float CountdownDuration = 3f;
        private AICarController _aiController;
        private PlayerCarController _playerController;

        void Start()
        {
            var trackManager = CreateTrack(TrackCheckpoints);
            var playerVehicle = CreateVehicle(PlayerStartPosition);
            var aiVehicle = CreateVehicle(AIStartPosition);

            InitializePlayer(playerVehicle, PlayerControllerPrefab);
            InitializeAI(aiVehicle, AIControllerPrefab, AIWaypoints);
            InitializeSpeedometer(playerVehicle);
            StartCoroutine(StartCountdown(playerVehicle, aiVehicle));
        }

        TrackManager CreateTrack(Transform[] checkpoints)
        {
            var trackManager = new GameObject("TrackManager").AddComponent<TrackManager>();
            trackManager.Checkpoints = checkpoints;

            foreach (var checkpoint in checkpoints)
            {
                var checkpointObject = checkpoint.gameObject.AddComponent<Checkpoint>();
                checkpointObject.TrackManager = trackManager;
            }

            return trackManager;
        }

        Vehicle CreateVehicle(Transform startPosition)
        {
            var vehicleObject = Instantiate(VehiclePrefab, startPosition.position, startPosition.rotation);
            return vehicleObject.GetComponent<Vehicle>();
        }

        void InitializePlayer(Vehicle vehicle, PlayerCarController controllerPrefab)
        {
            var controllerObject = Instantiate(controllerPrefab);
            _playerController = controllerObject.GetComponent<PlayerCarController>();
            _playerController.Vehicle = vehicle;
            var cam = Instantiate(Camera);
            cam.transform.SetParent(vehicle.transform);
            cam.transform.localPosition = new Vector3(0, 5, -7);
            cam.transform.localRotation = Quaternion.Euler(15, 0, 0);
            vehicle.gameObject.tag = "Player";
        }

        void InitializeAI(Vehicle vehicle, AICarController controllerPrefab, Transform[] waypoints)
        {
            var controllerObject = Instantiate(controllerPrefab);
            _aiController = controllerObject.GetComponent<AICarController>();
            _aiController.Vehicle = vehicle;
            _aiController.Waypoints = waypoints;

            vehicle.gameObject.tag = "AI";
        }

        void InitializeSpeedometer(Vehicle vehicle)
        {
            var speedometerObject = Instantiate(SpeedometerPrefab);
            var speedometer = speedometerObject.GetComponent<Speedometer>();
            //speedometer.Target = vehicle;
        }

        IEnumerator StartCountdown(Vehicle playerVehicle, Vehicle aiVehicle)
        {
            var countdownUI = Instantiate(CountdownUIPrefab).GetComponent<CountdownUI>();
            countdownUI.StartCountdown(CountdownDuration);

            _playerController.enabled = false;
            _aiController.enabled = false;

            yield return new WaitForSeconds(CountdownDuration);

            _playerController.enabled = true;
            _aiController.enabled = true;
        }
    }
}