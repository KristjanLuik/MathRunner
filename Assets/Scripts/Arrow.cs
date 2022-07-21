using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
    public static Arrow instance { get { return s_Instance; } }
    static protected Arrow s_Instance;
    public float m_Speed = 0;
    private float m_Time_to_move_to_place = 5f;
    private List<GameObject> Arrows = new List<GameObject>();
    private int number_of_arrows = 0;
    public int NumberOfArrows   // property
    {
        get { return number_of_arrows; }   // get method
        set {
            UpdateArrowVisual(value);
            number_of_arrows = value;
        }
    }
    public GameObject arrow_prefab;

    public int arrow_active_road;
    public int number_of_active_roads;

    public Vector3 newleft, newright;
    protected void Awake()
    {
        s_Instance = this;
    }

    void Start()
    {
        this.NumberOfArrows = 1;

        // Pick a starting road at random
        number_of_active_roads = GameManager.instance.RoadPosition.Count - 1;
        int roadIndex = Random.Range(0, GameManager.instance.RoadPosition.Count);
        arrow_active_road = roadIndex;
        Vector3 randomStartPosition = GameManager.instance.RoadPosition[roadIndex];
        transform.position = randomStartPosition;
    }

    public bool UpdateArrowVisual(int newAmountOfArrows) {

        // If less then one game over
        bool isLessThanOneArrow = newAmountOfArrows < 1;

        if (isLessThanOneArrow) {
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
        else {
            //Add arrows
            for (int i = 0; i < arrowDiffrence; i++)
            {
                GameObject gameObjectArrow = Instantiate(arrow_prefab, transform.position, Quaternion.Euler(90f, 0f, 0f), transform);
                Arrows.Add(gameObjectArrow);
                MoveArrowPosition(gameObjectArrow);
            }
        }
        UIManager.instance.updateArrowUI(newAmountOfArrows);

        return true;
    }

	private void OnDestroy()
	{
        //this.Arrows.Clear();
	}
	void Update()
    {
        transform.Translate(0f, 0f, this.m_Speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Keyboard.current[Key.A].isPressed && arrow_active_road > 0) {
            arrow_active_road--;
            newleft = new Vector3(GameManager.instance.RoadPosition[arrow_active_road].x, transform.position.y, transform.position.z);
            transform.position = newleft;
        }
        if (Keyboard.current[Key.D].isPressed && arrow_active_road < number_of_active_roads)
        {
            arrow_active_road++;
            newright = new Vector3(GameManager.instance.RoadPosition[arrow_active_road].x, transform.position.y, transform.position.z);
            transform.position = newright;
        }
    }

    private async void MoveArrowPosition(GameObject arrowToMove) {
        Vector3 arrowToMovePosition = arrowToMove.transform.position;
        float endTime = Time.time + this.m_Time_to_move_to_place;
        Vector3 endVector = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(arrowToMovePosition.y, arrowToMovePosition.y*2), 0f);
        float tValue = 0;
        while (arrowToMove != null && arrowToMovePosition != endVector) {
			tValue += Time.deltaTime;
            
            arrowToMove.transform.localPosition = Vector3.Lerp(arrowToMovePosition, endVector, tValue);

            await Task.Yield();
        }
    }

    private void OnTriggerEnter(Collider other) {
        MathProblem triggeredProblem = other.gameObject.GetComponentInParent<Obsticle>().WasHit(other.name);
        Debug.Log(triggeredProblem);
        Debug.Log(string.Format("number_of_arrows: {0}", number_of_arrows));

        int newArrowAmount = newArrowCount(triggeredProblem);
        Debug.Log(string.Format("newArrowAmount: {0}", newArrowAmount));
        this.NumberOfArrows = newArrowAmount;
    }

    public int newArrowCount(MathProblem mathProblem) {
        switch (mathProblem.op) {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(newleft, 0.1f);
        Gizmos.DrawSphere(newright, 0.1f);
    }
}
