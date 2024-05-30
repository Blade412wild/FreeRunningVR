using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolManager : MonoBehaviour
{
    public event Action<Target, Vector3> OnPistolHasHitTarget;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private float height = 0.4f;
    [SerializeField] private float heightOffset = 0.3f;
    private PlayerData playerData;
    private CheckIfHandsAreInBody checkHands;
    private CheckWhichHand WhichHand = new CheckWhichHand();
    private ChangeHandsManager changeHandsManager;
    

    private bool isPlayerSpawned;
    private bool isRightHandInArea;
    private bool isLeftHandInArea;
    private bool didTryToGrab;
    private bool GunInHand = false;

    private int gunHand;

    private List<IUpdateable> activeUpdates = new List<IUpdateable>();
    private Pistol[] pistols;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.OnSpawnPlayerDone += SetPlayerData;
        InputManager.Instance.playerInputActions.Shooting.Enable();
        InputManager.Instance.playerInputActions.Shooting.CheckForGun.performed += CheckGrab;
        InputManager.Instance.playerInputActions.Shooting.CheckForGun.canceled += CheckRelease;
    }



    // Update is called once per frame
    void Update()
    {
        foreach(IUpdateable update in activeUpdates)
        {
            update.OnUpdate();
        }
    }

    private void OnDisable()
    {
        InputManager.Instance.playerInputActions.Shooting.CheckForGun.performed -= CheckGrab;
        pistols[0].OnObjectHit += PistolHasHitObject;
        pistols[1].OnObjectHit += PistolHasHitObject;
    }

    private void SetPlayerData()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        GetPistols();
        CreateHandsTracking();
        CreateHandManager();


    }
    private void CreateHandManager()
    {
        changeHandsManager = new ChangeHandsManager(playerData);
    }

    private void CreateHandsTracking()
    {
        checkHands = new CheckIfHandsAreInBody(playerData);
        activeUpdates.Add(checkHands);
        checkHands.OnLeftHandChanged += CheckLeftHandStatus;
        checkHands.OnRightHandChanged += CheckRighHandStatus;
    }

    private void GetPistols()
    {
        pistols = FindObjectsOfType<Pistol>();
        pistols[0].gameObject.SetActive(false);
        pistols[1].gameObject.SetActive(false);

        pistols[0].OnObjectHit += PistolHasHitObject;
        pistols[1].OnObjectHit += PistolHasHitObject;
    }

    private void PistolHasHitObject(Target target, Vector3 hitPoint)
    {
        OnPistolHasHitTarget?.Invoke(target, hitPoint);
    }

    private void CheckLeftHandStatus(bool status)
    {
        isLeftHandInArea = status;
    }

    private void CheckRighHandStatus(bool status)
    {
        isRightHandInArea = status;
    }

    private void CheckGrab(InputAction.CallbackContext context)
    {
        if (GunInHand) return;
        if (isRightHandInArea == false && isLeftHandInArea == false) return;
        gunHand = WhichHand.CheckGrabGun(isLeftHandInArea, isRightHandInArea);
        GunInHand = true;
        GrabGun(gunHand);
    }

    private void CheckRelease(InputAction.CallbackContext context)
    {
        ReleaseGun(gunHand);
        GunInHand = false;

    }

    private void GrabGun(int hand)
    {
        changeHandsManager.ChangeHands(hand, true);
    }

    private void ReleaseGun(int hand)
    {
        changeHandsManager.ChangeHands(hand, false);
    }
}
