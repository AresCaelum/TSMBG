using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static int ProjectileSpawnRate = 3;
    public AudioSource Player;
    public WaveBuilder builder;
    public float delay = 3.0f;
    public float stageSpeed = -4.0f;
    public Rigidbody2D stageMover;
    public float scale = 100.0f;
    public Player thePlayer;
    public Projectile projectilePrefab;

    public float RmsValue;
    public float DbValue;
    public float PitchValue;

    public int BuffersToUse = 25;
    public int ChannelsToUse = 10;

    private const int QSamples = 512;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;
    private Queue<float> sampleBuffer = new Queue<float>();
    private float spectrum = 0.0f;
    //private bool SongFinished = false;
    private Coroutine stageCompletion;

    int bps = 0;
    int lastBPS = 0;
    int currentBPS = 0;
    int averageBPS;
    int totalBeats = 0;
    public float proceduralScale = 0.1f;
    public float proceduralOffset = 0.0f;
    bool stageCompleted = false;

    public void IncrementBPS()
    {
        bps++;
        totalBeats++;
        if (thePlayer != null && totalBeats % ProjectileSpawnRate == 0)
        {
            float rate = Mathf.PerlinNoise((Mathf.RoundToInt(Player.time) * proceduralScale) + proceduralOffset, proceduralOffset);
            int Position = (Mathf.RoundToInt((rate - 0.5f) * (6 - (2 - ProjectileSpawnRate+1))));
            Instantiate(projectilePrefab, new Vector3(transform.position.x, Mathf.Max(thePlayer.transform.position.y + Position, transform.position.y + 1), transform.position.z), Quaternion.identity);
        }
    }

    void AnalyzeSound()
    {
        Player.GetOutputData(_samples, 0);
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        Player.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency

        sampleBuffer.Enqueue(_spectrum[0]);
        if (sampleBuffer.Count >= BuffersToUse)
        {
            spectrum *= BuffersToUse;
            spectrum -= sampleBuffer.Dequeue();
            spectrum += _spectrum[0];
            spectrum = spectrum / (float)BuffersToUse;
        }
        else
        {
            spectrum = (spectrum + _spectrum[0]) * 0.5f;
        }
        //spectrum = CalculateSpectrum();
    }

    float CalculateSpectrum()
    {
        float ratio = 0.0f;
        for (int i = 0; i < ChannelsToUse; ++i)
        {
            ratio += _spectrum[i];
        }

        return ratio / (float)ChannelsToUse * scale;
    }

    IEnumerator HandleBPSData()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            lastBPS = currentBPS;
            currentBPS = bps * 4;
            if (currentBPS <= lastBPS)
            {
                stageMover.velocity = new Vector2(stageMover.velocity.x + 0.1f, 0f);
                if (stageMover.velocity.x > stageSpeed - currentBPS)
                {
                    stageMover.velocity = new Vector2(stageSpeed - currentBPS, 0f);

                }
            }
            else
            {
                stageMover.velocity = new Vector2(stageMover.velocity.x - 0.1f, 0f);
                if (stageMover.velocity.x < stageSpeed - currentBPS)
                {
                    stageMover.velocity = new Vector2(stageSpeed - currentBPS, 0f);

                }
            }
            //Debug.Log(averageBPS + " - BPS");
            //stageMover.velocity = new Vector2(stageSpeed - averageBPS, 0f);
            bps = 0;
        }
    }

    ////////////////////////////
    // Use this for initialization
    IEnumerator Start()
    {
        //Random.InitState(AudioHolder.instance.GetSongName());
        proceduralOffset = (Mathf.Abs(AudioManager.SongName.GetHashCode()) % 2048) * proceduralScale; 
        Debug.Log("Seed: " + proceduralOffset);

        StartCoroutine(HandleBPSData());
        if (builder != null)
        {
            transform.localScale = new Vector3(builder.transform.localScale.x, builder.transform.localScale.y, 1f);
            transform.position = new Vector3(transform.position.x + (transform.localScale.x - 1f), transform.position.y, transform.position.z);
        }

        /*WaveBuilder newBuilder = Instantiate(builder, transform.position, Quaternion.identity);
        newBuilder.transform.SetParent(stageMover.transform);
        //newBuilder.SetSpeed(stageSpeed);
        newBuilder.CreateTile(0.9f);*/
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;


        Player.clip = AudioManager.GetClip();
        Player.Play();
        AudioVisualizerManager.audioSource = Player;
        SpawnTile(transform.position);

        while (!Player.isPlaying)
        {
            yield return null;
            /*if (Player.time >= Player.clip.length && !SongFinished)
            {
                SongFinished = true;
                StartCoroutine(StageComplete());
                break;
            }
            */
        }


        stageCompletion = StartCoroutine(StageComplete());
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound();
        
    }

    public void Fail()
    {
        if(stageCompletion != null)
        {
            StopCoroutine(stageCompletion);
            stageCompletion = null;
            stageCompleted = true;
        }
    }

    IEnumerator StageComplete()
    {
        yield return new WaitForSeconds(Player.clip.length + 5);
        if (stageCompletion != null)
        {
            stageCompletion = null;
            stageCompleted = true;
            WindowManager.Instance.CreateCompleteWindow();
        }
    }

    private void SpawnTile(Vector3 position)
    {
        WaveBuilder newBuilder = Instantiate(builder, position, Quaternion.identity);
        float rate = Mathf.PerlinNoise((Mathf.RoundToInt(Player.time) * proceduralScale) + proceduralOffset, proceduralOffset);
        newBuilder.CreateTile(rate * scale);
        newBuilder.GetComponent<SpriteRenderer>().color = Color.white * rate;
        Debug.Log(rate);

        newBuilder.transform.SetParent(stageMover.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<WaveBuilder>() != null)
        {
            if (stageCompleted != true)
            {
                SpawnTile(collision.transform.position + Vector3.right * transform.localScale.x);
            }
            /*
            WaveBuilder newBuilder = Instantiate(builder, , Quaternion.identity);
            float rate = Mathf.PerlinNoise((Mathf.RoundToInt(Player.time) * proceduralScale) + ((AudioHolder.instance.GetSongName() % 500) * proceduralScale), 3.0f);
            newBuilder.CreateTile(rate * scale);
            newBuilder.GetComponent<SpriteRenderer>().color = Color.white * rate;
            Debug.Log(rate);

            newBuilder.transform.SetParent(stageMover.transform);
            */
        }
    }
}
    
