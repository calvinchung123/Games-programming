using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public static float speed = 20f;

    public static int starthealth = 100;
    public float health;
    public int value = 10;
    private Transform target;
	private int wavepointIndex = 0;

    [Header("Unity Things")]
    public Image HealthBar;

    void Start()
    {
		target = Waypoints_Red.points[0];
        health = starthealth;
    }

	public void TakeDamage(float amount)
    {
        health -= amount;
        
        HealthBar.fillAmount = health / starthealth;
        
        if (health <= 0)
        {
            Die();
        }
     }
 
	void Die()
    {
        RedPlayerStats.Money += value;
        Destroy(gameObject);
        GameControl.EnemiesAlive--;
    }

    void Update() {
		Vector3 dir = target.position  - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.5f) {
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint() {

		if (wavepointIndex >= Waypoints_Red.points.Length - 1) {
            EndPath();
            return;
		}
        else {
            wavepointIndex++;
            target = Waypoints_Red.points[wavepointIndex];
        }
	}

    void EndPath()
    {
        RedPlayerStats.Lives--;
        GameControl.EnemiesAlive--;
        Debug.Log(RedPlayerStats.Lives);
        Destroy(gameObject);
    }

}