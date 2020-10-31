using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopUpManager
{
    public static MessageBox NewMessageBox(Action<bool> OnMessageBoxClosed) {       
            MessageBox msgBox = GameObject.Instantiate(PopUpManagerMain.instance.prbMessageBox, PopUpManagerMain.instance.canvas.transform).GetComponent<MessageBox>();

        msgBox.OnMessageBoxClosedEvent += OnMessageBoxClosed;
        
        return msgBox;
    }

    internal static MessageBox NewMessage(Action<bool> OnMessageBoxClosed) {
        MessageBox msgBox = GameObject.Instantiate(PopUpManagerMain.instance.prbMessageBoxYES, PopUpManagerMain.instance.canvas.transform).GetComponent<MessageBox>();

        msgBox.OnMessageBoxClosedEvent += OnMessageBoxClosed;

        return msgBox;
    }
}
