using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using static GameManager;

public class Arrow : MonoBehaviour
{
	private GameManager gameManagerInstance;
	private InputManager inputManagerInstance;
	private UIManager uiManagerInstane;

	public float m_Speed = 0;
	private float m_Time_to_move_to_place = 5f;
	private readonly List<GameObject> Arrows = new List<GameObject>();
	public ArrowState currentArrowState = ArrowState.HALT;

	private int number_of_arrows = 0;


	private Vector3 lastposition;

	public int NumberOfArrows   // property
	{
		get { return number_of_arrows; }   // get method
		set
		{
			UpdateArrowVisual(value);
			number_of_arrows = value;
		}
	}
	public GameObject arrow_prefab;

	public int arrow_active_road;
	public int number_of_active_roads;

	public Vector3 newleft, newright;


	private void OnEnable()
	{
		inputManagerInstance.OnKeyboardPress += KeyboardButtonPress;
		gameManagerInstance.OngameStateChanged += GameModeSwitch;
	}


	private void OnDisable()
	{
		inputManagerInstance.OnKeyboardPress -= KeyboardButtonPress;
		gameManagerInstance.OngameStateChanged -= GameModeSwitch;
	}

	private void Awake()
	{
		gameManagerInstance = GameManager.Instance;
		inputManagerInstance = InputManager.Instance;
		uiManagerInstane = UIManager.Instance;
	}

	void Start()
	{
		this.NumberOfArrows = 1;
		lastposition = this.Arrows[0].transform.position;
		// Pick a starting road at random
		number_of_active_roads = gameManagerInstance.RoadPosition.Count - 1;
		int roadIndex = Random.Range(0, gameManagerInstance.RoadPosition.Count);
		arrow_active_road = roadIndex;
		Vector3 randomStartPosition = gameManagerInstance.RoadPosition[roadIndex];
		transform.position = randomStartPosition;
	}

	public bool UpdateArrowVisual(int newAmountOfArrows)
	{

		// If less then one game over
		bool isLessThanOneArrow = newAmountOfArrows < 1;

		if (isLessThanOneArrow)
		{
			return false;
		}

		int arrowDiffrence = Mathf.Abs(number_of_arrows - newAmountOfArrows);

		//Have to subtrack
		if (number_of_arrows > newAmountOfArrows)
		{
			for (int i = 0; i < arrowDiffrence; i++)
			{
				int randomIndex = Random.Range(0, Arrows.Count);
				Destroy(Arrows[randomIndex]);
				Arrows.RemoveAt(randomIndex);
			}
		}
		else
		{
			//Add arrows
			for (int i = 0; i < arrowDiffrence; i++)
			{
				GameObject gameObjectArrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity, transform);
				Arrows.Add(gameObjectArrow);
				MoveArrowPosition(gameObjectArrow);
			}
		}
		uiManagerInstane.updateArrowUI(newAmountOfArrows);

		return true;
	}

	void Update()
	{
		if (currentArrowState == ArrowState.MOVING) {
			transform.Translate(0f, 0f, this.m_Speed * Time.deltaTime);
		}

	}

	private void FixedUpdate()
	{
		if (Keyboard.current[Key.Q].wasReleasedThisFrame)
		{
			int randomIndex = Random.Range(0, Arrows.Count);
			//Arrows[randomIndex].transform.Translate(new Vector3(5,5,5));
			//Arrows[randomIndex].transform.DOShakePosition(3);
			Arrows[randomIndex].transform.DOScaleX(3, 3);
			Arrows[randomIndex].transform.DOScaleY(3, 3);
			//Arrows[randomIndex].transform.DOLocalRotate(new Vector3(180f, 0, 0), 1f, RotateMode.LocalAxisAdd);
		}
	}


	private async void MoveArrowPosition(GameObject arrowToMove)
	{
		Vector3 arrowToMovePosition = arrowToMove.transform.position;
		float endTime = Time.time + this.m_Time_to_move_to_place;
		Vector3 endVector = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(arrowToMovePosition.y, arrowToMovePosition.y * 2), 0f);
		float tValue = 0;
		while (arrowToMove != null && arrowToMovePosition != endVector)
		{
			tValue += Time.deltaTime;

			arrowToMove.transform.localPosition = Vector3.Lerp(arrowToMovePosition, endVector, tValue);

			await Task.Yield();
		}
	}

	private async Task AttackArrowToTarget(float durration, GameObject arrow, Vector3 target) {
		float endTime = Time.time + durration;
		Vector3 startPosition = arrow.transform.position;
		float timeBetween = endTime - Time.time;
		Quaternion lookRotation;

		Vector3 targetDirection = target - transform.position;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, timeBetween, 0.0f);

		while (Time.time < endTime) {
			arrow.transform.position = Vector3.Lerp(target, startPosition, timeBetween);
			lastposition = arrow.transform.position;
			lookRotation = Quaternion.LookRotation(targetDirection.normalized);
			arrow.transform.rotation = Quaternion.Slerp(lookRotation, arrow.transform.rotation, timeBetween);
			Debug.Log(arrow.transform.rotation.eulerAngles);
			timeBetween = endTime - Time.time;
			await Task.Yield();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "EndTrigger") {
			other.gameObject.GetComponentInParent<EndTrigger>().WasHit();
			return;
		}
		MathProblem triggeredProblem = other.gameObject.GetComponentInParent<Obsticle>().WasHit(other.name);
		int newArrowAmount = newArrowCount(triggeredProblem);
		this.NumberOfArrows = newArrowAmount;
	}

	public int newArrowCount(MathProblem mathProblem)
	{
		switch (mathProblem.op)
		{
			case Operatorns.Operator.Add:
				return number_of_arrows + mathProblem.number;
			case Operatorns.Operator.Divide:
				return Mathf.RoundToInt(number_of_arrows / mathProblem.number);
			case Operatorns.Operator.Subtrack:
				return number_of_arrows - mathProblem.number;
			case Operatorns.Operator.Multiply:
				return number_of_arrows * mathProblem.number;
			default:
				return number_of_arrows;
		}
	}

	public void HitEndGoal(Vector3 target) {
		this.currentArrowState = ArrowState.HALT;
		List<Task> tasks = new List<Task>();
		transform.DetachChildren();
		for (int i = 0; i < Arrows.Count; i++)
		{
			//tasks.Add(AttackArrowToTarget(1f, Arrows[i], target));
			_ = AttackArrowToTarget(2f, Arrows[i], target);
		}
		//Task.WhenAll(tasks).GetAwaiter().GetResult();
	}

	private void KeyboardButtonPress(Key pressedKey)
	{
		if (pressedKey == Key.A && arrow_active_road > 0)
		{
			arrow_active_road--;
			newleft = new Vector3(gameManagerInstance.RoadPosition[arrow_active_road].x, transform.position.y, transform.position.z);
			transform.position = newleft;
		}
		if (pressedKey == Key.D && arrow_active_road < number_of_active_roads)
		{
			arrow_active_road++;
			newright = new Vector3(gameManagerInstance.RoadPosition[arrow_active_road].x, transform.position.y, transform.position.z);
			transform.position = newright;
		}
	}

	private void GameModeSwitch(GameManager.GameState newState)
	{
		if (newState == GameState.GameRunning) {
			this.currentArrowState = ArrowState.MOVING;
		}
	}

	public enum ArrowState
	{
		HALT,
		MOVING
	}

	void OnDrawGizmos()
     {
         Gizmos.color = Color.green;
         Gizmos.DrawSphere(lastposition, 0.1f);
     }
}
