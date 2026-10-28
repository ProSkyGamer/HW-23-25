#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class AudioUI : MonoBehaviour
{
    [SerializeField] private Button _toggleMuteSoundsButton;
    [SerializeField] private Button _toggleMuteMusicButton;

    private AudioHandler _audioHandler;

    public void Initialize(AudioHandler audioHandler)
    {
        _audioHandler = audioHandler;

        _toggleMuteSoundsButton.onClick.AddListener(() =>
        {
            if (_audioHandler.IsSoundsOn())
                _audioHandler.OffSounds();
            else
                _audioHandler.OnSounds();
        });

        _toggleMuteMusicButton.onClick.AddListener(() =>
        {
            if (_audioHandler.IsMusicOn())
                _audioHandler.OffMusic();
            else
                _audioHandler.OnMusic();
        });
    }
}