using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubMachineGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform pivot;
    public TMP_Text ammoText;
    public AudioClip reloadSfx;

    private const float AttackSpeed = 0.25f;
    private const int MaxAmmo = 30;
    private int _currentAmmo;
    private float _time = 0f;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentAmmo = MaxAmmo;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        // if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        if(Input.GetButton("Fire1"))
        {
            if (_time >= AttackSpeed && _currentAmmo > 0)
            {
                _time = 0;
                _currentAmmo--;
                _audioSource.Play();
                Instantiate(bullet, pivot.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pivot.forward * 100f, ForceMode.Impulse);;
                OVRInput.SetControllerVibration(0.3f, 0.2f);
                UpdateUI();
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Two)) // reload B
        {
            Reloading();
        }
    }
    public void Reloading()
    {
        ammoText.text = $"Reloading...!";
        _audioSource.PlayOneShot(reloadSfx);
        StartCoroutine(OnReloading());
    }

    IEnumerator OnReloading()
    {
        yield return new WaitForSeconds(3.0f);
        _currentAmmo = MaxAmmo;
        UpdateUI();
    }
    private void UpdateUI()
    {
        ammoText.text = $"{_currentAmmo} / {MaxAmmo}";
    }
}
