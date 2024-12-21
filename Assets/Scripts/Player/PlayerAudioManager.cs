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
        _footstepSfxList = new List<AudioClip>();
        _shootSfxList = new List<AudioClip>();
    }

    public void GetRandomShootSfx()
    {
        if (_shootSfxList.Count <= 0) { return; }
        int random = Random.Range(0, _shootSfxList.Count - 1);
        _audioSource.PlayOneShot(_shootSfxList[random]);
    }

    public void GetRandomFootstepSfx()
    {
        if (_footstepSfxList.Count <= 0) { return; }
        int random = Random.Range(0, _footstepSfxList.Count - 1);
        _audioSource.PlayOneShot(_footstepSfxList[random]);
    }

}
