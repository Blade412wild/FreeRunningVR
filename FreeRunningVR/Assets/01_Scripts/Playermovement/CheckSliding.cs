using UnityEngine;

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
    [SerializeField] private float minHeadDownPos = -5;

    private float previousHeight;

    //GameObjects
    private Rigidbody headRB;
    private float headYVelocity;
    private Transform orientationTrans;
    private CapsuleCollider bodyCollider;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = stateHandler.gameManager;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
        previousHeight = transform.position.y;
        Debug.Log(bodyCollider.height);
    }

    public bool IsSliding()
    {
        return CheckIfSliding();
    }
    private void SetGameObjects()
    {
        headRB = playerData.playerGameObjects.headRB;
        orientationTrans = playerData.playerGameObjects.orientation;
        bodyCollider = playerData.playerGameObjects.bodyCollider;
    }
    private bool CheckIfSliding()
    {
        headYVelocity = headRB.velocity.y;
        float speed = CalculateSpeed2();
        bool headIsLowEnough = CalculatePlayerHeight();
        //Debug.Log("own S : " + speed + " | headVelocity Y : " +  headYVelocity);

        if (speed <= -1 && headIsLowEnough)
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
        speed = (currenYpos - playerData.playerheight) / Time.deltaTime;
        //Debug.Log("speed : " + speed);
        previousHeight = currenYpos;
        return speed;
    }
    private float CalculateSpeed2()
    {
        float speed;
        float currenYpos = bodyCollider.height;
        speed = (currenYpos - previousHeight) / Time.deltaTime;
        Debug.Log("speed2 : " + speed);
        previousHeight = currenYpos;
        return speed;
    }

    private bool CalculatePlayerHeight()
    {
        float currentHeight = bodyCollider.height;
        float difference = currentHeight - playerData.playerheight;
        //Debug.Log("difference : " + difference * 100);
        //if (difference < )
        difference = difference * 100;
        if(difference < minHeadDownPos)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
