using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using Utilities;
using AMP_Configurable.Patches;

namespace AMP_Configurable
{
    [BepInPlugin("AMP_Configurable", "Auto Map Pins", "1.2.2")]
    [BepInProcess("valheim.exe")]
    public class Mod : BaseUnityPlugin
    {
        //***CONFIG ENTRIES***//
        //***GENERAL***//
        public static ManualLogSource Log;
        public static ConfigEntry<int> nexusID;
        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<float> pinClusterDistance;
        public static ConfigEntry<int> pinIcons;
        public static ConfigEntry<int> pinRange;
        public static ConfigEntry<bool> hideAllNames;
        //***ORES***//
        public static ConfigEntry<bool> pinCopper;
        public static ConfigEntry<bool> saveCopper;
        public static ConfigEntry<bool> showCopperName;
        public static ConfigEntry<string> copperName;
        public static ConfigEntry<bool> countCopperClusters;
        public static ConfigEntry<float> pinCopperSize;
        public static ConfigEntry<float> copperClusterDistance;
        public static ConfigEntry<bool> pinTin;
        public static ConfigEntry<bool> saveTin;
        public static ConfigEntry<bool> showTinName;
        public static ConfigEntry<string> tinName;
        public static ConfigEntry<bool> countTinClusters;
        public static ConfigEntry<float> pinTinSize;
        public static ConfigEntry<float> tinClusterDistance;
        public static ConfigEntry<bool> pinObsidian;
        public static ConfigEntry<bool> saveObsidian;
        public static ConfigEntry<bool> showObsidianName;
        public static ConfigEntry<string> obsidianName;
        public static ConfigEntry<bool> countObsidianClusters;
        public static ConfigEntry<float> pinObsidianSize;
        public static ConfigEntry<float> obsidianClusterDistance;
        public static ConfigEntry<bool> pinSilver;
        public static ConfigEntry<bool> saveSilver;
        public static ConfigEntry<bool> showSilverName;
        public static ConfigEntry<string> silverName;
        public static ConfigEntry<bool> countSilverClusters;
        public static ConfigEntry<float> pinSilverSize;
        public static ConfigEntry<float> silverClusterDistance;
        public static ConfigEntry<bool> pinIron;
        public static ConfigEntry<bool> saveIron;
        public static ConfigEntry<bool> showIronName;
        public static ConfigEntry<string> ironName;
        public static ConfigEntry<bool> countIronClusters;
        public static ConfigEntry<float> pinIronSize;
        public static ConfigEntry<float> ironClusterDistance;
        public static ConfigEntry<bool> pinFlametal;
        public static ConfigEntry<bool> saveFlametal;
        public static ConfigEntry<bool> showFlametalName;
        public static ConfigEntry<string> flametalName;
        public static ConfigEntry<bool> countFlametalClusters;
        public static ConfigEntry<float> pinFlametalSize;
        public static ConfigEntry<float> flametalClusterDistance;

        //***PICKABLES***//
        public static ConfigEntry<bool> pinBerries;
        public static ConfigEntry<bool> saveBerries;
        public static ConfigEntry<bool> showBerriesName;
        public static ConfigEntry<string> berriesName;
        public static ConfigEntry<bool> countBerriesClusters;
        public static ConfigEntry<float> pinBerriesSize;
        public static ConfigEntry<float> berriesClusterDistance;
        public static ConfigEntry<bool> pinBlueberries;
        public static ConfigEntry<bool> saveBlueberries;
        public static ConfigEntry<bool> showBlueberriesName;
        public static ConfigEntry<string> blueberriesName;
        public static ConfigEntry<bool> countBlueberriesClusters;
        public static ConfigEntry<float> pinBlueberriesSize;
        public static ConfigEntry<float> blueberriesClusterDistance;
        public static ConfigEntry<bool> pinCloudberries;
        public static ConfigEntry<bool> saveCloudberries;
        public static ConfigEntry<bool> showCloudberriesName;
        public static ConfigEntry<string> cloudberriesName;
        public static ConfigEntry<bool> countCloudberriesClusters;
        public static ConfigEntry<float> pinCloudberriesSize;
        public static ConfigEntry<float> cloudberriesClusterDistance;
        public static ConfigEntry<bool> pinThistle;
        public static ConfigEntry<bool> saveThistle;
        public static ConfigEntry<bool> showThistleName;
        public static ConfigEntry<string> thistleName;
        public static ConfigEntry<bool> countThistleClusters;
        public static ConfigEntry<float> pinThistleSize;
        public static ConfigEntry<float> thistleClusterDistance;
        public static ConfigEntry<bool> pinMushroom;
        public static ConfigEntry<bool> saveMushroom;
        public static ConfigEntry<bool> showMushroomName;
        public static ConfigEntry<string> mushroomName;
        public static ConfigEntry<bool> countMushroomClusters;
        public static ConfigEntry<float> pinMushroomSize;
        public static ConfigEntry<float> mushroomClusterDistance;
        public static ConfigEntry<bool> pinCarrot;
        public static ConfigEntry<bool> saveCarrot;
        public static ConfigEntry<bool> showCarrotName;
        public static ConfigEntry<string> carrotName;
        public static ConfigEntry<bool> countCarrotClusters;
        public static ConfigEntry<float> pinCarrotSize;
        public static ConfigEntry<float> carrotClusterDistance;
        public static ConfigEntry<bool> pinTurnip;
        public static ConfigEntry<bool> saveTurnip;
        public static ConfigEntry<bool> showTurnipName;
        public static ConfigEntry<string> turnipName;
        public static ConfigEntry<bool> countTurnipClusters;
        public static ConfigEntry<float> pinTurnipSize;
        public static ConfigEntry<float> turnipClusterDistance;
        
        public static ConfigEntry<bool> pinOnion;
        public static ConfigEntry<bool> saveOnion;
        public static ConfigEntry<bool> showOnionName;
        public static ConfigEntry<string> onionName;
        public static ConfigEntry<bool> countOnionClusters;
        public static ConfigEntry<float> pinOnionSize;
        public static ConfigEntry<float> onionClusterDistance;

        public static ConfigEntry<bool> pinDragonEgg;
        public static ConfigEntry<bool> saveDragonEgg;
        public static ConfigEntry<bool> showDragonEggName;
        public static ConfigEntry<string> dragonEggName;
        public static ConfigEntry<bool> countDragonEggClusters;
        public static ConfigEntry<float> pinDragonEggSize;
        public static ConfigEntry<float> dragonEggClusterDistance;

        //***LOCATIONS***//
        public static ConfigEntry<bool> pinCrypt;
        public static ConfigEntry<bool> saveCrypt;
        public static ConfigEntry<bool> showCryptName;
        public static ConfigEntry<string> cryptName;
        public static ConfigEntry<bool> countCryptClusters;
        public static ConfigEntry<float> pinCryptSize;
        public static ConfigEntry<float> cryptClusterDistance;
        public static ConfigEntry<bool> pinSunkenCrypt;
        public static ConfigEntry<bool> saveSunkenCrypt;
        public static ConfigEntry<bool> showSunkenCryptName;
        public static ConfigEntry<string> sunkenCryptName;
        public static ConfigEntry<bool> countSunkenCryptClusters;
        public static ConfigEntry<float> pinSunkenCryptSize;
        public static ConfigEntry<float> sunkenCryptClusterDistance;
        public static ConfigEntry<bool> pinTrollCave;
        public static ConfigEntry<bool> saveTrollCave;
        public static ConfigEntry<bool> showTrollCaveName;
        public static ConfigEntry<string> trollCaveName;
        public static ConfigEntry<bool> countTrollCaveClusters;
        public static ConfigEntry<float> pinTrollCaveSize;
        public static ConfigEntry<float> trollCaveClusterDistance;

