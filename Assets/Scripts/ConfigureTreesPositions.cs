using UnityEngine;

public class ConfigureTreesPositions : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayers;

    [ContextMenu("Configure Trees Positions")]
    public void ConfigureTrees()
    {
        foreach (var tf in GetComponentsInChildren<Transform>(true))
        {
            if (tf.gameObject.name.Contains("drzewo"))
            {
                PlaceTreePoint(tf);
            }
        }
    }

    private void PlaceTreePoint(Transform parent)
    {
        int count = parent.childCount;
        if (count < 1)
            return;

        var pointPosition = Vector3.zero;
        for(int i = 0; i < count; i++)
        {
            var child = parent.GetChild(i);
            pointPosition += child.position;
        }
        pointPosition /= count;
        pointPosition.y = 0;

        var treePoint = new GameObject("Tree Point");
        treePoint.transform.position = pointPosition;
        treePoint.transform.parent = transform;
    }
}
