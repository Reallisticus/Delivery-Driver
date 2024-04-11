using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	[SerializeField] GameObject thingToFollow;

	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = new Vector3(thingToFollow.transform.position.x, thingToFollow.transform.position.y, transform.position.z);
	}
}
