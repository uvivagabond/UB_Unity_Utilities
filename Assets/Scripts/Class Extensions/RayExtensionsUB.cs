using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayExtensionsUB
{

	public static float DistanceToLine (Ray ray, Vector3 point)
	{
		return Vector3.Cross (ray.direction, point - ray.origin).magnitude;
	}
}
