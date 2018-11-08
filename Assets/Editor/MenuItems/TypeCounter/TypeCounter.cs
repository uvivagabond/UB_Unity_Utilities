using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.Xml;
using UnityEditorInternal;
using System.IO;
using UnityEngine.Internal;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace UB.TypeCounter
{
	public static class TypeCounter
	{
		

		#region Lists

		static string[] colors = new string[7] {
			"<color=#FF0048F9> ",
			"<color=#7BC043FF>",
			"<color=#1957C6FF>",
			"<color=#B92CFFFF>",
			"<color=#A7A7A7FF>",
			" </color> ",
			"<color=#C762E8FF>",
		};
		static List<string> UnityEngine = new List<string> ();

		static List<string> assets = new 	List<string> ();
		static List<string> importers = new  List<string> ();
		static List<string> so = new  List<string> ();
		static List<string> soClear = new  List<string> ();

		static List<string> soEW = new  List<string> ();
		static List<string> soEWClear = new  List<string> ();

		static List<string> soE = new  List<string> ();
		static List<string> soEClear = new  List<string> ();

		static List<string> notAssetsC = new  List<string> ();
		static List<string> notAssetsCClear = new  List<string> ();

		static List<string> notAssetsCWH = new  List<string> ();

		static List<string> notAssetsS = new  List<string> ();
		static List<string> notAssetsI = new  List<string> ();

		static List<string> yieldInstructions = new  List<string> ();

		static List<string> notAssetsCSettings = new  List<string> ();
		static List<Type> soEWClearT = new  List<Type> ();
		static List<Type> notAssetsCWHClear = new  List<Type> ();

		static void ClearLists ()
		{
			assets.Clear ();
			importers.Clear ();
			assets.Clear ();
			so.Clear ();
			soClear.Clear ();
			soEW.Clear ();
			soEWClear.Clear ();
			notAssetsC.Clear ();
			notAssetsCClear.Clear ();
			notAssetsCWH.Clear ();
			notAssetsS.Clear ();
			notAssetsI.Clear ();
			yieldInstructions.Clear ();
			notAssetsCSettings.Clear ();
			soEWClearT.Clear ();
			notAssetsCWHClear.Clear ();
		}

		#endregion

	
		internal static void GetAllComponents ()
		{
			List<string> components = new List<string> ();
			List<string> AudioModule = new List<string> ();
			List<string> PhysicsModule = new List<string> ();
			List<string> Physics2DModule = new List<string> ();
			List<string> UI = new List<string> ();
			List<string> Renderers = new List<string> ();
			List<string> Tilemap = new List<string> ();

			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();

			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref components, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsNOTAudioAndPhysicsAndUIAndRenderer);
				GetCertainTypes (assembly, ref AudioModule, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsAudioComponent);
				GetCertainTypes (assembly, ref PhysicsModule, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsPhysicsComponent);
				GetCertainTypes (assembly, ref Physics2DModule, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsPhysics2DComponent);
				GetCertainTypes (assembly, ref UI, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsUIBuiltinComponent);
				GetCertainTypes (assembly, ref Renderers, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsRendererComponent);
				GetCertainTypes (assembly, ref Tilemap, assembly.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent, IsTilemapComponent);
			}
			Assembly a = typeof(UnityEngine.UI.Button).Assembly;
			GetCertainTypes (a, ref UI, a.GetName ().Name + "       ", IsClassAndNotAsset, IsComponent);

			GetResults ("Components:", components);
			GetResults ("AudioModule:", AudioModule);
			GetResults ("PhysicsModule:", PhysicsModule);
			GetResults ("Physics2DModule:", Physics2DModule);
			GetResults ("Renderers:", Renderers);
			GetResults ("Tilemap:", Tilemap);
			GetResults ("UI:", UI);

		}

		internal	static void GetAllClasses ()
		{
			List<string> components = new List<string> ();
			List<string> AudioModule = new List<string> ();
			List<string> PhysicsModule = new List<string> ();
			List<string> Physics2DModule = new List<string> ();
			List<string> UI = new List<string> ();
			List<string> Renderers = new List<string> ();
			List<string> Tilemap = new List<string> ();

			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();

			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref components, assembly.GetName ().Name + "       ", IsClassAndNotAsset);

			}
			GetResults ("Components:", components);
		}

		internal static void GetIMGUI ()
		{
			List<string> IMGUIClasses = new List<string> ();
			Assembly a = typeof(GUI).Assembly;
			GetCertainTypes (a, ref IMGUIClasses, a.GetName ().Name, IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsNOTComponent, IsNOTAttibute, IsNOTYieldInstruction);
			GetResults ("IMGUI Classes", IMGUIClasses);
		}

		internal static void GetClasses ()
		{

			List<string> classesRoots = new List<string> ();
			List<string> classes = new List<string> ();
			List<string> components = new List<string> ();
			List<string> atributes = new List<string> ();


			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();

			//foreach (var item in dllsUnityEngine) {			
			//	Assembly assembly = Assembly.Load (item);
			Assembly a = typeof(GUI).Assembly;

			GetCertainTypes (a, ref classesRoots, a.GetName ().Name, IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsNOTComponent, IsNOTAttibute, IsNOTYieldInstruction);
			//GetCertainTypes (a, ref classesRoots, a.GetName ().Name, IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsNOTComponent, IsNOTAttibute, IsEditorGUI);

			//	GetCertainTypes (assembly, ref classes, "Class: ", IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsNOTComponent);
			//	GetCertainTypes (assembly, ref components, "components: ", IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsComponent);
			//GetCertainTypes (assembly, ref atributes, "atributes: ", IsClassAndNotAsset, IsNOTInheriteFromScriptableObject, IsAttibute);

			//	}
			GetResults ("Classes Roots", classesRoots);
			//	GetResults ("Classes", classes);
//		GetResults ("components", components);
			//	GetResults ("atributes", atributes);

		}

		//	else if (IsClassAndNotAsset (type) && !type.BaseType.Name.Contains ("Exception")) {
		//		if (type.BaseType.Name.Contains ("Object") && type.BaseType.Namespace.Contains ("System") && type.Name.Contains ("Importer") && type.Name.Contains ("Settings")) {
		//			notAssetsCSettings.Add (string.Format ("{0}{1}{2}{3}", "Not AssetSSSSett: ", ChangeColor (type), type.Name, colors [5]));
		//		} else if (type.BaseType.Name.Contains ("Object") && type.BaseType.Namespace.Contains ("System")) {
		//			notAssetsC.Add (string.Format ("{0}{1}{2}{3}", "Class: ", ChangeColor (type), type.Name, colors [5]));
		//
		//		} else {
		//			notAssetsCWH.Add (string.Format ("{0}{1}{2}{3}", "Not Asset: ", ChangeColor (type), type.Name, colors [5]));//+ "   " + type.BaseType.Namespace + "." + type.BaseType.Name
		//		}
		//	} else if (IsStructAndNotAsset (type)) {
		//		notAssetsS.Add (string.Format ("{0}{1}{2}{3}", "Struct: ", ChangeColor (type), type.Name, colors [5]));// + type.BaseType.Namespace
		//	} else if (IsInterfaceAndNotAsset (type)) {
		//		notAssetsI.Add (string.Format ("{0}{1}{2}{3}", "Interface: ", ChangeColor (type), type.Name, colors [5]));
		//	}

		#region GetSomething



		internal static void GetAssetsWithoutSO ()
		{
			List<string> assetUnityEditorWithSettings = new List<string> ();
			List<string> assetUnityEditorWithoutSettings = new List<string> ();

			List<string> assetUnityEngineWithSettings = new List<string> ();
			List<string> assetUnityEngineWithoutSettings = new List<string> ();

			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();	
			string[] dllsUnityEditor = GetUnityEditorAssemblysNames ();	

			foreach (var item in dllsUnityEditor) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref assetUnityEditorWithSettings, "Settings: ", IsAsset, IsNOTInheriteFromScriptableObject, IsNOTImporterOrImportSettings, IsSettings);
				GetCertainTypes (assembly, ref assetUnityEditorWithoutSettings, "Asset: ", IsAsset, IsNOTInheriteFromScriptableObject, IsNOTImporterOrImportSettings, IsNOTSettings);
			}	

			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref assetUnityEngineWithSettings, "Settings: ", IsAsset, IsNOTInheriteFromScriptableObject, IsNOTImporterOrImportSettings, IsSettings);
				GetCertainTypes (assembly, ref assetUnityEngineWithoutSettings, "Asset: ", IsAsset, IsNOTInheriteFromScriptableObject, IsNOTImporterOrImportSettings, IsNOTSettings);

			}

			GetResults ("Assets - UnityEngine.dll - (without Scriptable Objects)", assetUnityEngineWithoutSettings);
			GetResults ("Assets - Settings - UnityEngine.dll - (without Scriptable Objects)", assetUnityEngineWithSettings);

			GetResults ("Assets - UnityEditor.dll - (without Scriptable Objects)", assetUnityEditorWithoutSettings);
			GetResults ("Assets - Settings - UnityEditor.dll - (without Scriptable Objects)", assetUnityEditorWithSettings);

		}



		internal static void GetImporters ()
		{
			List<string> importers = new List<string> ();	
			List<string> importSettings = new List<string> ();	

			string[] dllsUnityEditor = GetUnityEditorAssemblysNames ();	

			foreach (var item in dllsUnityEditor) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref importers, IsImporter, "Importer: ");
				GetCertainTypes (assembly, ref importSettings, IsImportSettings, "Import Settings: ");
			}
	
			GetResults ("ASSETS IMPORTERS", importers);
			GetResults ("IMPORT SETTINGS", importSettings);
		}

		internal static void GetScriptableObject ()
		{
		
			List<string> importers = new List<string> ();
			List<string> soEW = new List<string> ();
			List<string> soE = new List<string> ();
			List<string> soUnityEditor = new List<string> ();
			List<string> soUnityEngine = new List<string> ();

			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();	
			string[] dllsUnityEditor = GetUnityEditorAssemblysNames ();	

			foreach (var item in dllsUnityEditor) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref importers, IsImporter, "Importer: ");
				GetCertainTypes (assembly, ref importers, IsImporter, "Importer: ");			
				GetCertainTypes (assembly, ref soEW, "SO: ", IsInheriteFromScriptableObject, IsEditorWindow);			
				GetCertainTypes (assembly, ref soE, "SO: ", IsInheriteFromScriptableObject, IsEditor);	
				GetCertainTypes (assembly, ref soUnityEditor, "SO: ", IsInheriteFromScriptableObject, IsNOTEditor, IsNOTEditorWindow);
			}
			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref soUnityEngine, IsInheriteFromScriptableObject, "SO: ");
			}
			GetResults ("SCRIPTABLEOBJECTS - UnityEngine.dll", soUnityEngine);
			GetResults ("SCRIPTABLEOBJECTS - UnityEditor.dll", soUnityEditor);
			GetResults ("EDITORWINDOWS - types which inherite from EditorWindow", soEW);
			GetResults ("EDITOR - types which inherite from Editor", soE);
		}

		internal static void GetYieldInstructions ()
		{
			List<string> yieldInstructions = new  List<string> ();
			List<string> customYieldInstructions = new List<string> ();
			string[] dlls = GetUnityEngineAssemblysNames ();	

			foreach (var item in dlls) {			
				Assembly a = Assembly.Load (item);
				GetCertainTypes (a, ref yieldInstructions, IsYieldInstruction, "YieldInstructions: ");
				GetCertainTypes (a, ref customYieldInstructions, IsCustomYieldInstruction, "YieldInstructions: ");
			}

			GetResults ("YieldInstruction", yieldInstructions);
			GetResults ("CustomYielInstruction", customYieldInstructions);
		}



		internal static void GetEffectors ()
		{
			List<string> effectors = new  List<string> ();
			string[] dlls = GetUnityEngineAssemblysNames ();	

			foreach (var item in dlls) {			
				Assembly a = Assembly.Load (item);
				GetCertainTypes (a, ref effectors, IsEffectorComponent, "Effectors: ");
			}

			GetResults ("Effectors", effectors);
		}

		static void GetClassesUsingInterface (string nameOfResult, System.Func<Type,bool> condition)
		{
			List<string> interfaceImplementation = new  List<string> ();

			string[] dlls = GetUnityEngineAssemblysNames ();	
			string[] dllsUnityEditor = GetUnityEditorAssemblysNames ();	

			foreach (var item in dlls) {			
				Assembly a = Assembly.Load (item);
				GetCertainTypes (a, ref interfaceImplementation, condition, "UnityEngine: ");
			}
			foreach (var item in dllsUnityEditor) {			
				Assembly assembly = Assembly.Load (item);
				GetCertainTypes (assembly, ref interfaceImplementation, condition, "UnityEditor: ");
			}

			GetResults (nameOfResult, interfaceImplementation);
		}

		internal static void GetIEnumerators ()
		{
			GetClassesUsingInterface ("IEnumerators", IsIEnumerable);
		}

		internal static void GetIDispose ()
		{
			GetClassesUsingInterface ("IDispose", IsIDisposable);
		}

		internal static string[] GetUnityEngineAssemblysNames ()
		{
			return GetAssemblysNames (InternalEditorUtility.GetEngineAssemblyPath ());
		}

		static string[] GetUnityEditorAssemblysNames ()
		{
			return new string[]{ "UnityEditor", "UnityEditor.Graphs" };
		}

		static string[] GetAssemblysNames (string pathToAssembly)
		{
			string path = Path.GetDirectoryName (pathToAssembly);
			List < string > dllsNames = new List<string> ();

			string[] paths = Directory.GetFiles (path);
			foreach (var item in paths) {
				if (Path.GetExtension (item) == ".dll" && !item.Contains ("Experimental")) {
					dllsNames.Add (Path.GetFileNameWithoutExtension (item));			
				}
			}
			return dllsNames.ToArray ();
		}

		static void SortByTypeName (ref List<string> nameOflist)
		{
			nameOflist = nameOflist.OrderBy (o => o).ToList ();
		}

		static void GetCertainTypes (Assembly a, ref List<string> listOfTypesNames, System.Func<Type,bool> firstCondition, string title)
		{
			Type[] types = a.GetTypes ().Where (IncludeType).OrderBy (t => t.FullName).ToArray ();
			foreach (Type type in types) {
				if (firstCondition (type)) {
					listOfTypesNames.Add (string.Format ("{0}{1}{2}{3}", title, ChangeColor (type), type.Name, colors [5]));	
				} 
			}
			SortByTypeName (ref listOfTypesNames);
		}

		static void GetCertainTypes (Assembly a, ref List<string> listOfTypesNames, Func<Type,bool>firstCondition, Func<Type,bool>andSecondCondition, string title)
		{
			Type[] types = a.GetTypes ().Where (IncludeType).OrderBy (t => t.FullName).ToArray ();
			foreach (Type type in types) {
				if (firstCondition (type) && andSecondCondition (type)) {
					listOfTypesNames.Add (string.Format ("{0}{1}{2}{3}", title, ChangeColor (type), type.Name, colors [5]));		
				} 
			}
			SortByTypeName (ref listOfTypesNames);
		}

		static void GetCertainTypes (Assembly a, ref List<string> listOfTypesNames, string title, params Func<Type,bool>[]conditions)
		{
			Type[] types = a.GetTypes ().Where (IncludeType).OrderBy (t => t.FullName).ToArray ();
			foreach (Type type in types) {
				bool condition = true;
				for (int i = 0; i < conditions.Length; i++) {
					condition = condition && conditions [i] (type);
				}
				if (condition) {
					listOfTypesNames.Add (string.Format ("{0}{1}{2}{3}", title, ChangeColor (type), type.Name, colors [5]));		
				} 
			}
			SortByTypeName (ref listOfTypesNames);
		}

		#endregion


		#region IsSomething

	

		internal static bool IsEffectorComponent (Type type)
		{
			return type.IsSubclassOf (typeof(UnityEngine.Effector2D)) || type.Name == "Effector2D";
		}


		internal static bool IsRendererComponent (Type type)
		{
			return type.IsSubclassOf (typeof(UnityEngine.Renderer)) || type.Name == "Renderer";
		}

		internal static bool IsTilemapComponent (Type type)
		{
			string assemblyName = type.Assembly.GetName ().Name;
			return (assemblyName.Contains ("TilemapModule") || assemblyName.Contains ("GridModule"));
		}

		internal static bool IsUIBuiltinComponent (Type type)
		{
			string assemblyName = type.Assembly.GetName ().Name;		
			return (assemblyName.Contains ("UIModule") || type.Name == "RectTransform");
		}

		internal static bool IsNOTAudioAndPhysicsAndUIAndRenderer (Type type)
		{
			return (!IsAudioComponent (type) && !IsPhysicsComponent (type) && !IsPhysics2DComponent (type) && !IsUIBuiltinComponent (type) && !IsRendererComponent (type) && !IsTilemapComponent (type));
		}

		internal static bool IsAudioComponent (Type type)
		{
			string assemblyName = type.Assembly.GetName ().Name;		
			return (assemblyName.Contains ("AudioModule"));
		}

		internal static bool IsPhysicsComponent (Type type)
		{
			string assemblyName = type.Assembly.GetName ().Name;		
			return (assemblyName.Contains ("PhysicsModule") || type.IsSubclassOf (typeof(UnityEngine.Collider)) || type.Name == "Cloth");
		}

		internal static bool IsPhysics2DComponent (Type type)
		{
			string assemblyName = type.Assembly.GetName ().Name;			
			return (assemblyName.Contains ("Physics2DModule") || type.IsSubclassOf (typeof(UnityEngine.Collider2D)));
		}

		internal static bool IsCollider2DOr3D (Type type)
		{
			return (type.IsSubclassOf (typeof(UnityEngine.Component))
			&& type.Name != "Component"

			&& type.IsSubclassOf (typeof(UnityEngine.Collider))
			|| type.IsSubclassOf (typeof(UnityEngine.Collider2D))

			);
		}

		internal static bool IsJoint2DOr3D (Type type)
		{
			return (type.IsSubclassOf (typeof(UnityEngine.Component))
			&& type.Name != "Component"

			&& type.IsSubclassOf (typeof(UnityEngine.Joint2D)) || type.IsSubclassOf (typeof(UnityEngine.Joint))

			);
		}

		internal static bool IsNOTYieldInstruction (Type type)
		{
			return (!IsYieldInstruction (type) && !IsCustomYieldInstruction (type));
		}

		internal static bool IsYieldInstruction (Type type)
		{
			return type.IsPublic && type.IsSubclassOf (typeof(UnityEngine.YieldInstruction));
		}

		internal static bool IsIEnumerator (Type type)
		{
			return type.IsPublic && (type.GetInterface ("IEnumerator") != null); 
		}

		internal static bool IsIEnumerable (Type type)
		{
			return type.IsPublic && (type.GetInterface ("IEnumerable") != null); 
		}

		internal static bool IsIDisposable (Type type)
		{
			return type.IsPublic && (type.GetInterface ("IDisposable") != null); 
		}

		internal static bool IsCustomYieldInstruction (Type type)
		{
			return type.IsPublic && type.IsSubclassOf (typeof(UnityEngine.CustomYieldInstruction));
		}

		internal static bool IncludeType (Type type)
		{
			return type.IsPublic;

			//	&& typeof(Attribute).IsAssignableFrom (t)
			//&& !t.IsDefined (typeof(ObsoleteAttribute), true);XXXXXXX
		}

		internal static bool IncludeTypeFromEditor (Type type)
		{
			return type.IsPublic;

			//	&& typeof(Attribute).IsAssignableFrom (t)
			//	&& !t.IsDefined (typeof(ObsoleteAttribute), true);XXXXXX
		}

		internal  static bool IsAttibute (Type type)
		{
			return typeof(Attribute).IsAssignableFrom (type);
		}

		internal  static bool IsNOTAttibute (Type type)
		{
			return !IsAttibute (type);
		}

		internal  static bool IsClassAndNotAsset (Type type)
		{
			return(!IsAsset (type) && type.IsClass && !type.BaseType.Name.Contains ("Exception"));
		}

		internal static bool IsStructAndNotAsset (Type type)
		{
			return(!IsAsset (type) && type.IsValueType && !type.IsEnum);
		}

		internal static bool IsInterfaceAndNotAsset (Type type)
		{
			return(!IsAsset (type) && type.IsInterface);
		}

		internal static bool IsAsset (Type type)
		{
			return (!type.IsSubclassOf (typeof(UnityEngine.Component))
			&& type.IsSubclassOf (typeof(UnityEngine.Object))
			&& !type.IsSubclassOf (typeof(UnityEditor.EditorWindow))
			&& !type.IsSubclassOf (typeof(UnityEditor.Editor))

			&& type.Name != "Component"
			&& type.Name != "Editor"
			&& type.Name != "EditorWindow"


			//&& type.IsSealed
			// &&!type.IsSubclassOf(typeof(UnityEngine.ScriptableObject))
			);
		}

		internal static bool IsTexture (Type type)
		{
			return IsAsset (type) && type.IsSubclassOf (typeof(UnityEngine.Texture));
		}

		internal static bool IsSO (Type type)
		{
			return IsAsset (type) && type.IsSubclassOf (typeof(UnityEngine.ScriptableObject));
		}

		internal static bool IsMotion (Type type)
		{
			return IsAsset (type) && type.IsSubclassOf (typeof(UnityEngine.Motion));
		}

		internal static bool IsInheriteFromScriptableObject (Type type)
		{
			return (!type.IsSubclassOf (typeof(UnityEngine.Component))
			&& type.IsSubclassOf (typeof(UnityEngine.Object))
			&& type.IsSubclassOf (typeof(UnityEngine.ScriptableObject))
			&& type.Name != "Component"
			&& type.Name != "EditorWindow"
			&& type.Name != "Editor"

			//&& type.IsSealed
			// &&!type.IsSubclassOf(typeof(UnityEngine.ScriptableObject))
			);
		}

		internal static bool IsNOTInheriteFromScriptableObject (Type type)
		{
			return !IsInheriteFromScriptableObject (type);
		}

		internal static bool IsEditorWindow (Type type)
		{
			return (
			    type.IsSubclassOf (typeof(UnityEditor.EditorWindow))
			    || typeof(EditorWindow).Equals (type)
			    // || type.Name == "EditorWindow"
			);
		}

		internal static bool IsNOTEditorWindow (Type type)
		{
			return !IsEditorWindow (type);
		}

		internal static bool IsEditor (Type type)
		{
			return (
			    type.IsSubclassOf (typeof(UnityEditor.Editor))
			    || typeof(Editor).Equals (type)
			    // || type.Name == "Editor"
			);
		}

		internal static bool IsNOTEditor (Type type)
		{
			return !IsEditor (type);
		}

		internal static bool IsComponent (Type type)
		{
			return ((type.IsSubclassOf (typeof(UnityEngine.Component))
			|| type.IsSubclassOf (typeof(UnityEngine.MonoBehaviour)))
			&& type.Name != "Component"
			// &&!type.IsSubclassOf(typeof(UnityEngine.ScriptableObject))
			);
		}

		internal static bool IsNOTComponent (Type type)
		{
			return !IsComponent (type);
		}

		static bool IsImporter (Type type)
		{
			return(IsAsset (type) && type.Name.EndsWith ("Importer"));
		}

		internal static bool IsNOTImporterOrImportSettings (Type type)
		{
			return !(IsImporter (type) || IsImportSettings (type));
		}

		internal static bool IsNOTSettings (Type type)
		{
			return !(type.Name.Contains ("Settings"));
		}

		internal static bool IsSettings (Type type)
		{
			return (type.Name.Contains ("Settings"));
		}

		internal static bool IsNOTExperimental (Type type)
		{
			return !(type.Namespace.Contains ("Experimental"));
		}

		internal static bool IsImportSettings (Type type)
		{
			return ((IsClassAndNotAsset (type) && !type.BaseType.Name.Contains ("Exception"))
			&& (type.BaseType.Name.Contains ("Object")
			&& type.BaseType.Namespace.Contains ("System")
			&& type.Name.Contains ("Importer")
			&& type.Name.Contains ("Settings")));
		}

		static string ChangeColor (Type type)
		{
			if (type.IsDefined (typeof(ObsoleteAttribute), true)) {
				return colors [4];
			} else if (!IsNOTExperimental (type)) {
				return colors [6];
			} else if (type.IsClass) {
				return colors [0];
			} else if (type.IsEnum) {
				return colors [3];
			} else if (type.IsInterface) {
				return colors [2];
			} else if (type.IsValueType) {
				return colors [1];
			} else
				return "aaa";
		}

		static void  ChangeColorInConsoleApp (Type t)
		{
			if (t.IsDefined (typeof(ObsoleteAttribute), true)) {
				Console.ForegroundColor = ConsoleColor.Gray;
				return;
			}
			if (t.IsClass) {
				Console.ForegroundColor = ConsoleColor.Red;
			} else if (t.IsEnum) {
				Console.ForegroundColor = ConsoleColor.Blue;
			} else if (t.IsInterface) {
				Console.ForegroundColor = ConsoleColor.Yellow;
			} else if (t.IsValueType) {
				Console.ForegroundColor = ConsoleColor.Green;
			}
		}

		#endregion

		#region hhh

		internal  static void CreateHierarchy (string name, System.Func<Type,bool> condition)
		{
			List<string> importers = new List<string> ();
			List<string> soEW = new List<string> ();
			List<string> soE = new List<string> ();
			List<string> soUnityEditor = new List<string> ();
			List<string> soUnityEngine = new List<string> ();
//		System.Func<Type,bool> firstCondition = IsTexture;

			#region AAAAA
//		List<Type> s0 = new List<Type> ();
			List<Type> s1 = new List<Type> ();
			List<Type> s2 = new List<Type> ();
			List<Type> s3 = new List<Type> ();
			List<Type> s4 = new List<Type> ();
			List<Type> s5 = new List<Type> ();

			#endregion
			string[] dllsUnityEngine = GetUnityEngineAssemblysNames ();	
			string[] dllsUnityEditor = GetUnityEditorAssemblysNames ();	
			string filename = Application.dataPath + "/" + name + ".xml";

			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);
				Type[] types = assembly.GetTypes ().Where (IncludeType).OrderBy (t => t.FullName).ToArray ();
				foreach (Type type in types) {
					if (condition (type)) {
						if (!s1.Contains (type)) {
							s1.Add (type);	
							Debug.Log (type.Name);
		
						}
					}
				}
				XmlDocument doc = new XmlDocument ();

				XmlNode docNode = doc.CreateXmlDeclaration ("1.0", "UTF-8", null);
				doc.AppendChild (docNode);

				XmlNode ClassHierarchy = doc.CreateElement ("System.Object");
				XmlNode root = doc.AppendChild (ClassHierarchy);
				XmlNode mObject = doc.CreateElement ("Object");
				root.AppendChild (mObject);
