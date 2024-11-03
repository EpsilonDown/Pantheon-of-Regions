namespace PantheonOfRegions;
public sealed partial class PantheonOfRegions 
{

	public List<(string, string)> preloads = new();
    public static Dictionary<string, GameObject> GameObjects = new();

    private Dictionary<string, (string, string)> _preloadDictionary = new()
        {
            #region
            ["broodingmawlek"] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            ["flukemarm"] = ("GG_Flukemarm", "Fluke Mother"),
            ["gorb"] = ("Cliffs_02_boss", "Warrior/Ghost Warrior Slug"),
            ["elderhu"] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ["ringholder"] = ("GG_Ghost_Hu", "Ring Holder"),
            ["markoth"] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            ["marmu"] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            ["noeyes"] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            ["xero"] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            ["greyprincezote"] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            ["hiveknight"] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            ["beedropper"] = ("GG_Hive_Knight", "Battle Scene/Droppers"),
            ["glob"] = ("GG_Hive_Knight", "Battle Scene/Globs"),
            ["hornetprotector"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ["lostkin"] = ("GG_Lost_Kin", "Lost Kin"),
            ["soulwarrior"] = ("GG_Mage_Knight", "Mage Knight"),
            ["galien"] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ["hammer"] = ("GG_Ghost_Galien", "Warrior/Galien Hammer"),
            ["oblobble"] = ("GG_Oblobbles", "Mega Fat Bee"),
            ["sheo"] = ("GG_Painter", "Battle Scene/Sheo Boss"),
	        ["painting"] = ("GG_Painter", "painting_nailsmith_0001_paint2"),
            ["greatnailsagesly"] = ("GG_Sly", "Battle Scene/Sly Boss"),
        ["stunnail"] = ("GG_Sly", "Battle Scene/Stun Nail"),
        ["deathnail"] = ("GG_Sly", "Battle Scene/Death Nail"),
        ["wallspotl"] = ("GG_Sly", "Battle Scene/Wallspot L"),
        ["wallspotr"] = ("GG_Sly", "Battle Scene/Wallspot R"),
        ["hatchercage"] = ("GG_Flukemarm", "Hatcher Cage (2)"),
            ["sibling"] = ("Abyss_15", "Shade Sibling (14)"),
            ["volatilezoteling"] = ("GG_Mighty_Zote", "Battle Control/Zote Balloon Ordeal"),
            ["zoteling"] = ("GG_Mighty_Zote", "Battle Control/Zotelings/Ordeal Zoteling"),
            ["watcherknight"] = ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
            ["wingedsentry"] = ("Ruins2_09", "Battle Scene/Wave 2/Ruins Flying Sentry"),
            ["belfly"] = ("Ruins1_01", "Ceiling Dropper (1)"),
        //["nightmarekinggrimm"] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
        //["nightmaregrimmbats"] = ("GG_Grimm_Nightmare", "Grimm Control/Grimm Bats"),
        //["nightmaregrimmspikeholder"] = ("GG_Grimm_Nightmare", "Grimm Spike Holder"),
        //["nosk"] = ("GG_Nosk", "Mimic Spider")
        #endregion
    };
    private List<(string, string)> GetEnemyPreloads()
    {
        foreach (KeyValuePair<string, (string, string)> boss in _preloadDictionary)
        {
            preloads.Add(boss.Value);
            GameObjects.Add(boss.Key, null);
        }
        return preloads;
    }
    public override List<(string, string)> GetPreloadNames()
    {
        var preloads = GetEnemyPreloads();
        return preloads;
    }
}
