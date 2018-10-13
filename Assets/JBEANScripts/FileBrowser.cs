using UnityEngine;
using System.IO;
using System;
using System.Collections;

public class FileBrowser : MonoBehaviour {
    public static event Action ArrowUsed = delegate { };
    public static event Action DirectoryChanged = delegate { };
    int currentIndex = 0;
    static string[] files;
    // Use this for initialization
    IEnumerator Start () {
        SelectFile.newPath = Application.persistentDataPath;
        files = Directory.GetFiles(SelectFile.newPath);
        yield return new WaitForSeconds(1);
        DirectoryChanged.Invoke();
    }

    public void Increment()
    {
        currentIndex++;

        ArrowUsed.Invoke();
    }

    public void Decrement()
    {
        currentIndex--;

        ArrowUsed.Invoke();
    }

    public void GoUpDirectory()
    {
        files = Directory.GetFiles(SelectFile.newPath);
        DirectoryChanged.Invoke();
    }

    public static string GetID(int index)
    {
        if(index < files.Length)
        {
            return files[index];
        }

        return "";
    }
}
