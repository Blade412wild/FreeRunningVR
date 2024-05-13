using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Store store;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = store.objectData.Read<PlayerData>("playerData");
        playerData.amountInCar = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.PlayerNeedsToPay)
        {
            Debug.Log("Pay : " + playerData.Price);
        }
    }
}
