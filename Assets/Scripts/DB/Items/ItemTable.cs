﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using UnityEngine;

public class Item {
    public int id;
    public string unidentifiedDisplayName = "";
    public string unidentifiedResourceName = "";
    public string unidentifiedDescriptionName = "";
    public string identifiedDisplayName = "";
    public string identifiedResourceName = "";
    public string identifiedDescriptionName = "";
    public int slotCount = 0;
    public int ClassNum = 0;
    public bool costume = false;
}

public enum EquipmentLocation : int {
    HEAD_BOTTOM = 1 << 0,
    WEAPON = 1 << 1,
    GARMENT = 1 << 2,
    ACCESSORY1 = 1 << 3,
    ARMOR = 1 << 4,
    SHIELD = 1 << 5,
    SHOES = 1 << 6,
    ACCESSORY2 = 1 << 7,
    HEAD_TOP = 1 << 8,
    HEAD_MID = 1 << 9,
    AMMO = 1 << 15
}

public enum ItemType : int {
    HEALING = 0,
    USABLE = 2,
    ETC = 3,
    WEAPON = 4,
    EQUIP = 5,
    CARD = 6,
    PETEGG = 7,
    PETEQUIP = 8,
    AMMO = 10,
    USABLE_SKILL = 11,
    USABLE_UNK = 18
}

public enum WeaponType : int {
    WEAPONTYPE_NONE = 0,
    WEAPONTYPE_SHORTSWORD = 1,
    WEAPONTYPE_SWORD = 2,
    WEAPONTYPE_TWOHANDSWORD = 3,
    WEAPONTYPE_SPEAR = 4,
    WEAPONTYPE_TWOHANDSPEAR = 5,
    WEAPONTYPE_AXE = 6,
    WEAPONTYPE_TWOHANDAXE = 7,
    WEAPONTYPE_MACE = 8,
    WEAPONTYPE_TWOHANDMACE = 9,
    WEAPONTYPE_ROD = 10,
    WEAPONTYPE_BOW = 11,
    WEAPONTYPE_KNUKLE = 12,
    WEAPONTYPE_INSTRUMENT = 13,
    WEAPONTYPE_WHIP = 14,
    WEAPONTYPE_BOOK = 15,
    WEAPONTYPE_CATARRH = 16,
    WPCLASS_GUN_HANDGUN = 17,
    WPCLASS_GUN_RIFLE = 18,
    WPCLASS_GUN_GATLING = 19,
    WPCLASS_GUN_SHOTGUN = 20,
    WPCLASS_GUN_GRANADE = 21,
    WPCLASS_SYURIKEN = 22,
    WPCLASS_TWOHANDROD = 23,
    WPCLASS_LAST = 24,
    WEAPONTYPE_SHORTSWORD_SHORTSWORD = 25,
    WEAPONTYPE_SWORD_SWORD = 26,
    WEAPONTYPE_AXE_AXE = 27,
    WEAPONTYPE_SHORTSWORD_SWORD = 28,
    WEAPONTYPE_SHORTSWORD_AXE = 29,
    WEAPONTYPE_SWORD_AXE = 30,
    WEAPONTYPE_Main_Gauche = 31,
    WEAPONTYPE_Stiletto = 32,
    WEAPONTYPE_Gladius = 33,
    WEAPONTYPE_Zeny_Knife = 34,
    WEAPONTYPE_Poison_Knife = 35,
    WEAPONTYPE_Princess_Knife = 36,
    WEAPONTYPE_Sasimi = 37,
    WEAPONTYPE_Lacma = 38,
    WEAPONTYPE_Tsurugi = 39,
    WEAPONTYPE_Ring_Pommel_Saber = 40,
    WEAPONTYPE_Haedonggum = 41,
    WEAPONTYPE_Saber = 42,
    WEAPONTYPE_Jewel_Sword = 43,
    WEAPONTYPE_Gaia_Sword = 44,
    WEAPONTYPE_Twin_Edge_B = 45,
    WEAPONTYPE_Twin_Edge_R = 46,
    WEAPONTYPE_Priest_Sword = 47,
    WEAPONTYPE_Katana = 48,
    WEAPONTYPE_Bastard_Sword = 49,
    WEAPONTYPE_Broad_Sword = 50,
    WEAPONTYPE_Violet_Fear = 51,
    WEAPONTYPE_Lance = 52,
    WEAPONTYPE_Partizan = 53,
    WEAPONTYPE_Trident = 54,
    WEAPONTYPE_Halberd = 55,
    WEAPONTYPE_Crescent_Scythe = 56,
    WEAPONTYPE_Zephyrus = 57,
    WEAPONTYPE_Hammer = 58,
    WEAPONTYPE_Buster = 59,
    WEAPONTYPE_Brood_Axe = 60,
    WEAPONTYPE_Right_Epsilon = 61,
    WEAPONTYPE_Mace = 62,
    WEAPONTYPE_Sword_Mace = 63,
    WEAPONTYPE_Chain = 64,
    WEAPONTYPE_Stunner = 65,
    WEAPONTYPE_Golden_Mace = 66,
    WEAPONTYPE_Iron_Driver = 67,
    WEAPONTYPE_Spanner = 68,
    WEAPONTYPE_Arc_Wand = 69,
    WEAPONTYPE_Mighty_Staff = 70,
    WEAPONTYPE_Blessed_Wand = 71,
    WEAPONTYPE_Bone_Wand = 72,
    WEAPONTYPE_CrossBow = 73,
    WEAPONTYPE_Arbalest = 74,
    WEAPONTYPE_Kakkung = 75,
    WEAPONTYPE_Hunter_Bow = 76,
    WEAPONTYPE_Bow_Of_Rudra = 77,
    WEAPONTYPE_Waghnakh = 78,
    WEAPONTYPE_Knuckle_Duster = 79,
    WEAPONTYPE_Hora = 80,
    WEAPONTYPE_Fist = 81,
    WEAPONTYPE_Claw = 82,
    WEAPONTYPE_Finger = 83,
    WEAPONTYPE_Kaiser_Knuckle = 84,
    WEAPONTYPE_Berserk = 85,
    WEAPONTYPE_Rante = 86,
    WEAPONTYPE_Tail = 87,
    WEAPONTYPE_Whip = 88,
    WEAPONTYPE_Bible = 89,
    WEAPONTYPE_Book_Of_Billows = 90,
    WEAPONTYPE_Book_Of_Mother_Earth = 91,
    WEAPONTYPE_Book_Of_Blazing_Sun = 92,
    WEAPONTYPE_Book_Of_Gust_Of_Wind = 93,
    WEAPONTYPE_Book_Of_The_Apocalypse = 94,
    WEAPONTYPE_Girls_Diary = 95,
    WEAPONTYPE_Staff_Of_Soul = 96,
    WEAPONTYPE_Wizardy_Staff = 97,
    WEAPONTYPE_Spoon = 98,
    WEAPONTYPE_FOXTAIL_BROWN = 99,
    WEAPONTYPE_FOXTAIL_GREEN = 100,
    WEAPONTYPE_CandyCaneRod = 101,
    WEAPONTYPE_FOXTAIL_METAL = 102
}

