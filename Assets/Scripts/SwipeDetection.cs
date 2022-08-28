using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Percentage of screen with/hight")]
	private float distancePercentage = 0.1f;
	private float minimumDistance = 200f;
	[SerializeField]
	private float maximumTime = 0.2f;
	[SerializeField, Range(0f, 1f)]
	private float directionTreshold = 0.9f;

	private InputManager inputManager;

	private Vector2 startPosition;
	private float startTime;
	private Vector2 endPosition;
	private float endTime;

	private void Awake()
	{

		inputManager = InputManager.Instance;
	}

	private void OnEnable()
	{
		inputManager.OnStartTouch += SwipeStart;
		inputManager.OnEndTouch += SwipeEnd;
	}

	private void OnDisable()
	{
		inputManager.OnStartTouch -= SwipeStart;
		inputManager.OnEndTouch -= SwipeEnd;
	}

	private void SwipeStart(Vector2 position, float time)
	{
		this.startPosition = position;
		this.startTime = time;
	}

	private void SwipeEnd(Vector2 position, float time)
	{
		this.endPosition = position;
		this.endTime = time;
		DetectSwipe();
	}

	private void DetectSwipe()
	{
		if (
			Vector3.Distance(startPosition, endPosition) >= this.minimumDistance &&
			(this.endTime - this.startTime) <= this.maximumTime)
		{
			Debug.DrawLine(startPosition, endPosition, Color.green, 0.5f);
			Vector3 direction = endPosition - startPosition;
			Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
			SwipeDirection(direction2d);
		}
	}

	private void SwipeDirection(Vector2 direction) {
		if (Vector2.Dot(Vector2.up, direction) > this.directionTreshold) {
			Debug.Log("---------------------UP-------------------------");
		}
		if (Vector2.Dot(Vector2.down, direction) > this.directionTreshold)
		{
			Debug.Log("---------------------DOWN-------------------------");
		}
		if (Vector2.Dot(Vector2.left, direction) > this.directionTreshold)
		{
			Debug.Log("---------------------LEFT-------------------------");
		}
		if (Vector2.Dot(Vector2.right, direction) > this.directionTreshold)
		{
			Debug.Log("---------------------RIGTH-------------------------");
		}
	}

	/*void OnGUI()
	{
		GUILayout.BeginArea(new Rect(200, 200, 500, 240));
		GUI.skin.label.fontSize = 30;
		GUILayout.Label(String.Format("start: {0}   end: {1}", startPosition, endPosition), GUILayout.Width(500), GUILayout.Height(150));
		GUILayout.Label(String.Format("Distance: {0}", Vector3.Distance(startPosition, endPosition)), GUILayout.Width(500), GUILayout.Height(150));
		GUILayout.EndArea();
	}*/

}
