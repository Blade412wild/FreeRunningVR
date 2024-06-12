using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class Keyboard : MonoBehaviour
{
    public static event Action<Keyboard> OnInsertedName;
    public static event Action<Keyboard> OnCloseLaptop;


    public TMP_InputField inputField;
    public GameObject normalButtons;
    public GameObject capsButtons;
    [SerializeField] private GameObject ScoreUI;
    [SerializeField] private float maxNameLenght;
    public string TypedName { get; private set; }
    private bool caps = false;

    private int counter = 0;

    public void InstertChar(string c)
    {

        if (inputField.text.Length >= maxNameLenght && inputField != null) return;
        inputField.text += c;
    }

    public void CloseScoreUI()
    {
        if (counter == 0)
        {
            ScoreUI.SetActive(false);
            counter++;
        }
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        inputField.text += " ";
    }

    public void CapsPressed()
    {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButtons.SetActive(true);
            caps = true;
        }
        else
        {
            capsButtons.SetActive(false);
            normalButtons.SetActive(true);
            caps = false;
        }
    }

    public void InsertName()
    {
        if (counter == 0) return;
        TypedName = inputField.text;
        OnInsertedName?.Invoke(this);
    }

    public void CloseLaptop()
    {
        OnCloseLaptop?.Invoke(this);
    }



}
