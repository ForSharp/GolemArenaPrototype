using UnityEngine;

namespace __Scripts.GameLoop
{
    public static class ExtensionMethodsContainer 
    {
        public static void ChangeAlpha(this Material mat, float alphaValue)
        {
            Color oldColor = mat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);         
            mat.SetColor("_Color", newColor);               
        }
    }
}
