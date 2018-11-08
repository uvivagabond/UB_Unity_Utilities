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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class RemoveEmptyFolders
{
	const string PATH = "Tools/";
	//http://www.tallior.com/remove-empty-folders-in-unity/ //	https://gist.github.com/liortal53/780075ddb17f9306ae32
	/// <summary> /// Use this flag to simulate a run, before really deleting any folders. /// </summary>
	private static bool dryRun = false;

	/// <summary> /// A helper method for determining if a folder is empty or not. /// </summary>
	private static bool IsEmptyRecursive (string path)
	{
		// A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
		return Directory.GetFiles (path).Where (file => !file.EndsWith (".meta")).Count () == 0
		&& Directory.GetDirectories (path, "*", SearchOption.AllDirectories).All (IsEmptyRecursive);
	}

	[MenuItem (PATH + "Folders/Remove empty folders", false, 56)]
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


}
