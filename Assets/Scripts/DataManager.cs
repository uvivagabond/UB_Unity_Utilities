using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataManager
{
	public static void SaveToJson<T> (string absolutePath, T dataToSave)
	{
		if (!string.IsNullOrEmpty (absolutePath)) {
			string dataAsJson = JsonUtility.ToJson (dataToSave);
			File.WriteAllText (absolutePath, dataAsJson);
		}
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}

	public static T LoadFromJson<T> (string absolutePath)
	{		
		if (!string.IsNullOrEmpty (absolutePath) && File.Exists (absolutePath)) {
			string dataAsJson = File.ReadAllText (absolutePath);
			T datas = JsonUtility.FromJson<T> (dataAsJson);
			return (T)System.Convert.ChangeType (datas, typeof(T));
		}
		Debug.Log ("NO FILE OR EMPTY PATH!");
		return (T)System.Convert.ChangeType (null, typeof(T));
	}

	#region MyRegion


	//	public static T LoadFromJsonOver<T> (string valuesToOverrid, Object toOreride)
	//	{
	////		Debug.Log ("LoadOver");
	////		string filePath = Path.Combine (Application.streamingAssetsPath, relativePath);
	////		if (!string.IsNullOrEmpty (filePath)) {
	////			string dataAsJson = File.ReadAllText (filePath);
	//
	//			//	JsonUtility.FromJsonOverwrite (dataAsJson,);
	//
	//			//	T datas = JsonUtility.FromJson<T> (dataAsJson);
	//			//return (T)System.Convert.ChangeType (datas, typeof(T));
	//		}
	//		return (T)System.Convert.ChangeType (null, typeof(T));
	//	}
	#endregion

	//https://www.codeproject.com/Articles/483055/XML-Serialization-and-Deserialization-Part
	public static T LoadFromXML<T> (string absolutePath)
	{
		if (!string.IsNullOrEmpty (absolutePath) && File.Exists (absolutePath)) {
			using (FileStream file = File.Open (absolutePath, FileMode.Open)) {
				XmlSerializer dataAsXML = new XmlSerializer (typeof(T));
				T datas = (T)dataAsXML.Deserialize (file);
				return datas;
			}
		} else {
			Debug.Log ("NO FILE OR EMPTY PATH!");
			return (T)System.Convert.ChangeType (null, typeof(T));	
		}
	}

	public static void SaveToXML<T> (string absolutePath, T dataToSave)
	{
		using (FileStream file = File.Create (absolutePath)) {
			XmlSerializer t = new XmlSerializer (typeof(T));
			t.Serialize (file, dataToSave);
		}
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}

	public static T LoadFromJsonForWebGL<T> (WWW www)
	{		
		using (TextReader textReader = new StringReader (www.text)) {
			string dataAsJson = textReader.ReadToEnd ();
			T datas = JsonUtility.FromJson<T> (dataAsJson);
			return (T)System.Convert.ChangeType (datas, typeof(T));
		}
		return (T)System.Convert.ChangeType (null, typeof(T));
	}

	public static T LoadFromBinary<T> (string absolutePath)
	{
		if (!string.IsNullOrEmpty (absolutePath) && File.Exists (absolutePath)) {
			using (FileStream file = File.Open (absolutePath, FileMode.Open)) {
				BinaryFormatter bf = new BinaryFormatter ();
				T datas = (T)bf.Deserialize (file);
				return datas;
			}
		} else {
			Debug.Log ("NO FILE OR EMPTY PATH!");
			return (T)System.Convert.ChangeType (null, typeof(T));	
		}
	}

	public static void SaveToBinary<T> (string absolutePath, T dataToSave)
	{
		using (FileStream file = File.Create (absolutePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			bf.Serialize (file, dataToSave);
		}
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}
}





