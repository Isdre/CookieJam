using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DoSomethingAfterOneFrame : MonoBehaviour
{
    [SerializeField]
    private UnityEvent actionAfterFrame;

    private IEnumerator Start()
    {
        yield return null;
        actionAfterFrame.Invoke();
    }
}
