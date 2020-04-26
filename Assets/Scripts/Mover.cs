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

	void Start()
	{
		handHold = GameObject.FindGameObjectWithTag("HandHold");
	}

	void Update()
	{
		if(!lockedIn)
		{
			time += Time.deltaTime;
			transform.position = new Vector3(Mathf.Cos(frequency * time) * amplitude, transform.position.y, 0f);
		}
	}

	public void LockIn(bool val = true)
	{
		lockedIn = val;

		if (val)
		{
			Vector2 handCollPos = transform.position;
			Vector2 handHoldCollPos = handHold.transform.position;

			Debug.Log(handCollPos + " " + handHoldCollPos + " " + Vector3.Distance(handCollPos, handHoldCollPos));

			handCollPos.x = RoundToNearestFinger(handCollPos.x);
			transform.position = handCollPos;
		}
	}

	float RoundToNearestFinger(float handX)
	{
		if (handX < 0.1f && handX > -0.1f)
		{
			return 0f;
		}
		else if(handX > 0f)
		{
			handX += leftSideOffset;
			if (handX < 0.3f)
			{
				rightSideOffset = 0.2f;
			}
			else if (handX < 0.5f)
			{
				rightSideOffset = 0.4f;
			}
			else if (handX < 0.7f)
			{
				rightSideOffset = 0.6f;
			}
			else if (handX < 0.9f)
			{
				rightSideOffset = 0.8f;
			}
			else
			{
				rightSideOffset = 1f;
			}
			return rightSideOffset - leftSideOffset;
		}
		else
		{
			handX -= rightSideOffset;
			if (handX > -0.3f)
			{
				leftSideOffset = 0.2f;
			}
			else if (handX > -0.5f)
			{
				leftSideOffset = 0.4f;
			}
			else if (handX > -0.7f)
			{
				leftSideOffset = 0.6f;
			}
			else if (handX > -0.9f)
			{
				leftSideOffset = 0.8f;
			}
			else
			{
				leftSideOffset = 1f;
			}
			return -leftSideOffset + rightSideOffset;
		}
	}
}
