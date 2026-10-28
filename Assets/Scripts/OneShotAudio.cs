#region

using UnityEngine;

#endregion

[RequireComponent(typeof(AudioSource))]
public class OneShotAudio : MonoBehaviour
{
    private void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        Destroy(gameObject, audioSource.clip.length);
    }
}