using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOver : UIBehaviour
{
    public System.Action ResetClick;

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnResetClcik() 
    {
        ResetClick?.Invoke();
    }
}
