using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

	[SerializeField] Color32 hasPackageColor = new Color32(0, 255, 0, 255);
	[SerializeField] Color32 noPackageColor = new Color32(0, 255, 0, 255);
	[SerializeField] float destrDelay = 0.5f;

	private SpriteRenderer spriteR;
	private DeliverySystem deliverySystem;

	void Start()
	{
		spriteR = GetComponent<SpriteRenderer>();
		deliverySystem = FindObjectOfType<DeliverySystem>(); // Ensure there is only one DeliverySystem in the scene
	}
	private bool hasPackage = false;

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision with " + other.gameObject.name);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Package" && !hasPackage)
		{
			hasPackage = true;
			Debug.Log("picked up");
			spriteR.color = hasPackageColor;
			Destroy(other.gameObject, destrDelay);
			deliverySystem.PackagePickedUp();
		}
		else if (other.tag == "Person" && hasPackage)
		{
			Debug.Log("delivered");
			spriteR.color = noPackageColor;
			hasPackage = false;
			deliverySystem.PackageDelivered();
		}
	}
}
