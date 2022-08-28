using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

	public enum Scenes {
		_init,
		MainGame,
	}

	public static void Load(Scene scene) {
		SceneManager.LoadScene(scene.ToString());
	}
}
