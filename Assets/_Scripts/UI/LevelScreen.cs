using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour, SaveGameObj
{
    [SerializeField] private GameObject[] Buttons;

    private int currentOptionIndex;
    private int unlockedLevels;
    private bool canMove;

    [SerializeField] private AudioClip changedSourceEffect;
    [SerializeField] private AudioClip selectedSourceEffect;

    private void OnEnable()
    {
        SaveManager.instance.LoadAllData();
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (UIManager.GodMode == true)
            {
                Buttons[i].GetComponent<Button>().interactable = true;
            }
            else if (this.unlockedLevels >= i)
            {
                Buttons[i].GetComponent<Button>().interactable = true;
            }
        }
        canMove = true;

    }

    private void Update()
    {
        if (canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeOption(-1);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeOption(1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Buttons[currentOptionIndex].GetComponent<Button>().IsInteractable())
                {
                    Interact();
                }
            }
        }
    }

    private void ChangeOption(int _change)
    {
        currentOptionIndex += _change;
        SoundManager.Instance.PlaySound(changedSourceEffect);

        if (currentOptionIndex < 0)
        {
            currentOptionIndex = Buttons.Length - 1;
        }
        else if (currentOptionIndex > Buttons.Length - 1)
        {
            currentOptionIndex = 0;
        }

        Buttons[currentOptionIndex].GetComponent<Animator>().SetTrigger(Buttons[currentOptionIndex].GetComponent<Button>().animationTriggers.highlightedTrigger);
    }

    private void Interact()
    {
        canMove = false;
        Buttons[currentOptionIndex].GetComponent<Animator>().SetTrigger(Buttons[currentOptionIndex].GetComponent<Button>().animationTriggers.pressedTrigger);
        SoundManager.Instance.PlaySound(selectedSourceEffect);
    }

    public void SaveData(ref SaveDataGame data)
    {

    }

    public void LoadData(SaveDataGame data)
    {
        this.unlockedLevels = data.unlockedLevels;
    }
}
