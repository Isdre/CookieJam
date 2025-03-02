using UnityEngine;

namespace LevelManagement{
    [CreateAssetMenu(fileName = "LevelDef", menuName = "ScriptableObject/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        public string LevelId;
        public string NextLevelVariantId;
        public string NextLevelId;

        public GameObject LevelPrefab;
    }
}