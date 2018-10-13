using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public enum FileType
{
    FT_File,
    FT_Directory
}

public class SelectFile : MonoBehaviour {
    public static event Action OpenedDirectory = delegate { };
    public static event Action UsedFile = delegate { };

    public static string newPath;
    public int index = 0;
    private TextMeshProUGUI tmp;
    private Image icon;
    private string path;
    private FileType fileType = FileType.FT_File;
    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        FileBrowser.ArrowUsed += UpdateButton;
        FileBrowser.DirectoryChanged += UpdateButton;
    }

    private void OnDestroy()
    {
        FileBrowser.ArrowUsed -= UpdateButton;
        FileBrowser.DirectoryChanged -= UpdateButton;
    }

    public void SetText(string thePath)
    {
        path = thePath;
        tmp.text = Path.GetFileName(path);

        string extension = Path.GetExtension(path);
        if (extension == null || extension.Equals(""))
        {
            fileType = FileType.FT_Directory;
            icon.color = Color.yellow;
        } 
        else
        {
            icon.color = Color.blue;
            fileType = FileType.FT_File;
        }
    }

    public void Use()
    {
        newPath = path;
        switch (fileType)
        {
            case FileType.FT_File:
                newPath = "file:///" + path;
                UsedFile.Invoke();
                break;
            case FileType.FT_Directory:
                if (path.Equals(""))
                    return;
                OpenedDirectory.Invoke();
                break;
        }
    }

    void UpdateButton()
    {
        SetText(FileBrowser.GetID(index));
    }
}
