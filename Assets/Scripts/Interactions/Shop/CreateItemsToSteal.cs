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
    
    [MenuItem("CreateItems")]
    static void CreateItems()
    {
        GameObject parent = GameObject.Find("dozebrania");
        if (parent == null)
        {
            Debug.LogError("ParentObject not found! Make sure there is a GameObject named 'ParentObject' in the scene.");
            return;
        }
        
        items = parent.GetComponentsInChildren<Transform>();

        foreach (var item in items)
        {
            if (item == parent.transform) continue; // Skip the parent itself

            // Create Parent "Body"
            GameObject body = new GameObject(item.gameObject.name + "Body");
            body.transform.position = item.position;
            body.transform.rotation = item.rotation;

            // Add Outline.cs to the original item
            if (item.gameObject.GetComponent<Outline>() == null)
            {
                item.gameObject.AddComponent<Outline>();
            }

            // Add RaycastCollider.cs to body
            if (body.gameObject.GetComponent<Outline>() == null)
            {
                body.gameObject.AddComponent<Outline>();
            }
            
            // Add HighlightController.cs to body
            if (body.gameObject.GetComponent<HighlightController>() == null)
            {
                body.gameObject.AddComponent<HighlightController>();
            }
            
            // Set original item as a child of "Body"
            item.SetParent(body.transform);

            // Create "Interactive Object" as a child of "Body"
            GameObject interactiveObject = new GameObject("Interactive Object");
            interactiveObject.transform.SetParent(body.transform);

            // Create "StealInteraction" as a child of "Interactive Object"
            GameObject stealInteraction = new GameObject("StealInteraction");
            stealInteraction.transform.SetParent(interactiveObject.transform);

            // Add components to "StealInteraction"
            if (stealInteraction.GetComponent<StealInteraction>() == null)
            {
                stealInteraction.AddComponent<StealInteraction>();
            }
            if (stealInteraction.GetComponent<Hint>() == null)
            {
                stealInteraction.AddComponent<Hint>();
            }

            // Add components to "Interactive Object"
            if (interactiveObject.GetComponent<InteractiveObject>() == null)
            {
                interactiveObject.AddComponent<InteractiveObject>();
            }
            if (interactiveObject.GetComponent<RaycastTarget>() == null)
            {
                interactiveObject.AddComponent<RaycastTarget>();
            }

            // Register undo for each new object
            Undo.RegisterCreatedObjectUndo(body, "Create Body Structure");

            Debug.Log($"Processed: {item.gameObject.name}");
        }
        
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}
