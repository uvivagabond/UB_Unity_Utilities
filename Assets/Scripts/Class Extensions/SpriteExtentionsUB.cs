using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif
public static class SpriteExtentionsUB
{
	/// <summary>
	/// Change pixel lenght of sprite to world lenght - include pivot position
	/// </summary>
	public static Vector2 PixelToWorldPoint (this Sprite s, Vector2 pointInPixelSpace)
	{
		Vector2 pivot = s.pivot;
		float PPU = s.pixelsPerUnit;
		return new Vector2 (
			(pointInPixelSpace.x) / 100f - pivot.x / PPU, 
			(pointInPixelSpace.y) / 100f - pivot.y / PPU
		); 
	}

	/// <summary>
	/// Change world lenght of sprite to pixel lenght - include pivot position
	/// </summary>
	public static Vector2 WorldToPixelPoint (this Sprite s, Vector2 pointInWorldSpace)
	{
		Vector2 pivot = s.pivot;
		float IPPU = 100 / s.pixelsPerUnit;
		return new Vector2 (
			Mathf.Round (pointInWorldSpace.x * 100 + pivot.x * IPPU), 
			Mathf.Round (pointInWorldSpace.y * 100 + pivot.y * IPPU));
	}

	/// <summary>
	/// Change normalized lenght of sprite to pixel lenght
	/// </summary>
	public static Vector2 NormalizedToPixelPoint (this Sprite s, Vector2 pointInNormalizedSpace)
	{
		Rect r = s.rect;
		return new Vector2 (Mathf.Round (pointInNormalizedSpace.x * r.width), Mathf.Round (pointInNormalizedSpace.y * r.height));		 
	}

	/// <summary>
	/// Change pixel lenght of sprite to normalized lenght
	/// </summary>
	public static Vector2 PixelToNormalizedPoint (this Sprite s, Vector2 pointInTextureSpace)
	{
		Rect r = s.rect;
		return new Vector2 (pointInTextureSpace.x / r.width, pointInTextureSpace.y / r.height);		 
	}


	/// <summary>
	/// Change normalized lenght of sprite to world lenght
	/// </summary>
	public static Vector2 NormalizedToWorldPoint (this Sprite s, Vector2 pointInNormalizedSpace)
	{
		Rect r = s.rect;
		Vector2 pivot = s.pivot;
		float PPU = s.pixelsPerUnit;
		return new Vector2 ((pointInNormalizedSpace.x * r.width / PPU)	//- pivot.x / r.height	- to pozwala na uwzględnianie pivota			
			, (pointInNormalizedSpace.y * r.height / PPU));	
	}

	/// <summary>
	/// Change world lenght of sprite to normalized lenght
	/// </summary>
	public static Vector2 WorldToNormalizedPoint (this Sprite s, Vector2 pointInWorldSpace)
	{
		Rect r = s.rect;
		Vector2 pivot = s.pivot;
		float PPU = s.pixelsPerUnit;
		return new Vector2 ((pointInWorldSpace.x / r.width * PPU)	//- pivot.x / r.height	- to pozwala na uwzględnianie pivota			
			, (pointInWorldSpace.y / r.height * PPU));	
		
	}

	/// <summary>
	/// Return vertices position in Sprite.rect space (pixels) not in local space (meters)
	/// </summary>

	public static Vector2[] GetVerticesInPixels (this Sprite s)
	{
		Vector2[] vertices = s.vertices;
		int tabLenght = vertices.Length;
		Vector2[] temp = new Vector2[tabLenght];

		for (int i = 0; i < tabLenght; i++) {
			temp [i] = s.WorldToPixelPoint (vertices [i]);
		}
		return temp;
	}

	/// <summary>
	/// Info about sprite mesh
	/// </summary>