public class ItemTable {

    public static Dictionary<int, Item> Items = new Dictionary<int, Item>();

    public static Dictionary<int, string> Shields = new Dictionary<int, string>() {
        { 1, "\xb0\xa1\xb5\xe5" },
        { 2, "\xb9\xf6\xc5\xac\xb7\xaf" },
        { 3, "\xbd\xaf\xb5\xe5" },
        { 4, "\xb9\xcc\xb7\xaf\xbd\xaf\xb5\xe5" }
    };

    public static Dictionary<WeaponType, string> Weapons = new Dictionary<WeaponType, string> {
        { WeaponType.WEAPONTYPE_NONE, "" },
        { WeaponType.WEAPONTYPE_SHORTSWORD, "_단검" },
        { WeaponType.WEAPONTYPE_SWORD, "_검" },
        { WeaponType.WEAPONTYPE_TWOHANDSWORD, "_양손검" },
        { WeaponType.WEAPONTYPE_SPEAR, "_창" },
        { WeaponType.WEAPONTYPE_TWOHANDSPEAR, "_양손창" },
        { WeaponType.WEAPONTYPE_AXE, "_도끼" },
        { WeaponType.WEAPONTYPE_TWOHANDAXE, "_양손도끼" },
        { WeaponType.WEAPONTYPE_MACE, "_클럽" },
        { WeaponType.WEAPONTYPE_TWOHANDMACE, "_클럽" },
        { WeaponType.WEAPONTYPE_ROD, "_롯드" },
        { WeaponType.WEAPONTYPE_BOW, "_활" },
        { WeaponType.WEAPONTYPE_KNUKLE, "_너클" },
        { WeaponType.WEAPONTYPE_INSTRUMENT, "_악기" },
        { WeaponType.WEAPONTYPE_WHIP, "_채찍" },
        { WeaponType.WEAPONTYPE_BOOK, "_책" },
        { WeaponType.WEAPONTYPE_CATARRH, "_카타르_카타르" },
        { WeaponType.WPCLASS_GUN_HANDGUN, "_권총" },
        { WeaponType.WPCLASS_GUN_RIFLE, "_라이플" },
        { WeaponType.WPCLASS_GUN_GATLING, "_기관총" },
        { WeaponType.WPCLASS_GUN_SHOTGUN, "_샷건" },
        { WeaponType.WPCLASS_GUN_GRANADE, "_샷건" },
        { WeaponType.WPCLASS_SYURIKEN, "_수리검" },
        { WeaponType.WPCLASS_TWOHANDROD, "_롯드" },
        { WeaponType.WEAPONTYPE_SHORTSWORD_SHORTSWORD, "_단검_단검" },
        { WeaponType.WEAPONTYPE_SWORD_SWORD, "_검_검" },
        { WeaponType.WEAPONTYPE_AXE_AXE, "_도끼_도끼" },
        { WeaponType.WEAPONTYPE_SHORTSWORD_SWORD, "_단검_검" },
        { WeaponType.WEAPONTYPE_SHORTSWORD_AXE, "_단검_도끼" },
        { WeaponType.WEAPONTYPE_SWORD_AXE, "_검_도끼" },
        { WeaponType.WEAPONTYPE_Main_Gauche, "_1207" },
        { WeaponType.WEAPONTYPE_Stiletto, "_1216" },
        { WeaponType.WEAPONTYPE_Gladius, "_1219" },
        { WeaponType.WEAPONTYPE_Zeny_Knife, "_1238" },
        { WeaponType.WEAPONTYPE_Poison_Knife, "_1239" },
        { WeaponType.WEAPONTYPE_Princess_Knife, "_1240" },
        { WeaponType.WEAPONTYPE_Sasimi, "_1144" },
        { WeaponType.WEAPONTYPE_Lacma, "_13049" },
        { WeaponType.WEAPONTYPE_Tsurugi, "_1119" },
        { WeaponType.WEAPONTYPE_Ring_Pommel_Saber, "_1122" },
        { WeaponType.WEAPONTYPE_Haedonggum, "_1123" },
        { WeaponType.WEAPONTYPE_Saber, "_1126" },
        { WeaponType.WEAPONTYPE_Jewel_Sword, "_1142" },
        { WeaponType.WEAPONTYPE_Gaia_Sword, "_1143" },
        { WeaponType.WEAPONTYPE_Twin_Edge_B, "_13412" },
        { WeaponType.WEAPONTYPE_Twin_Edge_R, "_13413" },
        { WeaponType.WEAPONTYPE_Priest_Sword, "_프리스트의검" },
        { WeaponType.WEAPONTYPE_Katana, "_1116" },
        { WeaponType.WEAPONTYPE_Bastard_Sword, "_1154" },
        { WeaponType.WEAPONTYPE_Broad_Sword, "_1160" },
        { WeaponType.WEAPONTYPE_Violet_Fear, "_1185" },
        { WeaponType.WEAPONTYPE_Lance, "_1410" },
        { WeaponType.WEAPONTYPE_Partizan, "_1457" },
        { WeaponType.WEAPONTYPE_Trident, "_1460" },
        { WeaponType.WEAPONTYPE_Halberd, "_1463" },
        { WeaponType.WEAPONTYPE_Crescent_Scythe, "_1466" },
        { WeaponType.WEAPONTYPE_Zephyrus, "_1468" },
        { WeaponType.WEAPONTYPE_Hammer, "_1354" },
        { WeaponType.WEAPONTYPE_Buster, "_1357" },
        { WeaponType.WEAPONTYPE_Brood_Axe, "_1363" },
        { WeaponType.WEAPONTYPE_Right_Epsilon, "_1366" },
        { WeaponType.WEAPONTYPE_Mace, "_1504" },
        { WeaponType.WEAPONTYPE_Sword_Mace, "_1516" },
        { WeaponType.WEAPONTYPE_Chain, "_1519" },
        { WeaponType.WEAPONTYPE_Stunner, "_1522" },
        { WeaponType.WEAPONTYPE_Golden_Mace, "_1524" },
        { WeaponType.WEAPONTYPE_Iron_Driver, "_1529" },
        { WeaponType.WEAPONTYPE_Spanner, "_1531" },
        { WeaponType.WEAPONTYPE_Spoon, "_16039" },
        { WeaponType.WEAPONTYPE_Arc_Wand, "_1610" },
        { WeaponType.WEAPONTYPE_Mighty_Staff, "_1613" },
        { WeaponType.WEAPONTYPE_Blessed_Wand, "_1614" },
        { WeaponType.WEAPONTYPE_Bone_Wand, "_1615" },
        { WeaponType.WEAPONTYPE_FOXTAIL_BROWN, "_강아지풀_갈색" },
        { WeaponType.WEAPONTYPE_FOXTAIL_GREEN, "_강아지풀_녹색" },
        { WeaponType.WEAPONTYPE_CandyCaneRod, "_캔디케인롯드" },
        { WeaponType.WEAPONTYPE_FOXTAIL_METAL, "_강아지풀_메탈" },
        { WeaponType.WEAPONTYPE_CrossBow, "_1710" },
        { WeaponType.WEAPONTYPE_Arbalest, "_1713" },
        { WeaponType.WEAPONTYPE_Kakkung, "_1714" },
        { WeaponType.WEAPONTYPE_Hunter_Bow, "_1718" },
        { WeaponType.WEAPONTYPE_Bow_Of_Rudra, "_1720" },
        { WeaponType.WEAPONTYPE_Waghnakh, "_1801" },
        { WeaponType.WEAPONTYPE_Knuckle_Duster, "_1803" },
        { WeaponType.WEAPONTYPE_Hora, "_1805" },
        { WeaponType.WEAPONTYPE_Fist, "_1807" },
        { WeaponType.WEAPONTYPE_Claw, "_1809" },
        { WeaponType.WEAPONTYPE_Finger, "_1811" },
        { WeaponType.WEAPONTYPE_Kaiser_Knuckle, "_1813" },
        { WeaponType.WEAPONTYPE_Berserk, "_1814" },
        { WeaponType.WEAPONTYPE_Rante, "_1956" },
        { WeaponType.WEAPONTYPE_Tail, "_1958" },
        { WeaponType.WEAPONTYPE_Whip, "_1960" },
        { WeaponType.WEAPONTYPE_Bible, "_1550" },
        { WeaponType.WEAPONTYPE_Book_Of_Billows, "_1553" },
        { WeaponType.WEAPONTYPE_Book_Of_Mother_Earth, "_1554" },
        { WeaponType.WEAPONTYPE_Book_Of_Blazing_Sun, "_1555" },
        { WeaponType.WEAPONTYPE_Book_Of_Gust_Of_Wind, "_1556" },
        { WeaponType.WEAPONTYPE_Book_Of_The_Apocalypse, "_1557" },
        { WeaponType.WEAPONTYPE_Girls_Diary, "_1558" },
        { WeaponType.WEAPONTYPE_Staff_Of_Soul, "_1472" },
        { WeaponType.WEAPONTYPE_Wizardy_Staff, "_1473" }
    };

