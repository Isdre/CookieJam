using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
    public class VolumeSetter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        private Slider Slider;

        private void Awake() {
            Slider = GetComponent<Slider>();
            Slider.value = PlayerPrefs.GetFloat("Volume", 1);
            audioMixerGroup.audioMixer.SetFloat("volume",Slider.value);
        }

        public void SetVolume(float value) {
            PlayerPrefs.SetFloat("Volume", value);

            audioMixerGroup.audioMixer.SetFloat("volume", value);

        }
    }
}