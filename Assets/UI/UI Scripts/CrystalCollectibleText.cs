using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollectibleText : MonoBehaviour
{
    [SerializeField]
    private Text crystalCountText;
    private int totalNumOfCrystals = 0;
    private int numOfCrystals = 0;

    public void GainedCrystal()
    {
        numOfCrystals++;
        crystalCountText.text = "Crystals: " + numOfCrystals + "/" + totalNumOfCrystals;
    }

    public void AddInGameCrystal()
    {
        totalNumOfCrystals++;
    }

    private void OnEnable()
    {
        CrystalText();
    }

    private void CrystalText()
    {
        crystalCountText.text = "Crystals: " + numOfCrystals + "/" + totalNumOfCrystals;
    }
}
