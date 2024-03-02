
namespace PantheonOfRegions
{
    public class Setting
    {
		
		public List<string> PantheonRooms = new List<string> {
			"GG_Vengefly_V",
			"GG_Mega_Moss_Charger",
			"GG_Failed_Champion",
			"GG_Mantis_Lords_V",
			"GG_Spa",
			"GG_Crystal_Guardian_2",
			"GG_Soul_Tyrant",
			"GG_Traitor_Lord",
			"GG_Nailmasters",
			"GG_Spa",
			"GG_Uumuu_V",
			"GG_Nosk_V",
			"GG_White_Defender",
			"GG_Hornet_2",
			"GG_Spa",
			"GG_God_Tamer",
			"GG_Collector",
			"GG_Gruz_Mother",
			"GG_Grimm_Nightmare",
			"GG_Spa",
			"GG_Wyrm",
			"GG_Hollow_Knight",
			"GG_Radiance"
        };
		public string ChangeText(string key, string sheetTitle, string orig) => key switch
		{
			"CustomBossDoorTitle" => "Pantheon of",
			"CustomBossDoorSuper" => "Regions",
			"CustomBossDoorDesc" => "Fight Gods Attuned through the Region",
			"VENGEFLY_MAIN" => "Howling",
            "VENGEFLY_SUPER" => "Ascenders",
			"MEGA_MOSS_MAIN" => "Ambushers",
            "MEGA_MOSS_SUPER" => "Green",
			"FALSE_KNIGHT_DREAM_MAIN" => "Guardians of",
            "FALSE_KNIGHT_DREAM_SUB" => "Crossroads",
			"SISTERS_MAIN" => "Alliance",
            "SISTERS_SUB" => "of Battle",
			"ENRAGED_GUARDIAN_SUPER" => "Restless",
            "ENRAGED_GUARDIAN_MAIN" => "Guardians",
			"MAGE_LORD_DREAM_SUPER" => "",
            "MAGE_LORD_DREAM_MAIN" => "Soul Masters",
			"TRAITOR_LORD_MAIN" => "Queen's",
            "TRAITOR_LORD_SUB" => "Tributes",
			"NM_ORO_SUPER" => "Family",
            "NM_ORO_MAIN" => "Nailmasters",
			"MEGA_JELLY_MAIN" => "Blind Protectors",
			"MIMIC_SPIDER_MAIN" => "Stalking Warriors",
			"WHITE_DEFENDER_MAIN" => "Guardians of ",
            "WHITE_DEFENDER_SUB" => "Waterways",
			"HORNET_MAIN" => "Stinger Knights",
            "LOBSTER_LANCER_C_SUPER" => "Champions of",
            "LOBSTER_LANCER_C_MAIN" => "Colosseum",
			"BIGFLY_MAIN" => "Lord of Flies",
            "BIGFLY_SUB" => "",
			"GRIMM_NIGHTMARE_SUPER" => "Reapers of",
            "GRIMM_NIGHTMARE_MAIN" => "Dreams",
            "HK_PRIME_MAIN" => "Void Vessels",
			"ABSOLUTE_RADIANCE_MAIN" => "RADIANCE",
            "ABOLUTE_RADIANCE_SUPER" => "Mother Of all Moths",
				
            _ =>orig

		};

	}
}
