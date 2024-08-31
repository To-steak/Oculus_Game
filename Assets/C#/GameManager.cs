using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsters;
    public GameObject boss;

    private float _time;
    private Player _player;
    private bool _flag;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.REMAININGTIME <= 150f && !_flag)
        {
            _flag = true;
            Instantiate(boss);
        }
        _time += Time.deltaTime;
        var num = Random.Range(15, 21);
        if (_time > num && !_flag)
        {
            _time = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            Instantiate(monsters[Random.Range(0, monsters.Length)], spawnPoint.position, Quaternion.identity);
        }
    }
}
