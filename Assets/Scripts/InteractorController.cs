using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using UnityEngine;

public class InteractorController : MonoBehaviour
{
    [SerializeField]
    private Interactor interactor;

    [SerializeField]
    private RaycastController raycastController;

    private void OnEnable()
    {
        raycastController.OnRayEntered += RaycastController_OnRayEntered;
        raycastController.OnRayExited += RaycastController_OnRayExited;
    }

    private void Update()
    {
        interactor.CheckInteractions();
    }

    private void RaycastController_OnRayEntered(RaycastTarget target)
    {
        if (target.TryGetComponent<InteractiveObject>(out var interactiveObject))
        {
            interactor.CurrentInteractiveObject = interactiveObject;
        }
    }

    private void RaycastController_OnRayExited(RaycastTarget target)
    {
        interactor.CurrentInteractiveObject = null;
    }

    private void OnDisable()
    {
        raycastController.OnRayEntered -= RaycastController_OnRayEntered;
        raycastController.OnRayExited -= RaycastController_OnRayExited;
    }

}