        //***SPAWNERS***//
        public static ConfigEntry<bool> pinSkeleton;
        public static ConfigEntry<bool> saveSkeleton;
        public static ConfigEntry<bool> showSkeletonName;
        public static ConfigEntry<string> skeletonName;
        public static ConfigEntry<bool> countSkeletonClusters;
        public static ConfigEntry<float> pinSkeletonSize;
        public static ConfigEntry<float> skeletonClusterDistance;
        public static ConfigEntry<bool> pinDraugr;
        public static ConfigEntry<bool> saveDraugr;
        public static ConfigEntry<bool> showDraugrName;
        public static ConfigEntry<string> draugrName;
        public static ConfigEntry<bool> countDraugrClusters;
        public static ConfigEntry<float> pinDraugrSize;
        public static ConfigEntry<float> draugrClusterDistance;
        public static ConfigEntry<bool> pinSurtling;
        public static ConfigEntry<bool> saveSurtling;
        public static ConfigEntry<bool> showSurtlingName;
        public static ConfigEntry<string> surtlingName;
        public static ConfigEntry<bool> countSurtlingClusters;
        public static ConfigEntry<float> pinSurtlingSize;
        public static ConfigEntry<float> surtlingClusterDistance;
        public static ConfigEntry<bool> pinGreydwarf;
        public static ConfigEntry<bool> saveGreydwarf;
        public static ConfigEntry<bool> showGreydwarfName;
        public static ConfigEntry<string> greydwarfName;
        public static ConfigEntry<bool> countGreydwarfClusters;
        public static ConfigEntry<float> pinGreydwarfSize;
        public static ConfigEntry<float> greydwarfClusterDistance;
        public static ConfigEntry<bool> pinSerpent;
        public static ConfigEntry<bool> saveSerpent;
        public static ConfigEntry<bool> showSerpentName;
        public static ConfigEntry<string> serpentName;
        public static ConfigEntry<bool> countSerpentClusters;
        public static ConfigEntry<float> pinSerpentSize;
        public static ConfigEntry<float> serpentClusterDistance;

        //***LEVIATHANS***//
        public static ConfigEntry<bool> pinLeviathan;
        public static ConfigEntry<bool> saveLeviathan;
        public static ConfigEntry<bool> showLeviathanName;
        public static ConfigEntry<string> leviathanName;
        public static ConfigEntry<bool> countLeviathanClusters;
        public static ConfigEntry<float> pinLeviathanSize;
        public static ConfigEntry<float> leviathanClusterDistance;

        //***PUBLIC VARIABLES***//
        public static List<Minimap.PinData> autoPins;
        public static List<Minimap.PinData> savedPins;
        public static List<Minimap.PinData> pinRemList;
        public static List<Vector3> addedPinLocs;
        public static List<Vector3> dupPinLocs;
        public static List<string> filteredPins;
        public static Vector3 position;
        public static Dictionary<Vector3, string> pinItems = new Dictionary<Vector3, string>();
        public static Dictionary<Vector3, Minimap.PinData> remPinDict = new Dictionary<Vector3, Minimap.PinData>();
        public static bool hasMoved = false;
        public static bool checkingPins = false;
        public static string currEnv = "";



