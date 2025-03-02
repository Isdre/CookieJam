using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
    public class CutTree  : Interaction
    {
    public UnityEvent OnInteract;
        [SerializeField]
        private Transform tree;

        [SerializeField]
        private float fallingTime = 1f;
        private bool isCut = false;

        public override void Interact(Interactor interactor)
        {
            if (isCut) return;
            isCut = true;
            Debug.Log("Cutting tree");
            tree.DORotateQuaternion(Quaternion.AngleAxis(90,interactor.transform.right),fallingTime);
            OnInteract.Invoke();
        }
    }
}