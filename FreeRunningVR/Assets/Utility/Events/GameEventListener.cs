using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent unityEvent;

    private void Awake() => gameEvent.Register(this);
    private void OnDestroy() => gameEvent.DeRegister(this);
    
    public void RaiseEvent() => unityEvent.Invoke();

} 
