using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensionsUB
{
	//https://forum.unity.com/threads/camera-to-object-distance.32643/
	/// <summary>
	/// Gets the distance to screen plane in which is gameObject.
	/// </summary>


	public static float GetDistanceToPlaneInWhichIsObject (this Camera camera, Transform objPosition)
	{
		Transform cameraTransform = camera.transform;
		Vector3 heading = objPosition.position - cameraTransform.position;
		return Vector3.Dot (heading, cameraTransform.forward);
	}

}
