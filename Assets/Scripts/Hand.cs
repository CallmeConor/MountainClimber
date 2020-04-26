using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool activeHand = false;

	public float frequency;
	public float amplitude;
	float time;

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.flipX = name.Contains("Right");
	}

	void Update()
	{
		time += Time.deltaTime;
		if(activeHand)
		{
			// fix
			transform.Translate(new Vector3(Mathf.Cos(frequency * time) * amplitude * Time.deltaTime, 0f, 0f));

			if(Input.GetKeyDown(KeyCode.Space))
			{
				// LOCK IN

			}
		}
	}

	public void SetAsActiveHand(bool active)
	{
		activeHand = active;
	}
}
