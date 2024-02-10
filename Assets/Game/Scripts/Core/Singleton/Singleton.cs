using UnityEngine;

namespace FireEmblemDuplicate.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T _instance;
        private static readonly object padlock = new object();
        public static T Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        T t = GameObject.FindObjectOfType<T>();
                        if (t != null)
                        {
                            _instance = t;
                        }
                        else
                        {
                            GameObject obj = new GameObject(typeof(T).Name + "");
                            _instance = obj.AddComponent<T>();
                            DontDestroyOnLoad(obj);
                        }
                    }
                    return _instance;
                }
            }
        }
    }
}