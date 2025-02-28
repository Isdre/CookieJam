using UnityEngine;

public class SoundSurface : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sounds;

    public AudioClip GetSound()
    {
        int index = Random.Range(0, sounds.Length);
        return sounds[index];
    }
}