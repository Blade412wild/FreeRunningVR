using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;
    private CheckIfHandsAreInBody checkHands;
    private CheckWhichHand WhichHand = new CheckWhichHand();
    private ChangeHandsManager changeHandsManager;

    private bool isPlayerSpawned;
    private bool isRightHandInArea;
    private bool isLeftHandInArea;
    private bool didTryToGrab;

    private List<IUpdateable> activeUpdates = new List<IUpdateable>(); 

    // Start is called before the first frame update
    void Start()
    {
        gameManager.OnSpawnPlayerDone += SetPlayerData;
        InputManager.Instance.playerInputActions.Shooting.Enable();
        InputManager.Instance.playerInputActions.Shooting.CheckForGun.performed += CheckHandInput;
        WhichHand.OnGrabGun += GrabGun;
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
        InputManager.Instance.playerInputActions.Shooting.CheckForGun.performed -= CheckHandInput;
    }

    private void SetPlayerData()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
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

    private void CheckLeftHandStatus(bool status)
    {
        isLeftHandInArea = status;
        Debug.Log("LeftHandChanged : " + isLeftHandInArea);
    }

    private void CheckRighHandStatus(bool status)
    {
        isRightHandInArea = status;
        Debug.Log("RightHandChanged : " + isRightHandInArea);
    }

    private void CheckHandInput(InputAction.CallbackContext context)
    {
        WhichHand.CheckGrabGun(true, isLeftHandInArea, isRightHandInArea);
    }


    private void GrabGun(int hand)
    {
        changeHandsManager.ChangeHands(hand);
    }
}
