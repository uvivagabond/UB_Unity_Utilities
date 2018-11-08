using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System;

namespace UB.TypeCounter
{
	public class MethodPropertyFieldPrinter : MonoBehaviour
	{
		static bool IsMethodNotFromSystemObject (string sss)
		{
			return (!(sss.Contains ("_") ||
			(sss.Contains ("GetHashCode")) ||
			(sss.Contains ("Equals")) ||
			(sss.Contains ("ToString")) ||
			(sss.Contains ("GetType"))));
		}

		static string ChangeNameOfPrimitiveType (string sss)
		{
			return sss.Replace ("String", "string").Replace ("Single", "float").Replace ("Boolean", "bool").Replace ("Int32", "int").Replace ("Void", "void");
		}

		internal static void GetAllReturningTypes (Type searchedType)
		{
			Debug.Log ("<b> <color=#483D8BFF>" + "All methods, properties and fields in type :</color><color=#CD1426> " + searchedType.Name + "</color><color=#483D8BFF></color></b>");
			LogInfoAboutAllMethodsInClass (searchedType);
			LogInfoAboutAllPropertiesInClass (searchedType);
			LogInfoAboutAllFieldsInClass (searchedType);
		}

		static void LogInfoAboutAllMethodsInClass (Type searchedType)
		{
			List<MethodInfo> methodInfos = searchedType.GetMethods ().ToList ();	
			bool isAnyMethod = false;
			foreach (var mi in methodInfos) {
				if ((IsMethodNotFromSystemObject (mi.Name))) {
					isAnyMethod = true;
				}
			}
			if (methodInfos.Count > 0 && isAnyMethod) {
				Debug.Log ("<b> <color=#483D8BFF>" + "METHODS:</color><color=#CD1426></color><color=#483D8BFF></color></b>");
				Debug.Log (" ");
				foreach (var mi in methodInfos) {
					string bracketColor = "483D8B";
					string paramsNames = CreateParameterLog (mi, bracketColor);
					if (IsCorrectMethodForLog (mi, searchedType)) {
						LogTypesToConsoleMI (mi.ReturnType.Name, mi.Name, paramsNames, bracketColor);
					}
				}
				Debug.Log (" ");
				Quaternion q = Quaternion.identity;

			}
		}

		static bool IsCorrectMethodForLog (MethodInfo mi, Type searchedType)
		{
			return	(IsMethodNotFromSystemObject (mi.Name)
			&& IsDeclarationType (searchedType, mi)
			&& (!TypeFinder.IsObsolete (mi)));
		}

		static bool IsDeclarationType (Type searchedType, MethodInfo declarationType)
		{
			return	searchedType == declarationType.DeclaringType;
		}

		static bool IsDeclarationType (Type searchedType, PropertyInfo declarationType)
		{
			return	searchedType == declarationType.DeclaringType;
		}

		static bool IsDeclarationType (Type searchedType, FieldInfo declarationType)
		{
			return	searchedType == declarationType.DeclaringType;
		}

		static void LogInfoAboutAllPropertiesInClass (Type searchedType)
		{			
			List<PropertyInfo> propertyInfos = searchedType.GetProperties ().ToList ();
			if (propertyInfos.Count > 0) {
				Debug.Log ("<b> <color=#483D8BFF>" + "PROPERTIES:</color><color=#CD1426></color><color=#483D8BFF></color></b>");
				Debug.Log (" ");
				foreach (var pi in propertyInfos) {
					if (!TypeFinder.IsObsolete (pi) && IsDeclarationType (searchedType, pi) && IsMethodNotFromSystemObject (pi.Name)) {
						LogTypesToConsolePI (pi.PropertyType.Name, pi.Name);
					}
				}
				Debug.Log (" ");
			}
		}

		static void LogInfoAboutAllFieldsInClass (Type searchedType)
		{			
			List<FieldInfo> fieldInfos = searchedType.GetFields ().ToList ();
			if (fieldInfos.Count > 0) {
				Debug.Log ("<b> <color=#483D8BFF>" + "FIELDS:</color><color=#CD1426></color><color=#483D8BFF></color></b>");
				Debug.Log (" ");
				foreach (var fi in fieldInfos) {
					if (!TypeFinder.IsObsolete (fi) && IsDeclarationType (searchedType, fi) && IsMethodNotFromSystemObject (fi.Name)) {
						LogTypesToConsoleFI (fi.FieldType.Name, fi.Name);
					}
				}
				Debug.Log (" ");
			}
		}

		static string CreateParameterLog (MethodInfo mi, string bracketColor)
		{			
			string paramsNames = "";			
			string paramColor = "EE4035";
			string paramTypeColor = "4492CF";
			ParameterInfo[] pi = mi.GetParameters ();
			foreach (var param in pi) {
				paramsNames += "<color=#" + paramColor + "> " + param.ParameterType.Name + " </color>"
				+ "<color=#" + paramTypeColor + ">" + param.Name + "</color>"
				+ "<color=#" + bracketColor + "> ,</color>";
			}
			if (pi.Length > 0) {
				paramsNames = paramsNames.Remove (paramsNames.Length - 10);
				paramsNames += "</color>";
			} 
			return paramsNames;
		}

		static void LogTypesToConsoleFI (string returnPropertyTypeName, string propertyName)
		{
			string returnColor = "F37736";//4492CF
			string log = "<b> <color=#" + returnColor + ">        " + returnPropertyTypeName
			             + " </color><color=#0AA374>  " + propertyName
			             + "</color><color=#0AA374> " + " </color></b>";
			Debug.Log (ChangeNameOfPrimitiveType (log));
		}

		static void LogTypesToConsolePI (string returnPropertyTypeName, string propertyName)
		{
			string returnColor = "4492CF";//4492CF
			string log = "<b> <color=#" + returnColor + ">        " + returnPropertyTypeName
			             + " </color><color=#0AA374>  " + propertyName
			             + "</color><color=#0AA374> " + " </color></b>";
			Debug.Log (ChangeNameOfPrimitiveType (log));
		}

		static void LogTypesToConsoleMI (string returnTypeName, string methodName, string paramsNames, string bracketColor)
		{

			string returnColor = "F37736";//4492CF

			string log = "<b> <color=#" + returnColor + ">        " + returnTypeName
			             + " </color><color=#0AA374>  " + methodName
			             + "</color><color=#" + bracketColor + ">  (</color>" + paramsNames + "<color=#" + bracketColor + ">)</color><color=#0AA374> " + " </color></b>";
			Debug.Log (ChangeNameOfPrimitiveType (log));
		}
	}
}
