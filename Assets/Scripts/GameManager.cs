using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Hand[] hands;
	public Hand activeHand;

	public SpriteRenderer character;

	public Mover handCollision;

	void Start()
	{
		hands = FindObjectsOfType<Hand>();
		handCollision = FindObjectOfType<Mover>();
		AdjustHands();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			handCollision.LockIn();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			handCollision.LockIn(false);
		}
	}

	void AdjustHands()
	{
		for (int i = 0; i < hands.Length; i++)
		{
			if (activeHand == null || activeHand.transform.position.y < hands[i].transform.position.y)
			{
				if (activeHand)
				{
					activeHand.SetAsActiveHand(false);
				}
				activeHand = hands[i];
				activeHand.SetAsActiveHand(true);
			}
		}
	}

	void LockInHand(Vector3 handPosRaw)
	{

	}
}