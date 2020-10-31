using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] Text txtMessage;

    public Action<bool> OnMessageBoxClosedEvent;

    public void Show(string message) {
        txtMessage.text = message;
    }

    public void Close(bool yes) {

        OnMessageBoxClosedEvent?.Invoke(yes);

        Destroy(this.gameObject);
    }
}
