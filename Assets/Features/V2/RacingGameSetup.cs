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
        public GameObject PlayerControllerPrefab;
        public GameObject AIControllerPrefab;
        public GameObject SpeedometerPrefab;
        public GameObject CountdownUIPrefab;

        public float CountdownDuration = 3f;

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

        void InitializePlayer(Vehicle vehicle, GameObject controllerPrefab)
        {
            var controllerObject = Instantiate(controllerPrefab);
            var playerController = controllerObject.GetComponent<PlayerCarController>();
            playerController.Vehicle = vehicle;

            vehicle.gameObject.tag = "Player";
        }

        void InitializeAI(Vehicle vehicle, GameObject controllerPrefab, Transform[] waypoints)
        {
            var controllerObject = Instantiate(controllerPrefab);
            var aiController = controllerObject.GetComponent<AICarController>();
            aiController.Vehicle = vehicle;
            aiController.Waypoints = waypoints;

            vehicle.gameObject.tag = "AI";
        }

        void InitializeSpeedometer(Vehicle vehicle)
        {
            var speedometerObject = Instantiate(SpeedometerPrefab);
            var speedometer = speedometerObject.GetComponent<Speedometer>();
            speedometer.Vehicle = vehicle;
        }

        IEnumerator StartCountdown(Vehicle playerVehicle, Vehicle aiVehicle)
        {
            var countdownUI = Instantiate(CountdownUIPrefab).GetComponent<CountdownUI>();
            countdownUI.StartCountdown(CountdownDuration);

            var playerController = playerVehicle.GetComponent<PlayerCarController>();
            var aiController = aiVehicle.GetComponent<AICarController>();

            if (playerController != null) playerController.enabled = false;
            if (aiController != null) aiController.enabled = false;

            yield return new WaitForSeconds(CountdownDuration);

            if (playerController != null) playerController.enabled = true;
            if (aiController != null) aiController.enabled = true;
        }
    }
}