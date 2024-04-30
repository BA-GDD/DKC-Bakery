using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public struct TrackData
{
    public CameraMoveNode node;
    public bool isPoint;
}
[CreateAssetMenu(menuName = "cameraTrack")]
public class CameraTrackSO : ScriptableObject
{
    [CustomEditor(typeof(CameraTrackSO))]
    public class CameraTrackEditor : Editor
    {
        private CameraTrackSO ownerSO;

        private ReorderableList points;
        private void OnEnable()
        {
            ownerSO = (CameraTrackSO)target;

            points = new ReorderableList(serializedObject,
                serializedObject.FindProperty("points"),
                true, true, true, true);
            points.multiSelect = true;
            points.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = points.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width - rect.x, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("node"), GUIContent.none, true);
                element.FindPropertyRelative("isPoint").boolValue = EditorGUI.Toggle(new Rect(rect.width + 5, rect.y, 20, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("isPoint").boolValue);
                //EditorGUI.PropertyField(
                //new Rect(rect.x + rect.width - 60, rect.y, 60, EditorGUIUtility.singleLineHeight),
                //    element.FindPropertyRelative("i"), GUIContent.none);
            };
            points.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
            {
                var menu = new GenericMenu();
                var types = TypeCache.GetTypesDerivedFrom<CameraMoveNode>();
                foreach (var t in types)
                {
                    menu.AddItem(new GUIContent("PointType/" + t.Name),
                    false, HandlerSpecialBuffAdd, t);
                }
                menu.ShowAsContext();
            };
            points.onRemoveCallback = (ReorderableList l) =>
            {
                if (l.selectedIndices.Count > 0)
                {
                    int minus = 0;
                    foreach (var i in l.selectedIndices)
                    {
                        //Undo.DestroyObjectImmediate(ownerSO.points[i - minus]);
                        AssetDatabase.SaveAssets();
                        ownerSO.points.RemoveAt(i - minus);
                        minus++;

                        EditorUtility.SetDirty(ownerSO);
                    }
                }
                else
                {
                    //Undo.DestroyObjectImmediate(ownerSO.points[ownerSO.points.Count - 1]);
                    ownerSO.points.RemoveAt(ownerSO.points.Count - 1);
                    AssetDatabase.SaveAssets();
                }
            };
            points.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Points");
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            points.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
        private void HandlerSpecialBuffAdd(object target)
        {
            Type type = (Type)target;
            Debug.Log(type.Name);
            var index = points.serializedProperty.arraySize;
            points.serializedProperty.arraySize++;
            points.index = index;
            var element = points.serializedProperty.GetArrayElementAtIndex(index);
            CameraMoveNode buff = ScriptableObject.CreateInstance(type) as CameraMoveNode;
            buff.name = type.Name;


            AssetDatabase.AddObjectToAsset(buff, ownerSO);
            AssetDatabase.SaveAssets();

            element.FindPropertyRelative("node").objectReferenceValue = buff;
            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(ownerSO);
            EditorUtility.SetDirty(buff);
        }
    }

    public List<TrackData> points = new();
}