	public static string ToString (this Sprite s, bool usePixels)
	{
		string nV = "new Vector2 (";
		string vert = "Vector2[] vertices = {" + Environment.NewLine + Environment.NewLine;
		string tri = "ushort[] triangles = {";
		string uv = "Vector2[] uv = {" + Environment.NewLine + Environment.NewLine;


		Vector2[] vertices = s.vertices;
		ushort[] triangles = s.triangles;
		Vector2[] uvis = s.uv;

		if (usePixels) {
			foreach (var item in vertices) {
				Vector2 p = s.WorldToPixelPoint (item);
				vert += nV + p.x + ", " + p.y + ")," + Environment.NewLine;
			}
		} else {
			foreach (var item in vertices) {
				vert += nV + item.x + "f, " + item.y + "f)," + Environment.NewLine;
			}
		}

		vert += Environment.NewLine + "};" + Environment.NewLine + Environment.NewLine;
		;
		int k = 0;
		foreach (var item in triangles) {
			k++;			
			tri += item + ", ";
			if (k % 3 == 0) {
				tri += Environment.NewLine;
			}
		}
		tri += "};" + Environment.NewLine + Environment.NewLine;


		vert += tri;

		foreach (var item in uvis) {
			uv += nV + (float)Math.Round (item.x, 3) + "f, " + (float)Math.Round (item.y, 3) + "f)," + Environment.NewLine;
		}
		vert += uv;
		return vert;
	}





	public static Mesh ConvertToMesh (this Sprite s)
	{
		if (!s)
			return null;
		int vCount = s.vertices.Length;
		int tCount = s.triangles.Length;
	
		Vector3[] tempV = new Vector3[vCount];
		int[] tempT = new int[tCount];
		for (int i = 0; i < vCount; i++) {
			tempV [i] = (Vector3)s.vertices [i];
		}
		for (int i = 0; i < tCount; i++) {
			tempT [i] = (int)s.triangles [i];
		}

		Vector3[] normals = new Vector3[vCount];			
		Plane p = new Plane (s.vertices [s.triangles [0]], s.vertices [s.triangles [1]], s.vertices [s.triangles [2]]);
		for (int i = 0; i < vCount; i++) {
			normals [i] = p.normal;
		}
		Mesh m = new Mesh ();
		m.vertices = tempV;
		m.triangles = tempT;
		m.uv = s.uv;
		m.normals = normals;
		return m;
	}

	#region Methods to copy info about sprite mesh


	#if UNITY_EDITOR

	private const string CopySpriteMeshWorldUnit = @"Assets/Copy/Sprite Mesh/Meters";
	private const string CopySpriteMeshPixelUnit = @"Assets/Copy/Sprite Mesh/Pixels";

	private static bool IsSelectAsset   { get { return Selection.objects != null && 0 < Selection.objects.Length; } }

	private static bool IsSprite   { get { return Selection.objects != null && Selection.objects [0].GetType () == typeof(UnityEngine.Sprite); } }

	[MenuItem (CopySpriteMeshPixelUnit, true)]
	[MenuItem (CopySpriteMeshWorldUnit, true)]
	private static bool ValidateSpriteSelection ()
	{
		return IsSelectAsset && IsSprite;
	}

	[MenuItem (CopySpriteMeshWorldUnit, false, 1111)]
	private static void GetMeshInMetersUnit ()
	{
		GetSpriteMesh ();
	}

	[MenuItem (CopySpriteMeshPixelUnit, false, 1111)]
	private static void GetMeshInPixelsUnit ()
	{
		GetSpriteMesh (true);
	}

	private static void GetSpriteMesh (bool usePixels = default(bool))
	{
		string nV = "new Vector2 (";
		string vert = "Vector2[] vertices = {" + Environment.NewLine;
		string tri = "ushort[] triangles = {";

		Sprite s = (Sprite)Selection.objects [0];
		Vector2[] vertices = s.vertices;
		ushort[] triangles = s.triangles;

		if (usePixels) {
			foreach (var item in vertices) {
				Vector2 p = s.WorldToPixelPoint (item);
				vert += nV + p.x + ", " + p.y + ")," + Environment.NewLine;
			}
		} else {
			foreach (var item in vertices) {
				vert += nV + item.x + "f, " + item.y + "f)," + Environment.NewLine;
			}
		}

		vert += "};";
		int k = 0;
		foreach (var item in triangles) {
			k++;
			tri += item + ", ";
			if (k % 3 == 0) {
				tri += Environment.NewLine;
			}
		}
		tri += "};";
		vert += tri;

		EditorGUIUtility.systemCopyBuffer = vert;
	}

	#endif
	#endregion

}
