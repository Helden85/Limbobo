using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

	public Transform player;

	public void LookAtPlayer()
	{
		if (transform.position.x > player.position.x)
		{
			transform.localScale = new Vector2(-1, 1);
		}
		else if (transform.position.x < player.position.x)
		{
            transform.localScale = new Vector2(1, 1);
		}
	}

}
