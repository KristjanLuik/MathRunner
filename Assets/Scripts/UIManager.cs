using UnityEngine;
using TMPro;

public class UIManager : SingletonPersistant<UIManager>
{
    public TextMeshProUGUI arrowAmount;
	public GameManager GameManagerInstance;

	public Canvas mainMenuCanvas;
	public Canvas gameUi;

	private void Start()
	{
		GameManagerInstance = GameManager.Instance;
		GameManagerInstance.OngameStateChanged += ShowMenu;
	}

	private void OnDestroy()
	{
		GameManagerInstance.OngameStateChanged -= ShowMenu;
	}

	private void ShowMenu(GameManager.GameState gameState)
	{
		switch (gameState) {
			case GameManager.GameState.MainMenu:
				mainMenuCanvas.enabled = true;
				gameUi.enabled = false;
				break;
			case GameManager.GameState.GameStart:
				mainMenuCanvas.enabled = false;
				gameUi.enabled = true;
				break;
			case GameManager.GameState.GameRunning:
				mainMenuCanvas.enabled = false;
				gameUi.enabled = true;
				break;
			case GameManager.GameState.Win:
				break;
			case GameManager.GameState.Lose:
				break;
			case GameManager.GameState.GameEnd:
				break;
			default:
				Debug.LogError("Missing game state");
				break;
		}
	}

	public void updateArrowUI(int amountOfArrows) {
        arrowAmount.text = string.Format("Arrows: {0}", amountOfArrows);
    }

}
