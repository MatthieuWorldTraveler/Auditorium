using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSync : MonoBehaviour
{
    [SerializeField] private Material _allume, _eteint;

    [SerializeField] private Renderer[] _volumeBarre;
    [SerializeField] private BoolVariables _MusicBox;
    [SerializeField] private int _id;
    AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _volumeBarre = GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        VolumeCheck();
        _MusicBox._musicBox[_id] = false;
    }

    public void VolumeCheck()
    {
        for (int i = 1; i < _volumeBarre.Length; i++)
        {
            if (_audioSource.volume >= Mathf.Abs(i * 0.2f))
            {
                _volumeBarre[i].material = _allume;
                //Debug.Log(_volumeBarre[i].material + "True : " + i + " // Audio volume : " + _audioSource.volume + "/ Valeur paramètre if : " + i * 0.2f);
                if(_audioSource.volume == 1)
                {
                    _MusicBox._musicBox[_id] = true;
                }
                else
                {
                    _MusicBox._musicBox[_id] = false;
                }
            }
            else
            {
                _volumeBarre[i].material = _eteint; 
                //Debug.Log(_volumeBarre[i].material + "False : " + i + " // Audio volume : " + _audioSource.volume + "/ Valeur paramètre if : " + i * 0.2f);
            }
        }
    }
}
