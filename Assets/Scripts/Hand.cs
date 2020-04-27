using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool activeHand = false;

	public float frequency;
	public float amplitude;

	SpriteRenderer spriteRenderer;

	private float xOffset;

	GameObject[] fingersRenderers;
	FingerTrigger[] fingers;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.flipX = name.Contains("Right");
		xOffset = transform.position.x;
		fingersRenderers = new GameObject[transform.childCount];
		if (spriteRenderer.flipX)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				fingersRenderers[i] = transform.GetChild((transform.childCount - 1) - i).gameObject;
			}
		}
		else
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				fingersRenderers[i] = transform.GetChild(i).gameObject;
			}
		}
	}

	public void BecameActiveHand()
	{
		MatchMoverToThisHand();
	}

	void MatchMoverToThisHand()
	{
		GameManager.Instance.handCollision.UpdateAvailableFingers(fingersRenderers);
	}

	public void MatchMoverFingersOnHandhold(FingerTrigger[] inFingers)
	{
		for(int f = 0; f < fingersRenderers.Length; f++)
		{
			fingersRenderers[f].SetActive(inFingers[f].gameObject.activeSelf);
		}
	}

	public void MatchMoverPos(Vector2 moverPos)
	{
		transform.position = new Vector2(moverPos.x + xOffset, transform.position.y);
	}

	public void SetAsActiveHand(bool active)
	{
		activeHand = active;

		if(activeHand)
		{
			BecameActiveHand();
		}
	}
}
