using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class VolumeSetter : MonoBehaviour
    {
        private Slider Slider;

        private void Awake() {
            Slider = GetComponent<Slider>();
            Slider.value = PlayerPrefs.GetFloat("Volume", 1);
        }

        public void SetVolume(float value) {
            PlayerPrefs.SetFloat("Volume", value);

        }
    }
}