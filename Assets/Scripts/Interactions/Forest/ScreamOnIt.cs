using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
    public class ScreamOnIt : Interaction
    {
        public UnityEvent OnInteract;
        [SerializeField]
        private AudioSource _audio; 
        [SerializeField]
        private Transform body;
        [SerializeField]
        private float fallingTime;
        private bool _isInteracted = false;
        public override void Interact(Interactor interactor){
            if (_isInteracted) return;
            _isInteracted = true;
            Debug.Log("Scream");
            _audio.Play();
            body.DOMoveY(0.3f,fallingTime);
            body.DORotate(new Vector3(0,0,90f),fallingTime);
            OnInteract.Invoke();
        }
    }
}