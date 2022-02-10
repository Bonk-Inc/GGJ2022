using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 [CustomPropertyDrawer(typeof(Tag))]
public class TagPropertyDrawer : MultipleChoicePropertyDrawer
{

    int tagIndex = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var tags = new List<string>(UnityEditorInternal.InternalEditorUtility.tags);
        var tagAttribute = this.attribute as Tag;
        
        if(property.propertyType == SerializedPropertyType.String) {

        
            if(tagAttribute.useUnityDefaultTagDropdown) {
                EditorGUI.BeginProperty(position, label, property);

                var selectedTag = EditorGUI.TagField(
                    position,
                    label,
                    property.stringValue
                );
                var newTagIndex = tags.IndexOf(selectedTag);
                property.stringValue = selectedTag;

                if( newTagIndex < 0){
                    Debug.LogWarning($"The selected tag {selectedTag} does not exist.");
                }

                EditorGUI.EndProperty();
            } else {
                base.OnGUI(position, property, label);
            }
        
            
        

        } 
        else {
            Debug.LogWarning("Tag attribute only allowed on string fields");
            EditorGUI.PropertyField(position, property, label);
        }     

    }

}
