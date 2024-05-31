using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AStarNodeCreator))]
public class AStarNodeCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AStarNodeCreator nodeCreator = (AStarNodeCreator)target;

        if (GUILayout.Button("Create Nodes"))
        {
            nodeCreator.CreateNodes();
        }
    }
}
