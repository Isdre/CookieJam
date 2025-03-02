using UnityEngine;

public class CounterBlockResizer : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Transform leftPost;
    [SerializeField]
    private Transform rightPost;
    [SerializeField]
    private Transform barrier;

    [SerializeField, Min(0.001f)]
    private float barrierWidth = 1;

    [Header("Settings")]
    [SerializeField]
    private float width;

    private void Awake()
    {
        Refresh();
    }

    private void Refresh()
    {
        float halfWidth = width / 2f;
        leftPost.localPosition = Vector3.left * halfWidth;
        rightPost.localPosition = Vector3.right * halfWidth;

        barrier.localScale = new Vector3(width / barrierWidth, 1, 1);
    }

    private void OnValidate()
    {
        Refresh();
    }
}
