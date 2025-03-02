using UnityEngine;
using LevelManagement;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Pause() {
            Time.timeScale = 0;
        }

        public void Resume() {
            Time.timeScale = 1;
        }
    }