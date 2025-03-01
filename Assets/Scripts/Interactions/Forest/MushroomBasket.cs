using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using LevelManagement;

namespace Forest {
    public class MushroomBasket : MonoBehaviour {
        public static MushroomBasket instance;

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

        private void Start() {
            mushroomCount = 0;
        }

        public void AddMushroom() {
            mushroomCount++;
            if (mushroomCount == 4) {
                LevelsManager.Instance.NextLevelVariant();
            }
        }
    }
}