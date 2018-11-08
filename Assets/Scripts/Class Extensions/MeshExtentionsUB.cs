using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MeshExtentionsUB
{

	public static string ToString (this Mesh m, bool withTriangles, bool withUV = false, bool withNormals = false)
	{
		string nV = "new Vector3 (";
		string vert = "Vector3[] vertices = {" + Environment.NewLine + Environment.NewLine;
		string tri = "int[] triangles = {" + Environment.NewLine;
		string uv = "Vector2[] uv = {" + Environment.NewLine + Environment.NewLine;
		string norm = "Vector3[] normals = {" + Environment.NewLine + Environment.NewLine;

		;

		Vector3[] vertices = m.vertices;
		int[] triangles = m.triangles;
		Vector2[] uvis = m.uv;
		Vector3[] normals = m.normals;


		foreach (var item in vertices) {
			vert += nV + item.x + "f, " + item.y + "f, " + item.z + "f)," + Environment.NewLine;
		}

		vert += Environment.NewLine + "};" + Environment.NewLine + Environment.NewLine;
		;
		if (withTriangles) {
			int k = 0;
			foreach (var item in triangles) {
				k++;
				tri += item + ", ";
				if (k % 3 == 0) {
					tri += Environment.NewLine;
				}
			}
			tri += "};" + Environment.NewLine + Environment.NewLine;
			;
			vert += tri;
		}
		if (withUV) {
			foreach (var item in uvis) {
				uv += nV + item.x + "f, " + item.y + "f)," + Environment.NewLine;
			}
			uv += "};" + Environment.NewLine + Environment.NewLine;
			vert += uv;
		}
		if (withNormals) {
			foreach (var item in normals) {
				norm += nV + item.x + "f, " + item.y + "f, " + item.z + "f)," + Environment.NewLine;
			}

			norm += Environment.NewLine + "};" + Environment.NewLine + Environment.NewLine;
			vert += norm;
		}


		return vert;
	}
}
