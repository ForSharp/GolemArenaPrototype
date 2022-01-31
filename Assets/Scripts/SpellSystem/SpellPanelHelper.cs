using UI;
using UnityEngine;

namespace SpellSystem
{
    public class SpellPanelHelper : MonoBehaviour
    {
        [SerializeField] private GameObject spellPanelPrefab;
        private GameObject _spellPanelObject;
        public SpellsPanel SpellsPanel { get; private set; }
        private void Awake()
        {
            CreateNewSpellPanel();
        }

        private void CreateNewSpellPanel()
        {
            _spellPanelObject = Instantiate(spellPanelPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("SpellPanelsContainer").transform);
            _spellPanelObject.transform.localPosition = Vector3.zero;
            SpellsPanel = _spellPanelObject.GetComponent<SpellsPanel>();
            SpellsPanel.Character = GetComponent<CharacterEntity.State.CharacterState>();
            
            SpellsPanel.HideAll();
        }
    }
}