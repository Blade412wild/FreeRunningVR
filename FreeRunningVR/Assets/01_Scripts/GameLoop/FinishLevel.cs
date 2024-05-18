using UnityEngine;
using System;

public class FinishLevel : MonoBehaviour
{
    public event Action OnFinishLevel;
    private bool firstTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (firstTrigger)
        {
            OnFinishLevel?.Invoke();
        }

        firstTrigger = false;
    }
}
