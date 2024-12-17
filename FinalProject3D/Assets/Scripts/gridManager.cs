using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class RenameRandomPlanes : MonoBehaviour
{
    
    public int numberOfMines = 10;

    private List<string> mineNames = new List<string>(); 

    void Start()
    {
       
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

       
        List<Transform> planes = new List<Transform>();
        foreach (var transform in childTransforms)
        {
            if (transform != this.transform)
            {
                planes.Add(transform);
            }
        }

        
        planes.RemoveAll(plane => plane.name == "tile1" || plane.name == "tile64");

        
        if (planes.Count < numberOfMines)
        {
            Debug.LogWarning("Not enough valid planes to rename (excluding tile1 and tile64)!");
            return;
        }

        
        int attempts = 0;
        while (mineNames.Count < numberOfMines && attempts < 100)
        {
            
            int randomIndex = Random.Range(0, planes.Count);
            Transform selectedPlane = planes[randomIndex];
            string selectedTileName = selectedPlane.name;

            
            if (!IsAdjacentToMine(selectedTileName))
            {
                
                selectedPlane.name = "mine";
                mineNames.Add(selectedTileName); 

                
                planes.RemoveAt(randomIndex);
            }

            attempts++;
        }

        if (attempts >= 100)
        {
            Debug.LogWarning("Too many attempts to place mines, may not be able to place all mines.");
        }
    }

    bool IsAdjacentToMine(string tileName)
    {
        
        if (!tileName.StartsWith("tile"))
        {
            return false; 
        }

       
        int tileNumber;
        if (!int.TryParse(tileName.Replace("tile", ""), out tileNumber))
        {
            return false;  
        }

        // 8x8 grid
        int row = (tileNumber - 1) / 8;
        int col = (tileNumber - 1) % 8;

        
        foreach (var mineName in mineNames)
        {
            if (!mineName.StartsWith("tile")) continue; 

            int mineNumber;
            if (!int.TryParse(mineName.Replace("tile", ""), out mineNumber))
            {
                continue;  
            }

            int mineRow = (mineNumber - 1) / 8;
            int mineCol = (mineNumber - 1) % 8;

            
            if (Mathf.Abs(mineRow - row) <= 1 && Mathf.Abs(mineCol - col) <= 1)
            {
                return true;  
            }
        }

        return false;
    }
}
