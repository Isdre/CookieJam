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

        public override void Interact(Interactor interactor){
            Debug.Log("Fire");
            _particle.Play();
            OnInteract.Invoke();
        }
    }
}