        private void Awake()
        {
            Log = Logger;

            modEnabled = Config.Bind("General", "Enabled", true, "Enable this mod");
            pinClusterDistance = Config.Bind<float>("General", "PinClusterDistance", 10, "Distance around pins to prevent overlapping of similar pins");
            pinIcons = Config.Bind("General", "PinIcons", 1, "Use [1] Game sprites(copper for copper, silver for silver, etc.) or [2] Colored pins (must be 1 or 2)");
            pinRange = Config.Bind("General", "PinRange", 150, "Sets the range that pins will appear on the mini-map. Lower value means you need to be closer to set pin.\nMin 5\nMax 150\nRecommended 50-75");
            if (pinRange.Value < 5)
                pinRange.Value = 5;
            if (pinRange.Value > 150)
                pinRange.Value = 150;
            hideAllNames = Config.Bind("General", "HideAllNames", false, "This option will hide all names for all pins.\n*THIS WILL OVERRIDE THE INDIVIDUAL SETTINGS*");
            nexusID = Config.Bind("General", "NexusID", 744, "Nexus mod ID for updates");
            //***ORES***//
            pinCopper = Config.Bind("Ores - Copper", "PinCopper", true, "Show pins for Copper");
            saveCopper = Config.Bind("Ores - Copper", "SaveCopper", false, "Save pins for Copper");
            showCopperName = Config.Bind("Ores - Copper", "ShowCopperName", true, "Show name for Copper");
            copperName = Config.Bind("Ores - Copper", "CopperNameDisplay", "Copper", "Display name for Copper");
            countCopperClusters = Config.Bind("Ores - Copper", "countCopperClusters", false, "Add count to name (display name must be enabled)");
            pinCopperSize = Config.Bind<float>("Ores - Copper", "PinCopperSize", 20, "Size of Copper pin on minimap/main Map (20 is recommended)");
            copperClusterDistance = Config.Bind<float>("Ores - Copper", "copperClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinTin = Config.Bind("Ores - Tin", "PinTin", true, "Show pins for Tin");
            saveTin = Config.Bind("Ores - Tin", "SaveTin", false, "Save pins for Tin");
            showTinName = Config.Bind("Ores - Tin", "ShowTinName", true, "Show name for Tin");
            tinName = Config.Bind<string>("Ores - Tin", "TinNameDisplay", "Tin", "Display name for Tin");
            countTinClusters = Config.Bind("Ores - Tin", "countTinClusters", true, "Add count to name (display name must be enabled)");
            pinTinSize = Config.Bind<float>("Ores - Tin", "PinTinSize", 20, "Size of pin Tin on minimap/main Map (20 is recommended)");
            tinClusterDistance = Config.Bind<float>("Ores - Tin", "tinClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinObsidian = Config.Bind("Ores - Obsidian", "PinObsidian", true, "Show pins for Obsidian");
            saveObsidian = Config.Bind("Ores - Obsidian", "SaveObsidian", false, "Save pins for Obsidian");
            showObsidianName = Config.Bind("Ores - Obsidian", "ShowObsidianName", true, "Show name for Obsidian");
            obsidianName = Config.Bind<string>("Ores - Obsidian", "ObsidianNameDisplay", "Obsidian", "Display name for Obsidian");
            countObsidianClusters = Config.Bind("Ores - Obsidian", "countObsidianClusters", true, "Add count to name (display name must be enabled)");
            pinObsidianSize = Config.Bind<float>("Ores - Obsidian", "PinObsidianSize", 20, "Size of Obsidian pin on minimap/main Map (20 is recommended)");
            obsidianClusterDistance = Config.Bind<float>("Ores - Obsidian", "obsidianClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinSilver = Config.Bind("Ores - Silver", "PinSilver", true, "Show pins for Silver");
            saveSilver = Config.Bind("Ores - Silver", "SaveSilver", false, "Save pins for Silver");
            showSilverName = Config.Bind("Ores - Silver", "ShowSilverName", true, "Show name for Silver");
            silverName = Config.Bind<string>("Ores - Silver", "SilverNameDisplay", "Silver", "Display name for Silver");
            countSilverClusters = Config.Bind("Ores - Silver", "countSilverClusters", false, "Add count to name (display name must be enabled)");
            pinSilverSize = Config.Bind<float>("Ores - Silver", "PinSilverSize", 20, "Size of Silver pin on minimap/main Map (20 is recommended)");
            silverClusterDistance = Config.Bind<float>("Ores - Silver", "silverClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinIron = Config.Bind("Ores - Iron", "PinIron", true, "Show pins for Iron");
            saveIron = Config.Bind("Ores - Iron", "SaveIron", false, "Save pins for Iron");
            showIronName = Config.Bind("Ores - Iron", "ShowIronName", true, "Show name for Iron");
            ironName = Config.Bind<string>("Ores - Iron", "IronNameDisplay", "Iron", "Display name for Iron");
            countIronClusters = Config.Bind("Ores - Iron", "countIronClusters", false, "Add count to name (display name must be enabled)");
            pinIronSize = Config.Bind<float>("Ores - Iron", "PinIronSize", 20, "Size of Iron pin on minimap/main Map (20 is recommended)");
            ironClusterDistance = Config.Bind<float>("Ores - Iron", "ironClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinFlametal = Config.Bind("Ores - Flametal", "PinFlametal", true, "Show pins for Flametal");
            saveFlametal = Config.Bind("Ores - Flametal", "SaveFlametal", false, "Save pins for Flametal");
            showFlametalName = Config.Bind("Ores - Flametal", "ShowFlametalName", true, "Show name for Flametal");
            flametalName = Config.Bind<string>("Ores - Flametal", "FlametalNameDisplay", "Flametal", "Display name for Flametal");
            countFlametalClusters = Config.Bind("Ores - Flametal", "countFlametalClusters", false, "Add count to name (display name must be enabled)");
            pinFlametalSize = Config.Bind<float>("Ores - Flametal", "PinFlametalSize", 20, "Size of Flametal pin on minimap/main Map (20 is recommended)");
            flametalClusterDistance = Config.Bind<float>("Ores - Flametal", "flametalClusterDistance", -1, "Override the overlap distance (-1 will disable override)");
            //***PICKABLES***//
            pinBerries = Config.Bind("Pickables - Berries", "PinBerries", true, "Show pins for Berries");
            saveBerries = Config.Bind("Pickables - Berries", "SaveBerries", false, "Save pins for Berries");
            showBerriesName = Config.Bind("Pickables - Berries", "ShowBerriesName", true, "Show name for Berries");
            berriesName = Config.Bind<string>("Pickables - Berries", "BerriesNameDisplay", "Berries", "Display name for Berries");
            countBerriesClusters = Config.Bind("Pickables - Berries", "countBerriesClusters", true, "Add count to name (display name must be enabled)");
            pinBerriesSize = Config.Bind<float>("Pickables - Berries", "PinBerriesSize", 20, "Size of Berries pin on minimap/main Map (20 is recommended)");
            berriesClusterDistance = Config.Bind<float>("Pickables - Berries", "berriesClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinBlueberries = Config.Bind("Pickables - Blueberries", "PinBlueberries", true, "Show pins for Blueberries");
            saveBlueberries = Config.Bind("Pickables - Blueberries", "SaveBlueberries", false, "Save pins for Blueberries");
            showBlueberriesName = Config.Bind("Pickables - Blueberries", "ShowBlueberriesName", true, "Show name for Blueberries");
            blueberriesName = Config.Bind<string>("Pickables - Blueberries", "BlueberriesNameDisplay", "Blueberries", "Display name for Blueberries");
            countBlueberriesClusters = Config.Bind("Pickables - Blueberries", "countBlueberriesClusters", true, "Add count to name (display name must be enabled)");
            pinBlueberriesSize = Config.Bind<float>("Pickables - Blueberries", "PinBlueberriesSize", 20, "Size of Blueberries pin on minimap/main Map (20 is recommended)");
            blueberriesClusterDistance = Config.Bind<float>("Pickables - Blueberries", "blueberriesClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinCloudberries = Config.Bind("Pickables - Cloudberries", "PinCloudberries", true, "Show pins for Cloudberries");
            saveCloudberries = Config.Bind("Pickables - Cloudberries", "SaveCloudberries", false, "Save pins for Cloudberries");
            showCloudberriesName = Config.Bind("Pickables - Cloudberries", "ShowCloudberriesName", true, "Show name for Cloudberries");
            cloudberriesName = Config.Bind<string>("Pickables - Cloudberries", "CloudberriesNameDisplay", "Cloudberries", "Display name for Cloudberries");
            countCloudberriesClusters = Config.Bind("Pickables - Cloudberries", "countCloudberriesClusters", true, "Add count to name (display name must be enabled)");
            pinCloudberriesSize = Config.Bind<float>("Pickables - Cloudberries", "PinCloudberriesSize", 20, "Size of Cloudberries pin on minimap/main Map (20 is recommended)");
            cloudberriesClusterDistance = Config.Bind<float>("Pickables - Cloudberries", "cloudberriesClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinThistle = Config.Bind("Pickables - Thistle", "PinThistle", true, "Show pins for Thistle");
            saveThistle = Config.Bind("Pickables - Thistle", "SaveThistle", false, "Save pins for Thistle");
            showThistleName = Config.Bind("Pickables - Thistle", "ShowThistleName", true, "Show name for Thistle");
            thistleName = Config.Bind<string>("Pickables - Thistle", "ThistleNameDisplay", "Thistle", "Display name for Thistle");
            countThistleClusters = Config.Bind("Pickables - Thistle", "countThistleClusters", true, "Add count to name (display name must be enabled)");
            pinThistleSize = Config.Bind<float>("Pickables - Thistle", "PinThistleSize", 20, "Size of Thistle pin on minimap/main Map (20 is recommended)");
            thistleClusterDistance = Config.Bind<float>("Pickables - Thistle", "thistleClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinMushroom = Config.Bind("Pickables - Mushroom", "PinMushroom", true, "Show pins for Mushroom");
            saveMushroom = Config.Bind("Pickables - Mushroom", "SaveMushroom", false, "Save pins for Mushroom");
            showMushroomName = Config.Bind("Pickables - Mushroom", "ShowMushroomName", true, "Show name for Mushroom");
            mushroomName = Config.Bind<string>("Pickables - Mushroom", "MushroomNameDisplay", "Mushroom", "Display name for Mushroom");
            countMushroomClusters = Config.Bind("Pickables - Mushroom", "countMushroomClusters", true, "Add count to name (display name must be enabled)");
            pinMushroomSize = Config.Bind<float>("Pickables - Mushroom", "PinMushroomSize", 20, "Size of Mushroom pin on minimap/main Map (20 is recommended)");
            mushroomClusterDistance = Config.Bind<float>("Pickables - Mushroom", "mushroomClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinCarrot = Config.Bind("Pickables - Carrot", "PinCarrot", true, "Show pins for Carrot");
            saveCarrot = Config.Bind("Pickables - Carrot", "SaveCarrot", false, "Save pins for Carrot");
            showCarrotName = Config.Bind("Pickables - Carrot", "ShowCarrotName", true, "Show name for Carrot");
            carrotName = Config.Bind<string>("Pickables - Carrot", "CarrotNameDisplay", "Carrot", "Display name for Carrot");
            countCarrotClusters = Config.Bind("Pickables - Carrot", "countCarrotClusters", true, "Add count to name (display name must be enabled)");
            pinCarrotSize = Config.Bind<float>("Pickables - Carrot", "PinCarrotSize", 25, "Size of Carrot pin on minimap/main Map (25 is recommended)");
            carrotClusterDistance = Config.Bind<float>("Pickables - Carrot", "carrotClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinTurnip = Config.Bind("Pickables - Turnip", "PinTurnip", true, "Show pins for Turnip");
            saveTurnip = Config.Bind("Pickables - Turnip", "SaveTurnip", false, "Save pins for Turnip");
            showTurnipName = Config.Bind("Pickables - Turnip", "ShowTurnipName", true, "Show name for Turnip");
            turnipName = Config.Bind<string>("Pickables - Turnip", "TurnipNameDisplay", "Turnip", "Display name for Turnip");
            countTurnipClusters = Config.Bind("Pickables - Turnip", "countTurnipClusters", true, "Add count to name (display name must be enabled)");
            pinTurnipSize = Config.Bind<float>("Pickables - Turnip", "PinTurnipSize", 25, "Size of Turnip pin on minimap/main Map (25 is recommended)");
            turnipClusterDistance = Config.Bind<float>("Pickables - Turnip", "turnipClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinOnion = Config.Bind("Pickables - Onion", "PinOnion", true, "Show pins for Onion");
            saveOnion = Config.Bind("Pickables - Onion", "SaveOnion", false, "Save pins for Onion");
            showOnionName = Config.Bind("Pickables - Onion", "ShowOnionName", true, "Show name for Onion");
            onionName = Config.Bind<string>("Pickables - Onion", "OnionNameDisplay", "Onion", "Display name for Onion");
            countOnionClusters = Config.Bind("Pickables - Onion", "countOnionClusters", true, "Add count to name (display name must be enabled)");
            pinOnionSize = Config.Bind<float>("Pickables - Onion", "PinOnionSize", 25, "Size of Onion pin on minimap/main Map (25 is recommended)");
            onionClusterDistance = Config.Bind<float>("Pickables - Onion", "onionClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinDragonEgg = Config.Bind("Misc - Dragon Eggs", "PinDragonEgg", true, "Show pins for Dragon Eggs");
            saveDragonEgg = Config.Bind("Misc - Dragon Eggs", "SaveDragonEgg", false, "Save pins for Dragon Eggs");
            showDragonEggName = Config.Bind("Misc - Dragon Eggs", "ShowDragonEggName", true, "Show name for Dragon Eggs");
            dragonEggName = Config.Bind<string>("Misc - Dragon Eggs", "DragonEggNameDisplay", "DragonEgg", "Display name for Dragon Eggs");
            countDragonEggClusters = Config.Bind("Misc - Dragon Eggs", "countDragonEggClusters", false, "Add count to name (display name must be enabled)");
            pinDragonEggSize = Config.Bind<float>("Misc - Dragon Eggs", "PinDragonEggSize", 20, "Size of Dragon Eggs pin on minimap/main Map (20 is recommended)");
            dragonEggClusterDistance = Config.Bind<float>("Misc - Dragon Eggs", "dragonEggClusterDistance", -1, "Override the overlap distance (-1 will disable override)");
            //***LOCATIONS***//
            pinCrypt = Config.Bind("Locations - Crypt", "PinCrypt", true, "Show pins for Crypts");
            saveCrypt = Config.Bind("Locations - Crypt", "SaveCrypt", false, "Save pins for Crypts");
            showCryptName = Config.Bind("Locations - Crypt", "ShowCryptName", true, "Show name for Crypts");
            cryptName = Config.Bind<string>("Locations - Crypt", "CryptNameDisplay", "Crypt", "Display name for Crypts");
            countCryptClusters = Config.Bind("Locations - Crypt", "countCryptClusters", false, "Add count to name (display name must be enabled)");
            pinCryptSize = Config.Bind<float>("Locations - Crypt", "PinCryptSize", 25, "Size of Crypts pin on minimap/main Map (25 is recommended)");
            cryptClusterDistance = Config.Bind<float>("Locations - Crypt", "cryptClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinSunkenCrypt = Config.Bind("Locations - SunkenCrypt", "PinSunkenCrypt", true, "Show pins for SunkenCrypts");
            saveSunkenCrypt = Config.Bind("Locations - SunkenCrypt", "SaveSunkenCrypt", false, "Save pins for SunkenCrypts");
            showSunkenCryptName = Config.Bind("Locations - SunkenCrypt", "ShowSunkenCryptName", true, "Show name for SunkenCrypts");
            sunkenCryptName = Config.Bind<string>("Locations - SunkenCrypt", "SunkenCryptNameDisplay", "SunkenCrypt", "Display name for SunkenCrypts");
            countSunkenCryptClusters = Config.Bind("Locations - SunkenCrypt", "countSunkenCryptClusters", false, "Add count to name (display name must be enabled)");
            pinSunkenCryptSize = Config.Bind<float>("Locations - SunkenCrypt", "PinSunkenCryptSize", 25, "Size of SunkenCrypts pin on minimap/main Map (25 is recommended)");
            sunkenCryptClusterDistance = Config.Bind<float>("Locations - SunkenCrypt", "sunkenCryptClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinTrollCave = Config.Bind("Locations - TrollCave", "PinTrollCave", true, "Show pins for TrollCaves");
            saveTrollCave = Config.Bind("Locations - TrollCave", "SaveTrollCave", false, "Save pins for TrollCaves");
            showTrollCaveName = Config.Bind("Locations - TrollCave", "ShowTrollCaveName", true, "Show name for TrollCaves");
            trollCaveName = Config.Bind<string>("Locations - TrollCave", "TrollCaveNameDisplay", "TrollCave", "Display name for TrollCaves");
            countTrollCaveClusters = Config.Bind("Locations - TrollCave", "countTrollCaveClusters", false, "Add count to name (display name must be enabled)");
            pinTrollCaveSize = Config.Bind<float>("Locations - TrollCave", "PinTrollCaveSize", 25, "Size of TrollCaves pin on minimap/main Map (25 is recommended)");
            trollCaveClusterDistance = Config.Bind<float>("Locations - TrollCave", "trollCaveClusterDistance", -1, "Override the overlap distance (-1 will disable override)");
            //***SPAWNERS***//
            pinSkeleton = Config.Bind("Spawners - Skeleton", "PinSkeleton", true, "Show pins for Skeleton");
            saveSkeleton = Config.Bind("Spawners - Skeleton", "SaveSkeleton", false, "Save pins for Skeleton");
            showSkeletonName = Config.Bind("Spawners - Skeleton", "ShowSkeletonName", true, "Show name for Skeleton");
            skeletonName = Config.Bind<string>("Spawners - Skeleton", "SkeletonNameDisplay", "Skeleton", "Display name for Skeleton");
            countSkeletonClusters = Config.Bind("Spawners - Skeleton", "countSkeletonClusters", false, "Add count to name (display name must be enabled)");
            pinSkeletonSize = Config.Bind<float>("Spawners - Skeleton", "PinSkeletonSize", 25, "Size of Skeleton pin on minimap/main Map (25 is recommended)");
            skeletonClusterDistance = Config.Bind<float>("Spawners - Skeleton", "skeletonClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinDraugr = Config.Bind("Spawners - Draugr", "PinDraugr", true, "Show pins for Draugr");
            saveDraugr = Config.Bind("Spawners - Draugr", "SaveDraugr", false, "Save pins for Draugr");
            showDraugrName = Config.Bind("Spawners - Draugr", "ShowDraugrName", true, "Show name for Draugr");
            draugrName = Config.Bind<string>("Spawners - Draugr", "DraugrNameDisplay", "Draugr", "Display name for Draugr");
            countDraugrClusters = Config.Bind("Spawners - Draugr", "countDraugrClusters", false, "Add count to name (display name must be enabled)");
            pinDraugrSize = Config.Bind<float>("Spawners - Draugr", "PinDraugrSize", 25, "Size of Draugr pin on minimap/main Map (25 is recommended)");
            draugrClusterDistance = Config.Bind<float>("Spawners - Draugr", "draugrClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinSurtling = Config.Bind("Spawners - Surtling", "PinSurtling", true, "Show pins for Surtling");
            saveSurtling = Config.Bind("Spawners - Surtling", "SaveSurtling", false, "Save pins for Surtling");
            showSurtlingName = Config.Bind("Spawners - Surtling", "ShowSurtlingName", true, "Show name for Surtling");
            surtlingName = Config.Bind<string>("Spawners - Surtling", "SurtlingNameDisplay", "Surtling", "Display name for Surtling");
            countSurtlingClusters = Config.Bind("Spawners - Surtling", "countSurtlingClusters", false, "Add count to name (display name must be enabled)");
            pinSurtlingSize = Config.Bind<float>("Spawners - Surtling", "PinSurtlingSize", 25, "Size of Surtling pin on minimap/main Map (25 is recommended)");
            surtlingClusterDistance = Config.Bind<float>("Spawners - Surtling", "surtlingClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinGreydwarf = Config.Bind("Spawners - Greydwarf", "PinGreydwarf", true, "Show pins for Greydwarf");
            saveGreydwarf = Config.Bind("Spawners - Greydwarf", "SaveGreydwarf", false, "Save pins for Greydwarf");
            showGreydwarfName = Config.Bind("Spawners - Greydwarf", "ShowGreydwarfName", true, "Show name for Greydwarf");
            greydwarfName = Config.Bind<string>("Spawners - Greydwarf", "GreydwarfNameDisplay", "Greydwarf", "Display name for Greydwarf");
            countGreydwarfClusters = Config.Bind("Spawners - Greydwarf", "countGreydwarfClusters", false, "Add count to name (display name must be enabled)");
            pinGreydwarfSize = Config.Bind<float>("Spawners - Greydwarf", "PinGreydwarfSize", 25, "Size of Greydwarf pin on minimap/main Map (25 is recommended)");
            greydwarfClusterDistance = Config.Bind<float>("Spawners - Greydwarf", "greydwarfClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            pinSerpent = Config.Bind("Spawners - Serpent", "PinSerpent", true, "Show pins for Serpent");
            saveSerpent = Config.Bind("Spawners - Serpent", "SaveSerpent", false, "Save pins for Serpent");
            showSerpentName = Config.Bind("Spawners - Serpent", "ShowSerpentName", true, "Show name for Serpent");
            serpentName = Config.Bind<string>("Spawners - Serpent", "SerpentNameDisplay", "Serpent", "Display name for Serpent");
            countSerpentClusters = Config.Bind("Spawners - Serpent", "countSerpentClusters", false, "Add count to name (display name must be enabled)");
            pinSerpentSize = Config.Bind<float>("Spawners - Serpent", "PinSerpentSize", 25, "Size of Serpent pin on minimap/main Map (25 is recommended)");
            serpentClusterDistance = Config.Bind<float>("Spawners - Serpent", "serpentClusterDistance", -1, "Override the overlap distance (-1 will disable override)");
            //***LEVIATHANS***//
            pinLeviathan = Config.Bind("Spawners - Leviathan", "PinLeviathan", true, "Show pins for Leviathan");
            saveLeviathan = Config.Bind("Spawners - Leviathan", "SaveLeviathan", false, "Save pins for Leviathan");
            showLeviathanName = Config.Bind("Spawners - Leviathan", "ShowLeviathanName", true, "Show name for Leviathan");
            leviathanName = Config.Bind<string>("Spawners - Leviathan", "LeviathanNameDisplay", "Leviathan", "Display name for Leviathan");
            countLeviathanClusters = Config.Bind("Spawners - Leviathan", "countLeviathanClusters", false, "Add count to name (display name must be enabled)");
            pinLeviathanSize = Config.Bind<float>("Spawners - Leviathan", "PinLeviathanSize", 25, "Size of Leviathan pin on minimap/main Map (25 is recommended)");
            leviathanClusterDistance = Config.Bind<float>("Spawners - Leviathan", "leviathanClusterDistance", -1, "Override the overlap distance (-1 will disable override)");

            if (!modEnabled.Value)
                enabled = false;
            else
            {
                new Harmony("materousapps.mods.automappins_configurable").PatchAll();
                
                Harmony.CreateAndPatchAll(typeof(Minimap_Patch), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(DestructiblePatchSpawn), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(PickablePatchSpawn), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(LocationPatchSpawn), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(SpawnAreaPatchSpawn), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(MineRockPatchSpawn), "materousapps.mods.automappins_configurable");
                Harmony.CreateAndPatchAll(typeof(Player_Patches), "materousapps.mods.automappins_configurable");
                //Harmony.CreateAndPatchAll(typeof(AMPCommandPatcher), "materousapps.mods.automappins_configurable");

                addedPinLocs = new List<Vector3>();
                dupPinLocs = new List<Vector3>();
                autoPins = new List<Minimap.PinData>();
                pinRemList = new List<Minimap.PinData>();
                savedPins = new List<Minimap.PinData>();
                filteredPins = new List<string>();

                Assets.Init(pinIcons.Value);
                //Commands.AMPLoader.Init();                
            }
        }

        public static bool SimilarPinExists(
          Vector3 pos,
          Minimap.PinType type,
          List<Minimap.PinData> pins,
          string aName,
          Sprite aIcon,
          float clusterDistance,
          out Minimap.PinData match)
        {
            foreach (Minimap.PinData pin in pins)
            {
                if(pos == pin.m_pos && aIcon == pin.m_icon)
                {
                    match = pin;
                    return true;
                }
                else
                //Log.LogInfo(string.Format("[AMP] Checking Distance between Pins {0} & {1}: {2}", aName, pin.m_name, (double)Utils.DistanceXZ(pos, pin.m_pos)));
                if ((double)Utils.DistanceXZ(pos, pin.m_pos) < clusterDistance
                    && type == pin.m_type 
                    && aIcon == pin.m_icon)
                {
                    //Log.LogInfo(string.Format("[AMP] Duplicate pins for {0} found", aName));
                    match = pin;
                    return true;
                }
            }
            match = null;
            return false;
        }

        public static void checkPins(Vector3 charPos)
        {
            //Log.LogInfo(string.Format("[AMP] checkingPins? {0}", checkingPins));
            if (checkingPins)
                return;

            foreach (KeyValuePair<Vector3, string> kvp in pinItems)
            {
                checkingPins = true;
                if (Vector3.Distance(charPos, kvp.Key) < pinRange.Value)
                {
                    if (!addedPinLocs.Contains(kvp.Key) && !dupPinLocs.Contains(kvp.Key))
                    {
                        //Log.LogInfo(string.Format("Checking Pin {0} at {1}", kvp.Value, kvp.Key));
                        PinnedObject.pinOb(kvp.Value, kvp.Key);
                    }
                }
                else if (Vector3.Distance(charPos, kvp.Key) > pinRange.Value)
                {
                    foreach (Minimap.PinData tempPin in autoPins)
                    {

                        if (!remPinDict.ContainsKey(kvp.Key) && !tempPin.m_save)
                        {
                            //Log.LogInfo(string.Format("Adding {0} at {1} to removal List", kvp.Value, kvp.Key));
                            remPinDict.Add(kvp.Key, tempPin);
                            pinRemList.Add(tempPin);
                        }
                    }
                }
            }
            checkingPins = false;

            if (pinRemList != null)
            {
                foreach (Minimap.PinData tempRemPin in pinRemList)
                {
                    if (Vector3.Distance(charPos, tempRemPin.m_pos) < pinRange.Value)
                        return;

                    Minimap.instance.RemovePin(tempRemPin);
                    autoPins.Remove(tempRemPin);
                    addedPinLocs.Remove(tempRemPin.m_pos);
                    remPinDict.Remove(tempRemPin.m_pos);
                    //Log.LogInfo(string.Format("Removed Pin {0} at {1}", tempRemPin.m_name, tempRemPin.m_pos));
                }
            }
            pinRemList.Clear();
            
        }

        public static Minimap.PinData GetNearestPin(Vector3 pos, float radius, List<Minimap.PinData> pins)
        {

            Minimap.PinData pinData = null;
            float num1 = 999999f;
            foreach (Minimap.PinData pin in pins)
            {
                float num2 = Utils.DistanceXZ(pos, pin.m_pos);
                if (num2 < radius && (num2 < num1 || pinData == null))
                {
                    pinData = pin;
                    num1 = num2;
                    //Log.LogInfo(string.Format("[AMP] Nearest Pin Type = {0} at {1}", pinData.m_type, pinData.m_pos));
                }
            }
            return pinData;
        }

        public static void FilterPins()
        {
            savedPins.ForEach(pin => pin.m_uiElement?.gameObject.SetActive(ShouldPinRender(pin)));
        }

        private static bool ShouldPinRender(Minimap.PinData pin)
        {
            if (filteredPins == null || filteredPins.Count == 0)
                return true;

            if (!filteredPins.Contains(pin.m_type.ToString()))
                return true;

            return false;
            //return (uint)((IEnumerable<string>)filteredPins).Count(filter => !pin.m_type.ToString().ToLower().Replace(' ', '_').Contains(filter)) > 0U;
        }

        public Mod()
        {
            return;
        }
    }

    internal class PinnedObject : MonoBehaviour
    {
        public static Minimap.PinData pin;
        public static bool aSave = false;
        public static bool showName = false;
        public static bool countClusters = false;
        public static float clusterDistance = 10;
        public static float pinSize = 20;
        public static Sprite aIcon;
        public static string aName = "";
        public static int pType;

        public static void pinOb(string tempName, Vector3 aPos)
        {
            if (Mod.currEnv == "Crypt" || Mod.currEnv == "SunkenCrypt")
                return;

            if (tempName != null && tempName != "")
            {
                loadData(tempName);

                //don't show filtered pins.
                if (Mod.filteredPins.Contains(pType.ToString()))
                    return;

                if (Mod.autoPins.Count > 0)
                {
                    if (Mod.SimilarPinExists(aPos, (Minimap.PinType)pType, Mod.autoPins, aName, aIcon, clusterDistance, out Minimap.PinData duplicate))
                    {
                        if (countClusters)
                        {
                            //extract the number
                            Regex pattern = new Regex(@".*\[([0-9]+)\]");
                            Match match = pattern.Match(duplicate.m_name);
                            if(match.Groups.Count >= 2)
                            {
                                //Mod.Log.LogDebug(string.Format("[AMP] Found count of {0} in {1}", match.Groups[1], duplicate.m_name));

                                //increment the number
                                int count = Int32.Parse(match.Groups[1].Value);
                                count++;

                                //change the name of the pin
                                string newName = aName + " [" + count + "]";
                                duplicate.m_name = newName;

                                //warn on multiple [numbers] and list them
                                if(match.Groups.Count > 2)
                                {
                                    Mod.Log.LogWarning(string.Format("[AMP] warning: found too many [numbers] in {0} so used first match {1}", duplicate.m_name, match.Groups[1]));
                                    for (int i = 1; i < match.Groups.Count; i++)
                                    {
                                        Mod.Log.LogWarning(string.Format("[AMP] match {0}: {1}", i.ToString(), match.Groups[i]));
                                    }
                                }
                                
                            }
                            else
                            {
                                //No matches for [number] found
                                Mod.Log.LogError(string.Format("[AMP] error: {0} does not contain a [number]", duplicate.m_name));
                            }
                        }

                        Mod.dupPinLocs.Add(aPos);
                        return;
                    }
                }

                //Mod.Log.LogInfo(string.Format("[AMP] Checking Distance between Player position {0} & {1} position {2}: {3}", GameCamera.instance.transform.position, aName, aPos, Vector3.Distance(GameCamera.instance.transform.position, aPos)));
                if (Mod.hideAllNames.Value)
                    showName = false;

                if (showName)
                {
                    if (countClusters)
                    {
                        //count the first of a cluster
                        aName += " [1]";
                    }
                    pin = Minimap.instance.AddPin(aPos, (Minimap.PinType)pType, aName, aSave, false);
                }
                else
                {
                    pin = Minimap.instance.AddPin(aPos, (Minimap.PinType)pType, string.Empty, aSave, false);
                }
                //Mod.Log.LogInfo(string.Format("[AMP] Added Pin {0} at {1}", pin.m_name, pin.m_pos));

                if (aIcon)
                    pin.m_icon = aIcon;

                pin.m_worldSize = pinSize;
                pin.m_save = aSave;

                
                //Mod.Log.LogInfo(string.Format("[AMP] Tracking: {0} at {1} {2} {3}", aName, aPos.x, aPos.y, aPos.z));
                if(!Mod.autoPins.Contains(pin))
                    Mod.autoPins.Add(pin);
                if(!Mod.addedPinLocs.Contains(aPos))
                    Mod.addedPinLocs.Add(aPos);

                if (pin.m_save && !Mod.savedPins.Contains(pin))
                    Mod.savedPins.Add(pin);
            }
        }

        private void Update()
        {
            
        }

        private void OnDestroy()
        {
            if (pin == null || Minimap.instance == null)
                return;

            loadData(pin.m_type.ToString());
            //Mod.Log.LogInfo(string.Format("OnDestroy Called for Type {0} at {1}", pin.m_type, pin.m_pos));
            //Mod.Log.LogInfo(string.Format("[AMP] Save Pin {0}? = {1}.", pin.m_name, aSave));

            if (aSave || pin.m_save)
            {
                //Mod.Log.LogInfo(string.Format("[AMP] Leaving Pin {0} on map.", pin.m_name));
                return;
            }
            if (Vector3.Distance(pin.m_pos, Player_Patches.currPos) < Mod.pinRange.Value)
                return;

            //Minimap.instance.RemovePin(pin);
            //Mod.autoPins.Remove(pin);
            //Mod.addedPinLocs.Remove(pin.m_pos);
            //Mod.dupPinLocs.Remove(pin.m_pos);
            //Mod.pinItems.Remove(pin.m_pos);
        }

        public static void loadData(string pin)
        {
            switch (pin)
            {
                case "Tin":
                case "101":
                    aName = Mod.tinName.Value;
                    pType = 101;
                    aSave = Mod.saveTin.Value;
                    aIcon = Assets.tinSprite;
                    showName = Mod.showTinName.Value;
                    pinSize = Mod.pinTinSize.Value;
                    countClusters = Mod.countTinClusters.Value;
                    clusterDistance = Mod.tinClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value: Mod.tinClusterDistance.Value;
                    break;
                case "Copper":
                case "102":
                    aName = Mod.copperName.Value;
                    pType = 102;
                    aSave = Mod.saveCopper.Value;
                    aIcon = Assets.copperSprite;
                    showName = Mod.showCopperName.Value;
                    pinSize = Mod.pinCopperSize.Value;
                    clusterDistance = Mod.copperClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.copperClusterDistance.Value;
                    break;
                case "Obsidian":
                case "103":
                    aName = Mod.obsidianName.Value;
                    pType = 103;
                    aSave = Mod.saveObsidian.Value;
                    aIcon = Assets.obsidianSprite;
                    showName = Mod.showObsidianName.Value;
                    pinSize = Mod.pinObsidianSize.Value;
                    countClusters = Mod.countObsidianClusters.Value;
                    clusterDistance = Mod.obsidianClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.obsidianClusterDistance.Value;
                    break;
                case "Silver":
                case "104":
                    aName = Mod.silverName.Value;
                    pType = 104;
                    aSave = Mod.saveSilver.Value;
                    aIcon = Assets.silverSprite;
                    showName = Mod.showSilverName.Value;
                    pinSize = Mod.pinSilverSize.Value;
                    clusterDistance = Mod.silverClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.silverClusterDistance.Value;
                    break;
                case "Berries":
                case "105":
                    aName = Mod.berriesName.Value;
                    pType = 105;
                    aSave = Mod.saveBerries.Value;
                    aIcon = Assets.raspberrySprite;
                    showName = Mod.showBerriesName.Value;
                    pinSize = Mod.pinBerriesSize.Value;
                    countClusters = Mod.countBerriesClusters.Value;
                    clusterDistance = Mod.berriesClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.berriesClusterDistance.Value;
                    break;
                case "Blueberries":
                case "106":
                    aName = Mod.blueberriesName.Value;
                    pType = 106;
                    aSave = Mod.saveBlueberries.Value;
                    aIcon = Assets.blueberrySprite;
                    showName = Mod.showBlueberriesName.Value;
                    pinSize = Mod.pinBlueberriesSize.Value;
                    countClusters = Mod.countBlueberriesClusters.Value;
                    clusterDistance = Mod.blueberriesClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.blueberriesClusterDistance.Value;
                    break;
                case "Cloudberries":
                case "107":
                    aName = Mod.cloudberriesName.Value;
                    pType = 107;
                    aSave = Mod.saveCloudberries.Value;
                    aIcon = Assets.cloudberrySprite;
                    showName = Mod.showCloudberriesName.Value;
                    pinSize = Mod.pinCloudberriesSize.Value;
                    countClusters = Mod.countCloudberriesClusters.Value;
                    clusterDistance = Mod.cloudberriesClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.cloudberriesClusterDistance.Value;
                    break;
                case "Thistle":
                case "108":
                    aName = Mod.thistleName.Value;
                    pType = 108;
                    aSave = Mod.saveThistle.Value;
                    aIcon = Assets.thistleSprite;
                    showName = Mod.showThistleName.Value;
                    pinSize = Mod.pinThistleSize.Value;
                    countClusters = Mod.countThistleClusters.Value;
                    clusterDistance = Mod.thistleClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.thistleClusterDistance.Value;
                    break;
                case "DragonEgg":
                case "109":
                    aName = Mod.dragonEggName.Value;
                    pType = 109;
                    aSave = Mod.saveDragonEgg.Value;
                    aIcon = Assets.eggSprite;
                    showName = Mod.showDragonEggName.Value;
                    pinSize = Mod.pinDragonEggSize.Value;
                    clusterDistance = Mod.dragonEggClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.dragonEggClusterDistance.Value;
                    break;
                case "Mushroom":
                case "110":
                    aName = Mod.mushroomName.Value;
                    pType = 110;
                    aSave = Mod.saveMushroom.Value;
                    aIcon = Assets.mushroomSprite;
                    showName = Mod.showMushroomName.Value;
                    pinSize = Mod.pinMushroomSize.Value;
                    countClusters = Mod.countMushroomClusters.Value;
                    clusterDistance = Mod.mushroomClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.mushroomClusterDistance.Value;
                    break;
                case "Carrot":
                case "111":
                    aName = Mod.carrotName.Value;
                    pType = 111;
                    aSave = Mod.saveCarrot.Value;
                    aIcon = Assets.carrotSprite;
                    showName = Mod.showCarrotName.Value;
                    pinSize = Mod.pinCarrotSize.Value;
                    countClusters = Mod.countCarrotClusters.Value;
                    clusterDistance = Mod.carrotClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.carrotClusterDistance.Value;
                    break;
                case "Turnip":
                case "112":
                    aName = Mod.turnipName.Value;
                    pType = 112;
                    aSave = Mod.saveTurnip.Value;
                    aIcon = Assets.turnipSprite;
                    showName = Mod.showTurnipName.Value;
                    pinSize = Mod.pinTurnipSize.Value;
                    countClusters = Mod.countTurnipClusters.Value;
                    clusterDistance = Mod.turnipClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.turnipClusterDistance.Value;
                    break;
                case "Crypt":
                case "113":
                    aName = Mod.cryptName.Value;
                    pType = 113;
                    aSave = Mod.saveCrypt.Value;
                    aIcon = Assets.cryptSprite;
                    showName = Mod.showCryptName.Value;
                    pinSize = Mod.pinCryptSize.Value;
                    clusterDistance = Mod.cryptClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.cryptClusterDistance.Value;
                    break;
                case "SunkenCrypt":
                case "114":
                    aName = Mod.sunkenCryptName.Value;
                    pType = 114;
                    aSave = Mod.saveSunkenCrypt.Value;
                    aIcon = Assets.sunkenCryptSprite;
                    showName = Mod.showSunkenCryptName.Value;
                    pinSize = Mod.pinSunkenCryptSize.Value;
                    clusterDistance = Mod.sunkenCryptClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.sunkenCryptClusterDistance.Value;
                    break;
                case "TrollCave":
                case "115":
                    aName = Mod.trollCaveName.Value;
                    pType = 115;
                    aSave = Mod.saveTrollCave.Value;
                    aIcon = Assets.trollCaveSprite;
                    showName = Mod.showTrollCaveName.Value;
                    pinSize = Mod.pinTrollCaveSize.Value;
                    clusterDistance = Mod.trollCaveClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.trollCaveClusterDistance.Value;
                    break;
                case "Skeleton":
                case "116":
                    aName = Mod.skeletonName.Value;
                    pType = 116;
                    aSave = Mod.saveSkeleton.Value;
                    aIcon = Assets.skeletonSprite;
                    showName = Mod.showSkeletonName.Value;
                    pinSize = Mod.pinSkeletonSize.Value;
                    clusterDistance = Mod.skeletonClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.skeletonClusterDistance.Value;
                    break;
                case "Surtling":
                case "117":
                    aName = Mod.surtlingName.Value;
                    pType = 117;
                    aSave = Mod.saveSurtling.Value;
                    aIcon = Assets.surtlingSprite;
                    showName = Mod.showSurtlingName.Value;
                    pinSize = Mod.pinSurtlingSize.Value;
                    clusterDistance = Mod.surtlingClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.surtlingClusterDistance.Value;
                    break;
                case "Draugr":
                case "118":
                    aName = Mod.draugrName.Value;
                    pType = 118;
                    aSave = Mod.saveDraugr.Value;
                    aIcon = Assets.draugrSprite;
                    showName = Mod.showDraugrName.Value;
                    pinSize = Mod.pinDraugrSize.Value;
                    clusterDistance = Mod.draugrClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.draugrClusterDistance.Value;
                    break;
                case "Greydwarf":
                case "119":
                    aName = Mod.greydwarfName.Value;
                    pType = 119;
                    aSave = Mod.saveGreydwarf.Value;
                    aIcon = Assets.greydwarfSprite;
                    showName = Mod.showGreydwarfName.Value;
                    pinSize = Mod.pinGreydwarfSize.Value;
                    clusterDistance = Mod.greydwarfClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.greydwarfClusterDistance.Value;
                    break;
                case "Serpent":
                case "120":
                    aName = Mod.serpentName.Value;
                    pType = 120;
                    aSave = Mod.saveSerpent.Value;
                    aIcon = Assets.serpentSprite;
                    showName = Mod.showSerpentName.Value;
                    pinSize = Mod.pinSerpentSize.Value;
                    clusterDistance = Mod.serpentClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.serpentClusterDistance.Value;
                    break;
                case "Iron":
                case "121":
                    aName = Mod.ironName.Value;
                    pType = 121;
                    aSave = Mod.saveIron.Value;
                    aIcon = Assets.ironSprite;
                    showName = Mod.showIronName.Value;
                    pinSize = Mod.pinIronSize.Value;
                    clusterDistance = Mod.ironClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.ironClusterDistance.Value;
                    break;
                case "Flametal":
                case "122":
                    aName = Mod.flametalName.Value;
                    pType = 122;
                    aSave = Mod.saveFlametal.Value;
                    aIcon = Assets.flametalSprite;
                    showName = Mod.showFlametalName.Value;
                    pinSize = Mod.pinFlametalSize.Value;
                    clusterDistance = Mod.flametalClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.flametalClusterDistance.Value;
                    break;
                case "Leviathan":
                case "123":
                    aName = Mod.leviathanName.Value;
                    pType = 123;
                    aSave = Mod.saveLeviathan.Value;
                    aIcon = Assets.leviathanSprite;
                    showName = Mod.showLeviathanName.Value;
                    pinSize = Mod.pinLeviathanSize.Value;
                    clusterDistance = Mod.leviathanClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.leviathanClusterDistance.Value;
                    break;
                case "Onion":
                case "124":
                    aName = Mod.onionName.Value;
                    pType = 124;
                    aSave = Mod.saveOnion.Value;
                    aIcon = Assets.onionSprite;
                    showName = Mod.showOnionName.Value;
                    pinSize = Mod.pinOnionSize.Value;
                    countClusters = Mod.countOnionClusters.Value;
                    clusterDistance = Mod.onionClusterDistance.Value < 0 ? Mod.pinClusterDistance.Value : Mod.onionClusterDistance.Value;
                    break;
            }
        }

        public PinnedObject()
        {
            return;
        }
    }

    public class Assets
    {
        public static Sprite tinSprite;
        public static Sprite copperSprite;
        public static Sprite silverSprite;
        public static Sprite obsidianSprite;
        public static Sprite ironSprite;
        public static Sprite flametalSprite;
        public static Sprite raspberrySprite;
        public static Sprite blueberrySprite;
        public static Sprite cloudberrySprite;
        public static Sprite thistleSprite;
        public static Sprite mushroomSprite;
        public static Sprite carrotSprite;
        public static Sprite turnipSprite;
        public static Sprite onionSprite;
        public static Sprite eggSprite;
        public static Sprite cryptSprite;
        public static Sprite sunkenCryptSprite;
        public static Sprite trollCaveSprite;
        public static Sprite skeletonSprite;
        public static Sprite surtlingSprite;
        public static Sprite draugrSprite;
        public static Sprite greydwarfSprite;
        public static Sprite serpentSprite;
        public static Sprite leviathanSprite;

        public static void Init(int pinIcons)
        {
            //Mod.Log.LogInfo(string.Format("pinIcons = {0}", pinIcons));
            switch (pinIcons)
            {
                case 1:
                    tinSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TinOre.png")));
                    copperSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.copperore.png")));
                    silverSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.silverore.png")));
                    obsidianSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.obsidian.png")));
                    ironSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.ironscrap.png")));
                    flametalSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.flametalore.png")));
                    raspberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.raspberry.png")));
                    blueberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.blueberries.png")));
                    cloudberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.cloudberry.png")));
                    thistleSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.thistle.png")));
                    mushroomSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mushroom.png")));
                    carrotSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.carrot.png")));
                    turnipSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.turnip.png")));
                    onionSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.onion.png")));
                    eggSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.dragonegg.png")));
                    cryptSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.surtling_core.png")));
                    sunkenCryptSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.witheredbone.png")));
                    trollCaveSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophyFrostTroll.png")));
                    skeletonSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophySkeleton.png")));
                    surtlingSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophySurtling.png")));
                    draugrSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophyDraugr.png")));
                    greydwarfSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophyGreydwarf.png")));
                    serpentSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.TrophySerpent.png")));
                    leviathanSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.chitin.png")));
                    break;
                default:
                    tinSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_tin.png")));
                    copperSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_copper.png")));
                    silverSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_silver.png")));
                    obsidianSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_obsidian.png")));
                    ironSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_iron.png")));
                    raspberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_raspberry.png")));
                    blueberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_blueberry.png")));
                    cloudberrySprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_cloudberry.png")));
                    thistleSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_pin_thistle.png")));
                    eggSprite = LoadSprite(LoadTexture(ResourceUtils.GetResource(Assembly.GetExecutingAssembly(), "AMP_Configurable.Resources.mapicon_egg.png")));
                    break;
            }
        }

        internal static Texture2D LoadTexture(byte[] file)
        {
            if (((IEnumerable<byte>)file).Count<byte>() > 0)
            {
                Texture2D texture2D = new Texture2D(2, 2);
                if (ImageConversion.LoadImage(texture2D, file))
                {
                    //Mod.Log.LogInfo(string.Format("Texture Found"));
                    return texture2D;
                }
            }
            //Mod.Log.LogInfo(string.Format("Texture not found"));
            return (Texture2D)null;
        }

        public static Sprite LoadSprite(Texture2D SpriteTexture, float PixelsPerUnit = 50f)
        { 
            return SpriteTexture? Sprite.Create(SpriteTexture, new Rect(0.0f, 0.0f, SpriteTexture.width, SpriteTexture.height), new Vector2(0.0f, 0.0f), PixelsPerUnit) : (Sprite)null;
        }
    }
}
