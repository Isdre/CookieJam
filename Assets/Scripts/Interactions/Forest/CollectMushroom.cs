using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;

namespace Forest {
public class CollectMushroom : Interaction {
        [SerializeField]
        private Transform mushroom;
        public override void Interact(Interactor interactor) {
            MushroomBasket.instance.AddMushroom();
            Destroy(mushroom.gameObject);
        }
    }
}