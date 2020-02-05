﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieSpawner : MonoBehaviour
{
    // 1,1,1,1,1,1,1,1,1,0 
    // 
    public Transform Ball;
    private Vector3 _StartPosition;
    readonly System.Collections.Generic.List<GameObject> _Instances;
    public Transform Root;
    public GameObject Origin;

    public Action EndEvent;
    public Action StartEvent;
    internal void Run(int seed, int count, int space)
    {
        Seed = seed;
        PieCount = count;
        Space = space;
        ResetPosition();
    }
    public Transform StartTransform;
    public GameObject End;
    public int Seed;
    public int PieCount;
    public float Space;


    [SerializeField]
    GameObject 中間夾層;

    [SerializeField]
    EventObj bigFood;

    [SerializeField]
    EventObj smailFood;

    [SerializeField]
    Material 可撞壞的材質;
    public EventObj startObject;

    [SerializeField]
    List<EventObj> eventObj;
    [SerializeField]
    Transform foodRoot;

    [SerializeField]
    public PieSpawner()
    {
        _Instances = new List<GameObject>();
    }
    void Start()
    {
        _StartPosition = Ball.position;

        _Reset();
    }

    private void _Reset()
    {
        foreach (var instance in _Instances)
        {
            GameObject.Destroy(instance);
        }
        void setEvent(EventObj obj,int idx) 
        {
            obj.gameObject.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            _Instances.Add(obj.gameObject);
            obj.AddTriggerEvent(
                () =>
                {
                    obj.gameObject.SetActive(false);
                    Game.PlayerState.CurrentState = (State)idx;
                }
            );
        }

        if (foodRoot) 
        {
            var count = foodRoot.childCount;

            for (int i = 0; i < count; i++)
            {
                var unityRandom = UnityEngine.Random.Range(0, 2);
                var t = foodRoot.GetChild(i);
                setEvent(GameObject.Instantiate(eventObj[unityRandom], t), unityRandom);
               
            }
        }
        

        if (startObject)
        {
            startObject.gameObject.SetActive(true);
            startObject.AddTriggerEvent(() =>
            {
                StartEvent?.Invoke();
                startObject.gameObject.SetActive(false);
            });
        }
        //if (bigFood != null) 
        //{
        //    bigFood.gameObject.SetActive(true);
        //    bigFood.AddTriggerEvent(
        //        () => {
        //            bigFood.gameObject.SetActive(false);
        //            Game.PlayerState.CurrentState = State.Big;
        //        }
        //    );
        //}
        //if (smailFood != null)
        //{
        //    smailFood.gameObject.SetActive(true);
        //    smailFood.AddTriggerEvent(
        //        () => {
        //            smailFood.gameObject.SetActive(false);
        //            Game.PlayerState.CurrentState = State.Small;
        //        }
        //    );
        //}

        //
        //var originPosition = Origin.transform.position;
        //for (var i = 2; i <= PieCount; ++i)
        //{
        //    var pirObj = GameObject.Instantiate(Origin);
        //    pirObj.transform.position = new Vector3(originPosition.x, originPosition.y - i * Space, originPosition.z);
        //    pirObj.SetActive(true);
        //    pirObj.transform.SetParent(Root);
        //    _Instances.Add(pirObj);

        //    var obstacles = pirObj.GetComponentsInChildren<Obstacle>();
        //    var floors = obstacles.Where(o => o.Type == Obstacle.TYPE.FLOOR).OrderBy((o) => rand.NextDouble()).Take(rand.Next(1, 8));
        //    foreach (var floor in floors)
        //    {
        //        floor.gameObject.transform.parent.gameObject.SetActive(false);
        //    }

        //    var boards = obstacles.Where(o => o.Type == Obstacle.TYPE.BOARD && o.gameObject.transform.parent.gameObject.activeSelf).OrderBy((o) => rand.NextDouble()).Take(rand.Next(7, 8));
        //    foreach (var board in boards)
        //    {
        //        board.gameObject.SetActive(false);
        //    }


        //}


        var endObj = GameObject.Instantiate(End);
        endObj.transform.position = new Vector3(0, StartTransform.position.y - (PieCount + 1) * Space, 0);
        endObj.SetActive(true);
        endObj.transform.SetParent(Root);
        var endComp = endObj.GetComponent<EventObj>() ?? endObj.AddComponent<EventObj>();
        endComp.AddTriggerEvent(() => { EndEvent?.Invoke(); Destroy(endComp); });
        foreach (var item in endObj.GetComponentsInChildren<MeshRenderer>())
        {
            item.material.color = Color.white;
        }
        var 夾層 = GameObject.Instantiate(中間夾層);
        夾層.SetActive(true);
        var childs = 夾層.GetComponentsInChildren<Obstacle>();


        Game.Schedule.Delay(() => {
            var ya = new Color(67f, 72f, 144f);
            foreach (var o in childs)
            {
                if (o.Type == Obstacle.TYPE.可撞破)
                {
                    o.gameObject.GetComponent<MeshRenderer>().material = 可撞壞的材質;
                    o.transform.parent.GetComponent<MeshRenderer>().material = 可撞壞的材質;
                    var s = Instantiate(eventObj[2], o.transform,false);
                    s.gameObject.SetActive(true);
                    s.transform.localPosition = new Vector3(0.158f, 0.517f, 0.87f);
                    s.transform.localEulerAngles = Vector3.zero;
                    s.AddTriggerEvent(() => {
                        if (Game.PlayerState.CurrentState == State.Big)
                        {
                            o.transform.parent.gameObject.SetActive(false);
                        }

                     });
                }
            }
        },0.1f);


        夾層.transform.SetParent(Root);
        _Instances.Add(夾層);
        _Instances.Add(endObj);
    }

    public void ResetPosition()
    {
        Ball.position = _StartPosition;
        Root.forward = Vector3.zero;
        Root.rotation = Quaternion.identity;
        _Reset();
    }
}
