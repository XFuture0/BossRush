using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingleTons<AudioManager>
{
   [Header("ÒôÆµµ÷½ÚÆ÷")]
   public AudioMixer MainAudio;
   public void SetMainAudioVolume()
   {
        var NewVolume = GameManager.Instance.GlobalData.AudioVolume * 100 - 80;
       MainAudio.SetFloat("MasterVolume",NewVolume);
   }
}
