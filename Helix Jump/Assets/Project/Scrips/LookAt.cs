//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class LookAt : MonoBehaviour
//{
//    public Transform Center;
//    public Transform Target;
//    public float UpdateRange;
//    public float Distance;
//    public float OffsetY;
//    Vector3 _Target;
//    private Vector3 _Start;

//    public Vector3 currentStage = Vector3.zero;
//    // Start is called before the first frame update
//    void Start()
//    {
//        _Target = Target.position;
//        _Start = _Target;
     
//    }

//    public void ResetY()
//    {
//        _Target = _Start;
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        var screenPoint = Game.Camera.WorldToScreenPoint(Target.transform.position);


//        if (screenPoint.y > Screen.height *0.5f && screenPoint.y < Screen.height)
//        {

//        }
//        else
//        {
//            //if (moving) return;
//            //moving = true;
//            var vec = new Vector2(Target.position.x, Target.position.z) - new Vector2(Center.position.x, Center.position.z);
//            var pos = vec.normalized * Distance;
////            transform.DOMoveY(Target.transform.position.y+OffsetY, 0.5f).OnComplete(()=> { moving = false; });
//            transform.position = new Vector3(pos.x, Target.transform.position.y, pos.y);
//            transform.forward = (Target.transform.position - transform.position).normalized;
//        }
 

//    }
//}
﻿using System.Collections;
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
    public float 大球仰角位移 =3;
    Vector3 _Target;
    private Vector3 _Start;
    public float ySmooth;


    bool isAni = false;
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
    public void SetValue(float dis , float offset , float big) 
    {
        this.Distance = dis;
        this.OffsetY = offset;
        this.大球仰角位移 = big;
        isAni = true;
        test();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (isAni) return;
       var pos = GetTarget();

        transform.position = new Vector3(pos.x, _Target.y + OffsetY + 大球仰角位移, pos.y);
        transform.LookAt(new Vector3(_Target.x, _Target.y - OffsetY , _Target.z));


    }
    Vector2 GetTarget(bool ygo = false) 
    {
        var target = Target.position;
        var delta = target - _Target;
        if (Mathf.Abs(delta.x) > UpdateRange)
        {
            var x = _Target.x + UnityEngine.Time.deltaTime * delta.x;
            _Target = new Vector3(x, _Target.y, _Target.z);
        }
        if (Mathf.Abs(delta.y) > UpdateRange)
        {
            var y = Mathf.Lerp(_Target.y, target.y, ygo?1: UnityEngine.Time.deltaTime * ySmooth);//  + UnityEngine.Time.deltaTime * delta.y * ySmooth;
            _Target = new Vector3(_Target.x, y, _Target.z);
        }
        if (Mathf.Abs(delta.z) > UpdateRange)
        {
            var z = _Target.z + UnityEngine.Time.deltaTime * delta.z;
            _Target = new Vector3(_Target.x, _Target.y, z);
        }
        var vec = new Vector2(Target.position.x, Target.position.z) - new Vector2(Center.position.x, Center.position.z);

        var pos = vec.normalized * Distance;
        return pos;
    }
    void test() 
    {
        Time.timeScale = 0.001f;
       var pos = GetTarget(true);
        var target = Target.position;
        transform.position = new Vector3(pos.x, _Target.y + OffsetY + 大球仰角位移, pos.y);
        transform.LookAt(new Vector3(_Target.x, _Target.y - OffsetY, _Target.z));
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLookAt(new Vector3(_Target.x, _Target.y - OffsetY, _Target.z), 0.5f));
        //.Append(transform.DOLocalMove(new Vector3(pos.x, _Target.y + OffsetY + 大球仰角位移, pos.y), 0.1f))

        sequence.SetUpdate(true);
        sequence.OnComplete(() => {
            isAni = false;
            Time.timeScale = 1.0f;
        });
        sequence.Play();

    }
}
