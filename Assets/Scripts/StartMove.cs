using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class StartMove : MonoBehaviour
{
    private GameManager gameManagerInstance;
    // Start is called before the first frame update
    void Start() {
        gameManagerInstance = GameManager.Instance;
        gameManagerInstance.OngameStateChanged += PrepareToStart;
	}

	private void PrepareToStart(GameManager.GameState gameState)
	{
		if (gameManagerInstance.gameState == GameState.GameStart) {
			// Play animation and call start
			new WaitForSeconds(3);
			gameManagerInstance.UpdateGameState(GameState.GameRunning);
		}

	}
}
