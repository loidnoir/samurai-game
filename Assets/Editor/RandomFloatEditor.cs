using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RandomFloat))]
public class RandomFloatEditor : PropertyDrawer
{
    public const float randomTextFieldsOffset = 75;
    public const float buttonOffset = 0;
    public const float buttonwidth = 35;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int inspectorWidth = Screen.width;

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fixedWidth = 50;
        gUIStyle.border = new RectOffset(1, 1, 1, 1);

        float inputFieldWidth;

        switch ((RandomType)property.FindPropertyRelative("type").enumValueIndex)
        {
            case RandomType.Constant:
                SerializedProperty constantValue = property.FindPropertyRelative("constantValue");

                inputFieldWidth = inspectorWidth - position.x - buttonOffset - buttonwidth;
                if (float.TryParse(EditorGUI.TextField(new Rect(position.position, new Vector2(inputFieldWidth, 17.5f)), constantValue.floatValue.ToString().Replace(',', '.')).Replace('.', ','), out float value))
                    constantValue.floatValue = value;

                position.position += Vector2.right * (inputFieldWidth + buttonOffset);
                break;

            case RandomType.RandomBetweenTwoConstants:

                break;
        }

        if (EditorGUI.DropdownButton(new Rect(position.position, Vector2.one * 100), new GUIContent(GetTexture()), FocusType.Keyboard,
            gUIStyle))
        {
            GenericMenu menu = new GenericMenu();
            RandomType type = (RandomType)property.FindPropertyRelative("type").enumValueIndex;
            Debug.Log(property.FindPropertyRelative("type").enumValueIndex);

            menu.AddItem(new GUIContent("Constant"), type == RandomType.Constant, () => SetEnumPropertyValue(property, "type", 0));
            menu.AddItem(new GUIContent("Random Value"), type == RandomType.RandomBetweenTwoConstants, () => SetEnumPropertyValue(property, "type", 1));
            menu.ShowAsContext();
        }

        EditorGUI.EndProperty();
    }

    Texture GetTexture()
    {
        return Resources.FindObjectsOfTypeAll<Texture>().Where(texture => texture.name.ToLower().Contains("dropdown")).ToList<Texture>()[7];
    }

    void SetEnumPropertyValue(SerializedProperty property, string typeName, int index)
    {
        Debug.Log(index);
        property.FindPropertyRelative(typeName).enumValueIndex = index;
        Debug.Log(property.FindPropertyRelative(typeName).enumValueIndex);
    }
}
