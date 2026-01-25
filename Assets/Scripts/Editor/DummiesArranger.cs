using UnityEditor;
using UnityEngine;
using System.Linq;

public class DummiesArranger : Editor
{
    [MenuItem("Tools/Arrange Dummies")]
    public static void ArrangeDummies()
    {
        GameObject dummiesRoot = GameObject.Find("Dummies");

        if (dummiesRoot == null)
        {
            Debug.LogError("Could not find 'Dummies' object in the scene.");
            return;
        }

        // Get all direct children
        var children = dummiesRoot.transform.Cast<Transform>().ToList();

        if (children.Count == 0)
        {
            Debug.LogWarning("'Dummies' object has no children to arrange.");
            return;
        }

        Undo.RecordObjects(children.Select(c => c.gameObject).Cast<Object>().ToArray(), "Arrange Dummies");

        float spacingX = 5f;
        float spacingZ = 5f;
        int columns = 5;

        // Use the Y of the first child or the root if children exist
        float targetY = children[0].position.y;
        Vector3 rootPosition = dummiesRoot.transform.position;

        for (int i = 0; i < children.Count; i++)
        {
            int row = i / columns;
            int col = i % columns;

            float posX = rootPosition.x + (col * spacingX);
            float posZ = rootPosition.z - (row * spacingZ); // Spreading backwards in Z

            children[i].position = new Vector3(posX, targetY, posZ);
        }

        Debug.Log($"Successfully arranged {children.Count} dummies in {columns} columns.");
    }
}
