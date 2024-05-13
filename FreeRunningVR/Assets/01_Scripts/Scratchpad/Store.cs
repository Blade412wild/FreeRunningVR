using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public Scratchpad objectData { get; private set; }
    public PlayerData playerData;
    private PlayerData data;

    private void Awake()
    {
        objectData = new Scratchpad();
        objectData.Write("playerData", playerData);
        data = objectData.Read<PlayerData>("playerData");
        Debug.Log(data);



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SayThanks()
    {
        Debug.Log("Thank you for Visting us");

    }
}
