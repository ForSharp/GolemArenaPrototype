
// =================================	
// Namespaces.
// =================================

using UnityEngine;

// =================================	
// Define namespace.
// =================================

namespace VFX.Mirza_Beig.Particle_Systems._Common.Scripts
{

    namespace ParticleSystems
    {

        // =================================	
        // Classes.
        // =================================
        
        public class DestroyAfterTime : MonoBehaviour
        {
            // =================================	
            // Nested classes and structures.
            // =================================

            // ...

            // =================================	
            // Variables.
            // =================================

            // ...

            public float time = 2.0f;

            // =================================	
            // Functions.
            // =================================
            
            // ...

            void Start()
            {
                Destroy(gameObject, time);
            }
            
            // =================================	
            // End functions.
            // =================================

        }

        // =================================	
        // End namespace.
        // =================================

    }

}

// =================================	
// --END-- //
// =================================
