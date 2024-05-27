using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Respawning : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private RespawnPoint respawnPoint;
    [SerializeField] private float respawnPointInterval;
    [SerializeField] private float RespawnPointDisTreshold;

    public List<RespawnPoint> respawnPointsList = new List<RespawnPoint>();
    private RespawnPoint previousPoint;
    private PlayerData playerData;
    private LevelData levelData;
    private Timer1 respawingTimer;
    private Transform player;
    private bool MayRunTimer = false;


    // Start is called before the first frame update
    void Start()
    {
        //levelManager.OnBeginLevel += StartRespwaningSystem;
        //levelManager.OnDataInputDone += ResetRespawning;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
    }

    // Update is called once per frame
    void Update()
    {
        if (respawingTimer != null && MayRunTimer)
        {
            respawingTimer.OnUpdate();
        }
    }

    private void StartRespwaningSystem()
    {
        ResetRespawning();
        InputManager.Instance.playerInputActions.Respawing.Enable();
        InputManager.Instance.playerInputActions.Respawing.TryToRespawn.performed += TryToRespawn;

        levelData = gameManager.ObjectData.Read<LevelData>("levelData");
        Debug.Log("StarRespawingSystem");
        respawingTimer = new Timer1(respawnPointInterval, true);
        respawingTimer.OnTimerIsDone += TryToSetRespawnPoint;
        MayRunTimer = true;
        player = playerData.playerGameObjects.orientation.parent.parent;
    }

    private void TryToSetRespawnPoint()
    {

        if (!playerData.grounded) return;

        RespawnPoint point;
        if (respawnPointsList.Count >= 3)
        {
            RespawnPoint destroyPoint = respawnPointsList[0];
            respawnPointsList.Remove(destroyPoint);
            Destroy(destroyPoint.gameObject);
            Debug.Log("destroyed");
        }


        if (respawnPointsList.Count == 0)
        {
            point = Instantiate(respawnPoint, player.position, Quaternion.identity);
            point.DistanceFromFinish = Vector3.Distance(point.transform.position, levelData.finishLevel.transform.position);
            previousPoint = point;


            respawnPointsList.Add(point);
            return;
        }

        float playerEndDistance = Vector3.Distance(player.position, levelData.finishLevel.transform.position);
        float pointEndDistance = previousPoint.DistanceFromFinish;
        float difference = playerEndDistance - pointEndDistance;

        if (difference < 0 && difference < -RespawnPointDisTreshold)
        {
            point = Instantiate(respawnPoint, player.position, Quaternion.identity);
            previousPoint = point;
            respawnPointsList.Add(point);
            previousPoint.DistanceFromFinish = playerEndDistance;
        }

    }
    private void TryToRespawn(InputAction.CallbackContext context)
    {
        if (respawnPointsList.Count == 0) return;

        // gets the latest respawnPoint
        player.position = respawnPointsList[respawnPointsList.Count - 1].transform.position;

        ResetRigidBody(playerData.playerGameObjects.bodyRB);
        ResetRigidBody(playerData.playerGameObjects.headRB);
        ResetRigidBody(playerData.playerGameObjects.rightHandRB);
        ResetRigidBody(playerData.playerGameObjects.leftHandRB);
    }

    private void ResetRigidBody(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void ResetRespawning()
    {
        foreach (RespawnPoint point in respawnPointsList)
        {
            respawnPointsList.Remove(point);
            Destroy(point.gameObject);
        }
        respawnPointsList.Clear();
    }
}
