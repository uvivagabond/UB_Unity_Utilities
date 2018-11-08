using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensionsUB
{

	public static Vector3 Round (this Vector3 v, int digits)
	{
		v.x = (float)System.Math.Round (v.x, digits);
		v.y = (float)System.Math.Round (v.y, digits);
		v.z = (float)System.Math.Round (v.z, digits); 
		return v;
	}

	public static Vector2 Round (this Vector2 v, int digits)
	{
		v.x = (float)System.Math.Round (v.x, digits);
		v.y = (float)System.Math.Round (v.y, digits);
		return v;
	}

	public static Vector4 Round (this Vector4 v, int digits)
	{
		v.x = (float)System.Math.Round (v.x, digits);
		v.y = (float)System.Math.Round (v.y, digits);
		v.z = (float)System.Math.Round (v.z, digits);
		v.w = (float)System.Math.Round (v.w, digits); 

		return v;
	}
}
