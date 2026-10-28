#region

using UnityEngine;
using UnityEngine.Audio;

#endregion

public class AudioHandler
{
    private const float OffVolumeValue = -80f;
    private const float OnVolumeValue = 0f;

    private const string MusicKey = "MusicVolume";
    private const string SoundKey = "SoundsVolume";

    private readonly AudioMixer _audioMixer;

    public AudioHandler(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public bool IsMusicOn()
    {
        return IsVolumeOn(MusicKey);
    }

    public bool IsSoundsOn()
    {
        return IsVolumeOn(SoundKey);
    }

    public void OffMusic()
    {
        OffVolume(MusicKey);
    }

    public void OnMusic()
    {
        OnVolume(MusicKey);
    }

    public void OnSounds()
    {
        OnVolume(SoundKey);
    }

    public void OffSounds()
    {
        OffVolume(SoundKey);
    }

    private bool IsVolumeOn(string key)
    {
        return _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - OnVolumeValue) <= 0.01f;
    }

    private void OnVolume(string key)
    {
        _audioMixer.SetFloat(key, OnVolumeValue);
        Debug.Log($"on {key} {_audioMixer.GetFloat(key, out float volume)} {volume}");
    }

    private void OffVolume(string key)
    {
        _audioMixer.SetFloat(key, OffVolumeValue);
        Debug.Log($"off {key} {_audioMixer.GetFloat(key, out float volume)} {volume}");
    }
}