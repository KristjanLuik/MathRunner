using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A base class to instanciate singletons
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
	public static T _instance;

	public static T Instance {
		get {
			if (_instance == null) {
				GameObject obj = new GameObject();
				obj.name = typeof(T).Name;
				obj.hideFlags = HideFlags.HideAndDontSave;
				_instance = obj.AddComponent<T>();
			}
			return _instance;
		}
	}

	private void OnDestroy()
	{
		if (_instance == this) {
			_instance = null;
		}
	}
}


public class SingletonPersistant<T> : MonoBehaviour where T : Component
{
	public static T _instance;

	public static T Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this as T;
			DontDestroyOnLoad(gameObject); 
		}
		else {
			Destroy(this);
		}
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}
}