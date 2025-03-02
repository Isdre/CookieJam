using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Shop {
    public class CashInteraction : Interaction {
        public UnityEvent OnInteract;

        public override void Interact(Interactor interactor){
            if (!DontStealStoryDirector.Instance.stealAnyItem) return;
            OnInteract.Invoke();
        }
    }
}