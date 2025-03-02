using Narrator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance;
        [SerializeField] private Transform player;

        public List<LevelSO> Levels = new();

        [SerializeField]
        private GameObject currentLevel;
        private int currentLevelId = -1;


        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(this);
            }
        }

        public void NextLevel() {
            if (currentLevelId == -1) {
                return;
            }
            ChangeLevel(Levels[currentLevelId].NextLevelId);
        }

        public void NextLevelVariant() {
            if (currentLevelId == -1) {
                return;
            }
            ChangeLevel(Levels[currentLevelId].NextLevelVariantId);
        }

        public void ChangeLevel(string id) {
            if (currentLevelId != -1) {
                Destroy(currentLevel);
                currentLevel = null;
                currentLevelId = -1;
            }
            
            

            foreach (LevelSO level in Levels) {
                if (level.LevelId == id) {
                    currentLevel = Instantiate(level.LevelPrefab) as GameObject;
                    currentLevelId = Levels.IndexOf(level);
                    player.position = currentLevel.GetComponent<Level>().StartPosition.position;
                    Debug.Log(player.position);
                    Physics.SyncTransforms();
                    player.rotation = currentLevel.GetComponent<Level>().StartPosition.rotation;
                    player.GetComponent<PlayerMovement>().enabled = false;
                    break;
                }
            }
        }

        public void ResetLevel() {
            NarratorController.ClearEvents();  
            if (currentLevelId != -1) {
                ChangeLevel(Levels[currentLevelId].LevelId);
            }
        }
    }
}