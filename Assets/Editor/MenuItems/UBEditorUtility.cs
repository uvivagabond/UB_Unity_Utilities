using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine.Rendering;
using System.Resources;
using UnityEngine.U2D;
using System.Text;
using UnityEngine.Tilemaps;


public class UBEditorUtility
{
	const string PATH = "Mein Menu/";
	const string FINDASSETS = "Find all assets of type /";
	const int consoleFontSize = 11;
	//15

	[MenuItem (PATH + "Unload Unused Assets", false, 14)]
	static void MethodInMenu1 ()
	{		
		Resources.UnloadUnusedAssets ();
	}

	[MenuItem (PATH + "Delete All Keys", false, 9999)]
	static void DeleteAllKeys ()
	{
		PlayerPrefs.DeleteAll ();
	}


	[MenuItem (PATH + "Folders/Create All Folders", false, 55)]
	static void MakeFolders ()
	{
		string projectPath = Application.dataPath + "/"; 
		Directory.CreateDirectory (projectPath + "Materials/Physics");
		Directory.CreateDirectory (projectPath + "Resources");
		Directory.CreateDirectory (projectPath + "Scripts");
		Directory.CreateDirectory (projectPath + "Sprites");
		Directory.CreateDirectory (projectPath + "Scenes");
		Directory.CreateDirectory (projectPath + "Prefabs");
		Directory.CreateDirectory (projectPath + "Gizmos");
		Directory.CreateDirectory (projectPath + "Fonts");
		Directory.CreateDirectory (projectPath + "Models");
		Directory.CreateDirectory (projectPath + "Editor/Utilities");
		Directory.CreateDirectory (projectPath + "Editor/PropertyDrawers");
		Directory.CreateDirectory (projectPath + "PropertyDrawers");
		Debug.Log ("Added new Folders");
		AssetDatabase.Refresh ();
	}

	#region Find Assets of Type

	[MenuItem (PATH + FINDASSETS + "EditorWindow", false, 183)]
	static void FindEditorWindow ()
	{
		FindAssetsInMemory (typeof(EditorWindow));
	}

	[MenuItem (PATH + FINDASSETS + "Editor", false, 183)]
	static void FindEditor ()
	{
		FindAssetsInMemory (typeof(Editor));
	}

	[MenuItem (PATH + FINDASSETS + "AssetBundle", false, 83)]
	static void FindAssetBundle ()
	{
		FindAssetsInMemory (typeof(AssetBundle));
	}

	[MenuItem (PATH + FINDASSETS + "DefaultAsset", false, 83)]
	static void FindDefaultAsset ()
	{
		FindAssetsInMemory (typeof(DefaultAsset));
	}


