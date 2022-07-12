using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
    static public Arrow instance { get { return s_Instance; } }
    static protected Arrow s_Instance;
    public float m_Speed = 0;
    private float m_Time_to_move_to_place = 5f;
    private List<GameObject> Arrows = new List<GameObject>();
    public int number_of_arrows = 1;
    public GameObject arrow_prefab;
    
    void Start()
    {
        UpdateArrowVisual(number_of_arrows);
    }

    public void UpdateArrowVisual(int numberOfArrows) {
        if (numberOfArrows > 0)
        {
            for (int i = 0; i < numberOfArrows; i++)
            {
                GameObject gameObjectArrow = Instantiate(arrow_prefab, transform.position, Quaternion.Euler(90f, 0f, 0f), transform);
                //Arrows.Add(gameObjectArrow);
                MoveArrowPosition(gameObjectArrow);
            }
        }
        else { 
        
        }

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
        if (Keyboard.current[Key.A].isPressed) {
            transform.Translate(-0.1f, 0f, 0f);
        }
        if (Keyboard.current[Key.D].isPressed)
        {
            transform.Translate(0.1f, 0f, 0f);
        }
    }

    private async void MoveArrowPosition(GameObject arrowToMove) {
        Vector3 arrowToMovePosition = arrowToMove.transform.position;
        var endTime = Time.time + this.m_Time_to_move_to_place;
        Vector3 endVector = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(arrowToMovePosition.y, arrowToMovePosition.y*2), arrowToMovePosition.z);
        float tValue = 0;
        while (arrowToMove != null && arrowToMovePosition != endVector) {
			tValue += Time.deltaTime;
            //arrowToMove.transform.position = Vector3.Lerp(arrowToMovePosition, endVector, tValue);
            arrowToMove.transform.localPosition = Vector3.Lerp(arrowToMovePosition, endVector, tValue);
            //arrowToMove.transform.Translate(endVector * Time.deltaTime);
            await Task.Yield();
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.name);
        //other.gameObject.GetComponent<Obsticle>().asi();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
