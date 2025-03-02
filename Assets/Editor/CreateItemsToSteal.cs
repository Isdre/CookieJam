using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using Shop;
using static UnityEditor.Events.UnityEventTools;
using UnityEditor.Events;

public class CreateItemsToSteal : MonoBehaviour
{
    private static Transform[] items;

    [MenuItem("Tools/Create Items")]
    static void CreateItems()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("dozebrania");
        if (parent == null)
        {
            Debug.LogError("ParentObject not found! Make sure there is a GameObject named 'dozebrania' in the scene.");
            return;
        }

        foreach (Transform item in parent.transform)
        {
            Debug.Log(item);
            if (item == parent.transform) continue;

        
            RaycastCollider rc = item.GetComponent<RaycastCollider>();

            HighlightController hc = item.GetComponent<HighlightController>();

            RaycastTarget rt = item.GetComponent<RaycastTarget>();

            rc.raycastTargets = new RaycastTarget[] { rt };

            InteractiveObject io = item.GetComponent<InteractiveObject>();


            AudioSource aS = item.GetComponent<AudioSource>();
            StealInteraction si = item.GetComponent<StealInteraction>();

            UnityEventTools.AddPersistentListener(si.OnInteract,aS.Play);
            io.interactions = new Interaction[] { si };
        }

        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}
