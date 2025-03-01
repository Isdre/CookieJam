using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bipolar.InteractionSystem;

namespace Forest {
public class CutTree  : Interaction
    {
        [SerializeField]
        private Transform tree;

        [SerializeField]
        private float fallingTime = 1f;

        public override void Interact(Interactor interactor)
        {
            Debug.Log("Cutting tree");
            Vector3 treePos = new Vector3(tree.position.x, 0f, tree.position.z);
            Vector3 playerPos = new Vector3(interactor.transform.position.x, 0f, interactor.transform.position.z);

            Vector3 direction = (playerPos - treePos).normalized;

            Quaternion start = tree.rotation;
            Quaternion target = new Quaternion(tree.rotation.x * direction.x, tree.rotation.y * direction.y, tree.rotation.z * direction.z, tree.rotation.w);

            StartCoroutine(TreeFalling(start,target));
        }


        private IEnumerator TreeFalling(Quaternion start,Quaternion target) {
        Debug.Log("Cutting tree");
        float fallingTimer = 0f;
        while (fallingTimer < fallingTime) {
            fallingTimer += Time.deltaTime;
            tree.rotation = Quaternion.Lerp(start, target, fallingTimer / fallingTime);
            yield return null;
        }
    }
    }
}