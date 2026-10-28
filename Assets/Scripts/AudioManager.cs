#region

using UnityEngine;
using UnityEngine.Audio;

#endregion

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioUI _audioUI;
    [SerializeField] private AudioMixer _audioMixer;
    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixer);

        _audioUI.Initialize(_audioHandler);
    }
}