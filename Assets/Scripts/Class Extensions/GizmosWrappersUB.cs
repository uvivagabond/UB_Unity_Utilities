using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosWrappersUB
{

	public static void DrawFructum (Camera camera)
	{
		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS (camera.transform.position, camera.transform.rotation, Vector3.one);

		if (camera.orthographic) { 
			float spread = camera.farClipPlane - camera.nearClipPlane;
			float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
			Gizmos.DrawWireCube (
				center: new Vector3 (0, 0, center), 
				size: new Vector3 (camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));		

		} else {

			Gizmos.DrawFrustum (
				center: new Vector3 (0, 0, (camera.nearClipPlane)),
				fov: camera.fieldOfView, 
				maxRange: camera.farClipPlane, 
				minRange: camera.nearClipPlane, 
				aspect: camera.aspect);
		}
		Gizmos.matrix = temp;
	}

	public static void DrawCoordinateSystem (Transform t, int lenght = 3)
	{
		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS (t.position, Quaternion.identity, Vector3.one);

		DrawVector (Vector3.zero, t.right, 5, Color.red, " Lx", Vector3.zero, false);
		ShowLabel (t.right.normalized * 5, "Lx  (1,0,0)", Color.red, Vector3.up);

		DrawVector (Vector3.zero, t.up, 5, Color.green, "Ly", Vector3.zero, false);
		ShowLabel (t.up.normalized * 5, "Ly (0,1,0)", Color.green, Vector3.up);

		DrawVector (Vector3.zero, t.forward, 5, Color.blue, "Lz", Vector3.zero, false);
		ShowLabel (t.forward.normalized * 5, "Lz (0,0,1)", Color.blue, Vector3.forward);
		Gizmos.matrix = temp;
	}

	#region Help Functions


	static void DrawVector (Vector3 origin, Vector3 direction, float vectorLenght, Color vectorColor, string name = "", Vector3 labelOffset = default(Vector3), bool showLabel = !default(bool))
	{
		#if UNITY_EDITOR
		Matrix4x4 m = UnityEditor.Handles.matrix;
		UnityEditor.Handles.matrix = Gizmos.matrix;
		Color temp = Gizmos.color;
		Color temp2 = UnityEditor.Handles.color;
		Vector3 tempPosition = direction;
		direction.Normalize ();
		GUIStyle g = new GUIStyle ();	
		vectorColor.a = 1;
		g.normal.textColor = vectorColor;
		vectorColor.a = 0.5f;
		Gizmos.color = vectorColor;
		UnityEditor.Handles.color = vectorColor;
		if (vectorLenght > 1) {
			UnityEditor.Handles.ArrowHandleCap (0, origin + direction * (vectorLenght - 1), Quaternion.LookRotation (direction), 0.88f, EventType.Repaint);
			Gizmos.DrawRay (origin, direction * (vectorLenght - 1));
		} else {
			UnityEditor.Handles.ArrowHandleCap (0, origin, Quaternion.LookRotation (direction), vectorLenght - 0.11f * vectorLenght, EventType.Repaint);
		}
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		sb.AppendFormat (name + " ({0}, {1}, {2})", System.Math.Round (tempPosition.x, 2), System.Math.Round (tempPosition.y, 2), System.Math.Round (tempPosition.z, 2));
		if (showLabel) {
			UnityEditor.Handles.Label (origin + labelOffset + direction * (vectorLenght + 0.3f), sb.ToString (), g);
		}
		UnityEditor.Handles.color = temp2;
		UnityEditor.Handles.matrix = m;
		Gizmos.color = temp;
		#endif
	}

	static void ShowLabel (Vector3 origin, string name, Color color = default(Color), Vector3 labelOffset = default(Vector3))
	{
		#if UNITY_EDITOR
		Matrix4x4 m = UnityEditor.Handles.matrix;
		UnityEditor.Handles.matrix = Gizmos.matrix;
		Color temp2 = UnityEditor.Handles.color;
		GUIStyle g = new GUIStyle ();	
		g.normal.textColor = color;
		UnityEditor.Handles.Label (origin + labelOffset, name, g);
		UnityEditor.Handles.color = temp2;
		UnityEditor.Handles.matrix = m;

		#endif
	}

	#endregion
}
