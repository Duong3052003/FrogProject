using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectedArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rectTransform;
    private int currentOptionIndex;

    [SerializeField] private AudioClip changedSourceEffect;
    [SerializeField] private AudioClip selectedSourceEffect;
    private Animator animator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangeOption(int _change)
    {
        currentOptionIndex += _change;
        SoundManager.Instance.PlaySound(changedSourceEffect);

        if (currentOptionIndex < 0)
        {
            currentOptionIndex = options.Length-1;
        }
        else if(currentOptionIndex > options.Length-1)
        {
            currentOptionIndex=0;
        }
        rectTransform.position = new Vector3(rectTransform.position.x, options[currentOptionIndex].position.y, rectTransform.position.z);
    }

    private void Interact()
    {
        animator.SetTrigger("Interacted");

        SoundManager.Instance.PlaySound(selectedSourceEffect);
    }
    private void Interacted()
    {
        options[currentOptionIndex].GetComponent<Button>().onClick.Invoke();
    }
}
