using System.Linq;
using UnityEditor;
using UnityEngine;

public class ShowColliderParents : EditorWindow
{
    [MenuItem("Tools/Select Parents of Colliders")]
    static void OpenWindow()
    {
        GetWindow<ShowColliderParents>("Collider Parents");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Select All Parents of Colliders"))
            SelectParents();
    }

    static void SelectParents()
    {
        // find every collider in the scene
        var allColliders = Object.FindObjectsOfType<Collider>();
        
        // pick their top-level parent (or use .transform.parent for one level up)
        var parents = allColliders
            .Select(c => c.transform.root.gameObject)
            .Distinct()
            .ToArray();

        // select them in the Hierarchy
        Selection.objects = parents;
        
        // ping them so they flash in the Hierarchy
        foreach (var go in parents)
            EditorGUIUtility.PingObject(go);
    }
}
