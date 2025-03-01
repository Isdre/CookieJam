using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagament;

public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public string StartLevelId;        

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(this);
            }
        }

        public void Start() {
            LevelsManager.Instance.ChangeLevel(StartLevelId);
        }

        public void Pause() {
            Time.timeScale = 0;
        }

        public void Resume() {
            Time.timeScale = 1;
        }
    }