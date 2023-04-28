using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Player player;
    [SerializeField] private Weapon weapon;

    [SerializeField] private Text waveText;
    [SerializeField] private Text ammoText;

    // Update is called once per frame
    void Update()
    {
        waveText.text = $"Wave: {enemySpawner.wave}";
        ammoText.text = $"Ammo: {weapon.Ammo}";
    }
}
