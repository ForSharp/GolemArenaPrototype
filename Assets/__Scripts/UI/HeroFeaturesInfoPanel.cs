using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroFeaturesInfoPanel : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;
        private GameObject _textInfo;

        public void FillValues(List<string> info, Transform parent)
        {
            foreach (var t in info)
            {
                _textInfo = Instantiate(textPrefab, parent);
                _textInfo.GetComponentInChildren<Text>().text = t;
            }
        }
    }
}