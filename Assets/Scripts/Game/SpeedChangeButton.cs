using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Game
{
    public class SpeedChangeButton : MonoBehaviour
    {
        [SerializeField]
        Button button;
    
        [SerializeField]
        LocalizeStringEvent speedLabel;
    
        [SerializeField]
        TextMeshProUGUI tmpText;

        public Button Button => button;

        public void Draw()
        {
            speedLabel.StringReference.Arguments = new object[] { Time.timeScale };
            speedLabel.RefreshString();
        }
    }
}
