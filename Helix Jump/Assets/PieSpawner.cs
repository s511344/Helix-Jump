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

    public GameObject End;
    public int Seed;
    public int PieCount;
    public float Space;
    [SerializeField]
    EventObj bigFood;

    [SerializeField]
    EventObj smailFood;

    public EventObj startObject;
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
        if (startObject) 
        {
            startObject.gameObject.SetActive(true);
            startObject.AddTriggerEvent(() => { 
                StartEvent?.Invoke();
                startObject.gameObject.SetActive(false);
            });
        }
        if (bigFood != null) 
        {
            bigFood.gameObject.SetActive(true);
            bigFood.AddTriggerEvent(
                () => {
                    bigFood.gameObject.SetActive(false);
                    Game.PlayerState.CurrentState = State.Big;
                }
            );
        }
        if (smailFood != null)
        {
            smailFood.gameObject.SetActive(true);
            smailFood.AddTriggerEvent(
                () => {
                    smailFood.gameObject.SetActive(false);
                    Game.PlayerState.CurrentState = State.Small;
                }
            );
        }

        var rand = new System.Random(Seed);
        var originPosition = Origin.transform.position;
        for (var i = 2; i <= PieCount; ++i)
        {
            var pirObj = GameObject.Instantiate(Origin);
            pirObj.transform.position = new Vector3(originPosition.x, originPosition.y - i * Space, originPosition.z);
            pirObj.SetActive(true);
            pirObj.transform.SetParent(Root);
            _Instances.Add(pirObj);

            var obstacles = pirObj.GetComponentsInChildren<Obstacle>();
            var floors = obstacles.Where(o => o.Type == Obstacle.TYPE.FLOOR).OrderBy((o) => rand.NextDouble()).Take(rand.Next(1, 8));
            foreach (var floor in floors)
            {
                floor.gameObject.transform.parent.gameObject.SetActive(false);
            }

            var boards = obstacles.Where(o => o.Type == Obstacle.TYPE.BOARD && o.gameObject.transform.parent.gameObject.activeSelf).OrderBy((o) => rand.NextDouble()).Take(rand.Next(7, 8));
            foreach (var board in boards)
            {
                board.gameObject.SetActive(false);
            }


        }


        var endObj = GameObject.Instantiate(End);
        endObj.transform.position = new Vector3(originPosition.x, originPosition.y - (PieCount + 1) * Space, originPosition.z);
        endObj.SetActive(true);
        endObj.transform.SetParent(Root);
        var endComp = endObj.GetComponent<EventObj>() ?? endObj.AddComponent<EventObj>();
        endComp.AddTriggerEvent(() => { EndEvent?.Invoke(); Destroy(endComp); });
        foreach (var item in endObj.GetComponentsInChildren<MeshRenderer>())
        {
            item.material.color = Color.white;
        } 
            
        _Instances.Add(endObj);
    }

    public void ResetPosition()
    {
        Ball.position = _StartPosition;
        _Reset();
    }
}
