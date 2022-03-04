using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    #region Variables
    public int maxHealth=1;
    private int currentHealth;

    public int deathSound;
    public Animator skeletonAnimator;
    public GameObject enemyExplosion;
    public GameObject lootToDrop;
    public int numberOfLootToDrop;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            skeletonAnimator.SetTrigger("Death");
            AudioManager.instance.PlaySFX(true, deathSound);
            Destroy(gameObject, 0.5f);
            DropLoot(lootToDrop, numberOfLootToDrop);
            GameObject explosion = Instantiate(enemyExplosion, transform.position + new Vector3(0, 1f, 0), transform.rotation);
            Destroy(explosion, 3f);
        }
    }

    /// <summary>
    /// Instantiate a number of GameObject at a random range of (-1f,1f) from the holder's transform position.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="numberToDrop"></param>
    public void DropLoot(GameObject item, int numberToDrop)
    {
        GameObject[] lootsToDrop = new GameObject[numberToDrop];
        for (int i = 0; i < numberOfLootToDrop; i++) lootsToDrop[i] = item;
        foreach (GameObject loot in lootsToDrop)
        {
            Instantiate(loot, transform.position + new Vector3(Random.Range(-1f, 1f), 0.4f, Random.Range(-1f, 1f)), transform.rotation);
        }
    }
    #endregion
}
