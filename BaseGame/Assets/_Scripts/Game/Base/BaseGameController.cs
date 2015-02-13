using UnityEngine;
using System.Collections;

public class BaseGameController : MonoBehaviour {

    bool paused;
    public GameObject explosionPrefab;

    public virtual void PlayerLostLife()
    { }
    public virtual void SpawnPlayer() { }
    public virtual void Respawn(){}
    public virtual void StartGame() { }
    public void Exploed(Vector3 aPosition) 
    { 
        Instantiate(explosionPrefab, aPosition, Quaternion.identity);
    }
    public virtual void EnemyDestroyed(Vector3 aPosition, int pointsValue, int hitByID) { }
    public virtual void RestartGameButtonPressed()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

}
