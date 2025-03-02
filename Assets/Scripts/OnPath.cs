using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPath : MonoBehaviour
{
    public int isOnPath = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnPath++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnPath--;
        }
    }
}
