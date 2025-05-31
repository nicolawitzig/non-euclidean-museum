using UnityEditor;
using UnityEngine;

public static class ColliderChildRemover
{
    // Adds a “Remove Colliders From Children” entry to right-click menu on any GameObject
    [MenuItem("GameObject/Remove Colliders From Children", false, 0)]
    static void RemoveCollidersFromChildren(MenuCommand cmd)
    {
        var root = cmd.context as GameObject;
        if (root == null) return;

        // gather ALL Colliders in descendants
        var colliders = root.GetComponentsInChildren<Collider>(includeInactive: true);

        Undo.RegisterCompleteObjectUndo(root, "Remove Child Colliders");

        int removed = 0;
        foreach (var col in colliders)
        {
            // skip any collider on the root itself
            if (col.gameObject == root) continue;

            // destroy the component with undo support
            Undo.DestroyObjectImmediate(col);
            removed++;
        }

        Debug.Log($"Removed {removed} Colliders from under '{root.name}'.");
    }

    // only enable this menu if you actually have a GameObject selected
    [MenuItem("GameObject/Remove Colliders From Children", true)]
    static bool ValidateRemoveCollidersFromChildren(MenuCommand cmd)
    {
        return Selection.activeGameObject != null;
    }
}
