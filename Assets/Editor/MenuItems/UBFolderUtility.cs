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

public class UBFolderUtility : MonoBehaviour
{
    const string PATH = "Tools/";
  
    #region Remove empty folders

    //http://www.tallior.com/remove-empty-folders-in-unity/ //	https://gist.github.com/liortal53/780075ddb17f9306ae32
    /// <summary> /// Use this flag to simulate a run, before really deleting any folders. /// </summary>
    private static bool dryRun = false;

    [MenuItem(PATH + "Folders/Remove empty folders", false, 56)]
    private static void RemoveEmptyFoldersMenuItem()
    {
        var index = Application.dataPath.IndexOf("/Assets");
        var projectSubfolders = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);
        // Create a list of all the empty subfolders under Assets.
        var emptyFolders = projectSubfolders.Where(path => IsEmptyRecursive(path)).ToArray();
        foreach (var folder in emptyFolders)
        {
            // Verify that the folder exists (may have been already removed).
            if (Directory.Exists(folder))
            {
                Debug.Log("<color=#CD1426FF>" + "Deleting : " + folder + "</color>");
                if (!dryRun)
                {
                    // Remove dir (recursively)
                    Directory.Delete(folder, true);
                    // Sync AssetDatabase with the delete operation.
                    AssetDatabase.DeleteAsset(folder.Substring(index + 1));
                }
            }
        }
        // Refresh the asset database once we're done.
        AssetDatabase.Refresh();
    }

    /// <summary> /// A helper method for determining if a folder is empty or not. /// </summary>
    private static bool IsEmptyRecursive(string path)
    {
        // A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
        return Directory.GetFiles(path).Where(file => !file.EndsWith(".meta")).Count() == 0
        && Directory.GetDirectories(path, "*", SearchOption.AllDirectories).All(IsEmptyRecursive);
    }

    #endregion

    #region Opening folders

    [MenuItem(PATH + "Folders/Open Project Folder", false, -10)]
    static void OpenFolderProject()
    {
        string path1 = Application.dataPath.Replace(@"Assets", @"");
        OpenFolder(path1);
    }  
    [MenuItem(PATH + "Folders/Open Assets Folder", false, -10)]

    static void OpenFolderAssets()
    {
        OpenFolder(Application.dataPath);
    }

    [MenuItem(PATH + "Folders/Open Persistent Cache", false, -9)]
    static void OpenFolderPersistentDataPath()
    {
        OpenFolder(Application.persistentDataPath);
    }

    [MenuItem(PATH + "Folders/Open Temporary Cache", false, -8)]
    static void OpenFolderTemporaryCachePath()
    {
        OpenFolder(Application.temporaryCachePath);
    }


    [MenuItem(PATH + "Folders/Open ScriptTemplates", false, 12)]
    static void OpenFolderScriptTemplates()
    {
        string path = "/Resources/ScriptTemplates";
        OpenFolder(EditorApplication.applicationContentsPath + path);
    }

    [MenuItem(PATH + "Folders/Open Standard Assets", false, 13)]
    static void OpenFolderStandardAssets()
    {
        string path = EditorApplication.applicationContentsPath.Replace(@"Data", @"") + "/Standard Assets";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        OpenFolder(path);
    }


    [MenuItem(PATH + "Folders/Open ScriptAssemblies", false, 14)]
    static void OpenScriptAssemblies()
    {
        string path1 = Application.dataPath.Replace(@"Assets", @"") + "/Library/ScriptAssemblies";
        OpenFolder(path1);
    }

    [MenuItem(PATH + "Folders/Open ProjectTemplates", false, 14)]
    static void OpenFolderProjectTemplates()
    {
        string path = "/Resources/PackageManager/ProjectTemplates";

        OpenFolder(EditorApplication.applicationContentsPath + path);
    }

    [MenuItem(PATH + "Folders/Open Library", false, 15)]
    static void OpenLibrary()
    {
        string path1 = Application.dataPath.Replace(@"Assets", @"") + "/Library";
        OpenFolder(path1);
    }

    [MenuItem(PATH + "Folders/Project Settings", false, 15)]
    static void OpenProjectSettings()
    {
        string path1 = Application.dataPath.Replace(@"Assets", @"") + "/ProjectSettings";
        OpenFolder(path1);
    }

    [MenuItem(PATH + "Folders/Open Build Folder", false, 15)]
    static void OpenBuild()
    {
        string path = Application.dataPath.Replace(@"Assets", @"") + "/Build";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        OpenFolder(path);
    }

    [MenuItem(PATH + "Folders/Open Application Contents Path", false, 31)]
    static void OpenFolderApplicationContentsPath()
    {
        OpenFolder(EditorApplication.applicationContentsPath);
    }

    [MenuItem(PATH + "Folders/Open Preferences Folder", false, 32)]
    static void OpenFolderPreferencesFolder()
    {
        OpenFolder(InternalEditorUtility.unityPreferencesFolder);
    }

    [MenuItem(PATH + "Folders/Open UnityExtensions", false, 33)]
    static void OpenFolderUnityExtensions()
    {
        string path = "/UnityExtensions/Unity";
        OpenFolder(EditorApplication.applicationContentsPath + path);
    }

    [MenuItem(PATH + "Folders/Open Console log directory", false, 44)]
    static void OpenFolderConsoleLogPath()
    {
        // Application.consoleLogPath return .../AppData/Local/Unity/Editor/Editor.log
        OpenFolder(Application.consoleLogPath.Replace("/Editor.log",""));  
    }

    
    static void OpenFolder(string path)
    {
        if (Directory.Exists(path))
        {
            System.Diagnostics.Process.Start(path);
        }
        else
        {
            Debug.Log("Directory don't exist!");
        }
    }

    #endregion

}

