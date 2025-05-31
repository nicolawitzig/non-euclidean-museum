using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ColliderChildSelector
{
    // This will appear when you right-click any GameObject in the Hierarchy
    [MenuItem("GameObject/Select Children With Collider", false, 0)]
    static void SelectColliderChildren(MenuCommand cmd)
    {
        var go = cmd.context as GameObject;
        if (go == null) return;

        // find ALL Colliders in its descendants (including itself if you like)
        var colliders = go.GetComponentsInChildren<Collider>();

        // pull out the distinct GameObjects
        var objectsWithColliders = colliders
            .Select(c => c.gameObject)
            .Distinct()
            .ToArray();

        // select them in the Hierarchy
        Selection.objects = objectsWithColliders;

        // ping so they flash into view
        foreach (var obj in objectsWithColliders)
            EditorGUIUtility.PingObject(obj);
    }

    // Validate the menu item: only active if you’ve got a GameObject selected
    [MenuItem("GameObject/Select Children With Collider", true)]
    static bool ValidateSelectColliderChildren(MenuCommand cmd)
    {
        return Selection.activeGameObject != null;
    }
}
