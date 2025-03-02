using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
    public class Fireplace : Interaction
    {
        public UnityEvent OnInteract;
        [SerializeField]
        private ParticleSystem _particle; 

        private bool _isInteracted = false;

        public override void Interact(Interactor interactor){
            if (_isInteracted) return;
            _isInteracted = true;
            Debug.Log("Fire");
            _particle.Play();
            OnInteract.Invoke();
        }
    }
}