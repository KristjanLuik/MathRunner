using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[DefaultExecutionOrder(-2)]
public class InputManager : Singleton<InputManager>
{
	#region Events
	public delegate void StartTouch(Vector2 position, float time);
	public event StartTouch OnStartTouch;
	public delegate void EndTouch(Vector2 position, float time);
	public event EndTouch OnEndTouch;
	public delegate void KeyboardPress(Key pressedKey);
	public event KeyboardPress OnKeyboardPress;
	#endregion

	private PlayerInput inputControls;
	private Camera mainCamera;

	private void Awake()
	{
		inputControls = new PlayerInput();
		mainCamera = FindObjectOfType<Camera>();
	}

	private void OnEnable()
	{
		inputControls.Enable();
	}

	private void OnDisable()
	{
		inputControls.Disable();
	}

	private void Start()
	{
		inputControls.Touch.TouchInput.started += ctx => StartTouchPrimary(ctx);
		inputControls.Touch.TouchInput.canceled += ctx => EndTouchPrimary(ctx);
		inputControls.Keyboard.Move.performed += ctx => DoKeyboardInput(ctx);
	}

	private void DoKeyboardInput(InputAction.CallbackContext ctx)
	{
		//ctx.control.keyCode
		OnKeyboardPress?.Invoke(((KeyControl)ctx.control).keyCode);
	}

	private void StartTouchPrimary(InputAction.CallbackContext ctx)
	{
		OnStartTouch?.Invoke(inputControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)ctx.startTime);
	}
	private void EndTouchPrimary(InputAction.CallbackContext ctx)
	{
		OnEndTouch?.Invoke(inputControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)ctx.time);
	}

	// To get finger position at all times
	public Vector2 PrimaryPosition() {
		return Utils.ScreenToWorld(mainCamera, inputControls.Touch.PrimaryPosition.ReadValue<Vector2>());
	}

	/*void OnGUI()
	{
		Vector2 mposition = inputControls.Touch.PrimaryPosition.ReadValue<Vector2>();
		Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(mposition.x, mposition.y, mainCamera.nearClipPlane));

		GUILayout.BeginArea(new Rect(100, 100, 500, 240));
		GUILayout.Label("Screen pixels: " + mainCamera.pixelWidth + ":" + mainCamera.pixelHeight);
		GUILayout.Label("Mouse position: " + mposition);
		GUILayout.Label("World position: " + Touchscreen.current.primaryTouch.position.ReadValue());
		GUILayout.EndArea();
	}*/
}
