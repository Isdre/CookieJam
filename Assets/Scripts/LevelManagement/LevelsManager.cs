using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagament
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance;
        [SerializeField] private Transform player;

        public List<GameObject> Levels = new();
        private GameObject currentLevel;
        private int currentLevelId = -1;


        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destry(this);
            }
        }

        public void ChangeLevel(int id) {
            if (currentLevelId != -1) {
                Destroy(currentLevel);
            }
            currentLevel = Instantiate(Levels[id]);
            currentLevelId = id;
            player.position = currentLevel.GetComponent<Level>().startPosition;
        }

        public void ResetLevel() {
            if (currentLevel != null) {
                Destroy(currentLevel);
            }
            currentLevel = Instantiate(Levels[currentLevelId]);
            player.position = currentLevel.GetComponent<Level>().startPosition;
        }
    }
}