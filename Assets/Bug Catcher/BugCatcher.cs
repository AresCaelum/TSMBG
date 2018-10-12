/// <summary>
/// Create an object with the Bug Catcher script and you are ready to go.
/// Use BugCatcher.Instance.ToggleDisplay() to toggle the Window on and off.
/// Use BugCatcher.Instance.AddEntry() to create an entry of your own into the BugCatcher.
/// Set BugCatcher.Instance.AllowDebugLog to true to allow the Bug Catcher to create entries for Debug.Log entries.
/// 
/// Use BugCatcherTest.cs for examples on how to use the BugCatcher.
/// </summary>

using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;

public enum MASTER_DEBUGGER_MESSAGE_TYPE { ALL, OTHER, NULL, SUCCESS, FAILURE, WARNING, DEBUG, EXCEPTION, LOG_MESSAGE_REVEIVED, ERROR, ASSERT, _LAST_ }
public class BugCatcher : MonoBehaviour
{
    /// <summary>
    /// Instance
    /// </summary>
    private static BugCatcher bugCatcher;
    public static BugCatcher Instance
    {
        get
        {
            return bugCatcher;
        }
    }

    /// <summary>
    /// Bug Catcher Variables
    /// </summary>
    List<Entry> Entries = new List<Entry>();
    string ExportFileName = "/BugCatcherLog.txt";
    string ToSave = "";
    bool Initialized = false;
    [Tooltip("Toggles the BugCatcher window On or Off.")]
    public bool Display = false;
    [Tooltip("Allows or Disallows Debug.Log entries in the BugCatcher window.")]
    public bool AllowDebugLog = false;
    [Tooltip("A small button on the corner of the screen that can toggle the BugCatcher window On or Off.")]
    public bool AllowCornerToggle = true;
    [Tooltip("Where in the screen the Corner Toggle will be and what size.")]
    public Rect CornerToggleRect = new Rect(0, 0, 50, 50);

    /// <summary>
    /// Log Variables
    /// </summary>
    Vector2 scrollPosition = Vector2.zero;
    Rect windowRect;
    Vector2 BoxPosition = new Vector2(290, 10);
    Vector2 BoxSize = new Vector2(760, 800);
    Vector2 ScrollViewSize = new Vector2(930, 0);

    /// <summary>
    /// Stats Variables
    /// </summary>
    Vector2 StatsBoxPosition = new Vector2(25, 50);
    Vector2 StatsBoxSize = new Vector2(250, 730);

    /// <summary>
    /// Export Button
    /// </summary>
    Vector2 ExportButtonPosition = new Vector2(22, 785);
    Vector2 ExportButtonSize = new Vector2(250, 65);

    /// <summary>
    /// Entry Variables
    /// </summary>
    int rectPositionX = 300;
    int rectPositionY = 10;
    int buttonWidth = 700;
    int entrySeparation = 5;
    GUIStyle entryStyle;

    /// <summary>
    /// Drop Down Variables
    /// </summary>
    private Vector2 scrollViewVector = Vector2.zero;
    Rect dropDownRect = new Rect(850, 50, 220, 300);
    string[] list = { "ALL", "OTHER", "NULL", "SUCCESS", "FAILURE", "WARNING", "DEBUG", "EXCEPTION", "LOG MESSAGE", "ERROR", "ASSERT", };
    int FilterIndex;
    bool show = false;

    void Awake()
    {
        if (bugCatcher != null)
        {
            Destroy(gameObject);
        }
        bugCatcher = this;
        DontDestroyOnLoad(gameObject);
        windowRect = new Rect(290, 10, 1075, 870);
        Application.logMessageReceived += HandleLog;
        Initialized = true;

        entryStyle = new GUIStyle();
        entryStyle.fontSize = 25;
        entryStyle.wordWrap = true;
        entryStyle.fixedWidth = buttonWidth;
        entryStyle.alignment = TextAnchor.UpperLeft;

        AddEntry("Initial Entry; Disregard");
    }

    void OnDestroy()
    {
        if (Initialized)
        {
            Application.logMessageReceived -= HandleLog;
        }
    }

    void OnGUI()
    {
        if (AllowCornerToggle)
        {
            if (GUI.Button(CornerToggleRect, "<size=25><></size>"))
            {
                ToggleDisplay();
            }
        }
        if (!Display)
            return;
        windowRect = GUI.Window(9991, windowRect, DrawWindow, "<size=25><color=orange>Bug Catcher</color></size>");
    }

