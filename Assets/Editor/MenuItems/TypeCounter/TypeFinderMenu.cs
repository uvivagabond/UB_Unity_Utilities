using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System.Reflection;

namespace UB.TypeCounter
{
	public class TypeFinderMenu
	{
		const string whereweUseType = "Tools/Where we use type?/";
		const int priority = 123;

		#region Physics3D

		[MenuItem (whereweUseType + "Physics3D/Collider", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseColliderType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Collider));
		}

		#endregion

		#region AB

		[MenuItem (whereweUseType + "AB/AssetBundle", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAssetBundleType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AssetBundle));
		}

		[MenuItem (whereweUseType + "AB/AssetBundleCreateRequest", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAssetBundleCreateRequestType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AssetBundleCreateRequest));
		}

		[MenuItem (whereweUseType + "AB/AssetBundleRequest", false, priority + 11)]
		private static void GetMethodsAndPropertiesWhichUseAssetBundleRequestType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AssetBundleRequest));
		}

		[MenuItem (whereweUseType + "AB/CachedAssetBundle", false, priority + 11)]
		private static void GetMethodsAndPropertiesWhichUseCachedAssetBundleType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(CachedAssetBundle));
		}

		[MenuItem (whereweUseType + "AB/Hash128", false, priority + 11)]
		private static void GetMethodsAndPropertiesWhichUseHash128Type ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Hash128));
		}



		[MenuItem (whereweUseType + "AB/DownloadHandlerAssetBundle", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseDownloadHandlerAssetBundleType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(DownloadHandlerAssetBundle));
		}

		#endregion

		#region Other

		[MenuItem (whereweUseType + "Other/IEnumerator", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseIEnumeratorType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(IEnumerator));
		}

		[MenuItem (whereweUseType + "/Other/Matrix4x4", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseMatrix4x4Type ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Matrix4x4));
		}

		[MenuItem (whereweUseType + "Other/Transform", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseRigidbodyType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Transform));
		}

	

		[MenuItem (whereweUseType + "Other/Component", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseComponentType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Component));
		}

		[MenuItem (whereweUseType + "Other/Scene", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseSceneType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Scene));
		}

		[MenuItem (whereweUseType + "Other/GameObject", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseGameObjectType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(GameObject));
		}

		[MenuItem (whereweUseType + "Other/Quaternion", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseQuaternionType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Quaternion));
		}

		[MenuItem (whereweUseType + "Other/Material", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseMaterialType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Material));
		}

		[MenuItem (whereweUseType + "Other/Plane", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUsePlaneType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Plane));
		}

		[MenuItem (whereweUseType + "Other/ScriptableObject", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseScriptableObjectType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(ScriptableObject));
		}

		#endregion

		#region Animation

		[MenuItem (whereweUseType + "Animation/AnimatorClipInfo", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorClipInfoType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimatorClipInfo));
		}

		[MenuItem (whereweUseType + "Animation/AnimatorController", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorControllerInfoType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(UnityEditor.Animations.AnimatorController));
		}

		[MenuItem (whereweUseType + "Animation/AnimatorControllerParameter", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorControllerParameterType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimatorControllerParameter));
		}

		[MenuItem (whereweUseType + "Animation/RuntimeAnimatorController", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseRuntimeAnimatorControllerType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(RuntimeAnimatorController));
		}

		[MenuItem (whereweUseType + "Animation/AnimatorStateInfo", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorStateInfoType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimatorStateInfo));
		}

		[MenuItem (whereweUseType + "Animation/AnimatorTransitionInfo", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorTransitionInfoType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimatorTransitionInfo));
		}

	
		[MenuItem (whereweUseType + "Animation/AnimationState", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimationStateType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimationState));
		}

		[MenuItem (whereweUseType + "Animation/Animator", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimatorType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Animator));
		}


		[MenuItem (whereweUseType + "Animation/Bones/HumanBodyBones", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseHumanBodyBonesType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(HumanBodyBones));
		}

		[MenuItem (whereweUseType + "Animation/AnimationCurve", false, priority)]
		private static void GetMethodsAndPropertiesWhichUseAnimationCurveType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AnimationCurve));
		}

		[MenuItem (whereweUseType + "Animation/Bones/SkeletonBone", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseSkeletonBoneType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(SkeletonBone));
		}

		[MenuItem (whereweUseType + "Animation/Bones/HumanBone", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseHumanBoneType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(HumanBone));
		}

		[MenuItem (whereweUseType + "Animation/Bones/HumanLimit", false, priority + 100)]
		private static void GetMethodsAndPropertiesWhichUseHumanLimitype ()
		{
			TypeFinder.GetMethodsReturningType (typeof(HumanLimit));
		}

		//		[MenuItem (whereweUseType + "Animation/Bones/HumanTrait", false, 200)]
		//		private static void GetMethodsAndPropertiesWhichUseHumanTrait ()
		//		{
		//			TypeFinder.GetMethodsReturningType (typeof(HumanTrait));
		//		}

		[MenuItem (whereweUseType + "Animation/Avatar/AvatarTarget", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseAvatarTargetType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AvatarTarget));
		}

		[MenuItem (whereweUseType + "Animation/Avatar/Avatar", false, priority + 21)]
		private static void GetMethodsAndPropertiesWhichUseAvatarType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(Avatar));
		}

		[MenuItem (whereweUseType + "Animation/Avatar/AvatarIKGoal", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseAvatarIKGoalType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AvatarIKGoal));
		}

		[MenuItem (whereweUseType + "Animation/Avatar/AvatarIKHint", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseAvatarIKHintType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AvatarIKHint));
		}

		[MenuItem (whereweUseType + "Animation/Avatar/AvatarMask", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseAvatarMaskType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AvatarMask));
		}

		[MenuItem (whereweUseType + "Animation/Avatar/AvatarMaskBodyPart", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseAvatarMaskBodyPartType ()
		{
			TypeFinder.GetMethodsReturningType (typeof(AvatarMaskBodyPart));
		}


		#endregion

		#region dotNet

		[MenuItem (whereweUseType + "dotNet/FileStream", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseFileStreamType ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(FileStream));
		}

		[MenuItem (whereweUseType + "dotNet/StreamWriter", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseStreamWriterType ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(StreamWriter));
		}

		[MenuItem (whereweUseType + "dotNet/StreamReader", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseStreamReaderType ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(StreamReader));
		}

		[MenuItem (whereweUseType + "dotNet/byte[]", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUsebytetabType ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(byte[]));
		}

		[MenuItem (whereweUseType + "dotNet/MethodInfo", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseTypeMethodInfo ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(MethodInfo));
		}

		[MenuItem (whereweUseType + "dotNet/MethodBase", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseTypeMethodBase ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(MethodBase));
		}

		[MenuItem (whereweUseType + "dotNet/MemberInfo", false, priority + 23)]
		private static void GetMethodsAndPropertiesWhichUseTypeMemberInfo ()
		{
			TypeFinder.GetMethodsReturningTypeDotNet (typeof(MemberInfo));
		}

		#endregion

	
	}
}