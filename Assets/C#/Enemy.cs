using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private Transform _target;
    private NavMeshAgent _navmeshagent;
    private const float Attackspeed = 3.0f;
    private float _time = 2.9f;
    private AudioSource _audioSource;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _navmeshagent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_navmeshagent.enabled)
        {
            if (_navmeshagent.SetDestination(_target.position))
            {
                _animator.SetBool("IsWalk", true);
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("IsWalk", false);
            _time += Time.deltaTime;
            if (_time >= Attackspeed)
            {
                _time = 0;
                _animator.SetTrigger("IsAttack");
                _audioSource.Play();
                other.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _time = 2.9f;
        }
    }
}
