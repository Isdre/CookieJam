using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
public class CollectMushroom : Interaction {
        public UnityEvent OnInteract;
        [SerializeField]
        private Transform mushroom;
        public override void Interact(Interactor interactor) {
            MushroomBasket.instance.AddMushroom();
            Destroy(mushroom.gameObject);
            OnInteract.Invoke();
        }
    }
}