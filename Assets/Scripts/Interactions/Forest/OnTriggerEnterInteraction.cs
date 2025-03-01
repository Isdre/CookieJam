using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Forest {
    public class OnTriggerEnterInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent interaction;

        public void OnTriggerEnter(Collider other){
            if (other.gameObject.CompareTag("Player")) {
                interaction.Invoke();
            }
        }
    }
}