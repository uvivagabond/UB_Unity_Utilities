using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.IO;
using UnityEngine.U2D;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;
using UnityEditor.Animations;
using UnityEngine.Playables;
using System.Xml.Serialization;

namespace UB.TypeCounter
{
	public class MethodPropertyFieldPrinterMenu : MonoBehaviour
	{
		const string whichTypesReturn = "Tools/All methods, properties, fields in/";
		const int priority = 123;

		#region Input

		[MenuItem (whichTypesReturn + "Input/Input", false, priority)]
		private static void GetReturnTypesFromInput ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Input));
		}

		[MenuItem (whichTypesReturn + "Input/Gyroscope", false, priority)]
		private static void GetReturnTypesFromGyroscope ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Gyroscope));
		}

		#endregion

		#region Physics

		[MenuItem (whichTypesReturn + "Physics3D/Rigidbody", false, priority)]
		private static void GetReturnTypesFromRigidbody ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Rigidbody));
		}

		[MenuItem (whichTypesReturn + "Physics2D/Rigidbody2D", false, priority)]
		private static void GetReturnTypesFromRigidbody2D ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Rigidbody2D));
		}

		#endregion

		#region Physics

		[MenuItem (whichTypesReturn + "AB/AssetBundle", false, priority)]
		private static void GetReturnTypesFromAssetBundle ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AssetBundle));
		}

		[MenuItem (whichTypesReturn + "Physics3D/CharacterController", false, priority)]
		private static void GetReturnTypesFromCharacterController ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(CharacterController));
		}

		[MenuItem (whichTypesReturn + "Physics3D/Collision", false, priority)]
		private static void GetReturnTypesFromCollision ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Collision));
		}

		[MenuItem (whichTypesReturn + "Physics3D/ContactPoint", false, priority)]
		private static void GetReturnTypesFromContactPoint ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(ContactPoint));
		}

		[MenuItem (whichTypesReturn + "Physics3D/WheelCollider", false, priority)]
		private static void GetReturnTypesFromWheelCollider ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(WheelCollider));
		}

		[MenuItem (whichTypesReturn + "Physics2D/ContactPoint2D", false, priority)]
		private static void GetReturnTypesFromContactPoint2D ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(ContactPoint2D));
		}

		[MenuItem (whichTypesReturn + "Physics2D/Collision2D", false, priority)]
		private static void GetReturnTypesFromCollision2D ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Collision2D));
		}

		#endregion

		#region IMGUI

		[MenuItem (whichTypesReturn + "IMGUI/GUI", false, priority)]
		private static void GetReturnTypesFromGUI ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(GUI));
		}

		[MenuItem (whichTypesReturn + "Other/Display", false, priority)]
		private static void GetReturnTypesFromDisplay ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Display));
		}

		#endregion

		#region SceneManagement

		[MenuItem (whichTypesReturn + "SceneManagement/SceneManager", false, priority)]
		private static void GetReturnTypesFromSceneManager ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(SceneManager));
		}

		#endregion

		#region IMGUI

		[MenuItem (whichTypesReturn + "Other/Matrix4x4", false, priority)]
		private static void GetReturnTypesFromMatrix4x4 ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Matrix4x4));
		}

		[MenuItem (whichTypesReturn + "Other/Quaternion", false, priority)]
		private static void GetReturnTypesFromQuaternion ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Quaternion));
		}

		[MenuItem (whichTypesReturn + "Other/Plane", false, priority)]
		private static void GetReturnTypesFromPlane ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Plane));
		}

		[MenuItem (whichTypesReturn + "Other/Color", false, priority)]
		private static void GetReturnTypesFromColor ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Color));
		}

		[MenuItem (whichTypesReturn + "Other/Color32", false, priority)]
		private static void GetReturnTypesFromColor32 ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Color32));
		}

		[MenuItem (whichTypesReturn + "Other/ColorUtility", false, priority)]
		private static void GetReturnTypesFromColorUtility ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(ColorUtility));
		}

		[MenuItem (whichTypesReturn + "Other/Resources", false, priority)]
		private static void GetReturnTypesFromResources ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Resources));
		}

		[MenuItem (whichTypesReturn + "Other/Mesh", false, priority)]
		private static void GetReturnTypesFromMesh ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Mesh));
		}

		[MenuItem (whichTypesReturn + "Other/MonoBehaviour", false, priority)]
		private static void GetReturnTypesFromMonoBehaviour ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(MonoBehaviour));
		}

		#endregion

		#region Renderer

		[MenuItem (whichTypesReturn + "Renderer/Renderer", false, priority)]
		private static void GetReturnTypesFromRenderer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Renderer));
		}

		[MenuItem (whichTypesReturn + "Renderer/MeshRenderer", false, priority)]
		private static void GetReturnTypesFromMeshRenderer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(MeshRenderer));
		}

		[MenuItem (whichTypesReturn + "Renderer/SkinnedMeshRenderer", false, priority)]
		private static void GetReturnTypesFromSkinnedMeshRenderer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(SkinnedMeshRenderer));
		}

		[MenuItem (whichTypesReturn + "Renderer/SpriteRenderer", false, priority)]
		private static void GetReturnTypesFromSpriteRenderer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(SpriteRenderer));
		}

		[MenuItem (whichTypesReturn + "Renderer/TilemapRenderer", false, priority)]
		private static void GetReturnTypesFromTilemapRenderer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(TilemapRenderer));
		}

		#endregion

		#region 2D

		[MenuItem (whichTypesReturn + "2D/Sprite", false, 23)]
		private static void GetReturnTypesFromSprite ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Sprite));
			TypeFinder.GetMethodsReturningType (typeof(Sprite));
		}

		[MenuItem (whichTypesReturn + "2D/SpriteAtlas", false, 23)]
		private static void GetReturnTypesFromSpriteAtlas ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(SpriteAtlas));
			TypeFinder.GetMethodsReturningType (typeof(SpriteAtlas));

		}

		[MenuItem (whichTypesReturn + "2D/SpriteMask", false, 23)]
		private static void GetReturnTypesFromSpriteMask ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(SpriteMask));
			TypeFinder.GetMethodsReturningType (typeof(SpriteMask));

		}



		[MenuItem (whichTypesReturn + "2D/GridLayout", false, priority)]
		private static void GetReturnTypesFromGridLayout ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(GridLayout));
			TypeFinder.GetMethodsReturningType (typeof(GridLayout));

		}

		[MenuItem (whichTypesReturn + "2D/Tilemap", false, priority)]
		private static void GetReturnTypesFromTilemap ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Tilemap));
			TypeFinder.GetMethodsReturningType (typeof(Tilemap));

		}

		[MenuItem (whichTypesReturn + "2D/Grid", false, priority)]
		private static void GetReturnTypesFromGrid ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Grid));
			TypeFinder.GetMethodsReturningType (typeof(Grid));

		}

		[MenuItem (whichTypesReturn + "2D/GridBrush", false, 223)]
		private static void GetReturnTypesFromGridBrush ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(GridBrush));
			TypeFinder.GetMethodsReturningType (typeof(GridBrush));

		}

		[MenuItem (whichTypesReturn + "2D/GridBrushBase", false, 223)]
		private static void GetReturnTypesFromGridBrushBase ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(GridBrushBase));
			TypeFinder.GetMethodsReturningType (typeof(GridBrushBase));

		}

		[MenuItem (whichTypesReturn + "2D/TilemapCollider2D", false, 223)]
		private static void GetReturnTypesFromTilemapCollider2D ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(TilemapCollider2D));
			TypeFinder.GetMethodsReturningType (typeof(TilemapCollider2D));

		}

		[MenuItem (whichTypesReturn + "2D/TileAnimationData", false, 253)]
		private static void GetReturnTypesFromTileAnimationData ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(TileAnimationData));
			TypeFinder.GetMethodsReturningType (typeof(TileAnimationData));

		}

		[MenuItem (whichTypesReturn + "2D/TileBase", false, 273)]
		private static void GetReturnTypesFromTileBase ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(TileBase));
			TypeFinder.GetMethodsReturningType (typeof(TileBase));

		}

		[MenuItem (whichTypesReturn + "2D/Tile", false, 273)]
		private static void GetReturnTypesFromTile ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Tile));
			TypeFinder.GetMethodsReturningType (typeof(Tile));

		}

		[MenuItem (whichTypesReturn + "2D/ITilemap", false, 273)]
		private static void GetReturnTypesFromITilemap ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(ITilemap));
			TypeFinder.GetMethodsReturningType (typeof(ITilemap));

		}

		#endregion

		#region Animation

		[MenuItem (whichTypesReturn + "Animation/Animator/Animator", false, priority)]
		private static void GetReturnTypesFromAnimator ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Animator));
		}

		[MenuItem (whichTypesReturn + "Animation/Animator/AnimatorController", false, priority)]
		private static void GetReturnTypesFromAnimatorController ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AnimatorController));
		}

		[MenuItem (whichTypesReturn + "Animation/Animator/RuntimeAnimatorController", false, priority)]
		private static void GetReturnTypesFromRuntimeAnimatorController ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(RuntimeAnimatorController));
		}

		[MenuItem (whichTypesReturn + "Animation/Animator/AnimatorTransitionInfo", false, priority)]
		private static void GetReturnTypesFromAnimatorTransitionInfo ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AnimatorTransitionInfo));
		}

		[MenuItem (whichTypesReturn + "Animation/Animator/AnimatorStateInfo", false, priority)]
		private static void GetReturnTypesFromAnimatorStateInfo ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AnimatorStateInfo));
		}

		[MenuItem (whichTypesReturn + "Animation/Animator/AnimatorControllerParameter", false, priority)]
		private static void GetReturnTypesFromAnimatorControllerParameter ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AnimatorControllerParameter));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/AvatarTarget", false, priority)]
		private static void GetReturnTypesFromAvatarTarget ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AvatarTarget));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/Avatar", false, 121)]
		private static void GetReturnTypesFromAvatar ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Avatar));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/AvatarIKGoal", false, priority)]
		private static void GetReturnTypesFromAvatarIKGoal ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AvatarIKGoal));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/HumanTrait", false, priority)]
		private static void GetReturnTypesFromHumanTrait ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(HumanTrait));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/AvatarIKHint", false, priority)]
		private static void GetReturnTypesFromAvatarIKHint ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AvatarIKHint));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/AvatarMask", false, priority)]
		private static void GetReturnTypesFromAvatarMask ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AvatarMask));
		}

		[MenuItem (whichTypesReturn + "Animation/Avatar/AvatarMaskBodyPart", false, priority)]
		private static void GetReturnTypesFromAvatarMaskBodyPart ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AvatarMaskBodyPart));
		}


		[MenuItem (whichTypesReturn + "Animation/Playables/PlayableGraph", false, priority)]
		private static void GetReturnTypesFromPlayableGraph ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(PlayableGraph));
		}

		[MenuItem (whichTypesReturn + "Animation/AnimationClip", false, priority)]
		private static void GetReturnTypesFromAnimationClip ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AnimationClip));
		}

		[MenuItem (whichTypesReturn + "Audio/AudioSource", false, priority)]
		private static void GetReturnTypesFromAudioSource ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AudioSource));
		}

		[MenuItem (whichTypesReturn + "Audio/AudioClip", false, priority)]
		private static void GetReturnTypesFromAudioClip ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(AudioClip));
		}

		#endregion

		#region dotNet

		[MenuItem (whichTypesReturn + "dotNet/Console", false, priority)]
		private static void GetReturnTypesFromConsole ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(System.Console));
		}

		[MenuItem (whichTypesReturn + "dotNet/File", false, priority)]
		private static void GetReturnTypesFromFile ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(File));
		}

		[MenuItem (whichTypesReturn + "dotNet/FileStream", false, priority)]
		private static void GetReturnTypesFromFileStream ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(FileStream));
		}

		[MenuItem (whichTypesReturn + "dotNet/Stream", false, priority)]
		private static void GetReturnTypesFromStream ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Stream));
		}

		[MenuItem (whichTypesReturn + "dotNet/Path", false, priority)]
		private static void GetReturnTypesFromPath ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Path));
		}

		[MenuItem (whichTypesReturn + "dotNet/Array", false, priority)]
		private static void GetReturnTypesFromArray ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(System.Array));
		}

		[MenuItem (whichTypesReturn + "dotNet/MethodInfo", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeMethodInfo ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(MethodInfo));
		}

		[MenuItem (whichTypesReturn + "dotNet/MethodBase", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeMethodBase ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(MethodBase));
		}

		[MenuItem (whichTypesReturn + "dotNet/MemberInfo", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeMemberInfo ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(MemberInfo));
		}

		[MenuItem (whichTypesReturn + "dotNet/Assembly", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeAssembly ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Assembly));
		}

		[MenuItem (whichTypesReturn + "dotNet/Type", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeType ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Type));
		}

		[MenuItem (whichTypesReturn + "dotNet/Regex", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeRegex ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(Regex));
		}

		[MenuItem (whichTypesReturn + "dotNet/XmlSerializer", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseTypeXmlSerializer ()
		{
			MethodPropertyFieldPrinter.GetAllReturningTypes (typeof(XmlSerializer));
		}

		#endregion

	}
}
