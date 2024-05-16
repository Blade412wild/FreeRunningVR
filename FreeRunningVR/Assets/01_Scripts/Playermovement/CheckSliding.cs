using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSliding : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;

    [SerializeField] private float camOriginalYPos = 0;
    [SerializeField] private float camSlidingDecrease = -0.65f;
    [SerializeField] private float maxSlideDuration = 1.0f;
    [SerializeField] private float SlideForce = 5.0f;

    //GameObjects
    private Rigidbody headRB;
    private float headYVelocity;


    // Start is called before the first frame update
    void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
    }

    public bool IsSliding()
    {
        return CheckIfSliding();
    }
    private void SetGameObjects()
    {
        headRB = playerData.playerGameObjects.headRB;
    }
    private bool CheckIfSliding()
    {
        headYVelocity = headRB.velocity.y;
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
