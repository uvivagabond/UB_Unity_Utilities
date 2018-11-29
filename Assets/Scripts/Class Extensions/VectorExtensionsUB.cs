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

	public static Vector3Int ConvertToVector3Int (this Vector3 v)
	{
		Vector3Int vI = Vector3Int.CeilToInt (v);
		return vI;
	}

	public static Vector3 ConvertToVector3 (this Vector3Int vI)
	{
		Vector3 v = new Vector3 (vI.x, vI.y, vI.z);
		return v;
	}
    public static Vector2Int ConvertToVector2Int(this Vector2 v)
    {
        Vector2Int vI = Vector2Int.CeilToInt(v);
        return vI;
    }

    public static Vector2 ConvertToVector2(this Vector2Int vI)
    {
        Vector2 v = new Vector2(vI.x, vI.y);
        return v;
    }


}
