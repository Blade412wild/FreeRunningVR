using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSliding : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;
    [SerializeField] private Walking walking;

    [SerializeField] private float camOriginalYPos = 0;
    [SerializeField] private float camSlidingDecrease = -0.65f;
    [SerializeField] private float maxSlideDuration = 2.0f;
    [SerializeField] private float SlideForce = 5.0f;

    //GameObjects
    private Rigidbody headRB;
    private Rigidbody bodyRB;

    private float walkingSpeed;
    private float headYVelocity;


    // Start is called before the first frame update
    void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
        walkingSpeed = playerData.WalkSpeed;
    }

    public bool IsSliding()
    {
        return CheckIfSliding();
    }


    // Update is called once per frame
    //void Update()
    //{
    //    UpdateTimer();

    //    if (walking.grounded == true && isSliding == false)
    //    {
    //        //Debug.Log("may check");
    //        CheckIfSliding();
    //    }
    //}
    private void SetGameObjects()
    {
        headRB = playerData.playerGameObjects.headRB;
        bodyRB = playerData.playerGameObjects.bodyRB;
    }
    private bool CheckIfSliding()
    {
        headYVelocity = headRB.velocity.y;
        //Debug.Log("HeadVelocity : " + headYVelocity);
        //Debug.Log(" headVelocity :" + headYVelocity);
        if (headYVelocity <= -0.5 && headYVelocity >= -2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
