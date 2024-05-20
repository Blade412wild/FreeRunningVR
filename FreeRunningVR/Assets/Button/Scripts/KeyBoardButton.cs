using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBoardButton : MonoBehaviour
{
    private Keyboard keyboard;
    private TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponentInParent<Keyboard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if(buttonText.text.Length == 1)
        {
            NameToButtonText();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate { keyboard.InstertChar(buttonText.text); });
        }
    }

    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }
}
