using System;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtMessage;

    public Action<bool> OnMessageBoxClosedEvent;

    public void Show(string message) {
        txtMessage.text = message;
    }

    public void Close(bool yes) {

        OnMessageBoxClosedEvent?.Invoke(yes);

        Destroy(this.gameObject);
    }
}
