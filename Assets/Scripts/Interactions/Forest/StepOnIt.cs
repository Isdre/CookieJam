using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bipolar.InteractionSystem;

namespace Forest {
public class StepOnIt : MonoBehaviour
    {
        [SerializeField]
        private Transform body;

        [SerializeField]
        private float targetY = 0.4f;

        [SerializeField]
        private float stepTime = 0.25f;

        public void Interact()
        {
            body.DOScaleY(targetY, stepTime);
        }
    }
}