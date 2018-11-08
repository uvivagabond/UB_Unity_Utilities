using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentionsUB
{
	//http://baba-s.hatenablog.com/entry/2014/07/14/101855
	public static bool HasComponent<T> (this GameObject self) where T : Component
	{
		return self.GetComponent<T> () != null;
	}
}
