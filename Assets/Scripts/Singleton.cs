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
				//obj.hideFlags = HideFlags.HideAndDontSave;
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

public class SingletonSpecial<T> : MonoBehaviour where T : MonoBehaviour
{

	private static T _instance;
	private static readonly object _instanceLock = new object();
	private static bool _quitting = false;

	public static T Instance
	{
		get
		{
			lock (_instanceLock)
			{
				if (_instance == null && !_quitting)
				{

					_instance = GameObject.FindObjectOfType<T>();
					if (_instance == null)
					{
						GameObject go = new GameObject(typeof(T).ToString());
						_instance = go.AddComponent<T>();

						DontDestroyOnLoad(_instance.gameObject);
					}
				}

				return _instance;
			}
		}
	}

	protected virtual void Awake()
	{
		if (_instance == null) _instance = gameObject.GetComponent<T>();
		else if (_instance.GetInstanceID() != GetInstanceID())
		{
			Destroy(gameObject);
			throw new System.Exception(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
		}
	}

	protected virtual void OnApplicationQuit()
	{
		_quitting = true;
	}

}