using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kaideu.Settings {

    public class SettingsController : Utils.SingletonPattern<SettingsController>
    {
        [Header("Audio")]
        [SerializeField] private bool hasAudio = false;
        [SerializeField] private AudioMixer soundMixer;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Toggle masterToggle;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;
        [Space(20)]
        [Header("Gameplay")]
        [SerializeField] private bool hasGameplay = false;
        [SerializeField] private Slider mouseSensitivitySlider;
        [SerializeField] private Slider mouseSmoothingSlider;
        [SerializeField] private Slider fovSlider;
        [SerializeField] private Toggle fpsCounterEnabled;
        [Header("Graphics")]
        [SerializeField] private bool hasGraphics = false;
        [SerializeField] private Vector2Int[] _resolutionOptions;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private QualityData[] _qualityOptions;
        [SerializeField] private Toggle fullScreenToggle;
        [Header("Accessibility")]
        [SerializeField] private bool hasAccessibility = false;
        [SerializeField] private TMP_Dropdown languageDropdown;

        private AudioSettings currentAudioSettings => SettingsDataHandler.AudioSetting;
        private GameplaySettings currentGameplaySettings => SettingsDataHandler.GameplaySetting;
        private GraphicSettings currentGraphicSettings => SettingsDataHandler.GraphicSetting;
        private AccessibilitySettings currentAccessibiltySettings => SettingsDataHandler.AccessibilitySetting;

        private bool initialized;

        
        void OnEnable()
        {
            //LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;
        }
        void OnDisable()
        {
            //LocalizationSettings.SelectedLocaleChanged -= SelectedLocaleChanged;

            CancelInvoke();
        }

        private void Start()
        {
            SettingsDataHandler.LoadSettings();

            if (hasAudio)
                InitializeAudio();
            if (hasGraphics)
                InitializeGraphics();
            if (hasGameplay)
                InitializeGameplay();
            if (hasAccessibility)
                InitializeAccessibility();
            initialized = true;
        }
        /**/


        #region Audio
        private void InitializeAudio()
        {
            SetMasterVolume(currentAudioSettings.MasterVolume);
            SetMusicVolume(currentAudioSettings.MusicVolume);
            SetSFXVolume(currentAudioSettings.SfxVolume);

            SetMasterToggle(currentAudioSettings.ToggleMaster);
            SetMusicToggle(currentAudioSettings.ToggleMusic);
            SetSFXToggle(currentAudioSettings.ToggleSfx);

            SaveAudio();
        }

        /// <summary>
        /// Changes the volume of everything in the game
        /// </summary>
        /// <param name="value"></param>
        public void SetMasterVolume(float value)
        {
            soundMixer.SetFloat(AudioSettings.MasterVolumeParameter, Mathf.Log10(value) * 80 + 20);
            masterVolumeSlider.value = value;
            currentAudioSettings.MasterVolume = value;

            if (value != -80)
                SetMasterToggle(true);
            //SaveAudio();
        }

        /// <summary>
        /// Changes the volume of the music
        /// </summary>
        /// <param name="value">The volume of the game music. value should be between 0-1</param>
        public void SetMusicVolume(float value)
        {
            soundMixer.SetFloat(AudioSettings.MusicVolumeParameter, Mathf.Log10(value) * 20);
            musicSlider.value = value;
            currentAudioSettings.MusicVolume = value;

            if (value != -80)
                SetMusicToggle(true);
            //SaveAudio();
        }

        /// <summary>
        /// Changes the volume of the sound effects
        /// </summary>
        /// <param name="value"></param>
        public void SetSFXVolume(float value)
        {
            soundMixer.SetFloat(AudioSettings.SfxVolumeParameter, Mathf.Log10(value) * 20);
            sfxSlider.value = value;
            currentAudioSettings.SfxVolume = value;

            if (value != -80)
                SetSFXToggle(true);
            //SaveAudio();
        }

        public void SetMasterToggle(bool toggled)
        {
            if (toggled)
            {
                soundMixer.SetFloat(AudioSettings.MasterVolumeParameter, Mathf.Log10(currentAudioSettings.MasterVolume) * 80 + 20);
                masterVolumeSlider.value = currentAudioSettings.MasterVolume;
            }
            else
            {
                soundMixer.SetFloat(AudioSettings.MasterVolumeParameter, Mathf.Log10(masterVolumeSlider.minValue) * 80 + 20);
                //masterVolumeSlider.value = masterVolumeSlider.minValue;
            }

            masterToggle.isOn = toggled;
            currentAudioSettings.ToggleMaster = toggled;
            //SaveAudio();
        }

        public void SetMusicToggle(bool toggled)
        {
            if (toggled)
            {
                //musicSlider.value = sfxSlider.minValue;
                soundMixer.SetFloat(AudioSettings.MusicVolumeParameter, Mathf.Log10(currentAudioSettings.MusicVolume) * 20);
            }
            else
            {
                soundMixer.SetFloat(AudioSettings.MusicVolumeParameter, Mathf.Log10(musicSlider.minValue) * 20);
                //musicSlider.value = musicSlider.minValue;
            }

            musicToggle.isOn = toggled;
            currentAudioSettings.ToggleMusic = toggled;
            //SaveAudio();
        }

        public void SetSFXToggle(bool toggled)
        {
            if (toggled)
            {
                //sfxSlider.value = currentAudioSettings.SfxVolume;
                soundMixer.SetFloat(AudioSettings.SfxVolumeParameter, Mathf.Log10(currentAudioSettings.SfxVolume) * 20);
            }
            else
            {
                soundMixer.SetFloat(AudioSettings.SfxVolumeParameter, Mathf.Log10(sfxSlider.minValue) * 20);
                /*
                var temp = currentAudioSettings.SfxVolume;
                sfxSlider.value = sfxSlider.minValue;
                currentAudioSettings.SfxVolume = temp;
                /**/
            }

            sfxToggle.isOn = toggled;
            currentAudioSettings.ToggleSfx = toggled;
            //SaveAudio();
        }

        public void SaveAudio() => SettingsDataHandler.SaveAudioSettings();
        #endregion

        #region Gameplay

        private void InitializeGameplay()
        {
            mouseSensitivitySlider.maxValue = 50;
            mouseSensitivitySlider.minValue = 0.5f;
            mouseSmoothingSlider.maxValue = .3f;
            mouseSmoothingSlider.minValue = 0f;
            SetMouseSensitivity(currentGameplaySettings.MouseXSensitivity); //Split if y gets own slider
            SetMouseSmoothing(currentGameplaySettings.MouseSmoothing);
            SetFov(currentGameplaySettings.FieldOfView);
            ToggleFPSCount(currentGameplaySettings.FpsCounterEnabled);
            SaveGameplay();
        }

        public void SetMouseSensitivity(float value)
        {
            currentGameplaySettings.MouseXSensitivity = value;
            currentGameplaySettings.MouseYSensitivity = value;
            mouseSensitivitySlider.value = value;
        }

        public void SetMouseSmoothing(float value)
        {
            currentGameplaySettings.MouseSmoothing = value;
            mouseSmoothingSlider.value = value;
        }

        public void SetFov(int value) => SetFov((float)value);

        public void SetFov(float value)
        {
            currentGameplaySettings.FieldOfView = value;
            fovSlider.value = value;
            FOVUpdater.Instance?.UpdateFOV(value);
        }

        public void ToggleFPSCount(bool isToggled)
        {
            currentGameplaySettings.FpsCounterEnabled = isToggled;
            fpsCounterEnabled.enabled = isToggled;
        }

        public void SaveGameplay() => SettingsDataHandler.SaveGameplaySettings();

        #endregion

        #region Graphics

        private void InitializeGraphics()
        {
            SetQualityOptions();
            SetResolutionOptions();

            ToggleFullScreen(currentGraphicSettings.IsFullscreen);
            SetQuality(currentGraphicSettings.QualityLevel);

            #region SetResolutionPreQual
            var resIndex = -1;
            var res = new Vector2Int();
            if (currentGraphicSettings.ResolutionX == default || currentGraphicSettings == default)
            {
                res.x = Display.main.renderingWidth;
                res.y = Display.main.renderingHeight;
                
                var found = false;
                for(int i = 0; i < _resolutionOptions.Length; i++)// foreach (var pRes in _resolutionOptions)
                {
                    if (_resolutionOptions[i].x == res.x && _resolutionOptions[i].y == res.y)
                    {
                        found = true;
                        resIndex = i;
                        break;
                    }
                }

                if (!found)
                    resIndex = DetermineClosestResolution((res.x < res.y) ? res.x : res.y, res.x < res.y);
            }
            else
            {
                res.x = currentGraphicSettings.ResolutionX;
                res.y = currentGraphicSettings.ResolutionY;

                for (int i = 0; i < _resolutionOptions.Length; i++)
                {
                    if (_resolutionOptions[i].x == res.x && _resolutionOptions[i].y == res.y)
                    {
                        resIndex = i;
                        break;
                    }
                }
            }
            #endregion
            SetResolution(resIndex);

            SaveGraphics();

        }

        private int DetermineClosestResolution(int value, bool isX)
        {
            var closestDifference = 999999;
            var closestIndex = _resolutionOptions.Length - 1;
            for (int i = 0; i < _resolutionOptions.Length; i++)
            {
                var dif = ((isX)?_resolutionOptions[i].x:_resolutionOptions[i].y) - value;
                if (dif >= 0 && dif < closestDifference)
                {
                    closestDifference = dif;
                    closestIndex = i;
                }
            }
            return closestIndex;
        }

        public void SaveGraphics() => SettingsDataHandler.SaveGraphicSettings();
        
        private void SelectedLocaleChanged(Locale obj)
        {
            SetQualityOptions();
        }
        /**/

        /// <summary>
        /// Sets the available resolutions 
        /// </summary>
        /// <param name="resolutionIndex"> List of available resolutions</param>
        public void SetResolution(int value)
        {
            var res = ConvertResolutions(_resolutionOptions)[value];
            resolutionDropdown.value = value;
            currentGraphicSettings.ResolutionX = res.width;
            currentGraphicSettings.ResolutionY = res.height;
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
            resolutionDropdown.RefreshShownValue();
        }
        /// <summary>
        /// Sets fullscreeen mode.
        /// </summary>
        public void ToggleFullScreen(bool isOn)
        {
            fullScreenToggle.isOn = isOn;
            currentGraphicSettings.IsFullscreen = isOn;
            Screen.fullScreen = isOn;
        }
        /// <summary>
        /// Sets the quality setting
        /// </summary>
        /// <param name="qualityIndex">List of qualities</param>
        public void SetQuality(int value)
        {
            qualityDropdown.value = value;
            currentGraphicSettings.QualityLevel = value;
            QualitySettings.SetQualityLevel(currentGraphicSettings.QualityLevel);
            qualityDropdown.RefreshShownValue();
        }

        private Resolution[] ConvertResolutions(Vector2Int[] resolutions)
        {
            Resolution[] _resolutions = new Resolution[resolutions.Length];
            for (int i = 0; i < _resolutions.Length; i++)
            {
                _resolutions[i].width = resolutions[i].x;
                _resolutions[i].height = resolutions[i].y;
            }
            return _resolutions;
        }

        private void SetQualityOptions()
        {
            qualityDropdown.ClearOptions();
            List<string> _qualityOptionsList = new List<string>();
            int _counter = 0;
            int _selectedValue = 0;
            foreach (var item in _qualityOptions)
            {
                //_qualityOptionsList.Add(item.QualityName.GetLocalizedString());
                if (QualitySettings.GetQualityLevel() == item.QualityLevel)
                    _selectedValue = _counter;
                _counter++;
            }
            qualityDropdown.AddOptions(_qualityOptionsList);
            qualityDropdown.value = _selectedValue;
            qualityDropdown.RefreshShownValue();
        }

        private void SetResolutionOptions()
        {
            //Finds resolutions and offers them in the settings menu

            resolutionDropdown.ClearOptions();
            int currentResolutionIndex = 0;
            List<string> options = new List<string>();
            int _counter = 0;
            foreach (var item in ConvertResolutions(_resolutionOptions))
            {
                if (!suportsResolution(item))
                    continue;

                string option = item.width + " x " + item.height;
                options.Add(option);

                if (item.width == Screen.width && item.height == Screen.height)
                    currentResolutionIndex = _counter;
                _counter++;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        bool suportsResolution(Resolution resolution)
        {
            foreach (var item in Screen.resolutions)
            {
                if (resolution.width == item.width && resolution.height == resolution.height)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Accessibility

        private void InitializeAccessibility()
        {

            StartCoroutine("SetLanguageOptions");

            SaveAccessibility();
        }

        public void SaveAccessibility() => SettingsDataHandler.SaveAccessibilitySettings();

        #region Language
        
        public void SetLanguage(int value)
        {
            languageDropdown.value = value;
            currentAccessibiltySettings.Language = value;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
            languageDropdown.RefreshShownValue();
        }

        private IEnumerator SetLanguageOptions()
        {
            yield return LocalizationSettings.InitializationOperation;
            List<string> _optionList = new List<string>();
            int _currentLanguage = 0;
            int _indexCounter = 0;
            languageDropdown.ClearOptions();
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                _optionList.Add(locale.name);
                if (LocalizationSettings.SelectedLocale == locale)
                    _currentLanguage = _indexCounter;
                _indexCounter++;
            }
            languageDropdown.AddOptions(_optionList);
            languageDropdown.value = _currentLanguage;

            SetLanguage(currentAccessibiltySettings.Language);
        }
        /**/
        #endregion

        #endregion


    }

    [System.Serializable]
    struct QualityData
    {
        [SerializeField]
        public LocalizedString QualityName;
        [SerializeField]
        public int QualityLevel;
    }
}