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
    private Transform orientationTrans;
    private CapsuleCollider bodyCollider;
    //private bool headIsUp = true;
    private bool isSliding = false;


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
        orientationTrans = playerData.playerGameObjects.orientation;
        bodyCollider = playerData.playerGameObjects.bodyCollider;
    }
    private bool CheckIfSliding()
    {

        float speed = CalculateSpeed2();
        float difference = CalculatePlayerHeightDifference();
        bool headIsLowEnough = CheckIfHeadIsLowEnough(difference);
        CheckIfHeadIsBackUp(difference);

        if (speed <= -1 && headIsLowEnough && playerData.HeadIsUp)
        {
            //isSliding = true;
            playerData.HeadIsUp = false;
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
        previousHeight = currenYpos;
        return speed;
    }
    private float CalculateSpeed2()
    {
        float speed;
        float currenYpos = bodyCollider.height;
        speed = (currenYpos - playerData.PreviousHeight) / Time.deltaTime;
        playerData.PreviousHeight = currenYpos;
        return speed;
    }

    private float CalculatePlayerHeightDifference()
    {
        float currentHeight = bodyCollider.height;
        float difference = currentHeight - playerData.playerheight;
        difference = difference * 100;

        return difference;
    }

    private bool CheckIfHeadIsLowEnough(float difference)
    {
        if (difference < minHeadDownPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckIfHeadIsBackUp(float difference)
    {
        if (difference > -2 && playerData.HeadIsUp == false)
        {
            playerData.HeadIsUp = true;
        }
    }
}
