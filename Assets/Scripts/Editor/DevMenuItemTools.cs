using Game.Currencies;
using UnityEditor;

namespace Editor
{
    public class DevMenuItemTools
    {
        [MenuItem("Tools/DevTool/Reset SaveData")]
        public static void ResetSaveData()
        {
            var keys = ES3.GetKeys();
            foreach (string key in keys)
            {
                ES3.DeleteKey(key);
            }
        }
        
        [MenuItem("Tools/DevTool/FucktonCurrency")]
        public static void SetFucktonCurrency()
        {
            ES3.Save(CurrencyManager.SaveKeyPrefix + CurrencyType.Soft, 999999);
        }
    }
}