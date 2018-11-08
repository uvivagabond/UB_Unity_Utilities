/* MIT License Copyright (c) 2018 Uvi Vagabond, UnityBerserkers Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public class AddHierarchySeparator
{
	[MenuItem ("GameObject/Add /Separator 1st &s", false, -11100000)]
	static void AddSeparator ()
	{
		UndoAdding ("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
	}

	[MenuItem ("GameObject/Add /Separator 2nd  &%s", false, -11100000)]
	static void AddSeparator2 ()
	{
		UndoAdding ("_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
	}

	[MenuItem ("GameObject/Add /Invisible GO &d", false, -11100000)]
	static void AddInvisible ()
	{
		UndoAdding ("    ");
	}

	[MenuItem ("GameObject/Add /Root &r", false, -11100000)]
	static void AddRoot ()
	{
		UndoAdding ("[]");
	}

	[MenuItem ("GameObject/Add /Mark As Root &%r", false, -11100000)]
	static void MakeRoot ()
	{
		MarkAsRoot ();
	}

	static void MarkAsRoot ()
	{
		GameObject[] gs = Selection.gameObjects;
		if (gs.Length > 0) {			
			string markedGOName = gs [0].name;
			Undo.RecordObject (gs [0], "Mark as Root");
			gs [0].name = "[" + markedGOName + "]";
		} 
	}

	static void UndoAdding (string name)
	{
		GameObject[] gs = Selection.gameObjects;
		if (gs.Length > 0) {			
			foreach (var item in gs) {
				GameObject child = new GameObject (name);
				Undo.RegisterCreatedObjectUndo (child, name);
				child.transform.SetParent (item.transform);
				Selection.activeGameObject = child;
			}
		} else {
			GameObject child = new GameObject (name);
			Undo.RegisterCreatedObjectUndo (child, name);
		}
	}
}
#endif
