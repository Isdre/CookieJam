using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepsSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private float stepLength = 0.2f;
    [SerializeField]
    private AudioClip defaultStepSound;
    [SerializeField]
    private LayerMask steppableLayers;

    [Header("States")]
    [SerializeField]
    private Vector3 previousPosition;
    [SerializeField]
    private bool isOnFloor;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        var position = transform.position;

        var wasOnFloor = isOnFloor;
        isOnFloor = Physics.Raycast(position + Vector3.up * 0.5f, Vector3.down, out var hitInfo, 1, steppableLayers);
        if (isOnFloor)
        {
            if (wasOnFloor == false || (position - previousPosition).sqrMagnitude > stepLength * stepLength)
            {
                PlayStepSound(hitInfo.collider.gameObject);
                previousPosition = position;
            }
        }
    }

    private void PlayStepSound(GameObject steppedObject)
    {
        var sound = GetStepSound(steppedObject);
        if (sound)  
            audioSource.PlayOneShot(sound);
    }

    private AudioClip GetStepSound(GameObject steppedObject) => steppedObject.TryGetComponent<SoundSurface>(out var surface)
        ? surface.GetSound()
        : defaultStepSound;
}
