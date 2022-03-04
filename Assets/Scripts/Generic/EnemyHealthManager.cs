using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    #region Variables
    public int maxHealth=1;
    private int currentHealth;

    public int deathSound;
    public GameObject enemyExplosion;
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
            AudioManager.instance.PlaySFX(true, deathSound);
            Destroy(gameObject);
            GameObject explosion = Instantiate(enemyExplosion, transform.position + new Vector3(0, 1f, 0), transform.rotation);
            Destroy(explosion, 3f);

        }
    }
    #endregion
}
