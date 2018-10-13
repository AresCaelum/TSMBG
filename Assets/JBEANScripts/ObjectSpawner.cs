using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public AudioSource Player;
    public WaveBuilder builder;
    public float delay = 3.0f;
    public float stageSpeed = 4.0f;
    public float scale = 100.0f;

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

    ////////////////////////////
    // Use this for initialization
    IEnumerator Start()
    {
        if (builder != null)
        {
            transform.localScale = new Vector3(builder.transform.localScale.x - 0.2f, builder.transform.localScale.y, 1f);
            transform.position = new Vector3(transform.position.x + (transform.localScale.x - 1f), transform.position.y, transform.position.z);
        }
        WaveBuilder newBuilder = Instantiate(builder, transform.position, Quaternion.identity);
        newBuilder.SetSpeed(stageSpeed);
        newBuilder.CreateTile(0.9f);
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;

        yield return new WaitForSeconds(1);

        Player.clip = AudioHolder.instance.GetAudioClip();
        Player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WaveBuilder>() != null)
        {
            WaveBuilder newBuilder = Instantiate(builder, transform.position, Quaternion.identity);
            newBuilder.CreateTile(spectrum * scale);
            newBuilder.SetSpeed(stageSpeed);
        }
    }
}
