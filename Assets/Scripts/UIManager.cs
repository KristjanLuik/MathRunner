using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : SingletonPersistant<UIManager>
{
    public TextMeshProUGUI arrowAmount;

	public void updateArrowUI(int amountOfArrows) {
        arrowAmount.text = string.Format("Arrows: {0}", amountOfArrows);
    }

}
