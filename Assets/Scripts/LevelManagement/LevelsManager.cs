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

        public int triesCount;

        [SerializeField]
        private Level currentLevel;
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
            PlayerPrefs.SetInt(Levels[currentLevelId].LevelId, triesCount);
            triesCount = 0;
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
                    currentLevel = Instantiate(level.LevelPrefab);
                    currentLevelId = Levels.IndexOf(level);
                    player.position = currentLevel.StartPosition.position;
                    Debug.Log(player.position);
                    Physics.SyncTransforms();
                    player.rotation = Quaternion.AngleAxis(currentLevel.StartPosition.eulerAngles.y, Vector3.up);
                    var playerMovement = player.GetComponent<PlayerMovement>();
                    playerMovement.ResetHead();
                    playerMovement.enabled = false;
                    break;
                }
            }
        }

        public void ResetLevel() {
            NarratorController.ClearEvents();  
            if (currentLevelId == -1) {
                return;
            }
            triesCount++;
            if (triesCount == 3) {
                NextLevel();
            }
            else ChangeLevel(Levels[currentLevelId].LevelId);
        }
    }
}