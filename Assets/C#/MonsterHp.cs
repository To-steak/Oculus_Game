using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MonsterHp : MonoBehaviour
{
    public GameObject deathEffect;
    public int maxHp;
    public bool isBoss;
    public int CurHp { get; set; }
    public int score;
    public AudioClip deadSfx;

    private Animator _animator;
    private NavMeshAgent _navmeshagent;
    private BoxCollider _boxCollider;
    private SphereCollider _sphereCollider;
    private Enemy _enemy;
    private EnemyBoss _enemyBoss;
    private Player _player;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        CurHp = maxHp;
        _animator = GetComponentInChildren<Animator>();
        _navmeshagent = GetComponent<NavMeshAgent>();
        _boxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (isBoss)
        {
            _enemyBoss = GetComponent<EnemyBoss>();
        }
        else
        {
            _enemy = GetComponent<Enemy>();
            _sphereCollider = GetComponent<SphereCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        if (CurHp <= 0)
        {
            CurHp = 0;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("IsDie");
        _navmeshagent.enabled = false;
        _boxCollider.enabled = false;
        if (isBoss)
        {
            _enemyBoss.enabled = false;
            _audioSource.PlayOneShot(deadSfx);
            _player.GameOver("You're survived!!!");
        }
        else
        {
            _enemy.enabled = false;
            _sphereCollider.enabled = false;
            _audioSource.PlayOneShot(deadSfx);
        }

        _player.Score += this.score;
        yield return new WaitForSeconds(2.0f);
        Instantiate(deathEffect, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
