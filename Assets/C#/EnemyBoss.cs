using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyBoss : MonoBehaviour
{
    public GameObject missile;
    public GameObject rock;
    public Transform leftPivot;
    public Transform rightPivot;
    public Transform centerPivot;
    public AudioClip tauntSfx;
    public AudioClip shotSfx;
    public AudioClip bigSfx;
    
    private Animator _animator;
    private Transform _target;
    private const float AttackSpeed = 3.0f;
    private float _time = 0;
    private bool _isFixed;

    private AudioSource _audioSource;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (!_isFixed) gameObject.transform.LookAt(_target);
        if (_time >= AttackSpeed)
        {
            _time = 0;
            Pattern();
        }
    }

    private void Pattern()
    {
        Action action = Random.Range(0, 3) switch
        {
            0 => () => Taunt(),
            1 => () => Shot(),
            2 => () => BigShot(),
            _ => throw new ArgumentOutOfRangeException()
        };
        action();
    }

    private void Taunt()
    {
        _animator.SetTrigger("IsTaunt");
        StartCoroutine(OnTaunt());
    }

    IEnumerator OnTaunt()
    {
        yield return new WaitForSeconds(1.5f);
        _audioSource.PlayOneShot(tauntSfx);
        _gameManager.Spawn();
    }
    private void Shot()
    {
        _animator.SetTrigger("IsShot");
        _audioSource.PlayOneShot(shotSfx);
        Instantiate(missile, leftPivot.position, Quaternion.identity);
        Instantiate(missile, rightPivot.position, Quaternion.identity);
    }

    private void BigShot()
    {
        _animator.SetTrigger("IsBigShot");
        StartCoroutine(OnBigShot());
    }

    IEnumerator OnBigShot()
    {
        yield return new WaitForSeconds(0.8f);
        _isFixed = true;
        Instantiate(rock, centerPivot.position, Quaternion.identity);
        _audioSource.PlayOneShot(bigSfx);
        
        yield return new WaitForSeconds(1.2f);
        _isFixed = false;
    }
}
