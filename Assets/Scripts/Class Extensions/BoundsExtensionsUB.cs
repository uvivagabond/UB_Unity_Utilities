using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtensionsUB
{
	public static BoundsInt ConvertToBoundInt (this Bounds b)
	{
		
		BoundsInt bI = new BoundsInt (b.center.ConvertToVector3Int (), b.size.ConvertToVector3Int ());
		return bI;
	}

	public static Bounds ConvertToBound (this BoundsInt bI)
	{
		Bounds b = new Bounds (bI.center, bI.size);
		return b;
	}
}
