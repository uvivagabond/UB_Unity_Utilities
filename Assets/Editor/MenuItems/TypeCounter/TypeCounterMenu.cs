using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace UB.TypeCounter
{
	public class TypeCounterMenu
	{
		#region Menu Items

		const string nameInMenu = "Tools/Get info about...";
		const int priority = 123;

		#region XML

		[MenuItem (nameInMenu + "/XML/Renderers", false, priority)]
		private static void ShowCreateRenderersHierarchy ()
		{
			TypeCounter.CreateHierarchy ("Renderers", TypeCounter.IsRendererComponent);
		}

		[MenuItem (nameInMenu + "/XML/Textures", false, priority)]
		private static void ShowCreateTexturesHierarchy ()
		{
			TypeCounter.CreateHierarchy ("Textures", TypeCounter.IsTexture);
		}

		[MenuItem (nameInMenu + "/XML/SO", false, priority)]
		private static void ShowCreateSOHierarchy ()
		{
			TypeCounter.CreateHierarchy ("SO", TypeCounter.IsSO);
		}

		[MenuItem (nameInMenu + "/XML/Motion", false, priority)]
		private static void ShowCreateMotionHierarchy ()
		{
			TypeCounter.CreateHierarchy ("Motion", TypeCounter.IsMotion);
		}

		[MenuItem (nameInMenu + "/XML/Joint2D", false, priority)]
		private static void ShowCreateJoint2DHierarchy ()
		{
			TypeCounter.CreateHierarchy ("Joint2DOr3D", TypeCounter.IsJoint2DOr3D);
		}

		[MenuItem (nameInMenu + "/XML/Collider", false, priority)]
		private static void ShowCreateColliderHierarchy ()
		{
			TypeCounter.CreateHierarchy ("Collider2DOr3D", TypeCounter.IsCollider2DOr3D);
		}

		#endregion

		[MenuItem (nameInMenu + "/Components/All", false, priority)]
		private static void ShowComponentsAll ()
		{
			TypeCounter.GetAllComponents ();
		}

		[MenuItem (nameInMenu + "/All", false, priority)]
		private static void ShowClassesAll ()
		{
			TypeCounter.GetAllClasses ();
		}

		[MenuItem (nameInMenu + "/Classes/YieldInstructions", false, priority)]
		private static void ShowYieldInstructionsAll ()
		{
			TypeCounter.GetYieldInstructions ();
		}

		[MenuItem (nameInMenu + "/Classes/Effectors2D", false, priority)]
		private static void ShowEffectors2DAll ()
		{
			TypeCounter.GetEffectors ();
		}

		[MenuItem (nameInMenu + "/Classes/IEnumerators", false, priority)]
		private static void ShowIEnumeratorsAll ()
		{
			TypeCounter.GetIEnumerators ();
		}

		[MenuItem (nameInMenu + "/Classes/IDisposables", false, priority)]
		private static void ShowIDisposablesAll ()
		{
			TypeCounter.GetIDispose ();
		}

		[MenuItem (nameInMenu + "/Assets/ScriptableObjects", false, priority - 45)]
		private static void ShowScriptableObjects ()
		{
			TypeCounter.GetScriptableObject ();
		}

		[MenuItem (nameInMenu + "/Assets/Importers", false, priority - 45)]
		private static void ShowImporters ()
		{
			TypeCounter.GetImporters ();
		}

		[MenuItem (nameInMenu + "/Assets/All except SO", false, priority - 45)]
		private static void ShowAssetsWithoutSO ()
		{
			TypeCounter.GetAssetsWithoutSO ();
		}

		[MenuItem (nameInMenu + "/Classes/IMGUI", false, priority - 45)]
		private static void ShowIMGUIClasses ()
		{
			TypeCounter.GetIMGUI ();
		}

	

		#endregion

	}
}