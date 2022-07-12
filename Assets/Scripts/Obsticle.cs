using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obsticle : MonoBehaviour
{
    public BoxCollider left;
    public BoxCollider right;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    // Start is called before the first frame update
    void Start()
    {
        MathProblem firstProblem = MathGenerator.GenerateProblem();
        MathProblem secondProblem = MathGenerator.GenerateProblem();
        leftText.text = firstProblem.OperationVisual();
        rightText.text = secondProblem.OperationVisual();
    }

    private void OnTriggerEnter(Collider other)
	{
        Debug.Log(other);
        if (other == left) {
            leftText.fontSize = (float)FontStyle.Bold;
        }
        if (other == right) { 
            
        }
	}
}
