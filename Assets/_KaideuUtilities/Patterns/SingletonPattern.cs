using UnityEngine;


namespace Kaideu.Utils
{
    public abstract class SingletonPattern<T> : MonoBehaviour where T : SingletonPattern<T>
    {
        public static T Instance;
        [SerializeField] private bool doesPersist = true;

        //To create Awake in derived classes, use an override Awake and call base.Awake()
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this as T;
            if(doesPersist)
                DontDestroyOnLoad(this);
        }
    }
}
