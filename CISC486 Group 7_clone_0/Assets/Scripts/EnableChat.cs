using UnityEngine;

public class EnableChat : MonoBehaviour
{
    public GameObject chat;

    // Start is called before the first frame update
    void Start()
    {
        CreateChat();
    }

    void CreateChat()
    {
        Instantiate(chat, gameObject.transform.position, Quaternion.identity);
    }
}
