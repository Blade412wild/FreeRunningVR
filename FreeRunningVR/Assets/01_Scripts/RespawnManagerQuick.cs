using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManagerQuick : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<RespawnBlock> respawnBlocks = new List<RespawnBlock>();
    [SerializeField] private List<RespawnPoint2> respawnPoints = new List<RespawnPoint2>();
    [SerializeField] private Transform player;

    private Transform currentRespawnPoint;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.OnSpawnPlayerDone += GetPlayerData;

        foreach (RespawnBlock block in respawnBlocks)
        {
            block.OnTriggerEnterEvent += RespawnPlayer;
        }

        foreach(RespawnPoint2 point in respawnPoints)
        {
            point.OnTriggerEnterEvent += SetCurrentSpawnPoint;
        }

    }

    private void GetPlayerData()
    {
        playerData = gameManager.playerData;
        player = playerData.playerGameObjects.orientation.parent.parent;
    }
    
    private void SetCurrentSpawnPoint(RespawnPoint point)
    {
        currentRespawnPoint = point.transform;
    }

    private void RespawnPlayer()
    {
        if (currentRespawnPoint == null) return;
        SetPlayerPos(currentRespawnPoint.position);
    }

    private void SetPlayerPos(Vector3 targetPos)
    {
        playerData.playerGameObjects.rightHandRB.isKinematic = true;
        playerData.playerGameObjects.leftHandRB.isKinematic = true;


        playerData.playerGameObjects.rightHandRB.position = targetPos;
        playerData.playerGameObjects.leftHandRB.position = targetPos;
        player.position = targetPos;

        playerData.playerGameObjects.rightHandRB.isKinematic = false;
        playerData.playerGameObjects.leftHandRB.isKinematic = false;


        ResetRigidBody(playerData.playerGameObjects.bodyRB);
        //ResetRigidBody(playerData.playerGameObjects.headRB);
        ResetRigidBody(playerData.playerGameObjects.rightHandRB);
        ResetRigidBody(playerData.playerGameObjects.leftHandRB);
    }

    private void ResetRigidBody(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
