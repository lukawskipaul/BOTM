using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRevealEffect : MonoBehaviour
{
    [SerializeField]
    private float delayBetweenCharacterReveals = 0.05f;

    [TextArea(3,6)]
    [SerializeField]
    private string fullTextColumnOne, fullTextColumnTwo, fullTextTitle;

    [SerializeField]
    private Text firstTextColumn, secondTextColumn, titleText;

    private string currentText = "";
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        titleText.text = "";
        firstTextColumn.text = "";
        secondTextColumn.text = "";
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        audioSource.Play();

        for (int g = 0; g < fullTextTitle.Length; g++)
        {
            currentText = fullTextTitle.Substring(0, g);
            titleText.text = currentText;
            yield return new WaitForSeconds(delayBetweenCharacterReveals);
        }

        for (int i = 0; i < fullTextColumnOne.Length; i++)
        {
            currentText = fullTextColumnOne.Substring(0, i);
            firstTextColumn.text = currentText;
            yield return new WaitForSeconds(delayBetweenCharacterReveals);
        }

        for (int j = 0; j < fullTextColumnTwo.Length; j++)
        {
            currentText = fullTextColumnTwo.Substring(0, j);
            secondTextColumn.text = currentText;
            yield return new WaitForSeconds(delayBetweenCharacterReveals);
        }

        audioSource.Stop();
    }
}
