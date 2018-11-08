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
//- add colors to logs
//- create more categories for attributes

using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public class AtributeCounter
{
	const string nameInMenu = "Tools/Print names of attributes...";
	//List<string> UnityEngine = new List<string> ();
	const int priority = 140;

	[MenuItem (nameInMenu + "/All", false, priority)]
	private static void GetAttributesAll ()
	{
		GetAttributes ();
	}

	[MenuItem (nameInMenu + "/UnityEditor", false, priority + 11)]
	private static void GetAttributesInEditor ()
	{
		GetAttributes ("UnityEditor");
	}

	[MenuItem (nameInMenu + "/mscorlib", false, priority + 30)]
	private static void GetAttributesInmscorlib ()
	{
		GetAttributes ("mscorlib");
	}

	[MenuItem (nameInMenu + "/UnityEngine", false, priority + 12)]
	private static void GetAttributesInUnityEngine ()
	{
		GetAttributes ("UnityEngine.CoreModule");
	}

	[MenuItem (nameInMenu + "/NUnit", false, priority + 29)]
	private static void GetAttributesInNunit ()
	{
		GetAttributes ("nunit.framework");
	}


	private static void GetAttributes (string assemblyName = "All")
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies ();
		string nameOfAssembly;
		foreach (var a in assemblies) {
			var attributes = a.GetExportedTypes ().Where (IncludeType).OrderBy (t => t.FullName).ToArray ();
			nameOfAssembly = a.GetName ().Name;
			int z = 0;
			foreach (var attrib in attributes) {
				z++;
				string color = (z % 2 == 0) ? "7BC043" : "0392CF";
				if (a.GetName ().Name.Contains (assemblyName) || assemblyName == "All") {	
					
					string fullName = attrib.FullName.Contains (nameOfAssembly) ? attrib.FullName.Remove (0, nameOfAssembly.Length + 1) : attrib.FullName;
					//	Debug.Log ("<color=#CD1426FF> " + a.GetName ().Name + " </color> " + "<color=#" + color + "> " + attrib.FullName + " </color> ");
					Debug.Log ("<color=#CD1426FF> " + nameOfAssembly + " </color> " + "<color=#" + color + "> " + fullName + " </color> ");
									
				}
			}		
		}
//		Assembly aaa = Assembly.Load ("UnityEditor");
//		Type[] types = aaa.GetTypes ().OrderBy (t => t.FullName).ToArray ();
//		Debug.Log ("aa" + types.Length);
//
//		foreach (Type type in types) {
//			Debug.Log ("aa: " + type.Name);
//
//			//        if (type.Namespace.Contains("UnityEngineInternal"))
//			{
//				//   if (IsAsset(type) )  
//				//   if (type.IsInterface)      
//
//				//if (type.Name.StartsWith("A"))
//
//				{
//					//	ChangeColor(type);
//					Console.WriteLine (type.Name + "                         ");
//				}
//			}
//		}
	}

	private static bool IncludeType (Type t)
	{
		return t.IsPublic
		&& !t.IsDefined (typeof(ObsoleteAttribute), true)
		&& typeof(Attribute).IsAssignableFrom (t);
	}
}

