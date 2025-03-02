using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using Shop;

public class CreateItemsToSteal : MonoBehaviour
{
    private static Transform[] items;

    [MenuItem("Tools/Create Items")]
    static void CreateItems()
    {
        GameObject parent = GameObject.Find("dozebrania");
        if (parent == null)
        {
            Debug.LogError("ParentObject not found! Make sure there is a GameObject named 'dozebrania' in the scene.");
            return;
        }

        items = parent.GetComponentsInChildren<Transform>();

        foreach (var item in items)
        {
            if (item == parent.transform) continue; // Skip the parent itself

            // Create Parent "Table"
            GameObject table = new GameObject(item.gameObject.name); // Name it after the original item
            table.transform.position = item.position;
            table.transform.rotation = item.rotation;

            // Create "Body" as a child of Table
            GameObject body = new GameObject("Body");
            body.transform.SetParent(table.transform);

            // Add RaycastCollider.cs to Body
            // Add RaycastCollider.cs to Body
            if (body.GetComponent<RaycastCollider>() == null)
            {
                // Ensure a collider exists before adding RaycastCollider
                if (!body.GetComponent<Collider>())
                {
                    body.AddComponent<BoxCollider>(); // Use a default collider (change if needed)
                }

                body.AddComponent<RaycastCollider>();
            }


            // Add HighlightController.cs to Body
            if (body.GetComponent<HighlightController>() == null)
            {
                body.AddComponent<HighlightController>();
            }

            // Set original item as a child of "Body"
            item.SetParent(body.transform);

            // Add Outline.cs to the original item
            if (item.gameObject.GetComponent<Outline>() == null)
            {
                item.gameObject.AddComponent<Outline>();
            }

            // Create "Interactive Object" as a child of "Body"
            GameObject interactiveObject = new GameObject("Interactive Object");
            interactiveObject.transform.SetParent(body.transform);

            // Add InteractiveObject.cs to "Interactive Object"
            if (interactiveObject.GetComponent<InteractiveObject>() == null)
            {
                interactiveObject.AddComponent<InteractiveObject>();
            }

            // Add RaycastTarget.cs to "Interactive Object"
            if (interactiveObject.GetComponent<RaycastTarget>() == null)
            {
                RaycastTarget t = interactiveObject.AddComponent<RaycastTarget>();
                t.OnRayEnter += () => { body.GetComponent<HighlightController>().IsHighlighted = true; };
                t.OnRayExit += () => { body.GetComponent<HighlightController>().IsHighlighted = false; };
            }

            // Create "StealInteraction" as a child of "Interactive Object"
            GameObject stealInteraction = new GameObject("StealInteraction");
            stealInteraction.transform.SetParent(interactiveObject.transform);

            // Add StealInteraction.cs to "StealInteraction"
            if (stealInteraction.GetComponent<StealInteraction>() == null)
            {
                stealInteraction.AddComponent<StealInteraction>();
            }

            // Add Hint.cs to "StealInteraction"
            if (stealInteraction.GetComponent<Hint>() == null)
            {
                stealInteraction.AddComponent<Hint>();
            }

            // Register undo for each new object
            Undo.RegisterCreatedObjectUndo(table, "Create Table Structure");

            Debug.Log($"Processed: {item.gameObject.name}");
        }

        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}
