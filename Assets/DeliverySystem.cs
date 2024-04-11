using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
	[SerializeField] private GameObject packagePrefab;
	[SerializeField] private GameObject personPrefab;
	[SerializeField] private GameObject[] roadObjects;

	private GameObject currentPackage;
	private GameObject currentPerson;
	private int deliveries = 0;

	void Start()
	{
		Debug.Log("Starting Delivery System...");
		SpawnPackage();
	}

	private void SpawnPackage()
	{
		if (currentPackage != null)
		{
			Destroy(currentPackage);
		}
		GameObject road = roadObjects[Random.Range(0, roadObjects.Length)];
		Vector3 spawnPosition = GetRandomPositionOnRoad(road);
		currentPackage = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
		currentPackage.SetActive(true);  // Ensure the package is active
		if (currentPackage.GetComponent<SpriteRenderer>() != null)
		{
			currentPackage.GetComponent<SpriteRenderer>().enabled = true;  // Ensure the sprite is visible
		}
	}

	private void SpawnPerson()
	{
		if (currentPerson != null)
		{
			Destroy(currentPerson);
		}
		GameObject road = roadObjects[Random.Range(0, roadObjects.Length)];
		Vector3 spawnPosition = GetRandomPositionOnRoad(road);
		currentPerson = Instantiate(personPrefab, spawnPosition, Quaternion.identity);
		currentPerson.SetActive(true);  // Ensure the person is active
		if (currentPerson.GetComponent<SpriteRenderer>() != null)
		{
			currentPerson.GetComponent<SpriteRenderer>().enabled = true;  // Ensure the sprite is visible
		}
	}

	bool IsPointInPolygon(Vector2 point, PolygonCollider2D polygon)
	{
		int intersectCount = 0;
		for (int j = 0; j < polygon.pathCount; ++j)
		{
			Vector2[] vertices = polygon.GetPath(j);
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector2 a = polygon.transform.TransformPoint(vertices[i]);
				Vector2 b = polygon.transform.TransformPoint(vertices[(i + 1) % vertices.Length]);
				if (RayIntersectsSegment(point, a, b))
				{
					intersectCount++;
				}
			}
		}
		return (intersectCount % 2) == 1;  // odd count means point is inside
	}

	bool RayIntersectsSegment(Vector2 p, Vector2 a, Vector2 b)
	{
		return (a.y > p.y) != (b.y > p.y) && (p.x < (b.x - a.x) * (p.y - a.y) / (b.y - a.y) + a.x);
	}


	private Vector3 GetRandomPositionOnRoad(GameObject road)
	{
		PolygonCollider2D collider = road.GetComponent<PolygonCollider2D>();
		Vector3 spawnPosition;
		do
		{
			float x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
			float y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
			spawnPosition = new Vector3(x, y, 0);
		}
		while (!IsPointInPolygon(new Vector2(spawnPosition.x, spawnPosition.y), collider));
		return spawnPosition;
	}

	public void PackagePickedUp()
	{
		Debug.Log("Package picked up.");
		SpawnPerson();
	}

	public void PackageDelivered()
	{
		Debug.Log("Package delivered.");
		deliveries++;
		SpawnPackage();
	}
}
