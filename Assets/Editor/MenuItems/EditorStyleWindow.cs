using UnityEditor;
using UnityEngine;
using System.Linq;

// based on
//http://baba-s.hatenablog.com/entry/2015/04/26/120413
public sealed class EditorStyleWindow : EditorWindow
{
	string lorem = "   Lorem ipsum dolor sit amet, consectetur adipisicing elit...";
	private const string MENU_ROOT = "WindowX/";

	void OnEnable ()
	{
		mList = new string[] {
			"boldFont",
			"boldLabel",
			"centeredGreyMiniLabel",
			"colorField",
			"foldout",
			"foldoutPreDrop",
			"helpBox",
			"inspectorDefaultMargins",
			"inspectorFullWidthMargins",
			"label",

			"largeLabel",
			"layerMaskField",
			"miniBoldFont",
			"miniBoldLabel",
			"miniButton",
			"miniButtonLeft",
			"miniButtonMid",
			"miniButtonRight",
			"miniFont",
			"miniLabel",

			"miniTextField",
			"numberField",
			"objectField",
			"objectFieldMiniThumb",
			"objectFieldThumb",
			"popup",
			"radioButton",
			"standardFont",
			"textArea",
			"textField",

			"toggle",
			"toggleGroup",
			"toolbar",
			"toolbarButton",
			"toolbarDropDown",
			"toolbarPopup",
			"toolbarTextField",
			"whiteBoldLabel",
			"whiteLabel",
			"whiteLargeLabel",

			"whiteMiniLabel",
			"wordWrappedLabel",
			"wordWrappedMiniLabel"
		};
	}

	[MenuItem (MENU_ROOT + "Get Editor Styles")]
	private static void Example ()
	{
		GetWindow<EditorStyleWindow> (true);
	}

	private static string[] mList;
	//	= {	"AboutWIndowLicenseLabel",
	//		EditorStyles.boldFont.name,
	//		EditorStyles.boldLabel.name,
	//		EditorStyles.centeredGreyMiniLabel.name,
	//		EditorStyles.colorField.name,
	//		EditorStyles.foldout.name,
	//		EditorStyles.foldoutPreDrop.name,
	//		EditorStyles.helpBox.name,
	//		EditorStyles.inspectorDefaultMargins,
	//		EditorStyles.inspectorFullWidthMargins.name,
	//		EditorStyles.label.name,
	//		EditorStyles.largeLabel.name,
	//		EditorStyles.layerMaskField.name,
	//		EditorStyles.miniBoldFont.name,
	//		EditorStyles.miniBoldLabel.name,
	//		EditorStyles.miniButton.name,
	//		EditorStyles.miniButtonLeft.name,
	//		EditorStyles.miniButtonMid.name,
	//		EditorStyles.miniButtonRight.name,
	//		EditorStyles.miniFont.name,
	//		EditorStyles.miniLabel.name,
	//		EditorStyles.miniTextField.name,
	//		EditorStyles.numberField.name,
	//		EditorStyles.objectField.name,
	//		EditorStyles.objectFieldMiniThumb.name,
	//		EditorStyles.objectFieldThumb.name,
	//		EditorStyles.popup.name,
	//		EditorStyles.radioButton.name,
	//		EditorStyles.standardFont.name,
	//		EditorStyles.textArea.name,
	//		EditorStyles.textField.name,
	//		EditorStyles.toggle.name,
	//		EditorStyles.toggleGroup.name,
	//		EditorStyles.toolbar.name,
	//		EditorStyles.toolbarButton.name,
	//		EditorStyles.toolbarDropDown.name,
	//		EditorStyles.toolbarPopup.name,
	//		EditorStyles.toolbarTextField.name,
	//		EditorStyles.whiteBoldLabel.name,
	//		EditorStyles.whiteLabel.name,
	//		EditorStyles.whiteLargeLabel.name,
	//		EditorStyles.whiteMiniLabel.name,
	//		EditorStyles.wordWrappedLabel.name,
	//		EditorStyles.wordWrappedMiniLabel.name
	//	};

	private Vector2 mScrollPos;

	private void OnGUI ()
	{
		DrawTexturesAndFontForAllStyles ();
	}

	void DrawTexturesAndFontForAllStyles ()
	{
		mList = mList.OrderBy (o => o).ToArray ();		

		mScrollPos = EditorGUILayout.BeginScrollView (mScrollPos);
		bool isNotEmpty = mList.Length > 0;
		if (!isNotEmpty)
			return;
		foreach (var n in mList) {
			GUIStyle gs = n;
			EditorGUILayout.BeginHorizontal (GUILayout.Height (48));
			EditorGUILayout.SelectableLabel (n);		
			Texture tn = gs.normal.background;
			Texture th = gs.hover.background;
			Texture ta = gs.active.background;
			Texture tf = gs.focused.background;
			Texture ton = gs.onNormal.background;
			Texture toh = gs.onHover.background;
			Texture toa = gs.onActive.background;
			Texture tof = gs.onFocused.background;
			GUILayout.BeginVertical ();
			DrawTexture (tn, ton, "Normal");
			DrawTexture (th, toh, "Hover");
			DrawTexture (ta, toa, "Active");
			DrawTexture (tf, tof, "Focused");
			DrawLoremIpsum (gs);
			GUILayout.Box ("", GUIStyle.none, GUILayout.Width (100));
			GUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
			GUILayout.Box (
				string.Empty,
				GUILayout.Width (position.width - 24), 
				GUILayout.Height (1) 
			);
		}
		EditorGUILayout.EndScrollView ();
		
	}

	void DrawTexture (Texture t, Texture t2, string text)
	{
		GUIContent gc = new GUIContent ("      " + text, t);
		GUIContent gc2 = new GUIContent ("      " + "On " + text, t2);

		if (t || t2) {
			GUILayout.BeginHorizontal ();
			if (t) {
				GUILayout.Box (gc);
			}
			if (t2) {
				GUILayout.Box (gc2);
			}
			GUILayout.EndHorizontal ();
		} else {
			GUILayout.Box ("", GUIStyle.none, GUILayout.Width (100));			
		}
	}

	void DrawLoremIpsum (GUIStyle gs)
	{
		GUIContent gc = new GUIContent ("      " + lorem);		
		GUILayout.Box (gc, gs);
	}
}