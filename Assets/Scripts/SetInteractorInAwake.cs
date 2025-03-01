using Bipolar.InteractionSystem;
using UnityEngine;

public class SetInteractorInAwake : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent<HintsUIController>(out var uiController))
            uiController.Interactor = FindObjectOfType<Interactor>();
    }
}
