using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class ExitButton : MonoBehaviour
    {
        public void Exit() {
            Application.Quit();
        }
    }
}