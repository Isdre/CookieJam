using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement{
    public class ValidateLevel : MonoBehaviour
    {
        private void Awake() {
            foreach (Transform child in transform) {
                child.localScale = new Vector3(Mathf.Abs(child.localScale.x),Mathf.Abs(child.localScale.y),Mathf.Abs(child.localScale.z));
            }
        }
    }
}