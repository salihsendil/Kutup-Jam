using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> _footstepSfxList;
    [SerializeField] private List<AudioClip> _shootSfxList;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void GetRandomShootSfx()
    {
        int random = Random.Range(0, _shootSfxList.Count);
        _audioSource.PlayOneShot(_shootSfxList[random]);
    }

    public void GetRandomFootstepSfx()
    {
        int random = Random.Range(0, _footstepSfxList.Count);
        _audioSource.PlayOneShot(_footstepSfxList[random]);
    }

}
