using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSource;
    //_audioSource = new AudioSource[i];
    [SerializeField] private BoolVariables _musicBox;
    [SerializeField] private float _winningTime = 5f;
    [SerializeField] private GameObject _victoryCanvas;
    bool _timerOn;                                                          
    bool won;                                                               
                                                                            
    private void Awake()                                                    
    {                                                                       
        _audioSource = FindObjectsOfType<AudioSource>();                    
    }                                                                       
                                                                            
    private void Start()                                                    
    {                                                                   
        foreach (AudioSource audioSource in _audioSource)               
            audioSource.Play();                                         
    }

    private void Update()
    {
        if (_musicBox._musicBox[0] && _musicBox._musicBox[1] && _musicBox._musicBox[2])
        {
            if (!_timerOn)
            {
                _timerOn = true;
            }
        }
        else
        {
            _timerOn = false;
            _winningTime = 5f;
        }

        if(_timerOn)
        {
            if (_winningTime > 0)
                _winningTime -= Time.deltaTime;
            else
                Won();
        }
    }

    private void Won()
    {
        if (!won)
        {
            won = true;
            _victoryCanvas.SetActive(true);
        }
    }
}
