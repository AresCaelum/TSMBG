using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Entry
{
    GUIStyle Style;
    GUIContent Content;
    MASTER_DEBUGGER_MESSAGE_TYPE TypeOfEntry;
    public Vector2 Size;

    public Entry(string text, MASTER_DEBUGGER_MESSAGE_TYPE typeOfEntry, GUIStyle style)
    {
        Size = Vector2.zero;
        TypeOfEntry = typeOfEntry;
        Style = style;
        Content = new GUIContent(text);
        Size.x = Style.fixedWidth;
        Size.y = Style.CalcHeight(Content, Size.x) + 10;
    }

    public bool Draw(Vector2 position, int filterIndex)
    {
        if ((int)TypeOfEntry == filterIndex || filterIndex == 0)
        {
            Rect box = new Rect(position, Size);
            GUI.Box(box, "");
            GUI.Label(box, Content, Style);
            return true;
        }
        return false;
    }
}