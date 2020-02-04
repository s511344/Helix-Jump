using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerEvent 
{
	public PlayerEvent()
	{
	}

	public void ToBig(GameObject player)
	{
		player.transform.DOScale(GameConfig.BigSize, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<PlayerState>().Speed = 0.5f;
		player.GetComponent<Rigidbody>().mass = 1.2f;
		var comp = Game.Camera.GetComponent<LookAt>();
		comp.SetValue(8f, 2f, -8.5f);
		//comp.Distance = 8;
		//comp.OffsetY = 2;
		//comp.大球仰角位移 = -8.5f;

	}

	public void ToSmall(GameObject player)
	{
		player.transform.DOScale(GameConfig.SmallSize, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.yellow, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<PlayerState>().Speed = 1.0f;
		player.GetComponent<Rigidbody>().mass = 0.8f;
		Game.Camera.GetComponent<LookAt>().OffsetY = 0;
		var comp = Game.Camera.GetComponent<LookAt>();
		comp.SetValue(12f, 0f, 0f);
		//comp.Distance = 12;
		//comp.OffsetY = 0;
		//comp.大球仰角位移 = 0;

	}
	public void ToGeneral(GameObject player)
	{
		player.transform.DOScale(GameConfig.GeneralSize, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.2f).SetUpdate(true).SetDelay(0.1f);
		player.GetComponent<PlayerState>().Speed = 0.75f;
		player.GetComponent<Rigidbody>().mass = 1.0f;
		var comp = Game.Camera.GetComponent<LookAt>();
		comp.SetValue(12f, 3f, 0f);
		//comp.Distance = 12;
		//comp.OffsetY = 3;
		//comp.大球仰角位移 = 0;
	}
}
