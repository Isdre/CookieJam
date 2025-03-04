using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using LevelManagement;
using UnityEngine.Events;

namespace Forest {
    public class MushroomBasket : MonoBehaviour {
        public static MushroomBasket instance;
        public UnityEvent OnCollectingAllMushroom;
        public int mushroomCount;

        private void Awake() {
            if (instance != null) {
                if (instance != this) {
                    Destroy(instance.gameObject);
                    instance = this;
                }
            }
            else instance = this;
        }

        public void AddMushroom() {
            mushroomCount++;
            if (mushroomCount == 4) {
                OnCollectingAllMushroom.Invoke();
            }
        }
    }
}