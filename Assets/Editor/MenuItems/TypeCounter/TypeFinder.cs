using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;
using System.Linq;

namespace UB.TypeCounter
{
	public class TypeFinder
	{
		#region Variables

		static bool showObsolete = false;

		#endregion

		#region MyRegion

		class TypeBag
		{
			public List<MethodInfo> allMethodsReturningChosenType = new List<MethodInfo> ();
			public List<MethodInfo> allMethodsWithParametersOfType = new List<MethodInfo> ();
			public List<PropertyInfo> allPropertiesReturningChosenType = new List<PropertyInfo> ();
			public List<FieldInfo> allFieldsReturningChosenType = new List<FieldInfo> ();
		}


		internal static void GetMethodsReturningTypeDotNet (Type searchedType)
		{
//			var assemblies = AppDomain.CurrentDomain.GetAssemblies ();
			var assembly = Assembly.GetAssembly (searchedType);
			string[] dllsUnityEngine2 = { assembly.GetName ().Name };

			string[] dllsUnityEngine3 = TypeCounter.GetUnityEngineAssemblysNames ();
			string[] dllsUnityEngine = new string[dllsUnityEngine3.Length + dllsUnityEngine2.Length];
			Array.Copy (dllsUnityEngine2, dllsUnityEngine, dllsUnityEngine2.Length);
			Array.Copy (dllsUnityEngine3, 0, dllsUnityEngine, dllsUnityEngine2.Length, dllsUnityEngine3.Length);

			TypeBag typebag = new TypeBag ();
			GetEverythingUsingChosenType (dllsUnityEngine, searchedType, ref typebag.allMethodsReturningChosenType, ref typebag.allMethodsWithParametersOfType,
				ref typebag.allPropertiesReturningChosenType, ref typebag.allFieldsReturningChosenType);

			PrintAllMethodsNames (typebag.allMethodsReturningChosenType, searchedType);
			PrintAllPropertiesNames (typebag.allPropertiesReturningChosenType, searchedType);
			PrintAllMethodsWithParametersOfType (typebag.allMethodsWithParametersOfType, searchedType);
			PrintAllFieldsNames (typebag.allFieldsReturningChosenType, searchedType);
		}

		internal static void GetMethodsReturningType (Type searchedType)
		{
			string[] dllsUnityEngine = TypeCounter.GetUnityEngineAssemblysNames ();
			TypeBag typebag = new TypeBag ();
			GetEverythingUsingChosenType (dllsUnityEngine, searchedType, ref typebag.allMethodsReturningChosenType, ref typebag.allMethodsWithParametersOfType,
				ref typebag.allPropertiesReturningChosenType, ref typebag.allFieldsReturningChosenType);

			PrintAllMethodsNames (typebag.allMethodsReturningChosenType, searchedType);
			PrintAllPropertiesNames (typebag.allPropertiesReturningChosenType, searchedType);
			PrintAllMethodsWithParametersOfType (typebag.allMethodsWithParametersOfType, searchedType);
			PrintAllFieldsNames (typebag.allFieldsReturningChosenType, searchedType);
		}

		static void GetEverythingUsingChosenType (string[] dllsUnityEngine, Type searchedType, ref List<MethodInfo> allMethodsReturningChosenType, ref List<MethodInfo> allMethodsWithParametersOfType,
		                                          ref List<PropertyInfo> allPropertiesReturningChosenType, ref List<FieldInfo> allFieldsReturningChosenType)
		{
			foreach (var item in dllsUnityEngine) {			
				Assembly assembly = Assembly.Load (item);

				Type[] types = assembly.GetTypes ().OrderBy (t => t.FullName).ToArray ();

				foreach (Type typeInAssembly in types) {
					List<MethodInfo> methodInfos = typeInAssembly.GetMethods ().ToList ();	
					List<PropertyInfo> propertyInfos = typeInAssembly.GetProperties ().ToList ();
					List<FieldInfo> fieldInfos = typeInAssembly.GetFields ().ToList ();

					GetMethods (ref allMethodsReturningChosenType, ref methodInfos, searchedType, typeInAssembly);
					GetProperties (ref allPropertiesReturningChosenType, ref propertyInfos, searchedType, typeInAssembly);
					GetMethodsWithParametersOfType (ref allMethodsWithParametersOfType, ref methodInfos, searchedType, typeInAssembly);
					GetVariables (ref allFieldsReturningChosenType, ref fieldInfos, searchedType, typeInAssembly);
				}
			}
		}

		public static bool IsGenericList (Type oType)
		{
			return (oType.IsGenericType && (oType.GetGenericTypeDefinition () == typeof(List<>)));
		}

