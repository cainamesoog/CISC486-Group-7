using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PurrNet;

public class Chat : NetworkIdentity
{
    [Header("UI References")]
    public GameObject chatPanel;
    public InputField chatInput;
    public Transform messageContainer;
    public GameObject messagePrefab;
    public ScrollRect scrollRect;

    [Header("Settings")]
    public KeyCode openChatKey = KeyCode.T;

    private bool isChatOpen = false;

    void Start()
    {
        chatInput.onEndEdit.AddListener(OnInputEndEdit);
        chatPanel.SetActive(true);
        chatInput.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(openChatKey) && !isChatOpen)
        {
            OpenChat();
        }
    }

    void OpenChat()
    {
        isChatOpen = true;
        chatInput.gameObject.SetActive(true);
        chatInput.Select();
        chatInput.ActivateInputField();
    }

    void CloseChat()
    {
        isChatOpen = false;
        chatInput.text = "";
        chatInput.gameObject.SetActive(false);
    }

    void OnInputEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(text))
        {
            AddMessage("You", text);
            CloseChat();
        }
    }

    void AddMessage(string sender, string message)
    {
        if (messagePrefab == null) return;

        GameObject newMessage = Instantiate(messagePrefab, messageContainer);
        Text messageText = newMessage.GetComponentInChildren<Text>();

        if (messageText != null)
        {
            messageText.text = $"{sender}: {message}";
        }

        // Scroll to bottom after a frame
        StartCoroutine(ScrollToBottomCoroutine());
    }

    IEnumerator ScrollToBottomCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}