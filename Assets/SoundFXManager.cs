using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource _audioObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn in Game Object
        AudioSource audioSource = Instantiate(_audioObject, spawnTransform.position, Quaternion.identity);

        //Assign Audio Clip
        audioSource.clip = audioClip;

        //Assign Volume
        audioSource.volume = volume;

        //Play Sound
        audioSource.Play();

        //Set length
        float clipLength = audioSource.clip.length;

        //Destroy this object when its done playing the sound
        Destroy(audioSource.gameObject, clipLength );

    }
}
