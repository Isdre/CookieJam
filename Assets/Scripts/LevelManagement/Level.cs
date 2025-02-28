using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string LevelId;
    public Transform StartPosition;
    public string NextLevelVariantId;
    public string NextLevelId;

    public GameObject LevelPrefab;

    private void Awake() {
        LevelPrefab = this.gameObject;
    }
}
