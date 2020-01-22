using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    [SerializeField]
    Text txtTime;
    // Start is called before the first frame update

    int i = 0;
    void Start()
    {
        //  txtTime = GetComponentInChildren<TextMeshProUGUI>();
        // UpdateTime(0);
       // StartCoroutine(Now());


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTime2(int time)
    {
        if (!txtTime) return;
       txtTime.text = (time.ToString());
    }
    [ContextMenu("Test function")]
    public void Test()
    {
        txtTime.text = txtTime.text;
    }
    public void Test2(int s)
    {
        txtTime.text = (s + "s");
    }
    IEnumerator Now()
    {
        i++;
        yield return new WaitForSeconds(1);
        Test2(i);
        StartCoroutine(Now());
    }
}
