using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DirectoryButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI folderNameText;

    public string Directory;
    public string FolderName;

    public void Initialize()
    {
        folderNameText.text = FolderName;
    }
}