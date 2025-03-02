using UnityEngine;

namespace LevelManagement{
    public class ValidateLevel : MonoBehaviour
    {
        [ContextMenu("Fix scales")]
        private void Awake() {
            foreach (var collider in GetComponentsInChildren<Collider>()) {
                collider.transform.localScale = new Vector3(
                    Mathf.Abs(collider.transform.localScale.x),
                    Mathf.Abs(collider.transform.localScale.y),
                    Mathf.Abs(collider.transform.localScale.z));
            }
        }
    }
}