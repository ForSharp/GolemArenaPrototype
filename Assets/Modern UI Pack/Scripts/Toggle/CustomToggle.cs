using UnityEngine;

namespace Modern_UI_Pack.Scripts.Toggle
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    [RequireComponent(typeof(Animator))]
    public class CustomToggle : MonoBehaviour
    {
        [HideInInspector] public UnityEngine.UI.Toggle toggleObject;
        [HideInInspector] public Animator toggleAnimator;

        void Start()
        {
            if (toggleObject == null)
                toggleObject = gameObject.GetComponent<UnityEngine.UI.Toggle>();
           
            if (toggleAnimator == null)
                toggleAnimator = toggleObject.GetComponent<Animator>();

            toggleObject.onValueChanged.AddListener(UpdateStateDynamic);
            UpdateState();
        }

        void OnEnable()
        {
            if (toggleObject == null)
                return;

            UpdateState();
        }

        public void UpdateState()
        {
            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");
            else
                toggleAnimator.Play("Toggle Off");
        }

        void UpdateStateDynamic(bool value)
        {
            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");
            else
                toggleAnimator.Play("Toggle Off");
        }
    }
}