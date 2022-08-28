using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTrigger : MonoBehaviour
{
    private Arrow arrowInstance;

    public TextMeshProUGUI endText;
    public GameObject heart;

	protected void Awake()
	{
        arrowInstance = FindObjectOfType<Arrow>();
    }

	public void WasHit()
    {
        arrowInstance.HitEndGoal(transform.position);
	}

    public void SetNeededArrowAMount(int neededArrows) {
        endText.text = string.Format("{0}", neededArrows);
    }
}
