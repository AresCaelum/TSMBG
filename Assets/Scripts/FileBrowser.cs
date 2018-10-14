using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class FileBrowser : MonoBehaviour
{
    public static event Action ArrowUsed = delegate { };
    public static event Action DirectoryChanged = delegate { };
    int currentIndex = 0;
    static List<String> storedFiles = new List<string>();
    public GameObject FileBrowserCanvas;
    public TextMeshProUGUI title;

    static FileBrowser instance;

    private static string GetAndroidExternalFilesDir()
    {
#if UNITY_ANDROID
        try
        {

            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // Get all available external file directories (emulated and sdCards)
                    AndroidJavaObject[] externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null);
                    AndroidJavaObject emulated = null;
                    AndroidJavaObject sdCard = null;

                    for (int i = 0; i < externalFilesDirectories.Length; i++)
                    {
                        AndroidJavaObject directory = externalFilesDirectories[i];
                        using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
                        {
                            // Check which one is the emulated and which the sdCard.
                            bool isRemovable = environment.CallStatic<bool>("isExternalStorageRemovable", directory);
                            bool isEmulated = environment.CallStatic<bool>("isExternalStorageEmulated", directory);
                            if (isEmulated)
                                emulated = directory;
                            else if (isRemovable && isEmulated == false)
                                sdCard = directory;
                        }
                    }
                    // Return the sdCard if available
                    if (sdCard != null)
                        return sdCard.Call<string>("getAbsolutePath");
                    else
                        return emulated.Call<string>("getAbsolutePath");
                }
            }
        }
        catch (Exception)
        {
            //if exception is not accesible
            return Application.persistentDataPath;
        }
#else
        return Application.persistentDataPath;
#endif
    }

    // Use this for initialization
    IEnumerator Start()
    {
        if (instance == null)
        {
            instance = this;
            SelectFile.newPath = GetAndroidExternalFilesDir();
            title.text = "Location: " + Path.GetFullPath(SelectFile.newPath);
            string[] files = Directory.GetDirectories(SelectFile.newPath);
            foreach (string sz in files)
            {
                storedFiles.Add(sz);
            }
            files = Directory.GetFiles(SelectFile.newPath);
            foreach (string sz in files)
            {
                storedFiles.Add(sz);
            }
            yield return new WaitForSeconds(1);
            DirectoryChanged.Invoke();

            SelectFile.OpenedDirectory += ChangeDirectory;
            SelectFile.UsedFile += CloseScreen;
        }
    }


    public static void CloseScreen()
    {
        if (instance != null)
        {
            storedFiles.Clear();
            Destroy(instance.FileBrowserCanvas);
            instance = null;
        }
    }
    private void OnDestroy()
    {
        SelectFile.OpenedDirectory -= ChangeDirectory;
    }

    public void Increment()
    {
        currentIndex++;
        ArrowUsed.Invoke();
    }

    public void Decrement()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = 0;
        ArrowUsed.Invoke();
    }

    public void GoUpDirectory()
    {
        SelectFile.newPath = Path.GetFullPath(SelectFile.newPath + "/..");
        Debug.Log(SelectFile.newPath);

        ChangeDirectory();
    }

    public void ChangeDirectory()
    {
        title.text = "Location: " + Path.GetFullPath(SelectFile.newPath);
        currentIndex = 0;
        string[] files = Directory.GetDirectories(SelectFile.newPath);
        storedFiles.Clear();

        foreach (string sz in files)
        {
            if (HavePermissions(sz))
                storedFiles.Add(Path.GetFullPath(sz));
        }
        files = Directory.GetFiles(SelectFile.newPath);
        foreach (string sz in files)
        {
            if (HasSupportedExtension(sz))
                storedFiles.Add(Path.GetFullPath(sz));
        }

        Debug.Log(SelectFile.newPath + "  - " + storedFiles.Count);

        DirectoryChanged.Invoke();
    }


    bool HavePermissions(string path)
    {
        DirectoryInfo realpath = new DirectoryInfo(path);
        try
        {
            //if GetDirectories works then is accessible
            realpath.GetDirectories();
            return true;
        }
        catch (Exception)
        {
            //if exception is not accesible
            return false;
        }
    }


    bool HasSupportedExtension(string path)
    {
        String extension = Path.GetExtension(path).ToLower();

        return extension.Contains("mp3") || extension.Contains("wav") || extension.Contains("ogg") || extension.Contains("aif") || extension.Contains("mod") || extension.Contains("it") || extension.Contains("s3m") || extension.Contains("xm");
    }

    public static string GetID(int index)
    {
        if (index + instance.currentIndex < storedFiles.Count)
        {
            //Debug.Log(storedFiles[index + currentIndex]);
            return storedFiles[index + instance.currentIndex];
        }

        return "";
    }
}
