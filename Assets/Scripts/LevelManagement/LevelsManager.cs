using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagament
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance;
        [SerializeField] private Transform player;

        public List<GameObject> LevelsGameObjects = new();
        private List<Level> Levels = new();

        private GameObject currentLevel;
        private int currentLevelId = -1;


        private void Awake() {
            if (Instance == null) {
                Instance = this;
                foreach (GameObject level in LevelsGameObjects) {
                    Level levelComponent = level.GetComponent<Level>();
                    if (levelComponent != null) {
                        Levels.Add(levelComponent);
                    }
                }
            } else if (Instance != this) {
                Destroy(this);
            }
        }

        public void NextLevel() {
            ChangeLevel(Levels[currentLevelId].NextLevelId);
        }

        public void NextLevelVariant() {
            ChangeLevel(Levels[currentLevelId].NextLevelVariantId);
        }

        public void ChangeLevel(string id) {
            if (currentLevelId != -1) {
                Destroy(currentLevel);
            }
            
            foreach (Level level in Levels) {
                if (level.LevelId == id) {
                    currentLevel = Instantiate(level.LevelPrefab);
                    currentLevelId = Levels.IndexOf(level);
                    player.position = level.StartPosition.position;
                    player.rotation = level.StartPosition.rotation;
                    break;
                }
            }
        }

        public void ResetLevel() {
            if (currentLevelId != -1) {
                Destroy(currentLevel);
            }
            currentLevel = Instantiate(Levels[currentLevelId].LevelPrefab);
            player.position = Levels[currentLevelId].StartPosition.position;
            player.rotation = Levels[currentLevelId].StartPosition.rotation;
        }
    }
}