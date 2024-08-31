using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int Maxhp;
    public TMP_Text hpText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text uiText;
    public float speed;
    public GameObject[] weapons;
    public GameObject panel;
    public AudioSource AudioSource { get; set; }
    private int Hp { get; set; }
    public int Score { get; set; }
    public float REMAININGTIME { get; set; }
    private bool _isChange;
    private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        REMAININGTIME = 153f;
        AudioSource = GetComponent<AudioSource>();
        Hp = Maxhp;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (REMAININGTIME > 0)
        {
            REMAININGTIME -= Time.deltaTime;
        }
        else
        {
            GameOver("Time Out...");
        }
        UpdateUI();
        Vector2 mov2 = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 mov3 = new Vector3(mov2.x * Time.deltaTime * speed, 0f, mov2.y * Time.deltaTime * speed);
        _characterController.Move(mov3);
        if (OVRInput.GetDown(OVRInput.Button.One)) // change A
        {
            Change();
        }
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            GameOver("You're died...");
        }
    }
    public void GameOver(string reason)
    {
        panel.SetActive(true);
        RemoveMob();
        uiText.text = $"{reason}\nScore: {Score}\nRemained Time: {(int)(REMAININGTIME / 60):D2}:{(int)(REMAININGTIME % 60):D2}";
    }

    private void UpdateUI()
    {
        hpText.text = $"{Hp}/{Maxhp}";
        scoreText.text = $"{Score}";
        timeText.text = $"{(int)(REMAININGTIME / 60):D2}:{(int)(REMAININGTIME % 60):D2}";
    }

    public void Change()
    {
        _isChange = !_isChange;
        weapons[0].SetActive(!_isChange);
        weapons[1].SetActive(_isChange);
    }
    
    private void RemoveMob()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        
        foreach(GameObject monster in monsters)
        {
            monster.SetActive(false);
        }
    }
}
