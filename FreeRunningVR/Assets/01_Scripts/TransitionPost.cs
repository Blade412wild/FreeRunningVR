using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;

public class TransitionPost : MonoBehaviour
{
    public static Action<int> OnFinishedFadeInTransition;
    public static Action<int> OnFinishedFadeOutTransition;

    [Header("settings")]
    [SerializeField] private Volume volume;
    [SerializeField] private int transState = 1;  // 1 = trans in || 0 = trans out 
    [SerializeField] private bool isTransitionActive = false;
    [SerializeField] private int beginStateValue = 0;
    [SerializeField] private int endStateValue = 1;
    [SerializeField] private int targetScene;
    [SerializeField] private AudioSource audioSource;


    [Header("Material")]
    [SerializeField] private Material material;
    [SerializeField] private Color maxColor;
    [SerializeField] private Color minColor;
    [SerializeField] private float t;
    [SerializeField] private float speedj;
    [SerializeField] private bool activateMaterial;


    [Header("Gamma")]
    [SerializeField] private float j;
    [SerializeField] private float speedt;
    [SerializeField] private bool activateGamma;


    private LiftGammaGain liftGammaGain;
    private Vector4 liftMinValue = new Vector4(0, 0, 0, 0);
    private Vector4 liftMaxValue = new Vector4(0, 0, 0, 1);
    private Vector4 liftCurrentValue = new Vector4(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        SetupTransitionValues();
        //gameManager.OnActivateSceneSwitch += StartFadeInTransition;
        //ChangePlayerPos.OnChangePlayerPosFinished += StartFadeOutTransition;

    }

    private void Update()
    {
        if (isTransitionActive == false) return;

        CalculateDuration();

        if (activateGamma)
        {
            StartTransitionGamma();
        }

        if (activateMaterial)
        {
            StartTransitionMaterial();
        }

        if (t > endStateValue)
        {
            t = 1;
            isTransitionActive = false;
            OnFinishedFadeInTransition?.Invoke(targetScene);
        }

        if (t < beginStateValue)
        {
            //Debug.Log("update loop");
            t = 0;
            isTransitionActive = false;
            OnFinishedFadeOutTransition?.Invoke(targetScene);
        }
    }
    private void SetupTransitionValues()
    {
        volume.profile.TryGet(out liftGammaGain);
        minColor = material.color;
        maxColor = minColor;
        maxColor = minColor * 10;
    }
    private void StartTransitionMaterial()
    {
        Color color = Vector4.Lerp(minColor, maxColor, j);
        material.SetColor("_EmissionColor", color);
    }

    private void StartTransitionGamma()
    {
        liftCurrentValue = Vector4.Lerp(liftMinValue, liftMaxValue, t);
        liftGammaGain.lift.value = liftCurrentValue;
    }

    private void CalculateDuration()
    {
        t += (Time.deltaTime * speedt) * transState;
        j += (Time.deltaTime * speedj) * transState * 0.5f;
    }

    private void StartFadeInTransition(int _targetScene)
    {
        targetScene = _targetScene;
        transState = 1;
        isTransitionActive = true;
    }
    private void StartFadeOutTransition()
    {
        transState = -1;
        isTransitionActive = true;
    }

}
