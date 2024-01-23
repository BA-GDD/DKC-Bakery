using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(PanelUI))]
public class PanelUIEditor : Editor
{
    private PanelUI _targetPanelUI;

    private void OnEnable()
    {
        _targetPanelUI = (PanelUI)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PanelUI inspectorObj = _targetPanelUI;
        if (inspectorObj.useBlackPanel)
        {
            inspectorObj.blackPanel = EditorGUILayout.ObjectField("BlackPanel",
                                      inspectorObj.blackPanel,
                                      typeof(Image), true) as Image;

            inspectorObj.easingTime = EditorGUILayout.FloatField("EasingTime",
                                      inspectorObj.easingTime);

            inspectorObj.endOfAlpha = EditorGUILayout.FloatField("End of alpha",
                                      inspectorObj.endOfAlpha);
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(_targetPanelUI);
        }
    }
}
