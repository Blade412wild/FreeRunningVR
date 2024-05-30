using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    public event Action<Target> OnAnimtionIsDone;
    public Vector3 Center { get; private set; }
    public float Accuracy;
    public float Radius;
    public Animator Animator { get; private set; }
    public AudioSource AudioSource { get; private set; }
    
    

    // Start is called before the first frame update
    void Start()
    {
        Center = transform.GetChild(0).position;
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    public void AnimtionIsDone()
    {
        OnAnimtionIsDone?.Invoke(this);
    }

}
