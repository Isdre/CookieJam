using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class StartButton : MonoBehaviour
    {
        public void StartGame() {
            SceneManager.LoadScene("Game");
        }
    }
}