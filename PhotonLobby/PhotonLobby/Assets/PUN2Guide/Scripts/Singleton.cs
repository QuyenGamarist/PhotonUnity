using UnityEngine;

    public class Singleton<T> where T : new()
    {
        private static T singleton = new T();

        public static T instance
        {
            get
            {
                return singleton;
            }
        }
    }
    public class SingletonMono<T> : MonoBehaviour where T : UnityEngine.Component
    {

        private void Reset()
        {
            gameObject.name = typeof(T).ToString();
        }

        protected static T singleton;

        public static T Instance
        {
            get
            {
                if (SingletonMono<T>.singleton == null)
                {
                    SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
                    if (SingletonMono<T>.singleton == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof(T).ToString();
                        SingletonMono<T>.singleton = go.AddComponent<T>();
                    }
                }
                return SingletonMono<T>.singleton;
            }
        }

        protected virtual void OnDestroy()
        {
            if (singleton == this)
            {
                singleton = null;
            }
        }
    }

    public class SingletonDontDestroy<T> : SingletonMono<T> where T : UnityEngine.Component
    {
        #region Methods

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake()
        {
            if (singleton == null)
            {
                singleton = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        #endregion
    }