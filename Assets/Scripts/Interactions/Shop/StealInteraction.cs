using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Shop {
    public class StealInteraction : Interaction {
        public UnityEvent OnInteract;
        //[SerializeField]
        //private Transform body;

        public override void Interact(Interactor interactor){
            //Destroy(body.gameObject);
            OnInteract.Invoke();
        }
    }
}