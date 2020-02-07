using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Narrative : MonoBehaviour
{
    [SerializeField]
    public TextAsset jsonAss;

    [SerializeField]
    private Story inkStory;

    [SerializeField]
    private Canvas canvas;

    // UI Prefabs
    [SerializeField]
    private TMP_Text textPrefab;

    [SerializeField]
    public GameObject textBox;

    [SerializeField]
    private Button buttonPrefab;

    [SerializeField]
    private Image Frank;

    public Button contintueButton; 

    [SerializeField]
    private GameObject speakerTag;

    [SerializeField]
    private Image Karen;

    [SerializeField]
    private List<GameObject> cardList;

    [SerializeField]
    private ShowDescriptionText s;

    private bool isFrank;
    [SerializeField]
    public bool cardChooing = false;

    private void Awake()
    {
        Debug.Log("Awake");
        inkStory = new Story(jsonAss.text);
        isFrank = true;
        DontDestroyOnLoad(this);

        readStory();
    }

    public void readStory()
    {
        if (inkStory.canContinue && !cardChooing)
        {
            Debug.Log("Start reading");
            StartCoroutine("WaitForClick", inkStory.Continue());

            if (inkStory.currentTags.Count >= 1) 
            {
                if (inkStory.currentTags[0] == "F")
                {
                    Debug.Log("Frank is talking");
                    speakerTag.GetComponentInChildren<TextMeshProUGUI>().text = "Frank";
                    speakerTag.gameObject.SetActive(true);
                }
                else if (inkStory.currentTags[0] == "K")
                {
                    Debug.Log("Karen is talking");
                    speakerTag.GetComponentInChildren<TextMeshProUGUI>().text = "Karen";
                    speakerTag.gameObject.SetActive(true);
                }
                else if (inkStory.currentTags[0].Length >=6 && inkStory.currentTags[0].Substring(0, 6) == "Cards-")
                {
                    Debug.Log("Change Scene to: " + inkStory.currentTags[0]);
                    StopAllCoroutines();
                    SceneManager.LoadScene(inkStory.currentTags[0]);
                    Karen.gameObject.SetActive(false);
                    Frank.gameObject.SetActive(false);
            
                    PlaceCards();
                }
                else if (inkStory.currentTags[0].Length >= 5 && inkStory.currentTags[0].Substring(0, 5) == "Chair")
                {
                    if (inkStory.currentTags[0].Substring(5) == "Frank")
                    {
                        Frank.gameObject.SetActive(true);
                    }
                    else if (inkStory.currentTags[0].Substring(5) == "Karen")
                    {
                        Karen.gameObject.SetActive(true);
                    }

                    Debug.Log(inkStory.currentTags[0]);
                    contintueButton.gameObject.SetActive(false);
                    SceneManager.LoadScene(inkStory.currentTags[0]);
                }
                else if (inkStory.currentTags[0].Length >=  7 && inkStory.currentTags[0].Substring(0, 7) == "Endings")
                {
                    Debug.Log("Endings");
                    if (inkStory.currentTags[0].Substring(7) == "Frank")
                    {
                        ShowEndings(CardLocations.Endings("Frank"));
                    }
                    else if (inkStory.currentTags[0].Substring(7) == "Karen")
                    {
                        ShowEndings(CardLocations.Endings("Karen"));
                    }
                }

                switch (inkStory.currentTags[0])
                {
                    /*
                    case "Chair":
                        contintueButton.gameObject.SetActive(false);
                        SceneManager.LoadScene("Chair");
                            if (isFrank)
                            {
                                audScript.audSource.clip = audScript.frankAud;
                            }
                            else
                            {
                                audScript.audSource.clip = audScript.karenAud;
                            }
                            break; */
                    case "Frank": 
                        Debug.Log("Frank");
                        Frank.gameObject.SetActive(true);
                    break;
                    case "DisableFrank":
                        Debug.Log("DisableFrank");
                        Frank.gameObject.SetActive(false);
                        break;
                    case "Karen":
                        Debug.Log("Karen");
                        Karen.gameObject.SetActive(true);
                    break;
                    case "DisableKaren":
                        Debug.Log("DisableKaren");
                        Karen.gameObject.SetActive(false);
                        break;
                    case "DisableCards":
                        Debug.Log("Disable Cards: " + s.isDisabled);
                        s.isDisabled = true;
                    break;
                    default:
                        Debug.Log("Nothing to see here...");
                    break; 
                }
            }       
        }
        else
        {
            if(inkStory.currentChoices.Count > 0 && !cardChooing)
            {
                makeChoice();
            }
        }
    }

    private void PlaceCards()
    {
        contintueButton.gameObject.SetActive(true);
        textBox.SetActive(false);
        s.isDisabled = false;
        cardChooing = true;

        Debug.Log("Place");
        Debug.Log("Card Choosing: " + cardChooing);
    }

    IEnumerator WaitForClick(string text)
    {
        textPrefab.text = text;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit"));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"));
        speakerTag.gameObject.SetActive(false);
        readStory();
    }

    private void makeChoice()
    {

        for (int i = 0; i < inkStory.currentChoices.Count; i++)
        {
            Debug.Log("Make Choice");
            Choice choice = inkStory.currentChoices[i];
            Button button = CreateChoiceView(choice.text);
            // Tell the button what to do when we press it
            button.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });
        }
    }

    private Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
        choiceText.text = text;
        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;
        return choice; 

    }

    private void OnClickChoiceButton(Choice choice)
    {
        inkStory.ChooseChoiceIndex(choice.index);
        clearButtons();
        readStory();
    }

    public void ContinueFromStop() 
    {
        Debug.Log("re-start coroutine");
        StartCoroutine("WaitForClick", inkStory.Continue());
    }

    private void clearButtons()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    } 

    private void ShowEndings(string[] ends)
    {
        // Frank_Fool_Past
        // Frank_High_Past
        //Frank_Wheel_Past
        // inkStory.ChoosePathString("myKnotName");

        inkStory.ChoosePathString(ends[0]);

    }
}
