using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Hand[] hands;
	public Hand activeHand;

	public SpriteRenderer character;

	public Mover handCollision;

	float handsMinY, handsMaxY;

	int starthand = 0;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		hands = FindObjectsOfType<Hand>();
		handCollision = FindObjectOfType<Mover>();
		AdjustActiveHand();
		RecordHandsMinMaxY();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			handCollision.LockIn();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(SwapHands());
		}
	}

	IEnumerator SwapHands()
	{
		Vector2 hand1StartPos = hands[0 + starthand].transform.position;
		Vector2 hand1EndPos = hands[1 - starthand].transform.position;
		hand1EndPos.x = hand1StartPos.x;
		Vector2 hand2StartPos = hands[1 - starthand].transform.position;
		Vector2 hand2EndPos = hands[0 + starthand].transform.position;
		hand2EndPos.x = hand2StartPos.x;
		float speed = 4f;
		float startTime = Time.time;
		float journeyLength = Vector3.Distance(hand1StartPos, hand1EndPos);
		float fractionOfJourney = 0f;

		while (fractionOfJourney < 1f)
		{
			// Distance moved equals elapsed time times speed..
			float distCovered = (Time.time - startTime) * speed;

			// Fraction of journey completed equals current distance divided by total distance.
			fractionOfJourney = distCovered / journeyLength;

			// Set our position as a fraction of the distance between the markers.
			hands[0 + starthand].transform.position = Vector3.Lerp(hand1StartPos, hand1EndPos, fractionOfJourney);

			yield return null;
		}

		startTime = Time.time;
		fractionOfJourney = 0f;
		journeyLength = Vector3.Distance(hand2StartPos, hand2EndPos);
		speed *= 2f;

		while (fractionOfJourney < 1f)
		{
			// Distance moved equals elapsed time times speed..
			float distCovered = (Time.time - startTime) * speed;

			// Fraction of journey completed equals current distance divided by total distance.
			fractionOfJourney = distCovered / journeyLength;

			// Set our position as a fraction of the distance between the markers.
			hands[1 - starthand].transform.position = Vector3.Lerp(hand2StartPos, hand2EndPos, fractionOfJourney);

			yield return null;
		}

		yield return null;

		AdjustActiveHand();
		handCollision.LockIn(false);
		starthand++;
		if(starthand > 1)
		{
			starthand = 0;
		}
	}

	void AdjustActiveHand()
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

	private void RecordHandsMinMaxY()
	{
		for(int i = 0; i < hands.Length; i++)
		{
			float handY = hands[i].transform.position.y;
			if (handY > handsMaxY)
			{
				handsMaxY = handY;
			}
			else if(handY < handsMinY)
			{
				handsMinY = handY;
			}
		}
	}

}