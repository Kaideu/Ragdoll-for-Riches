using UnityEngine;
//using Newtonsoft.Json.Converters;
//using Newtonsoft.Json;
using System.IO;
using System;

namespace Kaideu.Settings
{
    public static class SettingsDataHandler
    {
        public static AudioSettings AudioSetting;
        public static GameplaySettings GameplaySetting;
        public static GraphicSettings GraphicSetting;
        public static AccessibilitySettings AccessibilitySetting;
        /// <summary>
        /// Quality level of graphics.
        /// </summary>
        public static int QualityLevel;

        private static string GetDataPath<T>() => $"{Application.persistentDataPath}/{typeof(T).Name}.txt";


        /// <summary>
        /// Loads the settgings configuration.
        /// </summary>
        public static void LoadSettings()
        {
            AudioSetting = Load<AudioSettings>();
            GameplaySetting = Load<GameplaySettings>();
            GraphicSetting = Load<GraphicSettings>();
            AccessibilitySetting = Load<AccessibilitySettings>();

            //use clamp to avoid hacking values
            #region Audio
            AudioSetting.MasterVolume = Mathf.Clamp(AudioSetting.MasterVolume, 0, 1);
            AudioSetting.MusicVolume = Mathf.Clamp(AudioSetting.MusicVolume, 0, 1);
            AudioSetting.SfxVolume = Mathf.Clamp(AudioSetting.SfxVolume, 0, 1);
            #endregion

            GameplaySetting.MouseXSensitivity = Mathf.Clamp(GameplaySetting.MouseXSensitivity, 0, 50);
            GameplaySetting.MouseYSensitivity = Mathf.Clamp(GameplaySetting.MouseYSensitivity, 0, 50);
            GameplaySetting.MouseSmoothing = Mathf.Clamp(GameplaySetting.MouseSmoothing, 0, .3f);
            GraphicSetting.QualityLevel = Mathf.Clamp(GraphicSetting.QualityLevel, 0, 5);
            /**/
        }
        /// <summary>
        /// Saves in playerpref the settings configuration.
        /// </summary>
        public static void SaveSettings(float masterVolume, float musicVolume, float sfxVolume, bool fpsToggle, Vector2 mouseSensitivity)
        {
            #region Audio
            PlayerPrefs.SetFloat(AudioSettings.MasterVolumeParameter, masterVolume);
            PlayerPrefs.SetFloat(AudioSettings.MusicVolumeParameter, musicVolume);
            PlayerPrefs.SetFloat(AudioSettings.SfxVolumeParameter, sfxVolume);
            #endregion

            GameplaySetting.FpsCounterEnabled = fpsToggle;
            GameplaySetting.MouseXSensitivity = mouseSensitivity.x;
            GameplaySetting.MouseYSensitivity = mouseSensitivity.y;
            PlayerPrefs.SetFloat(GameplaySettings.MouseXSensitivityParameter, mouseSensitivity.x);
            PlayerPrefs.SetFloat(GameplaySettings.MouseYSensitivityParameter, mouseSensitivity.y);
            PlayerPrefs.SetInt(GameplaySettings.FpsStateParameter, fpsToggle ? 1 : 0);
            PlayerPrefs.SetInt(GraphicSettings.QualityLevelParameter, QualitySettings.GetQualityLevel());
        }

        public static void SaveSettings()
        {
            SaveAudioSettings();
            SaveGameplaySettings();
            SaveGraphicSettings();

        }

        public static void SaveAudioSettings()
        {
            Save(AudioSetting);
        }

        public static void SaveGameplaySettings()
        {
            Save(GameplaySetting);
        }

        public static void SaveGraphicSettings()
        {
            Save(GraphicSetting);
        }

        public static void SaveAccessibilitySettings()
        {
            Save(AccessibilitySetting);
        }



        private static void Save<T>(T settings)
        {
            string json = JsonUtility.ToJson(settings);
            File.WriteAllText(GetDataPath<T>(), json);
        }

        private static T Load<T>() where T : new()
        {
            string path = GetDataPath<T>();
            Debug.Log($"Attempt Loading {path}");
            if (File.Exists(path))
            {
                T settings = JsonUtility.FromJson<T>(File.ReadAllText(path));
                if (settings is null)
                    return new T();
                return settings;
            }
            else
                return new T();
        }
    }

    [Serializable]
    public class AudioSettings
    {
        public static string MasterVolumeParameter = "MasterVolume";
        public static string MusicVolumeParameter = "MusicVolume";
        public static string SfxVolumeParameter = "SfxVolume";

        [SerializeField]
        public float MasterVolume = 0.5f;
        [SerializeField]
        public float MusicVolume = 0.5f;
        [SerializeField]
        public float SfxVolume = 0.5f;

        [SerializeField]
        public bool ToggleMaster = true;
        [SerializeField]
        public bool ToggleMusic = true;
        [SerializeField]
        public bool ToggleSfx = true;

        public override string ToString()
        {
            return $"Master {ToggleMaster}: {MasterVolume} - Music {ToggleMusic}: {MusicVolume} - SFX {ToggleSfx}: {SfxVolume}";
        }
    }


    [Serializable]
    public class GameplaySettings
    {
        public static string FpsStateParameter = "FpsState";
        public static string MouseXSensitivityParameter = "MouseXSensitivity";
        public static string MouseYSensitivityParameter = "MouseYSensitivity";
        public static string MouseAccelerationParameter = "MouseAcceleration";
        public static string FovParameter = "FieldOfView";

        [SerializeField]
        public bool FpsCounterEnabled;
        [SerializeField]
        public float MouseXSensitivity = 20;
        [SerializeField]
        public float MouseYSensitivity = 20;
        [SerializeField]
        public float MouseSmoothing = .1f;
        [SerializeField]
        public float FieldOfView = 65;

        public override string ToString()
        {
            return $"FPS Counter: {FpsCounterEnabled} - Mouse Sensitivity: X({MouseXSensitivity}) Y({MouseYSensitivity} - FOV: {FieldOfView})";
        }
    }


    [Serializable]
    public class GraphicSettings
    {
        public static string QualityLevelParameter = "QualityLevel";

        [SerializeField]
        public int QualityLevel = 0;
        [SerializeField]
        public int ResolutionX = default;
        [SerializeField]
        public int ResolutionY = default;
        [SerializeField]
        public bool IsFullscreen = true;
    }

    [Serializable]
    public class AccessibilitySettings
    {
        public static string LanguageOptionParameter = "LanguageOption";

        [SerializeField]
        public int Language = 0;
    }

}