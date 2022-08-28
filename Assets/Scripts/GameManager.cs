using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistant<GameManager>
{
    public GameState gameState;

	public delegate void GameStateChanged(GameState gameState);
	public GameStateChanged OngameStateChanged;

	[Tooltip("The X position of lains")]
    public List<Vector3> RoadPosition = new List<Vector3>{new Vector3(-0.5f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0f) };

	private void Start()
	{
		UpdateGameState(GameState.MainMenu);
	}


	public void UpdateGameState(GameState gameState) {
		this.gameState = gameState;

		switch (gameState)
		{
			case GameState.MainMenu:
				//HandleMenuShow();
				break;
			case GameState.GameStart:
				break;
			case GameState.GameRunning:
				break;
			case GameState.Win:
				break;
			case GameState.Lose:
				break;
			case GameState.GameEnd:
				break;
			default:
				Debug.LogError("Missing game state");
				break;
		}

		OngameStateChanged?.Invoke(gameState);
	}

	#region GameState handlers
	private void HandleMenuShow()
	{
		throw new NotImplementedException();
	}
	#endregion


	public void PlayClick() {
		UpdateGameState(GameState.GameStart);
	}
	private void OnDrawGizmos()
    {
        foreach (Vector3 road in this.RoadPosition) {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(road, 0.1f);
        }
    }

    public enum GameState {
        MainMenu,
        GameStart,
        GameRunning,
        Win,
        Lose,
        GameEnd
    }
}
