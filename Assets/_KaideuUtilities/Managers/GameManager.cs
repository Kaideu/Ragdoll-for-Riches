using UnityEngine;
using Kaideu.Input;

namespace Kaideu.Managers
{
    public class GameManager : Utils.SingletonPattern<GameManager>
    {
        [SerializeField] private bool debug;
        protected override void Awake()
        {
            base.Awake();
            SetupManagers();
            InitializeManagers();

            if (!debug)
                //Loads index 1. This script should be in index 0
                SceneLoadManager.Instance.LoadIndex(1);
        }

        //Managers are not referenced through the game manager due to managers requirement of being singletons via SingletonPattern<T> inheritance.
        //To reference a manager: MyManager.Instance
        private void SetupManagers()
        {
            gameObject.AddComponent<SceneLoadManager>();
            gameObject.AddComponent<Events.EventManager>(); 
            gameObject.AddComponent<InputManager>();
        }

        private void InitializeManagers()
        {
            InputManager.Instance.Initialize();
        }

        public void QuitApplication()
        {
            //---Perform any save data here---

#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        /**/
    }
}