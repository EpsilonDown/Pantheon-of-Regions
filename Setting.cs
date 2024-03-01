
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
			"VENGEFLY_MAIN" => "Ascenders",
            "VENGEFLY_SUB" => "",
			"MOSSCHARGER_MAIN" => "Green Ambushers",
            "MOSSCHARGER_SUB" => "",
			"FAILED_CHAMPION_SUPER" => "Guardians of",
            "FAILED_CHAMPION_MAIN" => "Crossroads",
			"SISTERS_MAIN" => "Alliance",
            "SISTERS_SUB" => "of Battle",
			"ENRAGED_GUARDIAN_MAIN" => "Restless Guardians",
            "ENRAGED_GUARDIAN_SUB" => "",
			"SOUL_TYRANT_MAIN" => "Soul Masters",
            "SOUL_TYRANT_SUB" => "",
			"TRAITOR_LORD_MAIN" => "Queen's",
            "TRAITOR_LORD_SUB" => "Tributes",
			"NAILMASTERS_MAIN" => "The Nailmasters",
            "NAILMASTERS_SUB" => "",
			"UUMUU_MAIN" => "Blind Protectors",
            "UUMUU_SUB" => "",
			"NOSK_MAIN" => "Stalking Warriors",
            "NOSK_SUB" => "",
			"WHITE_DEFENDER_MAIN" => "Guardians of Waterways",
            "WHITE_DEFENDER_SUB" => "",
			"HORNET_MAIN" => "Stinger",
            "HORNET_SUB" => "Knights",
            "GOD_TAMER_SUPER" => "Champions of",
            "GOD_TAMER_MAIN" => "Colosseum",
			"GRUZ_MOTHER_MAIN" => "Lord of Flies",
            "GRUZ_MOTHER_SUB" => "",
            "GRIMM_NIGHTMARE_MAIN" => "Reapers of Dreams",
            "GRIMM_NIGHTMARE_SUB" => "",
            "PURE_VESSEL_MAIN" => "Void Vessels",
			"ABSOLUTE_RADIANCE_MAIN" => "RADIANCE",
            "ABOLUTE_RADIANCE_SUB" => "Mother of All Moths",
				
            _ =>orig

		};

	}
}
