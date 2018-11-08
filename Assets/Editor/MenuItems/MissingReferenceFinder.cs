//https://github.com/liortal53/MissingReferencesUnity


//Copyright (c) 2018 Lior Tal
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//	limitations under the License.

// My changes:
// - names of options in menu
// - ignore missing references in UI elements
// - added colors to logs, more info in logs and polish language description



using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class MissingReferencesFinder
{
	private const string MENU_ROOT = "Tools/Show Missing References/";

	static bool areSearchingInAssets = false;
	const string sceneDescription = "Scena: ";
	//"Scene: "
	const string missingScriptDescription = "Brakuje skryptu w ";
	//"Missing script in "
	const string missingReferenceDescription = "Brakuje referencji w";
	// "Missing reference in "
	const string InWhichComponentDescription = "W jakim komponencie?:";
	// "In which component?:"
	const string InWhichPropertyDescription = "W jakiej property?:";
	// "In which property?:"
	const string prefabDescription = "prefabie: ";
	// "prefab:"
	const string gameObjectDescription = "gameObjectie: ";
	// "gameObject:"
	const int priority = 100;

	#region Finder

	#region Invoking from menu

	[MenuItem (MENU_ROOT + "In actual scene", false, priority + 50)]
	public static void FindMissingReferencesInCurrentScene ()
	{
		var objects = GetSceneObjects ();
		FindMissingReferences (EditorApplication.currentScene, objects);
	}

	[MenuItem (MENU_ROOT + "In all scenes ( added in Build Settings)", false, priority + 51)]
	public static void MissingReferencesInAllScenes ()
	{
		string startingScenePath = SceneManager.GetActiveScene ().path;
		foreach (var scene in EditorBuildSettings.scenes.Where(s => s.enabled)) {
			EditorApplication.OpenScene (scene.path);
			FindMissingReferences (scene.path, GetSceneObjects ());
		}
		EditorSceneManager.OpenScene (startingScenePath);
	}

	[MenuItem (MENU_ROOT + "In Assets Folder", false, priority + 52)]
	public static void MissingSpritesInAssets ()
	{
		areSearchingInAssets = true;
		var allAssets = AssetDatabase.GetAllAssetPaths ();
		var objs = allAssets.Select (a => AssetDatabase.LoadAssetAtPath (a, typeof(GameObject)) as GameObject).Where (a => a != null).ToArray ();

		FindMissingReferences ("Project", objs);
		areSearchingInAssets = false;
	}

	#endregion

	static string PrintActualSceneName (string actualScene)
	{
		if (actualScene != SceneManager.GetActiveScene ().name && !areSearchingInAssets) {
			
			Debug.LogError ("<color=#F37736> <b><size=20> " + sceneDescription + " </size></b> </color>" + "<b><size=18> <color=#7BC043FF>" + SceneManager.GetActiveScene ().name + "</color> </size></b>");
			actualScene = SceneManager.GetActiveScene ().name;
		}
		return actualScene;
	}

	static bool DontDisplayUIReferences (MonoBehaviour mb)
	{
		if (mb != null) {
			Type t = mb.GetType ();
			return (t.Namespace == "UnityEngine.EventSystems" || t.Namespace == "UnityEngine.UI") ? true : false;		
		}
		return false;
		
	}

	private static void FindMissingReferences (string context, GameObject[] objects)
	{
		string kindOfSearchedItem = areSearchingInAssets ? prefabDescription : gameObjectDescription;
		string actualScene = "";

		foreach (var go in objects) {
			var components = go.GetComponents<MonoBehaviour> ();// w jakigo typu elementach szukać

			foreach (var mbScript in components) {	
				if (!mbScript) {					
					Debug.LogError ("<color=#EE4035> <b><size=13> " + missingScriptDescription + " " + kindOfSearchedItem + " </size></b> </color>" + "<color=#1957C6><b><size=13>" + FullPath (go) + " </size></b></color>", go);
					continue;
				}
				if (DontDisplayUIReferences (mbScript))
					break;
				SerializedObject so = new SerializedObject (mbScript);
				var sp = so.GetIterator ();

				while (sp.NextVisible (true)) {
					if (sp.propertyType == SerializedPropertyType.ObjectReference) {
						if (sp.objectReferenceValue == null) {
							
							actualScene =	PrintActualSceneName (actualScene);
							Debug.LogError (string.Format ("<color=#EE4035FF> <b><size=13> " + missingReferenceDescription + " " + kindOfSearchedItem + " </size></b>  </color>" + "<color=#1957C6><b><size=13> {0} </size></b></color>", FullPath (go)), go);

							//   && sp.objectReferenceInstanceIDValue != 0) {
							ShowError (context, go, mbScript.GetType ().Name, ObjectNames.NicifyVariableName (sp.name));
						}
					}
				}
			}
		}
	}

	private static GameObject[] GetSceneObjects ()
	{
		return Resources.FindObjectsOfTypeAll<GameObject> ()
			.Where (go => string.IsNullOrEmpty (AssetDatabase.GetAssetPath (go))
		&& go.hideFlags == HideFlags.None).ToArray ();
	}

	private const string err = "              <color=#1957C6>  " + InWhichComponentDescription + " </color><color=#F37736>{1} </color><color=#1957C6>  " + InWhichPropertyDescription + " </color><color=#7BC043> {2}</color>";

	private static void ShowError (string context, GameObject go, string c, string property)
	{
		Debug.LogError (string.Format (err, FullPath (go), c, property, context), go);
	}

	private static string FullPath (GameObject go)
	{
		return go.transform.parent == null
			? go.name
				: FullPath (go.transform.parent.gameObject) + "/" + go.name;
	}

	#endregion
}
	

