using IPA;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BSDarthMaul
{
    public class Plugin : IBeatSaberPlugin
    {
        public static string PluginName = "DarthMaul";
        public const string VersionNum = "0.7.5";

        public string Name => PluginName;
        public string Version => VersionNum;

        private static Plugin _instance;
        // Characteristic controller to check what the current mode is
        //private static PanelBehavior panelBehavior;
        private static DarthMaulBehavior darthMaulBehavior;

        //private static AsyncScenesLoader loader;

        public const string KeyDarthMode = "DMDarthMode";
        public const string KeyOneHanded = "DMOneHanded";
        public const string KeyMainController = "DMMainController";
        public const string KeySeparation = "DMSerparation";
        public const string KeyAutoDetect = "DMAutoDetect";
        public const string KeyNoArrowRandLv = "DMNoArrowRandLv";

        public static BS_Utils.Utilities.Config config;

        public static bool IsDarthModeOn
        {
            get
            {
                return config.GetBool(PluginName, KeyDarthMode, true);
            }

            set
            {
                config.SetBool(PluginName, KeyDarthMode, value);
            }
        }

        public static bool IsOneHanded
        {
            get
            {
                return config.GetBool(PluginName, KeyOneHanded, true);
            }

            set
            {
                config.SetBool(PluginName, KeyOneHanded, value);
            }
        }

        public enum ControllerType
        {
            LEFT,
            RIGHT
        }

        public static ControllerType MainController
        {
            get
            {
                return (ControllerType)config.GetInt(PluginName, KeyMainController, 1);
            }

            set
            {
                config.SetInt(PluginName, KeyMainController, (int)value);
            }
        }

        public static bool IsAutoDetect
        {
            get
            {
                return config.GetBool(PluginName, KeyAutoDetect, true);
            }

            set
            {
                config.SetBool(PluginName, KeyAutoDetect, value);
            }
        }

        public static int Separation
        {
            get
            {
                return config.GetInt(PluginName, KeySeparation, 15);
            }

            set
            {
                config.SetInt(PluginName, KeySeparation, value);
            }
        }

        public static int NoArrowRandLv
        {
            get
            {
                return config.GetInt(Plugin.PluginName, KeyNoArrowRandLv, 0);
            }

            set
            {
                config.SetInt(PluginName, KeyNoArrowRandLv, value);
            }
        }

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Plugin.config = new BS_Utils.Utilities.Config(Plugin.PluginName);
            CheckForUserDataFolder();
            _instance = this;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == "MenuCore")
                DarthMaulUI.CreateUI();
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            _instance = null;
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
        }

        public void OnSceneUnloaded(Scene scene)
        {
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
            try
            {
                Console.WriteLine("scene.name == " + scene.name);
                if (scene.name == "MenuCore")
                {
                    //panelBehavior = new GameObject("panelBehavior").AddComponent<PanelBehavior>();
                }
                else if (scene.name == "GameCore")
                {
                    //if (!loader)
                    //    loader = Resources.FindObjectsOfTypeAll<AsyncScenesLoader>().FirstOrDefault();
                    //loader.loadingDidFinishEvent += OnLoadingDidFinishGame;
                    SharedCoroutineStarter.instance.StartCoroutine(OnLoadingDidFinishGame());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static System.Collections.IEnumerator OnLoadingDidFinishGame()
        {
            yield return new WaitForSeconds(0.1f);
            darthMaulBehavior = new GameObject("DarthMaulBehavior").AddComponent<DarthMaulBehavior>();
        }

        private void CheckForUserDataFolder()
        {
            string userDataPath = Environment.CurrentDirectory + "/UserData";
            if (!Directory.Exists(userDataPath))
            {
                Directory.CreateDirectory(userDataPath);
            }
            if ("".Equals(config.GetString(PluginName, KeyDarthMode, "")))
            {
                config.SetBool(PluginName, KeyDarthMode, false);
            }
            if ("".Equals(config.GetString(PluginName, KeyOneHanded, "")))
            {
                config.SetBool(PluginName, KeyOneHanded, false);
            }
            if ("".Equals(config.GetString(PluginName, KeyMainController, "")))
            {
                config.SetInt(PluginName, KeyMainController, 1);
            }
            if ("".Equals(config.GetString(PluginName, KeyAutoDetect, "")))
            {
                config.SetBool(PluginName, KeyAutoDetect, false);
            }
            if ("".Equals(config.GetString(PluginName, KeySeparation, "")))
            {
                config.SetInt(PluginName, KeySeparation, 15);
            }
            if ("".Equals(config.GetString(PluginName, KeyNoArrowRandLv, "")))
            {
                config.SetInt(PluginName, KeyNoArrowRandLv, 0);
            }
        }

        public static void ToggleDarthMode()
        {
            if (darthMaulBehavior != null)
            {
                darthMaulBehavior.ToggleDarthMode();
            }
        }

        public static void ToggleDarthMode(bool enable)
        {
            if (enable != IsDarthModeOn)
            {
                ToggleDarthMode();
            }
        }

        public static void ToggleOneHanded()
        {
            if (darthMaulBehavior != null)
            {
                darthMaulBehavior.ToggleOneHanded();
            }
        }

        public static void ToggleOneHanded(bool enable)
        {
            if (enable != IsOneHanded)
            {
                ToggleOneHanded();
            }
        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnLevelWasInitialized(int level)
        {
        }

        public void OnUpdate()
        {
        }

        public void OnLateUpdate()
        {

        }

        public void OnFixedUpdate()
        {
        }


    }
}
