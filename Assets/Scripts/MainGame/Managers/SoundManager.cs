using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private const string MASTER_VOLUME_PARAMETER = "MasterVolume";

    private const string CUT_OFF_LOW_PASS_PARAMTER = "CutoffPass";
   // private static string SFX_VOLUME_PARAMETER = "SFXVolume";

   [SerializeField] private float HZ;
    [SerializeField] private AudioMixerGroup masterMixer;
    // [SerializeField] private AudioMixerGroup sfxMixer;
    // [SerializeField] private AudioMixerGroup musicMixer;

    public void MasterVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (1 - newValue) * -40;
        masterMixer.audioMixer.SetFloat(MASTER_VOLUME_PARAMETER, actualVolumeValue);
    }
    
    // public void SFXVolumeSliderChanged(float newValue)
    // {
    //     float actualVolumeValue = (1 - newValue) * -40;
    //     mainMixer.audioMixer.SetFloat(SFX_VOLUME_PARAMETER, actualVolumeValue);
    // }

    private void Update()
    {
        masterMixer.audioMixer.SetFloat(CUT_OFF_LOW_PASS_PARAMTER, HZ);
    }
}
