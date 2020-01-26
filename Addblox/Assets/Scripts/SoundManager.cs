using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioClip impactSFX;
    [SerializeField]
    private AudioClip mergeDifferentColorsSFX;
    [SerializeField]
    private AudioClip mergeSameColorSFX;
    [SerializeField]
    private AudioClip addblocksSFX;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayImpactSound()
    {
        sfxSource.clip = impactSFX;
        sfxSource.Play();
    }

    public void PlayMergeSound(bool isSameColor)
    {
        sfxSource.clip = isSameColor ? mergeSameColorSFX : mergeDifferentColorsSFX;
        sfxSource.Play();
    }

    public void PlayAddBlocksSound()
    {
        sfxSource.clip = addblocksSFX;
        sfxSource.Play();
    }
}
