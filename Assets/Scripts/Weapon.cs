using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float zRotOffset;
    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1f;

    private AudioManager audioManager;
    private bool timeToShoot = true;
    private float timeBtwShots;
    private bool reloadEnded = true;
    private int ammo;

    public int Ammo { private set => ammo = value; get => ammo; }

    public float offset;

    private void Start()
    {
        Ammo = maxAmmo;
        audioManager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShotsHandler();
        RotateWeapon();

        Shoot();
    }

    private void RotateWeapon()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (timeToShoot && reloadEnded)
            {
                timeToShoot = false;
                SpawnProjectile();
                timeBtwShots = startTimeBtwShots;
                audioManager.Play("PlayerFire");
                Ammo--;
                player.ShotsFired += 1;
            }
            else if (Ammo <= 0 && reloadEnded)
            {
                StartCoroutine(ReloadCoroutine(reloadTime));
            }
        }
    }

    private void timeBetweenShotsHandler()
    {
        timeBtwShots -= Time.deltaTime;
        if (timeBtwShots <= 0)
        {
            timeToShoot = true;
        }
    }

    private void SpawnProjectile()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float zRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, zRot + zRotOffset);
        Instantiate(projectile, shotPoint.position, rotation);
    }

    private IEnumerator ReloadCoroutine(float waitTime)
    {
        reloadEnded = false;
        audioManager.Play("ReloadGun");
        yield return new WaitForSeconds(waitTime);
        Ammo = maxAmmo;
        reloadEnded = true;
    }
}
