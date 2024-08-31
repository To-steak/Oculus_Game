using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int damage;
    public AudioClip explosionSfx;
    
    private Rigidbody _rigidbody;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        var d = _target.position - this.transform.position;
        transform.LookAt(_target);
        _rigidbody.AddForce(d.normalized * 10f, ForceMode.Impulse);
        Destroy(gameObject, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(25);
            other.gameObject.GetComponent<Player>().AudioSource.PlayOneShot(explosionSfx);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
