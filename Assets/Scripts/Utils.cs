using UnityEngine;
using Cinemachine;

public class Utils : MonoBehaviour
{
	public static Vector3 ScreenToWorld(Camera camera, Vector3 position) {
		position.z = camera.nearClipPlane;
		return camera.ScreenToWorldPoint(position);
	}
}
