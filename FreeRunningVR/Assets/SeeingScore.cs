using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingScore : MonoBehaviour
{
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject KeyBoardPrefab;
    [SerializeField] private bool MayUpdate = false;
    private GameObject keyBoard;

    private PlayerData playerData;

    private void Start()
    {
        highScoreManager.OnSeeingScore += CreatingComputer;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        //InputManager.Instance.playerInputActions.Walking.Enable();
        //InputManager.Instance.playerInputActions.Walking.SetLaptop.performed += SetKeyBoardPos;
        Keyboard.OnCloseLaptop += DestroyComputer;
    }

    private void Update()
    {
        if (!MayUpdate) return;
        //SetKeyBoardPos();
        MayUpdate = false;
    }
    private void CreatingComputer(Vector4 score)
    {
        SetKeyBoardPos();
        //ComputerUIUpdate computerUIUpdate = keyboard.GetComponent<ComputerUIUpdate>();
        
        if(keyBoard.TryGetComponent(out ComputerUIUpdate computerUIUpdate))
        {
            computerUIUpdate.UpdateHighScoreUI(score);
        }
    }

    private void SetKeyBoardPos()
    {

        float height = playerData.playerGameObjects.bodyCollider.height;
        Transform orientation = playerData.playerGameObjects.orientation;
        Vector3 dir = orientation.forward;
        //Vector3 Position = new Vector3(orientation.position.x, height - 0.5f, orientation.position.z);
        keyBoard = Instantiate(KeyBoardPrefab, orientation);
        keyBoard.transform.localPosition = new Vector3(0, -0.5f, 0.4f);
        float directionY = orientation.rotation.eulerAngles.y;
        Quaternion target = Quaternion.Euler(0, directionY, 0);
        keyBoard.transform.rotation = target;
        keyBoard.transform.parent = null;
    }

    private void DestroyComputer(Keyboard keyboard)
    {
        Destroy(keyboard.transform.parent.gameObject);
        gameManager.PlayerStateHandler.stateMachine.SwitchState(typeof(WalkingState));
    }
}
