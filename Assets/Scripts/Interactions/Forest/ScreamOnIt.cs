using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;

namespace Forest {
    public class ScreamOnIt : Interaction
    {
        [SerializeField]
        private AudioSource _audio; 
        [SerializeField]
        private Transform body;
        [SerializeField]
        private float fallingTime;

        public override void Interact(Interactor interactor){
            Debug.Log("Scream");
            _audio.volume = PlayerPrefs.GetFloat("Volume", 1);
            _audio.Play();
            body.DOMoveY(0.3f,fallingTime);
            body.DORotate(new Vector3(0,0,90f),fallingTime);
        }
    }
}