using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions;

public sealed partial class PantheonOfRegions {

    private static GameObject? MarmuPrefab = null;
    private static GameObject? HuPrefab = null;
    private static GameObject? RingPrefab = null;
    private static GameObject? HivePrefab = null;
	
    private static GameObject? BossInstance = null;
	private static GameObject? RingInstance = null;


	public List<(string, string)> preloads = new();
    private Dictionary<string, (string, string)> _preloadDictionary = new()
        {
            #region
            ["broodingmawlek"] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            ["collector"] = ("GG_Collector_V", "Battle Scene/Jar Collector"),
            ["failedchampion"] = ("GG_Failed_Champion", "False Knight Dream"),
            ["flukemarm"] = ("GG_Flukemarm", "Fluke Mother"),
            ["galien"] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ["hammer"] = ("GG_Ghost_Galien", "Warrior/Galien Hammer"),
            ["gorb"] = ("Cliffs_02_boss", "Warrior/Ghost Warrior Slug"),
            ["elderhu"] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ["ringholder"] = ("GG_Ghost_Hu", "Ring Holder"),
            ["markoth"] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            ["marmu"] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            ["noeyes"] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            ["xero"] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            ["greyprincezote"] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            ["hiveknight"] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
	    ["hornetprotector"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ["lostkin"] = ("GG_Lost_Kin", "Lost Kin"),
            ["soulwarrior"] = ("GG_Mage_Knight", "Mage Knight"),
            ["mato"] = ("GG_Nailmasters", "Brothers/Mato"),
            ["oro"] = ("GG_Nailmasters", "Brothers/Oro"),
            ["nosk"] = ("GG_Nosk", "Mimic Spider"),
            ["oblobble"] = ("GG_Oblobbles", "Mega Fat Bee"),
            ["paintmastersheo"] = ("GG_Painter", "Battle Scene/Sheo Boss"),
            ["greatnailsagesly"] = ("GG_Sly", "Battle Scene/Sly Boss"),
            ["hatchercage"] = ("GG_Flukemarm", "Hatcher Cage (2)"),
            ["sibling"] = ("Abyss_15", "Shade Sibling (14)"),
            #endregion
        };
	
	foreach (KeyValuePair<string, string> Boss in _preloadDictionary)
	{
		preloads.Add(_preloadDictionary[Boss]);
        GameObjects.Add(Boss, null);
	}
		
	private GameObject SpawnBoss(string Boss, Vector2 spawnPoint) 
    {
		GameObject boss = Instantiate(PantheonOfRegions.GameObjects[Boss], spawnPoint, Quaternion.identity);
        enemy.SetActive(false);
        enemy.AddComponent<EnemyTracker>();
        var hm = enemy.GetComponent<HealthManager>();
        hm.SetGeoSmall(0);
        hm.SetGeoMedium(0);
        hm.SetGeoLarge(0);

        return boss;
    }
}
