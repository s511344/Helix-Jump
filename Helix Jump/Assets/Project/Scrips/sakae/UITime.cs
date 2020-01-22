using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtTime;

    public void UpdateTime2(int time)
    {
        if (!txtTime) return;
       txtTime.text = (time.ToString());
    }
}
