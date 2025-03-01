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
        private float stepTime = 0.25f;

        public void Interact()
        {
            body.DOScaleY(0.4f, stepTime);
        }
    }
}