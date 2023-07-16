using UnityEngine;

namespace NotDecided
{
    public static class SaveManager
    {
        public static int GetData(string key)
        {
            return PlayerPrefs.GetInt(key, 1);
        }

        public static void SaveData(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }
}