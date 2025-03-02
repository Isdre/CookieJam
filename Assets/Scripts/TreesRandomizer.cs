using UnityEngine;

public class TreesRandomizer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treePrototypes;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            var treePoint = transform.GetChild(i);
            var treePrototype = treePrototypes[Random.Range(0, treePrototypes.Length)];

            var rotation =Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            var tree = Instantiate(treePrototype, treePoint.position, rotation, treePoint);
        }
    }
}
