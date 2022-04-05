using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //can change in editor
public class MonsterLevel   //sub class of monsterdata
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;
}

public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;
    
    private MonsterLevel currentLevel;

    public MonsterLevel CurrentLevel
    {

        get { return currentLevel; }

        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;

            for (int i = 0; i < levels.Count; i++)  //checking for active monster level
            {
                if (levelVisualization != null)
                    if (i == currentLevelIndex)
                        levels[i].visualization.SetActive(true);    //turning on correct monster visuals
                    else
                        levels[i].visualization.SetActive(false);   //turning off the other monster visuals
            }
        }

    }

    void OnEnable()
    {
        CurrentLevel = levels[0];   //starts monsters at level 1
    }

    public MonsterLevel GetNextLevel() 
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)  //checking if we can upgrade
            return levels[currentLevelIndex + 1];   //upgrade level
        else
            return null;
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
            CurrentLevel = levels[currentLevelIndex + 1];
    }
}
