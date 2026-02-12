using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using NAudio.Wave;
using System.Collections.Generic;

class BFSKDemodulator
{
    const int sampleRate = 44100;
    const int bitRate = 100;
    const double bitxsec = 1d / bitRate;
    const double samplesPerBit = sampleRate * bitxsec; // (44100/1200)
    const int fftSize = 1024;

    const double f0 = 2100.0;
    const double f1 = 1300.0;

    public static string DecodeBits(string wavFile)
    {
        List<float> samples = new List<float>();

        // Leer WAV
        using (var reader = new AudioFileReader(wavFile))
        {
            float[] buffer = new float[2048];
            int read;

            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                for (int i = 0; i < read; i++)
                    samples.Add(buffer[i]);
        }

        int binF0 = (int)Math.Round(f0 * fftSize / sampleRate);
        int binF1 = (int)Math.Round(f1 * fftSize / sampleRate);

        List<int> bits = new List<int>();

        int h = (int)Math.Round(samplesPerBit);
        // Demodulación
        for (int i = 0; i + fftSize < samples.Count; i += h)
        {
            Complex[] fftBuffer = new Complex[fftSize];

            for (int n = 0; n < fftSize; n++)
            {
                double window = 0.5 * (1 - Math.Cos(2 * Math.PI * n / (fftSize - 1)));
                fftBuffer[n] = new Complex(samples[i + n] * window, 0);
            }

            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            double magF0 = fftBuffer[binF0].Magnitude;
            double magF1 = fftBuffer[binF1].Magnitude;

            int bit = (magF1 > magF0) ? 1 : 0;
            bits.Add(bit);
        }

        string bitstream = string.Join("", bits);

        return bitstream;
    }
}
