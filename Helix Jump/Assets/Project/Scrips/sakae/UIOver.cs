using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIOver : UIBehaviour
{
    public System.Action ResetClick;


    public Text text;
    public TextMeshProUGUI txtMessage;
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnResetClcik() 
    {
        ResetClick?.Invoke();
    }
    public void SetText(string s) 
    {
        txtMessage.text = s;
    }
}
