using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InsertingName : MonoBehaviour
{
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject KeyBoardPrefab;
    [SerializeField] private bool MayUpdate = false;

    private PlayerData playerData;
    GameObject KeyBoard;

    private void Start()
    {
        highScoreManager.OnInsertName += BeginInsertingName;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        //InputManager.Instance.playerInputActions.Walking.Enable();
        //InputManager.Instance.playerInputActions.Walking.SetLaptop.performed += SetKeyBoardPos;
        Keyboard.OnInsertedName += DestroyComputer;
    }

    private void Update()
    {
        if (!MayUpdate) return;
        //SetKeyBoardPos();
        MayUpdate = false;
    }
    private void BeginInsertingName()
    {
        SetKeyBoardPos();
    }

    private void SetKeyBoardPos()
    {

        float height = playerData.playerGameObjects.bodyCollider.height;
        Transform orientation = playerData.playerGameObjects.orientation;
        Vector3 dir = orientation.forward;
        //Vector3 Position = new Vector3(orientation.position.x, height - 0.5f, orientation.position.z);
        GameObject KeyBoard = Instantiate(KeyBoardPrefab, orientation);
        KeyBoard.transform.localPosition = new Vector3(0, -0.5f, 0.4f);
        KeyBoard.transform.parent = null;

        Quaternion target = Quaternion.Euler(0, orientation.rotation.y, 0);
        KeyBoard.transform.rotation = target;
    }

    private void DestroyComputer(Keyboard keyboard)
    {
        Destroy(keyboard.transform.parent.gameObject);
    }
}
