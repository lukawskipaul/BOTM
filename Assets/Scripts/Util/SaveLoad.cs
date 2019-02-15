using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class CharacterData
{
    public float characterLocation;
    public float enemyLocation;
    public int characterHealth;
    public int enemyHealth;
}
public class SaveLoad : MonoBehaviour
{
    public CharacterData characterData;   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveCharacter(characterData, 0);

        if (Input.GetKeyDown(KeyCode.L))
            characterData = LoadCharacter(0);
            
        
    }

    static void SaveCharacter(CharacterData data, int characterSlot)
    {

    }

    static CharacterData LoadCharacter(int characterSlot)
    {
        CharacterData loadedCharacter = new CharacterData();
        return loadedCharacter;
    }
}
