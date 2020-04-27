using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	private float frequency = 1f;
	private float amplitude = 1f;
	private float time;

	public float rightSideOffset;
	public float leftSideOffset;

	public bool lockedIn = false;

	private GameObject handHold;

	private FingerTrigger[] fingers;

	void Start()
	{
		handHold = GameObject.FindGameObjectWithTag("HandHold");
		fingers = GetComponentsInChildren<FingerTrigger>();
	}

	void Update()
	{
		if(!lockedIn)
		{
			time += Time.deltaTime;
			transform.position = new Vector3(Mathf.Cos(frequency * time) * amplitude, transform.position.y, 0f);
			GameManager.Instance.activeHand.MatchMoverPos(transform.position);
		}
	}

	public void UpdateAvailableFingers(GameObject[] activeHandFingers)
	{
		int speedPunishment = 5;
		for(int i = 0; i < activeHandFingers.Length; i++)
		{
			fingers[i].gameObject.SetActive(activeHandFingers[i].activeSelf);
			speedPunishment--;
		}
		frequency = 1 + speedPunishment;
	}

	public void LockIn(bool val = true)
	{
		lockedIn = val;

		if (val)
		{
			Vector2 handCollPos = transform.position;
			Vector2 handHoldCollPos = handHold.transform.position;

			handCollPos.x = RoundToNearestFifth(handCollPos.x);
			transform.position = handCollPos;
			GameManager.Instance.activeHand.MatchMoverPos(transform.position);

			CheckFingersOnHandhold();
		}
	}

	void CheckFingersOnHandhold()
	{
		int fingersOnHandhold = 5;
		foreach(FingerTrigger finger in fingers)
		{
			if(finger.transform.position.x > 0.5f
				|| finger.transform.position.x < -0.5f)
			{
				finger.gameObject.SetActive(false);
				fingersOnHandhold--;
			}
		}
		GameManager.Instance.activeHand.MatchMoverFingersOnHandhold(fingers);
	}

	float RoundToNearestFifth(float handX)
	{
		if (handX < 0.1f && handX > -0.1f)
		{
			return 0f;
		}
		else if(handX > 0f)
		{
			if (handX < 0.3f)
			{
				return 0.2f;
			}
			else if (handX < 0.5f)
			{
				return 0.4f;
			}
			else if (handX < 0.7f)
			{
				return 0.6f;
			}
			else if (handX < 0.9f)
			{
				return 0.8f;
			}
			else
			{
				return 1f;
			}
		}
		else
		{
			if (handX > -0.3f)
			{
				return -0.2f;
			}
			else if (handX > -0.5f)
			{
				return -0.4f;
			}
			else if (handX > -0.7f)
			{
				return -0.6f;
			}
			else if (handX > -0.9f)
			{
				return -0.8f;
			}
			else
			{
				return -1f;
			}
		}
	}
}
