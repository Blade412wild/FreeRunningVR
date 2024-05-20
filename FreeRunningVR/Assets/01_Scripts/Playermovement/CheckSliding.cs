using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSliding : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerStateHandler stateHandler;
    private GameManager gameManager;
    private PlayerData playerData;

    [SerializeField] private float camOriginalYPos = 0;
    [SerializeField] private float camSlidingDecrease = -0.65f;
    [SerializeField] private float maxSlideDuration = 1.0f;
    [SerializeField] private float SlideForce = 5.0f;
    [SerializeField] private float minHeadDownPos = -10;

    private float previousPosY;

    //GameObjects
    private Rigidbody headRB;
    private float headYVelocity;
    private Transform orientationTrans;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = stateHandler.gameManager;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
        previousPosY = transform.position.y;
    }

    public bool IsSliding()
    {
        return CheckIfSliding();
    }
    private void SetGameObjects()
    {
        headRB = playerData.playerGameObjects.headRB;
        orientationTrans = playerData.playerGameObjects.orientation;
    }
    private bool CheckIfSliding()
    {
        headYVelocity = headRB.velocity.y;
        float speed = CalculateSpeed();
        //Debug.Log("own S : " + speed + " | headVelocity Y : " +  headYVelocity);
        if (speed <= -0.5 && speed >= -1 && orientationTrans.localPosition.y <= minHeadDownPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private float CalculateSpeed()
    {
        float speed;
        float currenYpos = orientationTrans.position.y;
        speed = (currenYpos - previousPosY) / Time.deltaTime;
        previousPosY = currenYpos;
        return speed;
    }
}
