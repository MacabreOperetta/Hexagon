using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(IntReference))]
public class IntReferenceEditor : PropertyDrawer 
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		bool useConstant = property.FindPropertyRelative ("UseConstant").boolValue;
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID(FocusType.Passive), label);

		var rect = new Rect (position.position, Vector2.one * 15);

		if (EditorGUI.DropdownButton(rect, new GUIContent(GetTexture()), FocusType.Keyboard, new GUIStyle(){ fixedWidth = 50f, border = new RectOffset(1, 1, 1, 1) })) {
			GenericMenu menu = new GenericMenu ();
			menu.AddItem (new GUIContent("Constant"), useConstant, () => SetProperty(property, true));
			menu.AddItem (new GUIContent("Variable"), !useConstant, () => SetProperty(property, false));
			menu.ShowAsContext ();
		}

		position.position += Vector2.right * 15;
		int value = property.FindPropertyRelative ("ConstantValue").intValue;

		if (useConstant) {
			string newValue = EditorGUI.TextField (position, value.ToString ());
			int.TryParse (newValue, out value);
			property.FindPropertyRelative ("ConstantValue").intValue = value;
		} else {
			EditorGUI.ObjectField (position, property.FindPropertyRelative ("Variable"), GUIContent.none);
		}
		EditorGUI.EndProperty ();
	}

	void SetProperty(SerializedProperty property, bool value){
		var propRelativite = property.FindPropertyRelative ("UseConstant");
		propRelativite.boolValue = value;
		property.serializedObject.ApplyModifiedProperties ();
	}

	Texture GetTexture(){
		var a = Resources.Load<Texture2D> ("Textures/propertyDrawerItem");
		return a;
	}
}
