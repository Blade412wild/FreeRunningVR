using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    [SerializeField] private Store store;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = store.objectData.Read<PlayerData>("playerData");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("bieb");
            playerData.amountInCar -= 1;
            playerData.Price += 2;
            Debug.Log("amount in car : " + playerData.amountInCar + "current price to pay : " + playerData.Price);

            if (playerData.amountInCar > 0) return;
            playerData.PlayerNeedsToPay = true; 
        }
    }
}
