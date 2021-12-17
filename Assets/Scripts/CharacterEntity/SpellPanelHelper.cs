using System;
using UI;
using UnityEngine;

namespace CharacterEntity
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
            SpellsPanel = _spellPanelObject.GetComponent<SpellsPanel>();
            SpellsPanel.character = GetComponent<State.CharacterState>();
            
            SpellsPanel.HideAll();
        }
    }
}