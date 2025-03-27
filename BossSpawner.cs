
namespace PantheonOfRegions;
public class BossSpawner : MonoBehaviour
{
    public GameObject SpawnBoss(string Boss, Vector2 spawnPoint)
    {
        GameObject boss = Instantiate(PantheonOfRegions.GameObjects[Boss], spawnPoint, Quaternion.identity);
        GameObject.DontDestroyOnLoad(boss);
        boss.AddComponent<EnemyTracker>();
        boss.SetActive(false);
        var hm = boss.GetComponent<HealthManager>();
        hm.SetGeoSmall(0);
        hm.SetGeoMedium(0);
        hm.SetGeoLarge(0);

        return boss;
    }
}
