using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bipolar.InteractionSystem;

namespace Forest {
    public class Fireplace : Interaction
    {
        [SerializeField]
        private ParticleSystem _particle; 

        public override void Interact(Interactor interactor){
            Debug.Log("Fire");
            _particle.Play();
        }
    }
}