		static void GetMethodsWithParametersOfType (ref List<MethodInfo> allMethodsWithParametersOfType, ref List<MethodInfo> methodInfos, Type searchedType, Type typeInAssembly)
		{
			for (int i = 0; i < methodInfos.Count; i++) {
				ParameterInfo[] pi =	methodInfos [i].GetParameters ();
				foreach (var item in pi) {		
					//				bool isList = typeof(IEnumerable).IsAssignableFrom (item.ParameterType);
					if (IsCorrectType (item.ParameterType, searchedType, methodInfos [i]) && methodInfos [i].DeclaringType.IsPublic) {
						allMethodsWithParametersOfType.Add (methodInfos [i]);
					}
				}
			}
		}

		static void GetVariables (ref List<FieldInfo> allFieldsReturningChosenType, ref List<FieldInfo> fieldInfos, Type searchedType, Type typeInAssembly)
		{
			for (int i = 0; i < fieldInfos.Count; i++) {
				Type t = fieldInfos [i].FieldType;
				bool isArray = IsArrayOfType (t, searchedType);
				bool isList = IsListOfSeachedType (t, searchedType);
				if ((t == searchedType || isArray || isList) && fieldInfos [i].IsPublic && fieldInfos [i].DeclaringType.IsPublic && (!fieldInfos [i].Name.Contains ("_"))) {
					allFieldsReturningChosenType.Add (fieldInfos [i]);
				}
			}
		}

		static void GetMethods (ref List<MethodInfo> allMethodsReturningChosenType, ref List<MethodInfo> methodInfos, Type searchedType, Type typeInAssembly)
		{
			for (int i = 0; i < methodInfos.Count; i++) {
				Type t = methodInfos [i].ReturnType;
				if (IsCorrectType (t, searchedType, methodInfos [i]) && methodInfos [i].DeclaringType.IsPublic) {
					allMethodsReturningChosenType.Add (methodInfos [i]);
				}
			}
		}

		static bool IsCorrectType (Type typeOfParam, Type searchedType, MethodInfo mi)
		{
			bool isArray = IsArrayOfType (typeOfParam, searchedType);
			bool isList = IsListOfSeachedType (typeOfParam, searchedType);
			return (typeOfParam == searchedType || isArray || isList) && mi.IsPublic && (!mi.Name.Contains ("_"));//
		}

		static bool IsListOfSeachedType (Type paramInMethod, Type searchedType)
		{
			bool isListOfType = IsGenericList (paramInMethod) && !paramInMethod.IsArray && paramInMethod.GetGenericArguments () [0] == searchedType;
			return isListOfType;
		}

		static bool IsArrayOfType (Type paramInMethod, Type searchedType)
		{
			bool isArray = paramInMethod.IsArray && paramInMethod.ToString ().Equals (searchedType.ToString () + "[]");//
			return isArray;
		}

		static void GetProperties (ref List<PropertyInfo> allPropertiesReturningChosenType, ref List<PropertyInfo> propertyInfos, Type searchedType, Type typeInAssembly)
		{
			for (int i = 0; i < propertyInfos.Count; i++) {
				Type t = propertyInfos [i].PropertyType;

				bool isArray = IsArrayOfType (t, searchedType);
				bool isList = IsListOfSeachedType (t, searchedType);
				if ((t == searchedType || isArray || isList) && propertyInfos [i].DeclaringType.IsPublic && (!propertyInfos [i].Name.Contains ("_"))) {
					allPropertiesReturningChosenType.Add (propertyInfos [i]);
				}
			}
		}

		static void PrintAllMethodsWithParametersOfType (List<MethodInfo> allMethodsWithParametersOfType, Type searchedType)
		{
			if (allMethodsWithParametersOfType.Count > 0) {
				Debug.Log ("                         ");
				Debug.Log ("<b> <color=#483D8BFF>" + "Methods in which we use type </color><color=#CD1426>" + searchedType.Name + "</color><color=#483D8BFF> as parameter </color></b>");
				Debug.Log ("                                "); 
			}
			PrintAllMethodsNamesRaw (allMethodsWithParametersOfType, searchedType);
		}

