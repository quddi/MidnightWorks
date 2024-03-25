using UnityEngine;

namespace Game.DependencyInjection
{
    public class SceneScope : CustomScope
    {
        private void OnValidate()
        {
            if (autoRun == false)
                Debug.LogWarning("The scene scope must run automatically!");
            
            autoRun = true;
        }
    }
}