using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExtentionsUB
{
	public static void DrawRect (Rect rect, Color color)
	{
		Color temp = Gizmos.color;
		Gizmos.color = color;
		Vector2 a = new Vector2 (rect.xMin, rect.yMin);
		Vector2 b = new Vector2 (rect.xMin + rect.width, rect.yMin);
		Vector2 c = new Vector2 (rect.xMax, rect.yMax);
		Vector2 d = new Vector2 (rect.xMin, rect.yMin + rect.height);
		Gizmos.DrawSphere (a, 0.1f);
		Gizmos.DrawLine (a, b);
		Gizmos.DrawLine (b, c);
		Gizmos.DrawLine (c, d);
		Gizmos.DrawLine (d, a);
		Gizmos.color = temp;
		Vector2 labelOffset = new Vector3 (0, 0);
		string infoMin = "(xMin: " + rect.xMin + "," + "" + "yMin: " + rect.yMin + ")";
		string infoMax = "(xMax: " + rect.xMax + "," + "" + "yMax: " + rect.yMax + ")";

		#if UNITY_EDITOR
		Color temp2 = UnityEditor.Handles.color;
		GUIStyle g = new GUIStyle ();	
		g.normal.textColor = color;
		UnityEditor.Handles.Label (a + labelOffset, infoMin, g);
		UnityEditor.Handles.Label (c + labelOffset, infoMax, g);

		UnityEditor.Handles.color = temp2;
		#endif


	}

	public static void DrawRect (Rect rect)
	{
		DrawRect (rect, Gizmos.color);
	}
}

