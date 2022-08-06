using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
    [SerializeField] private AudioSource source;
    [Range(0,1)]
    [SerializeField] private float volume;

    [Header("Sounds")]
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip dropdownSound;
    [SerializeField] private AudioClip tileClickedSound;
    [SerializeField] private AudioClip winSound;

    public void PlayTileClickedSound()
    {
        playSound(tileClickedSound);
    }
    public void PlayDropdownSound()
    {
        playSound(dropdownSound);
    }
    public void PlayButtonSound()
    {
        playSound(buttonSound);
    }
    public void PlayWinSound()
    {
        playSound(winSound);
    }

    private void playSound(AudioClip sound)
    {
        source.PlayOneShot(sound, volume);
    }
}
