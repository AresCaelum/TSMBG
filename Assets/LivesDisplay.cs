using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LivesDisplay : MonoBehaviour
{
    public List<Image> livesDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AudioHolder.instance != null)
        {
            for (int i = 0; i < 3; ++i)
            {
                Color color = Color.black;
                if (i < AudioHolder.GetLives())
                {
                    color = Color.yellow;
                }

                livesDisplay[i].color = color;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioHolder.UseLife();
        }
    }
}
