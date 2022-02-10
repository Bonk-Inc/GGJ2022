using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

 [CustomPropertyDrawer(typeof(MultipleChoice))]
public class MultipleChoicePropertyDrawer : PropertyDrawer
{

    int current = 0;

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var multipleChoiceAttribute = this.attribute as MultipleChoice;
        if (property.propertyType == SerializedPropertyType.String)
        {
            current = EditorGUI.Popup (position, label.text, current, multipleChoiceAttribute.choices);
            property.stringValue = multipleChoiceAttribute.choices[current];
        }
        else
        {
            Debug.LogWarning("MultipleChoice attribute only allowed on string fields");
            EditorGUI.PropertyField(position, property, label);
        }
        EditorGUI.EndProperty();
    }
}
