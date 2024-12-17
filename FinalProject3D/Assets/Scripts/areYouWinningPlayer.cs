using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCompletionChecker : MonoBehaviour
{
    
    private Renderer[] childRenderers;

  
    private bool sceneChanged = false;

    void Start()
    {
       
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        
        if (!sceneChanged && AreAllTilesPainted())
        {
            
            ChangeScene();
        }
    }

    bool AreAllTilesPainted()
    {
        
        if (childRenderers == null || childRenderers.Length == 0)
            return false;

        foreach (Renderer renderer in childRenderers)
        {
            
            if (renderer == null)
                continue;

           
            MaterialPropertyBlock props = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(props);

           
            Color currentColor = props.GetColor("_Color");

            if (currentColor != Color.red && currentColor != Color.green)
            {
             
                return false;
            }
        }

        
        return true;
    }

    void ChangeScene()
    {
       
        sceneChanged = true;

        
        SceneManager.LoadScene("you-win");
    }
}
