using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
    public class StepOnIt : Interaction {
        public UnityEvent OnInteract;
        
        [SerializeField]
        private Transform body;

        [SerializeField]
        private float targetY = 0.4f;

        [SerializeField]
        private float stepTime = 0.25f;

        public override void Interact(Interactor interactor)
        {
            body.DOScaleY(targetY, stepTime);
            OnInteract.Invoke();
        }
    }
}