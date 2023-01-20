using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { SoundEffect, Music}
    public AudioTypes audioType;

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range (.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    [Range(0f, 1f)]
    public float dopplerLevel;

    [Range(0f, 1f)]
    public float spatialBlend;

    public AudioRolloffMode volumeRolloff;

    public float minDistance;

    public float maxDistance;





    [HideInInspector]
    public AudioSource source;
}
