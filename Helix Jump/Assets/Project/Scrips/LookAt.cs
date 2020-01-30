using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAt : MonoBehaviour
{
    public Transform Center;
    public Transform Target;
    public float UpdateRange;
    public float Distance;
    public float OffsetY;
    Vector3 _Target;
    private Vector3 _Start;

    public Vector3 currentStage = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _Target = Target.position;
        _Start = _Target;
     
    }

    public void ResetY()
    {
        _Target = _Start;
    }
    bool moving = false;
    // Update is called once per frame
    void Update()
    {
        var screenPoint = Game.Camera.WorldToScreenPoint(Target.transform.position);


        if (screenPoint.y > 100 && screenPoint.y < Screen.height)
        {

        }
        else
        {
            //if (moving) return;
            //moving = true;
            var vec = new Vector2(Target.position.x, Target.position.z) - new Vector2(Center.position.x, Center.position.z);
            var pos = vec.normalized * Distance;
//            transform.DOMoveY(Target.transform.position.y+OffsetY, 0.5f).OnComplete(()=> { moving = false; });
            transform.position = new Vector3(pos.x, Target.transform.position.y, pos.y);
            transform.forward = (Target.transform.position - transform.position).normalized;
        }
 

    }
}
