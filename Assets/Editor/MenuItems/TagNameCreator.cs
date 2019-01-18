using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.Animations;

// Based on
//http://baba-s.hatenablog.com/entry/2014/02/25/000000
using System.Collections.Generic;


public static class TagNameCreator
{
	private static readonly string[] INVALID_CHARS = {
		" ", "!", "\"", "#", "$",
		"%", "&", "\'", "(", ")",
		"-", "=", "^",  "~", "\\",
		"|", "[", "{",  "@", "`",
		"]", "}", ":",  "*", ";",
		"+", "/", "?",  ".", ">",
		",", "<"
	};

	const string ITEM_NAME = "Tools/Create constants/All";


	const string PATH = "Assets/Scripts/Constants and Enums/Tags.cs";
	const string PATH2 = "Assets/Scripts/Constants and Enums/IDOfLayer.cs";
	const string PATH3 = "Assets/Scripts/Constants and Enums/LayersForMask.cs";
	const string PATH4 = "Assets/Scripts/Constants and Enums/VirtualAxesNames.cs";
	const string PATH5 = "Assets/Scripts/Constants and Enums/BuildScenesNames.cs";
	const string PATH6 = "Assets/Scripts/Constants and Enums/BuiltinMeshesNames.cs";
	const string PATH7 = "Assets/Scripts/Constants and Enums/HtmlColorNames.cs";
	const string PATH17 = "Assets/Scripts/Constants and Enums/AnimParam.cs";


	[MenuItem (ITEM_NAME)]
	public static void CreateTags ()
	{
		if (!CanCreate ()) {
			return;
		}
		CreateScript (GetTags, PATH);
		CreateScript (GetIDOfLayers, PATH2);
		CreateScript (GetValueOfLayers, PATH3);
		CreateScript (GetAxesNames, PATH4);
		CreateScript (GetBuildScenesNames, PATH5);
		CreateScript (GetBuiltinMeshes, PATH6);
		CreateScript (GetHtmlColorNames, PATH7);

	}

	static string GetBuiltinMeshes ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class BuiltinMeshesNames");
		builder.AppendLine ("{");
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Capsule").AppendLine ();
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Cube").AppendLine ();
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Cylinder").AppendLine ();
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Plane").AppendLine ();
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Quad").AppendLine ();
		builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}.fbx"";", "Sphere").AppendLine ();
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	#region Colors

	static string GetHtmlColorNames ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class HtmlColorNames");
		builder.AppendLine ("{");
		string[] colorsNames = {
			"red", "cyan", "blue", "darkblue", "lightblue", "purple", "yellow", "lime", "fuchsia", "white", "silver", "grey", "black", "orange", "brown", "maroon", "green", "olive", "navy", "teal", "aqua", "magenta"
		};
		foreach (string item in colorsNames) {
			builder.Append ("\t").AppendFormat (@"public const string {0} = ""{0}"";", item).AppendLine ();

		}
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	#endregion

	static string GetTags ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class Tags");
		builder.AppendLine ("{");

		foreach (var n in InternalEditorUtility.tags.
				Select(c => new { var = RemoveInvalidChars(c), val = c })) {
			builder.Append ("\t").AppendFormat (@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine ();
		}
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	static string GetIDOfLayers ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class IDOfLayer");
		builder.AppendLine ("{");

		int ID = 0, i = 0;				
		foreach (var n in InternalEditorUtility.layers.Select(c => new { var = RemoveInvalidChars(c), val = c })) {
			ID = (i < 5) ? ((i < 3) ? i : i + 1) : i + 3;
		
			builder.Append ("\t").AppendFormat (@"public const int {0} = {1};", n.var, ID).AppendLine ();
			if (i == 4) {
				builder.AppendLine ();
			}
			i++;
		}
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	static string GetBuildScenesNames ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class BuildScenesNames");
		builder.AppendLine ("{");
		EditorBuildSettingsScene[] allScenes = EditorBuildSettings.scenes;
		foreach (var item in allScenes) {
			if (item.enabled) {
				string sceneName = Path.GetFileNameWithoutExtension (item.path);
				builder.Append ("\t").AppendFormat (@"public const string {0} = ""{1}"";", sceneName, sceneName).AppendLine ();
			}
		}
		builder.AppendLine ("}");
		int sceneIndex = 0;
		builder.AppendLine ("public static class BuildScenesIndex");
		builder.AppendLine ("{");
		foreach (var item in allScenes) {
			if (item.enabled) {
				string sceneName = Path.GetFileNameWithoutExtension (item.path);
				builder.Append ("\t").AppendFormat (@"public const int {0} = {1};", sceneName, sceneIndex).AppendLine ();
				sceneIndex++;
			}
		}
		builder.AppendLine ("}");

		return  builder.ToString ();
	}

	static string GetAxesNames ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("public static class VirtualAxesNames");
		builder.AppendLine ("{");
		string path = Application.dataPath.Replace (@"Assets", @"") + "/ProjectSettings/InputManager.asset";
		string[] allText = File.ReadAllLines (path);
		HashSet<string> listOfAxes = new HashSet<string> ();
		foreach (var item in allText) {
			if (item.Contains ("m_Name: ")) {
				var temp = item.Replace ("m_Name: ", "");
				listOfAxes.Add (temp);
			}
		}
		foreach (var item in listOfAxes) {
			builder.Append ("\t").AppendFormat (@"public const string {0} = ""{1}"";", item.Replace (" ", ""), item.Remove (0, 4)).AppendLine ();
		}
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	static string GetValueOfLayers ()
	{
		var builder = new StringBuilder ();
		builder.AppendLine ("using System;\n");
		builder.AppendLine ("public static class LayersForMask");
		builder.AppendLine ("{");
		builder.AppendLine ("\tpublic const int Everything = -1;");
		builder.AppendLine ("\tpublic const int Nothing = 0;");

		int ID = 0, i = 0;				
		foreach (var n in InternalEditorUtility.layers.Select(c => new { var = RemoveInvalidChars(c), val = c })) {
			ID = (i < 5) ? ((i < 3) ? i : i + 1) : i + 3;
			builder.Append ("\t").AppendFormat (@"public const int {0}  = 1<<{1};", n.var, ID).AppendLine ();

			if (i == 4) {
				builder.AppendLine ();
			}
			i++;
		}
		builder.AppendLine ("}");
		return  builder.ToString ();
	}

	public static void CreateScript (Func<string> func, string path)
	{
		var directoryName = Path.GetDirectoryName (path);
		if (!Directory.Exists (directoryName)) {
			Directory.CreateDirectory (directoryName);
		}
		File.WriteAllText (path, func.Invoke (), Encoding.UTF8);
		AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
	}




	[MenuItem (ITEM_NAME, true)]
	public static bool CanCreate ()
	{
		return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
	}


	public static string RemoveInvalidChars (string str)
	{
		Array.ForEach (INVALID_CHARS, c => str = str.Replace (c, string.Empty));
		return str;
	}


}
