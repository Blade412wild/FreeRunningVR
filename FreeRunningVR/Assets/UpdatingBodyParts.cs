using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingBodyParts : MonoBehaviour
{
    [SerializeField] private PlayerStateHandler playerStateHandler;
    private GameManager gameManager;
    private PlayerData playerData;

    //GameObjects 
    private CapsuleCollider bodyCollider;
    private Transform leftHandTransform;
    private Transform rightHandTransform;
    private Transform orientation;
    private Transform handsMiddleTrans;
    private Transform centerBodyPrefabTrans;
    private Vector3 Centerbody;

    private bool mayUpdate = false;

    private void Start()
    {
        gameManager = playerStateHandler.gameManager;
        gameManager.OnSpawnPlayerDone += StartUpdatingBodyparts;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mayUpdate) return;
        CalculatingBodyparts();
    }

    private void SetGameObjects()
    {
        leftHandTransform = playerData.playerGameObjects.leftHandTransform;
        rightHandTransform = playerData.playerGameObjects.rightHandTransform;
        handsMiddleTrans = playerData.playerGameObjects.handsMiddleTrans;
        centerBodyPrefabTrans = playerData.playerGameObjects.centerBodyPrefabTSrans;
    }

    private void CalculatingBodyparts()
    {
        Centerbody = bodyCollider.transform.TransformPoint(bodyCollider.center);

        // calulating Body Pos & Rot
        centerBodyPrefabTrans.position = Centerbody;
        centerBodyPrefabTrans.rotation = Quaternion.Euler(centerBodyPrefabTrans.rotation.eulerAngles.x, orientation.rotation.eulerAngles.y, centerBodyPrefabTrans.rotation.eulerAngles.z);

        // calculating Hands MiddlePoint
        Vector3 NewPos = (leftHandTransform.position + rightHandTransform.position) / 2;
        //NewPos = new Vector3(CenterBodyPrefabTrans.localPosition.z, NewPos.y, NewPos.z);
        handsMiddleTrans.position = NewPos;
    }

    private void StartUpdatingBodyparts()
    {
        mayUpdate = true;
    }
}