	[MenuItem (PATH + FINDASSETS + "Textures/Texture2D", false, 84)]
	static void FindTextures2D ()
	{
		FindAssetsInMemory (typeof(Texture2D));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/Texture3D", false, 84)]
	static void FindTextures3D ()
	{
		FindAssetsInMemory (typeof(Texture3D));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/Texture2DArray", false, 84)]
	static void FindTextures2DArray ()
	{
		FindAssetsInMemory (typeof(Texture2DArray));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/Cubemap", false, 84)]
	static void FindCubemap ()
	{
		FindAssetsInMemory (typeof(Cubemap));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/CubemapArray", false, 84)]
	static void FindCubemapArray ()
	{
		FindAssetsInMemory (typeof(CubemapArray));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/MovieTexture", false, 84)]
	static void FindMovieTexture ()
	{
		FindAssetsInMemory (typeof(MovieTexture));
	}

	[MenuItem (PATH + FINDASSETS + "Textures/RenderTexture", false, 84)]
	static void FindRenderTexture ()
	{
		FindAssetsInMemory (typeof(RenderTexture));
	}

	[MenuItem (PATH + FINDASSETS + "ScriptableObject", false, 182)]
	static void FindScriptableObject ()
	{
		FindAssetsInMemory (typeof(ScriptableObject));
	}

	[MenuItem (PATH + FINDASSETS + "Material", false, 85)]
	static void FindMaterials ()
	{
		FindAssetsInMemory (typeof(Material));
	}

	[MenuItem (PATH + FINDASSETS + "GUISkin", false, 85)]
	static void FindGUISkin ()
	{
		FindAssetsInMemory (typeof(GUISkin));
	}

	[MenuItem (PATH + FINDASSETS + "GameObject", false, 2)]
	static void FindGO ()
	{
		FindAssetsInMemory (typeof(GameObject));
	}

	[MenuItem (PATH + FINDASSETS + "Object (without object from above types)", false, -11)]
	static void FindO ()
	{
		FindAssetsInMemory (typeof(Object));
	}

	[MenuItem (PATH + FINDASSETS + "Component", false, 3)]
	static void FindComponents ()
	{
		FindAssetsInMemory (typeof(Component));
	}

	[MenuItem (PATH + FINDASSETS + "Mesh", false, 15)]
	static void FindMesh ()
	{
		FindAssetsInMemory (typeof(Mesh));
	}

	[MenuItem (PATH + FINDASSETS + "2D/Sprite", false, 16)]
	static void FindSprite ()
	{
		FindAssetsInMemory (typeof(Sprite));
	}

	[MenuItem (PATH + FINDASSETS + "2D/Tile", false, 16)]
	static void FindTile ()
	{
		FindAssetsInMemory (typeof(Tile));
	}

	[MenuItem (PATH + FINDASSETS + "2D/SpriteAtlas", false, 16)]
	static void FindSpriteAtlas ()
	{
		FindAssetsInMemory (typeof(SpriteAtlas));
	}

	[MenuItem (PATH + FINDASSETS + "Font", false, 90)]
	static void FindFont ()
	{
		FindAssetsInMemory (typeof(Font));
	}

	[MenuItem (PATH + FINDASSETS + "Shader", false, 91)]
	static void FindShader ()
	{
		FindAssetsInMemory (typeof(Shader));
	}

	[MenuItem (PATH + FINDASSETS + "Importers/PluginImporter", false, 1111)]
	static void FindPluginImporter ()
	{
		FindAssetsInMemory (typeof(PluginImporter));
	}

	[MenuItem (PATH + FINDASSETS + "Importers/MonoImporter", false, 1111)]
	static void FindMonoImporter ()
	{
		FindAssetsInMemory (typeof(MonoImporter));
	}

	[MenuItem (PATH + FINDASSETS + "Importers/All importers", false, 1111)]
	static void FindAssetImporter ()
	{
		FindAssetsInMemory (typeof(AssetImporter));
	}

	[MenuItem (PATH + FINDASSETS + "MonoScript (our all scripts)", false, 1100)]
	static void FindMonoScript ()
	{
		FindAssetsInMemory (typeof(MonoScript));
	}

	#endregion

	#region Settings

	[MenuItem (PATH + FINDASSETS + "Settings/EditorUserSettings", false, 222)]
	static void FindEditorUserSettings ()
	{
		FindAssetsInMemory (typeof(EditorUserSettings));
	}

	[MenuItem (PATH + FINDASSETS + "Settings/EditorSettings", false, 222)]
	static void FindEditorSettings ()
	{
		FindAssetsInMemory (typeof(EditorSettings));
	}

	[MenuItem (PATH + FINDASSETS + "Settings/PlayerSettings", false, 222)]
	static void FindPlayerSettings ()
	{
		FindAssetsInMemory (typeof(PlayerSettings));
	}

	[MenuItem (PATH + FINDASSETS + "Settings/GraphicsSettings", false, 222)]
	static void FindGraphicsSettings ()
	{
		FindAssetsInMemory (typeof(GraphicsSettings));
	}


	#endregion

	#region Find object of type

	static bool IsAsset (System.Type type)
	{
		return (!type.IsSubclassOf (typeof(UnityEngine.Component))
		&& type.IsSubclassOf (typeof(UnityEngine.Object))
		&& type.Name != "Component"
			//exceptions
		&& type.Name != "LightmapSettings" && type.Name != "RenderSettings"
		);
	}

	static void FindAssetsInMemory (System.Type typAssetu)
	{
		#region Lists of elements
		List<UnityEngine.Object> sceneMemoryInProfiler = new List<UnityEngine.Object> ();
		List<UnityEngine.Object> assetsInProfilerInAssetFolder = new List<UnityEngine.Object> ();
		List<UnityEngine.Object> notSavedInProfiler = new List<UnityEngine.Object> ();
		List<UnityEngine.Object> assetsInProfilerInUnityEditorResources = new List<UnityEngine.Object> ();
		List<UnityEngine.Object> builtinResourcesInProfiler = new List<UnityEngine.Object> ();
		List<UnityEngine.Object> builtinResourcesInProfilerDefault = new List<UnityEngine.Object> ();

		#endregion
		UnityEngine.Object[] foundAssets = Resources.FindObjectsOfTypeAll (typAssetu);
		Debug.Log ("<b><size=" + consoleFontSize + "> <color=#000000FF>All objects loaded to memory, typeof  </color><color=#CD1426>" + typAssetu.ToString ().Substring (12)
		+ "</color><color=#000000FF>: " + foundAssets.Length + "  </color> </size></b>");

		Debug.Log ("");

		#region Filtrowanie i sortowanie

		foreach (var item in foundAssets) {
			string pathToAsset = UnityEditor.AssetDatabase.GetAssetPath (item);
            if (item.hideFlags == HideFlags.None && pathToAsset.Equals(""))
                sceneMemoryInProfiler.Add(item);
            else if (item.hideFlags == HideFlags.None && !pathToAsset.Equals ("") || pathToAsset.StartsWith ("Assets/") || item.hideFlags == HideFlags.None && IsAsset (item.GetType ()))
				assetsInProfilerInAssetFolder.Add (item);
			

			else if (item.hideFlags == HideFlags.NotEditable)//&& pathToAsset.StartsWith ("Resources/")
				assetsInProfilerInUnityEditorResources.Add (item);
			else if ((item.hideFlags & HideFlags.HideInInspector) != HideFlags.HideInInspector && pathToAsset.Equals (""))
				notSavedInProfiler.Add (item);
			else if (((item.hideFlags & (HideFlags.HideInInspector | HideFlags.DontSave)) == (HideFlags.HideInInspector | HideFlags.DontSave)) && pathToAsset.StartsWith ("Library/") && pathToAsset.Contains ("default"))
				builtinResourcesInProfilerDefault.Add (item);
			else if (((item.hideFlags & (HideFlags.HideInInspector | HideFlags.DontSave)) == (HideFlags.HideInInspector | HideFlags.DontSave)) && pathToAsset.StartsWith ("Library/"))
				builtinResourcesInProfiler.Add (item);
			else if (item.hideFlags == (HideFlags.HideAndDontSave) && pathToAsset.StartsWith ("Library/")) // dla monoscript
				builtinResourcesInProfiler.Add (item);
		}

		SortByTypeName (ref sceneMemoryInProfiler);
		SortByTypeName (ref assetsInProfilerInAssetFolder);
		SortByTypeName (ref notSavedInProfiler);
		SortByTypeName (ref assetsInProfilerInUnityEditorResources);
		SortByTypeName (ref builtinResourcesInProfiler);
		SortByTypeName (ref builtinResourcesInProfilerDefault);


		#endregion
		#region Writing to Console
		bool areAllObjects = (typAssetu == typeof(UnityEngine.Object) ? true : false);
		PutListOfObjectsOfTypeToConsole (sceneMemoryInProfiler, "Scene Memory - gameObjecty i komponenty w wczytanej scenie: ", areAllObjects);
		PutListOfObjectsOfTypeToConsole (assetsInProfilerInAssetFolder, "Assets - assety znajdujące się w folderze Assets: ", areAllObjects);
		PutListOfObjectsOfTypeToConsole (assetsInProfilerInUnityEditorResources, "Assets - znajdujące w folderze Resources w edytorze i możliwe do wykorzystania w scenie: ", areAllObjects);
		PutListOfObjectsOfTypeToConsole (builtinResourcesInProfilerDefault, "Builtin Resources (możliwe do wykorzystywane w Runtime): ", areAllObjects);
		PutListOfObjectsOfTypeToConsole (builtinResourcesInProfiler, "Builtin Resources (wykorzystywane do budowy edytora): ", areAllObjects);
		PutListOfObjectsOfTypeToConsole (notSavedInProfiler, "Not Saved (wykorzystywane do budowy edytora): ", areAllObjects);
		#endregion
	}

	static void SortByTypeName (ref List<UnityEngine.Object> nameOflist)
	{
		nameOflist = nameOflist.OrderBy (o => o.name).ToList ();		
		nameOflist = nameOflist.OrderBy (o => o.GetType ().Name).ToList ();
	}

	static void PutListOfObjectsOfTypeToConsole (List<UnityEngine.Object> nameOflist, string opis, bool addTypeInfo = default(bool))
	{
		if (nameOflist.Count > 0) {			
//			PrintUt.Print (opis + nazwaListy.Count, ColorManager.DarkSlateBlue, true, 15);
			Debug.Log ("<b><size=" + consoleFontSize + "> <color=#483D8BFF>" + opis + nameOflist.Count + "</color></size></b>");
			Debug.Log ("");
			foreach (var item in nameOflist) {
				WriteFullInfoAboutElementToConsole (item, addTypeInfo);
			}
			Debug.Log ("");
		}
	}


	static void WriteFullInfoAboutElementToConsole (UnityEngine.Object item, bool addTypeInfo = default(bool))
	{
		bool hasName = item.name == "";
		string typeName = item.GetType ().Name;
		string typeInfo = addTypeInfo && !hasName ? "<color=#878900> Type: </color>" + typeName + "   " : "";
		string pathToAsset = (UnityEditor.AssetDatabase.GetAssetPath (item));
		string colorPlusItemName = (hasName && addTypeInfo) || AreAddTypeInfo (item) ? @"<color=#878900> Type:</color><color=#000000>  " + item.GetType ().Name : @"<color=#CD1426FF> " + item.name;

		pathToAsset = (pathToAsset == "" ? " Exist in memory.   " : pathToAsset);

		if (addTypeInfo && IsNotInDefiniedCategoryOfTypes (item, typeName))
			return;

		Debug.Log ("<b><size=" + consoleFontSize + ">" + colorPlusItemName + "</color><color=#0392CF> HIDEFLAGS: " + item.hideFlags + "  </color> " +
		typeInfo +
		"  <color=#0AA374>PATH: " + pathToAsset + "</color>" +
		"</size></b>", item);
		//  <color=#F37736>MEMORY: " + UnityEngine.Profiling.Profiler.GetRuntimeMemorySize (item) + "</color><color=#0392CF>B</color>
	}

	static bool AreAddTypeInfo (UnityEngine.Object item)
	{
		return item.name.Length == 0 && item.GetType ().IsSubclassOf (typeof(ScriptableObject));
	}

	static bool IsNotInDefiniedCategoryOfTypes (UnityEngine.Object item, string typeName)
	{

		return (typeName == "PluginImporter" || typeName == "MonoImporter" || typeName == "MonoScript" || typeName == "Shader" || typeName == "AssetImporter" || typeName == "Mesh"
		|| item.GetType () == (typeof(GameObject)) || item.GetType ().IsSubclassOf (typeof(Component)) || item.GetType ().IsSubclassOf (typeof(Texture)) || item.GetType ().IsSubclassOf (typeof(AssetImporter))
		|| item.GetType () == (typeof(Sprite)) || item.GetType () == (typeof(SpriteAtlas)) || item.GetType () == (typeof(AssetBundle))
		|| item.GetType () == (typeof(DefaultAsset))
		|| item.GetType ().IsSubclassOf (typeof(ScriptableObject)) || item.GetType () == (typeof(Material)) || item.GetType ().IsSubclassOf (typeof(GUISkin))
		|| item.GetType () == (typeof(Font)) 
		);
		
	}

	#endregion

	#region Remove empty folders

	//http://www.tallior.com/remove-empty-folders-in-unity/ //	https://gist.github.com/liortal53/780075ddb17f9306ae32
	/// <summary> /// Use this flag to simulate a run, before really deleting any folders. /// </summary>
	private static bool dryRun = false;

	[MenuItem ("Mein Menu/Folders/Remove empty folders", false, 56)]
	private static void RemoveEmptyFoldersMenuItem ()
	{
		var index = Application.dataPath.IndexOf ("/Assets");
		var projectSubfolders = Directory.GetDirectories (Application.dataPath, "*", SearchOption.AllDirectories);
		// Create a list of all the empty subfolders under Assets.
		var emptyFolders = projectSubfolders.Where (path => IsEmptyRecursive (path)).ToArray ();
		foreach (var folder in emptyFolders) {
			// Verify that the folder exists (may have been already removed).
			if (Directory.Exists (folder)) {
				Debug.Log ("<color=#CD1426FF>" + "Deleting : " + folder + "</color>");
				if (!dryRun) {
					// Remove dir (recursively)
					Directory.Delete (folder, true);
					// Sync AssetDatabase with the delete operation.
					AssetDatabase.DeleteAsset (folder.Substring (index + 1));
				}
			}
		}
		// Refresh the asset database once we're done.
		AssetDatabase.Refresh ();
	}

	/// <summary> /// A helper method for determining if a folder is empty or not. /// </summary>
	private static bool IsEmptyRecursive (string path)
	{
		// A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
		return Directory.GetFiles (path).Where (file => !file.EndsWith (".meta")).Count () == 0
		&& Directory.GetDirectories (path, "*", SearchOption.AllDirectories).All (IsEmptyRecursive);
	}

	#endregion

	#region Opening folders


	[MenuItem (PATH + "Folders/Open Assets Folder", false, -10)]
	static void OpenFolder3 ()
	{
		OpenFolder (Application.dataPath);
	}

	[MenuItem (PATH + "Folders/Open Persistent Cache", false, -9)]
	static void OpenFolderPersistentDataPath ()
	{
		OpenFolder (Application.persistentDataPath);
	}

	[MenuItem (PATH + "Folders/Open Temporary Cache", false, -8)]
	static void OpenFolderTemporaryCachePath ()
	{
		OpenFolder (Application.temporaryCachePath);
	}


	[MenuItem (PATH + "Folders/Open ScriptTemplates", false, 12)]
	static void OpenFolder2 ()
	{
		string path = "/Resources/ScriptTemplates";
		OpenFolder (EditorApplication.applicationContentsPath + path);
	}

	[MenuItem (PATH + "Folders/Open Standard Assets", false, 13)]
	static void OpenFolder22 ()
	{
		string path = EditorApplication.applicationContentsPath.Replace (@"Data", @"") + "/Standard Assets";

		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
		}
		OpenFolder (path);
	}


	[MenuItem (PATH + "Folders/Open ScriptAssemblies", false, 14)]
	static void OpenScriptAssemblies ()
	{
		string path1 = Application.dataPath.Replace (@"Assets", @"") + "/Library/ScriptAssemblies";
		OpenFolder (path1);
	}

	[MenuItem (PATH + "Folders/Open Library", false, 15)]
	static void OpenLibrary ()
	{
		string path1 = Application.dataPath.Replace (@"Assets", @"") + "/Library";
		OpenFolder (path1);
	}

	[MenuItem (PATH + "Folders/Project Settings", false, 15)]
	static void OpenProjectSettings ()
	{
		string path1 = Application.dataPath.Replace (@"Assets", @"") + "/ProjectSettings";
		OpenFolder (path1);
	}

	[MenuItem (PATH + "Folders/Open Build Folder", false, 15)]
	static void OpenBuild ()
	{
		string path = Application.dataPath.Replace (@"Assets", @"") + "/Build";
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
		}
		OpenFolder (path);
	}

	[MenuItem (PATH + "Folders/Open Application Contents Path", false, 31)]
	static void OpenFolderApplicationContentsPath ()
	{
		OpenFolder (EditorApplication.applicationContentsPath);
	}

	[MenuItem (PATH + "Folders/Open Preferences Folder", false, 32)]
	static void OpenFolderPreferencesFolder ()
	{
		OpenFolder (InternalEditorUtility.unityPreferencesFolder);
	}

	[MenuItem (PATH + "Folders/Open UnityExtensions", false, 33)]
	static void OpenFolder21 ()
	{
		string path = "/UnityExtensions/Unity";
		OpenFolder (EditorApplication.applicationContentsPath + path);
	}



	static void OpenFolder (string path)
	{
		if (Directory.Exists (path)) {
			System.Diagnostics.Process.Start (path);
		} else {
			Debug.Log ("Directory don't exist!");
		}
	}

	#endregion


	[System.Diagnostics.Conditional ("UNITY_EDITOR")]
	static void ClearLog ()
	{
		#if UNITY_EDITOR

		var logEntries = System.Type.GetType ("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod ("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke (null, null);
		#endif
	}
}