    void DrawStats()
    {
        GUI.Box(new Rect(StatsBoxPosition, StatsBoxSize), "<size=25><color=red>Stats</color></size>");

        string data = "<b>Frame Data</b>\n";

        //FRAMES
        data += "<i>FPS:</i> \n" + (Mathf.Round((1 / Time.deltaTime))) + "\n";
        data += "<i>Render:</i> \n" + (Time.deltaTime * 1000f).ToString("F2") + "ms\n";

        data += "\n<b>Memory</b>\n";
        //MEMORY
        data += "<i>Allocated Memory:</i> " + (((Profiler.GetTotalAllocatedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";
        data += "<i>Unused Memory:</i> " + (((Profiler.GetTotalUnusedReservedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";
        data += "<i>Reserved Memory:</i> " + (((Profiler.GetTotalReservedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";

        data += "\n<b>Screen</b>\n";
        //RESOLUTION
        data += "<i>Resolution:</i> \n" + Screen.currentResolution + "\n";
        data += "<i>Screen DPI:</i> " + Screen.dpi + "\n";

        data += "\n<b>Version</b>\n";
        //Version
        data += "<i>Unity Version:</i> \n" + Application.unityVersion + "\n";
        data += "<i>App Version:</i> \n" + Application.version + "\n";

        data += "<i>Platform:</i> \n" + Application.platform + "\n";
        data += "<i>Persistent Path:</i> \n" + Application.persistentDataPath + "\n";

        GUI.Label(new Rect(StatsBoxPosition.x, StatsBoxPosition.y + 30, StatsBoxSize.x, StatsBoxSize.y), "<size=21>" + data + "</size>");
    }

    void DrawWindow(int id)
    {
        if (Entries.Count > 0)
        {
            DrawStats();

            GUI.Box(new Rect(BoxPosition.x, BoxPosition.y + 40, BoxSize.x, BoxSize.y), "<size=25><color=red>Log</color></size>");
            scrollPosition = GUI.BeginScrollView(new Rect(10, 90, ScrollViewSize.x * 1.1f, 680), scrollPosition, new Rect(0, 0, ScrollViewSize.x, ScrollViewSize.y + 10));
            DrawEntries();
            GUI.EndScrollView();
            DropDown();

            if (GUI.Button(new Rect(ExportButtonPosition, ExportButtonSize), "<size=25>Export</size>"))
            {
                Export();
            }

            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
    }

    void DrawEntries()
    {
        rectPositionY = 10;
        ScrollViewSize.y = 0.0f;
        foreach (Entry entry in Entries)
        {
            if (entry.Draw(new Vector2(rectPositionX, rectPositionY), FilterIndex))
            {
                rectPositionY += (int)entry.Size.y + entrySeparation;
                ScrollViewSize.y += entry.Size.y + entrySeparation;
            }
        }
    }

    void DropDown()
    {
        if (GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
        {
            if (!show)
            {
                show = true;
            }
            else
            {
                show = false;
            }
        }

        if (show)
        {
            scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height), scrollViewVector, new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))));

            GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))), "");

            for (int index = 0; index < list.Length; index++)
            {

                if (GUI.Button(new Rect(0, (index * 25), dropDownRect.height, 25), ""))
                {
                    show = false;
                    FilterIndex = index;
                }

                GUI.Label(new Rect(5, (index * 25), dropDownRect.height, 150), "<size=25>" + list[index] + "</size>");

            }

            GUI.EndScrollView();
        }
        else
        {
            GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 150), "<size=25>" + list[FilterIndex] + "</size>");
        }
    }

