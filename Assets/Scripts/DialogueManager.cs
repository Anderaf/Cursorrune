using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text faceText;
    [SerializeField] TMP_Text noFaceText;
    [SerializeField] Image faceImage;

    [SerializeField] GameObject enemyTextBoxPrefab; //<--- this after main system

    [SerializeField] AudioClip defaultTalkSound;
    [SerializeField] float defaultDelayBetweenLetters = 0.04f;

    BattleManager battleManager;
    AudioSource audioSource;
    Coroutine currentCoroutine;
    
    void Start()
    {
        noFaceText.text = "";
        faceText.text = "";
        faceImage.sprite = null;
        //battleManager = FindObjectOfType<BattleManager>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisplayDialogue("* This is the text from the script. Look, Delay is different: ...!...??..!?.", defaultTalkSound, 0.04f);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            DisplayDialogue("* gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg", defaultTalkSound, 0.04f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            DisplayDialogue("* The words <color=yellow>RUM</color> and <color=red>DEATH</color> mean the same thing to <b>YOU</b>!", defaultTalkSound, 0.04f);
        }
    }
    public void DisplayDialogue(string text, bool punctuationDelay = true)
    {
        DisplayDialogue(text, defaultTalkSound, defaultDelayBetweenLetters, punctuationDelay);
    }
    public void DisplayDialogue(string text, AudioClip talkSound, bool punctuationDelay = true)
    {
        DisplayDialogue(text, talkSound, defaultDelayBetweenLetters, punctuationDelay);
    }
    public void DisplayDialogueWithFace(string text, Sprite _faceSprite, bool punctuationDelay = true)
    {
        DisplayDialogue(text, defaultTalkSound, defaultDelayBetweenLetters, _faceSprite, punctuationDelay);
    }
    public void DisplayDialogueWithFace(string text, AudioClip talkSound, Sprite _faceSprite, bool punctuationDelay = true)
    {
        DisplayDialogue(text, talkSound, defaultDelayBetweenLetters, _faceSprite, punctuationDelay);
    }
    public void DisplayDialogue(string text, AudioClip talkSound, float delayBetweenLetters, bool punctuationDelay = true)
    {
        noFaceText.text = "";
        faceText.text = "";
        faceImage.sprite = null;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        
        currentCoroutine = StartCoroutine(DisplayDialogueCoroutine(text,talkSound,delayBetweenLetters,punctuationDelay));
    }
    public void DisplayDialogue(string text, AudioClip talkSound, float delayBetweenLetters, Sprite _faceSprite, bool punctuationDelay = true)
    {
        noFaceText.text = "";
        faceText.text = "";
        faceImage.sprite = _faceSprite;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DisplayDialogueCoroutine(text, talkSound, delayBetweenLetters, punctuationDelay));
    }
    IEnumerator DisplayDialogueCoroutine(string text, AudioClip talkSound, float delayBetweenLetters, bool punctuationDelay)
    {
        audioSource.clip = talkSound;
        bool richTextCodeFound = false;
        int richTextCodeLength = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '<')
            {
                for (int y = i; y < text.Length; y++)
                {
                    if (text[y] == '>' && !richTextCodeFound)
                    {
                        richTextCodeFound = true;
                        richTextCodeLength = y - i;
                    }
                    
                }
                if (richTextCodeFound)
                {
                    richTextCodeFound = false;
                    noFaceText.text += text.Substring(i,richTextCodeLength);
                    faceText.text += text.Substring(i, richTextCodeLength);
                    i += richTextCodeLength;
                    richTextCodeLength = 0;                  
                }
            }
            float delay = delayBetweenLetters;
            if (punctuationDelay)
            {
                switch (text[i])
                {
                    case '.':
                        delay *= 6;
                        break;
                    case '!':
                        delay *= 6;
                        break;
                    case '?':
                        delay *= 6;
                        break;
                    case ',':
                        delay *= 4;
                        break;
                    case ':':
                        delay *= 4;
                        break;
                    /*case '-':
                        delay *= 4;
                        break;*/
                    /*case ' ':
                        delay *= 0;
                        break;*/
                    default:
                        break;
                }
            }
            if (delay != 0 && text[i] != ' ')
            {
                audioSource.Play();
            }
            
            noFaceText.text += text[i];
            faceText.text += text[i];

            yield return new WaitForSeconds(delay);
        }
    }
}