//			XmlNode mmObject = doc.CreateElement ("Object");
//			mmObject.AppendChild (mmObject);
				foreach (var itemaa in s1) {
					AddNode (itemaa, ref doc, mObject);//mm
				}
				doc.Save (filename);

			}
			#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh ();
			#endif
		}

		static void AddNode (Type type, ref XmlDocument doc, XmlNode root)
		{
			List<Type> listOfInheritedTypes = new List<Type> ();
			listOfInheritedTypes.Add (type);
			Type temp = type.BaseType;
			listOfInheritedTypes.Add (temp);

			while (temp != typeof(System.Object)) {
				temp = temp.BaseType;
				listOfInheritedTypes.Add (temp);
			}
			listOfInheritedTypes.Reverse ();
			foreach (var item in listOfInheritedTypes) {
				XmlNode element = root.SelectSingleNode ("//" + item.Name);
				if (element == null) {
					XmlNode nowyNode = doc.CreateElement (item.Name);
					XmlNode znalezionyPrzodek = root.SelectSingleNode ("//" + item.BaseType.Name);
					znalezionyPrzodek.AppendChild (nowyNode);
					root = nowyNode;
				}
			}
		}

		static void AddNodesa (XmlDocument doc, XmlNode classHierarchy, string name)
		{
			if (name.Contains ("1")) {
				name = name.Trim (new char[]{ '1', '`' });
			}
			name = name.Trim ();
			XmlNode nameNode = doc.CreateElement (name);
			nameNode.AppendChild (doc.CreateTextNode (name));
			classHierarchy.AppendChild (nameNode);
		}

		static void GetResults (string Name, List<string> listName)
		{			//Debug.Log (string.Format ("{0}{1}{2}{3}", "", ChangeColor (type), type.Name, colors [5]));
			if (listName.Count == 0)
				return;

			Debug.Log ("");
			Debug.Log (Name);
			foreach (var item in listName) {
				Debug.Log (item);
			}
		}

		#endregion

	}
}