    public static Dictionary<WeaponType, WeaponType> ExpansionWeapons = new Dictionary<WeaponType, WeaponType>() {
        { WeaponType.WEAPONTYPE_Main_Gauche, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Stiletto, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Gladius, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Zeny_Knife, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Poison_Knife, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Princess_Knife, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Sasimi, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Lacma, WeaponType.WEAPONTYPE_SHORTSWORD },
        { WeaponType.WEAPONTYPE_Tsurugi, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Ring_Pommel_Saber, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Haedonggum, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Saber, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Jewel_Sword, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Gaia_Sword, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Twin_Edge_B, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Twin_Edge_R, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Priest_Sword, WeaponType.WEAPONTYPE_SWORD },
        { WeaponType.WEAPONTYPE_Katana, WeaponType.WEAPONTYPE_TWOHANDSWORD },
        { WeaponType.WEAPONTYPE_Bastard_Sword, WeaponType.WEAPONTYPE_TWOHANDSWORD },
        { WeaponType.WEAPONTYPE_Broad_Sword, WeaponType.WEAPONTYPE_TWOHANDSWORD },
        { WeaponType.WEAPONTYPE_Violet_Fear, WeaponType.WEAPONTYPE_TWOHANDSWORD },
        { WeaponType.WEAPONTYPE_Lance, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Partizan, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Trident, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Halberd, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Crescent_Scythe, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Zephyrus, WeaponType.WEAPONTYPE_SPEAR },
        { WeaponType.WEAPONTYPE_Hammer, WeaponType.WEAPONTYPE_AXE },
        { WeaponType.WEAPONTYPE_Buster, WeaponType.WEAPONTYPE_AXE },
        { WeaponType.WEAPONTYPE_Brood_Axe, WeaponType.WEAPONTYPE_AXE },
        { WeaponType.WEAPONTYPE_Right_Epsilon, WeaponType.WEAPONTYPE_AXE },
        { WeaponType.WEAPONTYPE_Mace, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Sword_Mace, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Chain, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Stunner, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Golden_Mace, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Iron_Driver, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Spanner, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Spoon, WeaponType.WEAPONTYPE_MACE },
        { WeaponType.WEAPONTYPE_Arc_Wand, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_Mighty_Staff, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_Blessed_Wand, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_Bone_Wand, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_CrossBow, WeaponType.WEAPONTYPE_BOW },
        { WeaponType.WEAPONTYPE_Arbalest, WeaponType.WEAPONTYPE_BOW },
        { WeaponType.WEAPONTYPE_Kakkung, WeaponType.WEAPONTYPE_BOW },
        { WeaponType.WEAPONTYPE_Hunter_Bow, WeaponType.WEAPONTYPE_BOW },
        { WeaponType.WEAPONTYPE_Bow_Of_Rudra, WeaponType.WEAPONTYPE_BOW },
        { WeaponType.WEAPONTYPE_Waghnakh, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Knuckle_Duster, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Hora, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Fist, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Claw, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Finger, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Kaiser_Knuckle, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Berserk, WeaponType.WEAPONTYPE_KNUKLE },
        { WeaponType.WEAPONTYPE_Rante, WeaponType.WEAPONTYPE_WHIP },
        { WeaponType.WEAPONTYPE_Tail, WeaponType.WEAPONTYPE_WHIP },
        { WeaponType.WEAPONTYPE_Whip, WeaponType.WEAPONTYPE_WHIP },
        { WeaponType.WEAPONTYPE_Bible, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Book_Of_Billows, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Book_Of_Mother_Earth, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Book_Of_Blazing_Sun, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Book_Of_Gust_Of_Wind, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Book_Of_The_Apocalypse, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Girls_Diary, WeaponType.WEAPONTYPE_BOOK },
        { WeaponType.WEAPONTYPE_Staff_Of_Soul, WeaponType.WPCLASS_TWOHANDROD },
        { WeaponType.WEAPONTYPE_Wizardy_Staff, WeaponType.WPCLASS_TWOHANDROD },
        { WeaponType.WEAPONTYPE_FOXTAIL_BROWN, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_FOXTAIL_GREEN, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_CandyCaneRod, WeaponType.WEAPONTYPE_ROD },
        { WeaponType.WEAPONTYPE_FOXTAIL_METAL, WeaponType.WEAPONTYPE_ROD }
    };

