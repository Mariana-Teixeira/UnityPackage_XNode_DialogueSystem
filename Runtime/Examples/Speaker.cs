using DialogueSystem;
using PlasticGui.PreferencesWindow;
using System;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph m_dialogueGraph;

    private DialogueManager m_dialogueManager;

    public DialogueGraph DialogueGraph
    {
        get
        {
            return m_dialogueGraph;
        }
    }

    private void Awake()
    {
        m_dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_dialogueManager.IsRunning) m_dialogueManager.Click();
            else m_dialogueManager.SetGraph(m_dialogueGraph);
        }
    }
}