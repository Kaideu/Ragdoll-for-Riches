using System;
using UnityEngine.SceneManagement;

namespace Kaideu.Managers
{
    public class SceneLoadManager : Utils.SingletonPattern<SceneLoadManager>
    {
        public Action onBeforeLoadMainMenu;
        public Action onAfterLoadMainMenu;
        public void LoadMainMenu()
        {
            onBeforeLoadMainMenu?.Invoke();
            SceneManager.LoadScene("MainMenu");
            onAfterLoadMainMenu?.Invoke();
        }

        public Action onBeforeLoadLevel;
        public Action onAfterLoadLevel;
        public void LoadLevel(int level)
        {
            print("Load Level");
            onBeforeLoadLevel?.Invoke();
            SceneManager.LoadScene($"Level_{level}");
            onAfterLoadLevel?.Invoke();
        }

        public void LoadIndex(int level)
        {
            print("Load Level");
            onBeforeLoadLevel?.Invoke();
            SceneManager.LoadScene(level);
            onAfterLoadLevel?.Invoke();
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public Action<string> onLoadScene;
        public void LoadScene(string sceneName) => onLoadScene?.Invoke(sceneName);

    }
}
