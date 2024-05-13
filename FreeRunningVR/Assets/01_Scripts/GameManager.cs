using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Scratchpad ObjectData { get; private set; }
    public PlayerData playerData;

    private void Awake()
    {
        ObjectData = new Scratchpad();
        ObjectData.Write("playerData", playerData);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
