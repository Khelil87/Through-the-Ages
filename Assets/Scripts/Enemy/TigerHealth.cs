using UnityEngine;
using UnityEngine.SceneManagement;

public class TigerHealth : MonoBehaviour
{
    public float health = 30f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
	
    void Die()
    {
        //Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
