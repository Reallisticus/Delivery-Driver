using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
	[SerializeField] float steerSpeed = 200f;  // Steering responsiveness
	[SerializeField] float maxSpeed = 30f;     // Maximum speed
	[SerializeField] float acceleration = 5f;  // Acceleration rate
	[SerializeField] float deceleration = 10f; // Deceleration rate when no input
	[SerializeField] float brakingPower = 20f; // Braking power
	private float currentSpeed = 0f;           // Current speed

	void Update()
	{
		float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
		float throttleInput = Input.GetAxis("Vertical");

		// Acceleration or braking based on input
		if (throttleInput != 0)
		{
			if (throttleInput > 0)
			{
				// Accelerate forward
				currentSpeed += acceleration * throttleInput * Time.deltaTime;
			}
			else
			{
				// Apply braking or reverse acceleration
				if (currentSpeed > 0)
				{
					// Braking
					currentSpeed += throttleInput * brakingPower * Time.deltaTime;
				}
				else
				{
					// Accelerating in reverse
					currentSpeed += acceleration * throttleInput * Time.deltaTime;
				}
			}
		}
		else
		{
			// Apply natural deceleration
			if (currentSpeed > 0)
			{
				currentSpeed -= deceleration * Time.deltaTime;
			}
			else if (currentSpeed < 0)
			{
				currentSpeed += deceleration * Time.deltaTime;
			}
		}

		// Clamp the speed to avoid exceeding maximum speeds
		currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

		// Calculate the movement amount
		float moveAmount = currentSpeed * Time.deltaTime;
		transform.Rotate(0, 0, -steerAmount);
		transform.Translate(0, moveAmount, 0);
	}
}