		static void PrintAllMethodsNamesRaw (List<MethodInfo> allMethodsReturningChosenType, Type searchedType)
		{		
			string color = "0392CF";	
			bool isObsolete = false;
			for (int i = 0; i < allMethodsReturningChosenType.Count; i++) {
				color = allMethodsReturningChosenType [i].IsVirtual ? "3387D0" : "0392CF";
				string type = allMethodsReturningChosenType [i].GetType ().IsClass ? "class" : "structure";	

				isObsolete = IsObsolete (allMethodsReturningChosenType [i]);
				color = (isObsolete) ? "7F7F7F" : "0392CF";
				if (!isObsolete || showObsolete) {	
					string generic = allMethodsReturningChosenType [i].IsGenericMethodDefinition ? "<T>" : "";
					Debug.Log ("<b> <color=#" + color + ">        " + allMethodsReturningChosenType [i].Name
					+ generic + "() </color><color=#0AA374>  from" + "</color><color=#EE4035>  " + allMethodsReturningChosenType [i].DeclaringType.Name + "</color><color=#0AA374> " + type + " </color></b>");
				}
				isObsolete = false;
			}
		}

		internal static bool IsObsolete (MethodInfo method)
		{
			bool isObsolete = false;			
			object[] attributes = method.GetCustomAttributes (false);
			foreach (var item in attributes) {
				isObsolete = (isObsolete || item is ObsoleteAttribute) ? true : false;
			}
			return isObsolete;
		}

		internal static bool IsObsolete (FieldInfo method)
		{
			bool isObsolete = false;			
			object[] attributes = method.GetCustomAttributes (false);
			foreach (var item in attributes) {
				isObsolete = (isObsolete || item is ObsoleteAttribute) ? true : false;
			}
			return isObsolete;
		}

		internal static bool IsObsolete (PropertyInfo method)
		{
			bool isObsolete = false;			
			object[] attributes = method.GetCustomAttributes (false);
			foreach (var item in attributes) {
				isObsolete = (isObsolete || item is ObsoleteAttribute) ? true : false;
			}
			return isObsolete;
		}

		static void PrintAllMethodsNames (List<MethodInfo> allMethodsReturningChosenType, Type searchedType)
		{		
			if (allMethodsReturningChosenType.Count > 0) {
				Debug.Log (" ");		
				Debug.Log ("<b> <color=#483D8BFF>" + "Methods returning type  </color><color=#CD1426>" + searchedType.Name + "</color></b>");
				Debug.Log (" ");
			}
			PrintAllMethodsNamesRaw (allMethodsReturningChosenType, searchedType);
		}

		static void PrintAllFieldsNames (List<FieldInfo> allFieldsReturningChosenType, Type searchedType)
		{		
			if (allFieldsReturningChosenType.Count > 0) {	
				Debug.Log ("                                                                   ");
				Debug.Log ("<b> <color=#483D8BFF>" + "Fields returning type  </color><color=#CD1426>" + searchedType.Name + "</color></b>");
				Debug.Log ("                                                                     ");
			}
			for (int i = 0; i < allFieldsReturningChosenType.Count; i++) {
				string type = allFieldsReturningChosenType [i].GetType ().IsClass ? "class" : "structure";		
				string color = "0392CF";	
				bool isObsolete = false;
				isObsolete = IsObsolete (allFieldsReturningChosenType [i]);

				color = (isObsolete) ? "7F7F7F" : "0392CF";

				if (!isObsolete || showObsolete) {					
					Debug.Log ("<b> <color=#" + color + ">        " + allFieldsReturningChosenType [i].Name
					+ " </color><color=#0AA374>  from" + "</color><color=#EE4035>  " + allFieldsReturningChosenType [i].DeclaringType.Name + "</color><color=#0AA374> " + type + " </color></b>");
				}
			}
		}

		static void PrintAllPropertiesNames (List<PropertyInfo> allPropertiesReturningChosenType, Type searchedType)
		{
			if (allPropertiesReturningChosenType.Count > 0) {
				Debug.Log ("       ");
				Debug.Log ("<b> <color=#483D8BFF>" + "Properties returning type  </color><color=#CD1426>" + searchedType.Name + "</color></b>");
				Debug.Log ("           ");
			}

			for (int i = 0; i < allPropertiesReturningChosenType.Count; i++) {
				string type = allPropertiesReturningChosenType [i].GetType ().IsClass ? "class" : "structure";		
				string color = "0392CF";	
				bool isObsolete = false;
				isObsolete = IsObsolete (allPropertiesReturningChosenType [i]);
				color = (isObsolete) ? "7F7F7F" : "0392CF";

				if (!isObsolete || showObsolete) {					
					Debug.Log ("<b> <color=#" + color + ">        " + allPropertiesReturningChosenType [i].Name
					+ " </color><color=#0AA374>  from" + "</color><color=#EE4035>  " + allPropertiesReturningChosenType [i].DeclaringType.Name + "</color><color=#0AA374> " + type + " </color></b>");
				}
			}
		}

		#endregion
	}
}