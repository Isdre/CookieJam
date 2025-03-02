using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPath : MonoBehaviour
{
    public int isOnPath = 0;

    public UnityEvent OnEnteringPath;
    public UnityEvent OnLeavingPath;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            isOnPath++;
            if (isOnPath == 1) 
                OnEnteringPath.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnPath--;
            if (isOnPath == 0)
                OnLeavingPath.Invoke();
        }
    }
}
