using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static public UIManager instance { get { return s_Instance; } }
    static protected UIManager s_Instance;
    public TextMeshProUGUI arrowAmount;
    protected void Awake()
    {
        s_Instance = this;
        updateArrowUI(Arrow.instance.NumberOfArrows);
    }

	public void updateArrowUI(int amountOfArrows) {
        arrowAmount.text = string.Format("Arrows: {0}", amountOfArrows);
    }

}