    public static void LoadItemDb() {
        Script script = new Script();
        script.Options.ScriptLoader = new CustomScriptLoader();
        script.DoFile(Core.Configs["itemInfo"] as string);
        Table table = (Table)script.Globals["tbl"];

        foreach (var key in table.Keys) {
            try {
                var it = table[key] as Table;

                List<string> unidentifiedDescriptionName = new List<string>();
                foreach (var desc in ((Table)it["unidentifiedDescriptionName"]).Values) {
                    unidentifiedDescriptionName.Add(desc.ToString());
                }

                List<string> identifiedDescriptionName = new List<string>();
                foreach (var desc in ((Table)it["identifiedDescriptionName"]).Values) {
                    identifiedDescriptionName.Add(desc.ToString());
                }

                var item = new Item() {
                    id = int.Parse(key.ToString()),
                    unidentifiedDisplayName = it["unidentifiedDisplayName"].ToString(),
                    unidentifiedResourceName = it["unidentifiedResourceName"].ToString(),
                    unidentifiedDescriptionName = string.Join("\n", unidentifiedDescriptionName),
                    identifiedDisplayName = it["identifiedDisplayName"].ToString(),
                    identifiedResourceName = it["identifiedResourceName"].ToString(),
                    identifiedDescriptionName = string.Join("\n", identifiedDescriptionName),
                    slotCount = int.Parse(it["slotCount"].ToString()),
                    ClassNum = int.Parse(it["ClassNum"].ToString()),
                    costume = bool.Parse(it["costume"].ToString())
                };

                Items.Add(item.id, item);
            } catch (Exception e) {
                Debug.LogError($"Could not load item {key} - {e}");
            }
        }
    }
}