    string MessageType(MASTER_DEBUGGER_MESSAGE_TYPE messageType)
    {
        switch (messageType)
        {
            case MASTER_DEBUGGER_MESSAGE_TYPE.OTHER:
                {
                    return "Message Type: <color=black>Other</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.NULL:
                {
                    return "Message Type: <color=red>Null</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.EXCEPTION:
                {
                    return "Message Type: <color=red>Exception</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.SUCCESS:
                {
                    return "Message Type: <color=green>Success</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.FAILURE:
                {
                    return "Message Type: <color=purple>Failure</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.WARNING:
                {
                    return "Message Type: <color=orange>Warning</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.DEBUG:
                {
                    return "Message Type: <color=gray>Debug</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.LOG_MESSAGE_REVEIVED:
                {
                    return "Message Type: <color=red>Log Message</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.ASSERT:
                {
                    return "Message Type: <color=yellow>Assert</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE.ERROR:
                {
                    return "Message Type: <color=magenta>Error</color>";
                }
            case MASTER_DEBUGGER_MESSAGE_TYPE._LAST_:
                {
                    return "Message Type: <color=gray>_LAST_ USED - DO NOT USE LAST</color>";
                }
            default:
                {
                    return "";
                }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string toDisplay = "";
        MASTER_DEBUGGER_MESSAGE_TYPE TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.ALL;
        switch (type)
        {
            case LogType.Assert:
                {
                    TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.ASSERT;
                    toDisplay = "<color=gray>" + logString + "</color>";
                    break;
                }
            case LogType.Error:
                {
                    TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.ERROR;
                    toDisplay = "<color=red>" + logString + "</color>";
                    break;
                }
            case LogType.Exception:
                {
                    TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.EXCEPTION;
                    toDisplay = "<color=red>" + logString + "</color>";
                    break;
                }
            case LogType.Log:
                {
                    if (!AllowDebugLog)
                        return;
                    TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.LOG_MESSAGE_REVEIVED;
                    toDisplay = "<color=gray>" + logString + "</color>";
                    break;
                }
            case LogType.Warning:
                {
                    TypeOfMessage = MASTER_DEBUGGER_MESSAGE_TYPE.WARNING;
                    toDisplay = "<color=yellow>" + logString + "</color>";
                    break;
                }
        }
        stackTrace = stackTrace.Replace("\n", "");

        toDisplay += "\n" + "<color=orange>" + stackTrace + "</color>";
        AddEntry(toDisplay, TypeOfMessage);
    }

    void AddDebugWindowEntry(string entryMessage, MASTER_DEBUGGER_MESSAGE_TYPE entryType)
    {
        Entries.Add(new Entry(entryMessage, entryType, entryStyle));
    }

    void Export()
    {
        string data = "\nFrame Data\n";

        //FRAMES
        data += "FPS: " + (Mathf.Round((1 / Time.deltaTime))) + "\n";
        data += "Render: " + (Time.deltaTime * 1000f).ToString("F2") + "ms\n";

        data += "\nMemory\n";
        //MEMORY
        data += "Allocated Memory: " + (((Profiler.GetTotalAllocatedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";
        data += "Unused Memory: " + (((Profiler.GetTotalUnusedReservedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";
        data += "Reserved Memory: " + (((Profiler.GetTotalReservedMemoryLong()) / 1024) / 1024).ToString() + "MBs\n";

        data += "\nScreen\n";
        //RESOLUTION
        data += "Resolution: " + Screen.currentResolution + "\n";
        data += "Screen DPI: " + Screen.dpi + "\n";

        data += "\nVersion\n";
        //Version
        data += "Unity Version: " + Application.unityVersion + "\n";
        data += "App Version: " + Application.version + "\n";

        data += "Platform: " + Application.platform + "\n";
        data += "Persistent Path: " + Application.persistentDataPath + "\n";

        data += "Time Since Startup: " + Time.realtimeSinceStartup + "\n";
        data += "Total Frames Since Startup: " + Time.frameCount + "\n";
        data += "Total Rendered Frames Since Startup: " + Time.renderedFrameCount + "\n";

        data += "\nOther\n";
        data += "Installed by: " + Application.installerName + "\n";
        data += "Network Reachability: " + Application.internetReachability + "\n";

        string toRemove = "<color=gray>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<color=red>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<color=blue>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<color=yellow>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<color=orange>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<color=black>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "</color>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<size=25>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "</size>";
        ToSave = ToSave.Replace(toRemove, "");

        toRemove = "<b>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "</b>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "<i>";
        ToSave = ToSave.Replace(toRemove, "");
        toRemove = "</i>";
        ToSave = ToSave.Replace(toRemove, "");

        File.WriteAllText(Application.persistentDataPath + ExportFileName, ToSave + data);
    }

    public void AddEntry(string message, MASTER_DEBUGGER_MESSAGE_TYPE messageType = MASTER_DEBUGGER_MESSAGE_TYPE.OTHER, string messageColor = "blue", string fileColor = "blue", string lineColor = "blue", string methodColor = "blue")
    {
        //Grab stack frame to grab information from.
        StackFrame stackFrame = new StackFrame(1, true);
        string fileName = Path.GetFileName(stackFrame.GetFileName());
        string method = stackFrame.GetMethod().ToString();
        int line = stackFrame.GetFileLineNumber();

        //Color to print the message on.
        string entryNumberColStart = "<color=black>";
        string messageColStart = "<color=" + messageColor + ">";
        string fileColStart = "<color=" + fileColor + ">";
        string lineColStart = "<color=" + lineColor + ">";
        string methodcolStart = "<color=" + methodColor + ">";
        string colEnd = "</color>";

        //Message to be printed with the color.
        string entryNumber = "<b>" + entryNumberColStart + (Entries.Count + 1) + ") " + colEnd;

        string DebugMessageToPrint = "";
        //if (messageType != MASTER_DEBUGGER_MESSAGE_TYPE.LOG_MESSAGE_REVEIVED)
        //{
        DebugMessageToPrint = entryNumber +
                                MessageType(messageType) + "</b>" + "\t<i>Time: " + System.DateTime.Now.ToLongTimeString() + "</i>\nMessage: " +
                                messageColStart + message + colEnd + "\n" +
                                "File: " + fileColStart + fileName + colEnd +
                                " | Line: " + lineColStart + " line " + line + colEnd +
                                " | Method: " + methodcolStart + method + colEnd;
        //}
        //else
        //{
        //    DebugMessageToPrint = entryNumber +
        //                            MessageType(messageType) + "</b>" + "\t<i>Time: " + System.DateTime.Now.ToLongTimeString() + "</i>\nMessage: " +
        //                            messageColStart + message + colEnd;
        //}

        AddDebugWindowEntry(DebugMessageToPrint, messageType);
    }

    public void ToggleDisplay()
    {
        Display = !Display;
    }
}