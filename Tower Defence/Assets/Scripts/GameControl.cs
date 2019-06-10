﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform redSpawnPoint;


    public float timeBetWaves = 0.1f;
    private float countDown = 2f;

    public Text redwaveCountdownText;

    static string leveltwo;
    static int waveIndex = 0;

    void Update()
    {
        if(EnemiesAlive > 0 )
        {
            return;
        }
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetWaves;
            return;
        }
       
        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        redwaveCountdownText.text = string.Format("{0:0.0}", countDown);

    }
   
    IEnumerator SpawnWave()
    {
        
        RedPlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        Enemy.starthealth += 3;

        Enemy.speed += 0.2f;

        Debug.Log(Enemy.speed);
        Debug.Log(Enemy.starthealth);

        for (int i = 0; i < wave.count; i++)
        {
            
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            
        }
        waveIndex++;

    }
        
            void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, redSpawnPoint.position, redSpawnPoint.rotation);
        EnemiesAlive++;
    }

}