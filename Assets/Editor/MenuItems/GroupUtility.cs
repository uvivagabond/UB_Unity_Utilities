using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

///
/// Simple Group Utility for Unity3D - made by Ryan Miller ryan@reptoidgames.com
///
public class GroupUtility
{
	[MenuItem ("GameObject/Add /Group %g", false, -11100000)]
	public static void Group ()
	{
		if (Selection.transforms.Length > 0) {
			GameObject group = new GameObject ("[ Group ]");

			// set pivot to average of selection
			Vector3 pivotPosition = Vector3.zero;
			foreach (Transform g in Selection.transforms) {
				pivotPosition += g.transform.position;
			}
			pivotPosition /= Selection.transforms.Length;
			group.transform.position = pivotPosition;

			// register undo as we parent objects into the group
//			Undo.RegisterCreatedObjectUndo (group, "Group");
//			foreach (GameObject s in Selection.gameObjects) {
//				Undo.SetTransformParent (s.transform, group.transform, "Group");
//			}

			Selection.activeGameObject = group;
		} else {
			Debug.LogWarning ("You must select one or more objects.");
		}
	}
}