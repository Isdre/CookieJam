using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(this);
            }
        }

        public void Pause() {
            Time.timeScale = 0;
        }

        public void Resume() {
            Time.timeScale = 1;
        }
    }