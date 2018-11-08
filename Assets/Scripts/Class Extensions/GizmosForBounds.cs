using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosForBounds
{
	public static void  DrawBounds (Bounds bounds)
	{
		Gizmos.DrawWireCube (bounds.center, bounds.size);
	}

}
