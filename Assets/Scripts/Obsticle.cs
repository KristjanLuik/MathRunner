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

	MathProblem leftProblem;
	MathProblem rightProblem;

	public const string leftBox = "LeftBox";
	public const string rightBox = "RightBox";

	public void InitObsticle(MathProblem leftProblem, MathProblem rightProblem)
	{
		this.leftProblem = leftProblem;
		this.rightProblem = rightProblem;
		leftText.text = leftProblem.OperationVisual();
		rightText.text = rightProblem.OperationVisual();
	}

	public void InitObsticle()
	{
		leftProblem = MathGenerator.GenerateProblem();
		rightProblem = MathGenerator.GenerateProblem();
		leftText.text = leftProblem.OperationVisual();
		rightText.text = rightProblem.OperationVisual();
	}

	public MathProblem WasHit(string side)
	{

		if (side == leftBox)
		{
			leftText.faceColor = Color.green;
			return leftProblem;
		}
		else
		{
			rightText.faceColor = Color.green;
			return rightProblem;
		}
	}
}
