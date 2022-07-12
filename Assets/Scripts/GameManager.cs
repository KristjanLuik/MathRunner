using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get { return s_Instance; } }
    static protected GameManager s_Instance;
    [Tooltip("The X position of lains")]
    public List<Vector3> RoadPosition = new List<Vector3>{new Vector3(-0.5f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0f) };

    protected void Awake()
    {
        s_Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore() {
        //Call arrow manager
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 road in this.RoadPosition) {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(road, 0.1f);
        }
    }

    public enum GameState {
        
    }
}
