using Narrator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class LevelsManager : MonoBehaviour
    {
        public static event System.Action OnLevelUnloading;
        public static event System.Action OnLevelUnloaded;
        public static event System.Action OnLevelLoading;
        public static event System.Action OnLevelLoaded;

        public static LevelsManager Instance;
        [SerializeField] private PlayerMovement player;

        public List<LevelSO> Levels = new();

        public int triesCount;

        [SerializeField]
        private Level currentLevel;
        private int currentLevelId = -1;

        [SerializeField]
        private float destroingLevelDuration;
        [SerializeField]
        private float loadingLevelDuration;

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
        public void ChangeLevel(string id)
        {
            StartCoroutine(ChangingLevelCo(id));
        }

        private IEnumerator ChangingLevelCo(string id)
        {
            if (currentLevelId != -1) {
                OnLevelUnloading?.Invoke();
                yield return new WaitForSeconds(destroingLevelDuration / 3f);
                player.enabled = false;
                yield return new WaitForSeconds(destroingLevelDuration / 3f);
                Destroy(currentLevel.gameObject);
                currentLevel = null;
                currentLevelId = -1;
                yield return new WaitForSeconds(destroingLevelDuration / 3f);
                OnLevelUnloaded?.Invoke();
            }

            foreach (LevelSO level in Levels)
            {
                if (level.LevelId == id)
                {
                    OnLevelLoading?.Invoke();
                    yield return new WaitForSeconds(loadingLevelDuration * 0.5f);
                    currentLevel = Instantiate(level.LevelPrefab);
                    currentLevelId = Levels.IndexOf(level);
                    player.transform.position = currentLevel.StartPosition.position;
                    Debug.Log(player.transform.position);
                    Physics.SyncTransforms();
                    player.transform.rotation = Quaternion.AngleAxis(currentLevel.StartPosition.eulerAngles.y, Vector3.up);
                    player.ResetHead();
                    player.enabled = false;
                    yield return new WaitForSeconds(loadingLevelDuration * 0.5f);
                    OnLevelLoaded?.Invoke();
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