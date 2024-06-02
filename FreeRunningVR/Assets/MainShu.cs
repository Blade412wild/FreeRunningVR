using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShu : MonoBehaviour
{
    public event Action OnShutterClosed;
    public Animator animator;

    public void ShutterClosed()
    {
        OnShutterClosed?.Invoke();
    }
   


}
