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
		player.transform.DOScale(GameConfig.BigSize, 0.2f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.2f);
		player.GetComponent<PlayerState>().Speed = 0.8f;
		Game.Camera.GetComponent<LookAt>().OffsetY = 3;
	}

	public void ToSmall(GameObject player)
	{
		player.transform.DOScale(GameConfig.SmallSize, 0.2f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.yellow, 0.2f);
		player.GetComponent<PlayerState>().Speed = 1.5f;
		Game.Camera.GetComponent<LookAt>().OffsetY = 1;
	}
	public void ToGeneral(GameObject player)
	{
		player.transform.DOScale(GameConfig.GeneralSize, 0.2f);
		player.GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.2f);
		player.GetComponent<PlayerState>().Speed = 1f;
		Game.Camera.GetComponent<LookAt>().OffsetY = 3;
	}
}
