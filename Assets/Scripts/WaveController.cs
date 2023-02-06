using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    [Serializable]
    public class EnemySet
    {
        public float spawnTime;

        public int numFirstEnemy;
        public Enemy typeFirstEnemy;

        public int numSecondEnemy;
        public Enemy typeSecondEnemy;
    }
    public bool gameWaiting = true;
    public int waveIndex;
    public List<EnemySet> waves;
    public EnemySet currentWave => waves[waveIndex];
    public List<Enemy> currentEnemies;
    public List<Enemy> activeWave;
    public Dictionary<string, Queue<Enemy>> poolDictionary;
    public static WaveController instance;
    public Transform spawnPosition;
    private Animator portalAnim;
    public UnityEvent onAllWavesDefeat;
    public Image enemyImageOne;
    public Image enemyImageTwo;
    private IEnumerator summonWaves;

    public void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<Enemy>>();
        if (!instance)
            instance = this;
    }

    private void Start()
    {
        InitEnemyPools();
        portalAnim = GetComponent<Animator>();
        activeWave = new List<Enemy>();

        summonWaves = SummonEnemyWaves();
        StartCoroutine(summonWaves);
    }

    public Dictionary<Enemy, int> EnemyPools;

    public void InitEnemyPools()
    {
        EnemyPools = new Dictionary<Enemy, int>();

        foreach (EnemySet set in waves)
        {
            if (!EnemyPools.ContainsKey(set.typeFirstEnemy))
            {
                EnemyPools.Add(set.typeFirstEnemy, set.numFirstEnemy);
            }
            else
            {
                EnemyPools[set.typeFirstEnemy] = Mathf.Max(EnemyPools[set.typeFirstEnemy], set.numFirstEnemy);
            }

            if (!EnemyPools.ContainsKey(set.typeSecondEnemy))
            {
                EnemyPools.Add(set.typeSecondEnemy, set.numSecondEnemy);
            }
            else
            {
                EnemyPools[set.typeSecondEnemy] = Mathf.Max(EnemyPools[set.typeSecondEnemy], set.numSecondEnemy);
            }
        }

        foreach(KeyValuePair<Enemy, int> v in EnemyPools)
        {
            InitPool(v.Key.gameObject.name, v.Key, v.Value);
        }
    }

    public void InitPool(string tag, Enemy prefab, int count)
    {
        if (poolDictionary.ContainsKey(tag))
            return;

        Queue<Enemy> objectPool = new Queue<Enemy>();
        for (int i = 0; i < count; i++)
        {
            Enemy obj = Instantiate(prefab);

            EventManager.instance.OnEnemySpawn(obj);

            obj.name = prefab.name;
            obj.transform.parent = transform;
            obj.transform.position = Vector3.one * 100;
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(tag, objectPool);
    }


    public void AddToWave(Enemy enemy, bool callSpawn = true)
    {
        enemy.gameObject.SetActive(true);
        activeWave.Add(enemy);

        if (callSpawn)
        {
            IPooledObject pooledObj = enemy.GetComponent<IPooledObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
        }

        
    }

    public void SkipWait()
    {
        gameWaiting = false;
    }
    

    public IEnumerator SummonEnemyWaves()
    {
        while (!GameManager.gameOver)
        {
            //Grab the enemies for the next wave.
            currentEnemies = GetWaveEnemies();

            while (gameWaiting)
            {
                yield return new WaitForEndOfFrame();
            }

            portalAnim.SetTrigger("Open");
            yield return new WaitForSeconds(1);

            for (int i = 0; i < currentEnemies.Count; i++)
            {
                yield return new WaitForSeconds(currentWave.spawnTime);

                Enemy nextEnemy = currentEnemies[i];
                AddToWave(nextEnemy);
                nextEnemy.transform.position = spawnPosition.position;
            }

            portalAnim.SetTrigger("Close");

            while (activeWave.Count > 0)
            {
                yield return null;
            }

            gameWaiting = true;
            if (!GameManager.gameOver)
            {
                waveIndex++;
                if (waveIndex >= waves.Count)
                {
                    onAllWavesDefeat.Invoke();
                }
            }
        }
    }

    public void OnObjectDespawn(Enemy despawnedObject)
    {
        Debug.Log("Despawning Enemy: " + despawnedObject);

        activeWave.Remove(despawnedObject);

        if (!poolDictionary.ContainsKey(despawnedObject.name))
        {
            Destroy(despawnedObject);
        }
        else
        {
            despawnedObject.OnObjectDespawn();

            Queue<Enemy> enemyPool = poolDictionary[despawnedObject.name];
            enemyPool.Enqueue(despawnedObject);
        }
    }

    private List<Enemy> GetWaveEnemies()
    {
        List<Enemy> ret = new List<Enemy>();
        string enemyname = currentWave.typeFirstEnemy.name;
        Queue<Enemy> usedPool = poolDictionary[enemyname];
        for (int i = 0; i < currentWave.numFirstEnemy; i++)
        {
            if (!(usedPool.Count <= 0))
            {
                Enemy anEnemy = usedPool.Dequeue();
                if (anEnemy)
                {
                    enemyImageOne.enabled = true;
                    enemyImageOne.sprite = anEnemy.rend.sprite;
                    ret.Add(anEnemy);
                }
        
            }
            else
            {
                Debug.LogError("Can't get enough enemies for wave: " + waveIndex);
            }
        }

        if (currentWave.typeSecondEnemy)
        {
            enemyname = currentWave.typeSecondEnemy.name;
            usedPool = poolDictionary[enemyname];
            for (int i = 0; i < currentWave.numSecondEnemy; i++)
            {
                if (!(usedPool.Count <= 0))
                {
                    Enemy anEnemy = usedPool.Dequeue();

                    if (anEnemy)
                    {
                        enemyImageTwo.enabled = true;
                        enemyImageTwo.sprite = anEnemy.rend.sprite;
                        ret.Add(anEnemy);
                    }
                }
                else
                {
                    Debug.LogError("Can't get enough enemies for wave: " + waveIndex);
                }
            }
        }
       

        return ret;
    }
}

