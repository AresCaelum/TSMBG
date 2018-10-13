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
    private Button myButton;
    private TextMeshProUGUI tmp;
    private Image icon;
    private string path;
    private FileType fileType = FileType.FT_File;
    private void Awake()
    {
        myButton = GetComponent<Button>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        FileBrowser.ArrowUsed += UpdateButton;
        FileBrowser.DirectoryChanged += UpdateButton;
        myButton.onClick.AddListener(Use);
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

        myButton.interactable = !path.Equals("");

        if (myButton.interactable)
        {
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
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
    }

    public void Use()
    {
        newPath = Path.GetFullPath(path);
        
        switch (fileType)
        {
            case FileType.FT_File:
                newPath = "file:///" + newPath;
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
