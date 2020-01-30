using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public float Force;
    
    readonly static Regulus.Utility.TimeCounter _CollisionCooldown = new Regulus.Utility.TimeCounter();
    readonly static long _CooldownTicks = (long)(((double)Regulus.Utility.TimeCounter.SecondTicks) * 0.2);
    public Reflect()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_CollisionCooldown.Ticks < _CooldownTicks)
            return;

        var rig = collision.gameObject.GetComponent<Rigidbody>();
        
        rig.velocity = Vector3.zero;
        var speed = collision.gameObject.GetComponent<PlayerState>().Speed;
        rig.AddForce(transform.forward * Force * speed, ForceMode.Impulse);

        Game.Camera.GetComponent<LookAt>().currentStage = gameObject.transform.position;

        _CollisionCooldown.Reset();
    }
}
