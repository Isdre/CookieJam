using Bipolar.InteractionSystem;
using UnityEngine.Events;

namespace Shop
{
    public class ExitInteraction : Interaction {
        public UnityEvent OnInteract;

        public override void Interact(Interactor interactor){
            OnInteract.Invoke();
            OnInteract.RemoveAllListeners();
        }
    }
}