using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public AudioClip hitSfx;
    private Transform _target;
    private Rigidbody _rigidbody;
    private float _rotate = 10.0f;
    private float _size = 0.1f;
    private Vector3 _aim;
    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
        _aim = _target.position - transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(_target.position);
        StartCoroutine(Resizing());
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.right * _rotate);
    }

    IEnumerator Resizing()
    {
        int idx = 0;
        while (idx <= 10)
        {
            gameObject.transform.localScale += new Vector3(_size, _size, _size);
            yield return new WaitForSeconds(0.1f);
            idx++;
        }
        _rigidbody.AddForce(_aim.normalized * 50.0f, ForceMode.Impulse);
        yield return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(40);
            other.gameObject.GetComponent<Player>().AudioSource.PlayOneShot(hitSfx);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
