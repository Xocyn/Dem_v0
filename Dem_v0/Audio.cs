//using MathNet.Numerics.IntegralTransforms;
//using NAudio.Wave;
//using System.Collections.Generic;
//using System.Numerics;
//using System.Text;

//class BFSKDemodulator
//{
//    const int sampleRate = 44100;
//    const int bitRate = 100;
//    const double bitxsec = 1d / bitRate;
//    const double samplesPerBit = sampleRate * bitxsec; // (44100/1200)
//    const int fftSize = 1024;

//    const double f0 = 2100.0;
//    const double f1 = 1300.0;

//    public static string DecodeBits(string wavFile)
//    {
//        List<float> samples = new List<float>();

//        // Leer WAV
//        using (var reader = new AudioFileReader(wavFile))
//        {
//            float[] buffer = new float[2048];
//            int read;

//            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
//                for (int i = 0; i < read; i++)
//                    samples.Add(buffer[i]);
//        }

//        int binF0 = (int)Math.Round(f0 * fftSize / sampleRate);
//        int binF1 = (int)Math.Round(f1 * fftSize / sampleRate);

//        List<int> bits = new List<int>();

//        int h = (int)Math.Round(samplesPerBit);
//        // Demodulación
//        for (int i = 0; i + fftSize < samples.Count; i += h)
//        {
//            Complex[] fftBuffer = new Complex[fftSize];

//            for (int n = 0; n < fftSize; n++)
//            {
//                double window = 0.5 * (1 - Math.Cos(2 * Math.PI * n / (fftSize - 1)));
//                fftBuffer[n] = new Complex(samples[i + n] * window, 0);
//            }

//            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

//            double magF0 = fftBuffer[binF0].Magnitude;
//            double magF1 = fftBuffer[binF1].Magnitude;

//            int bit = (magF1 > magF0) ? 1 : 0;
//            bits.Add(bit);
//        }

//        string bitstream = string.Join("", bits);

//        return bitstream;
//    }
//}

using System;
using System.IO;
using System.Text;

public class BFSKDemodulator
{
    const int SampleRate = 44100;
    const int BaudRate = 1200;

    // NUEVA ASIGNACION
    const double FreqBit1 = 1300.0; // '1'
    const double FreqBit0 = 2100.0; // '0'

    public static string DemodulateToString(string wavPath)
    {
        short[] samples = ReadWav16BitMono(wavPath);

        int samplesPerSymbol = (int)Math.Round((double)SampleRate / BaudRate);

        StringBuilder bits = new StringBuilder();

        for (int i = 0; i + samplesPerSymbol < samples.Length; i += samplesPerSymbol)
        {
            double e0 = EnergyIQ(samples, i, samplesPerSymbol, FreqBit0);
            double e1 = EnergyIQ(samples, i, samplesPerSymbol, FreqBit1);

            // decision
            bits.Append(e1 > e0 ? '1' : '0');
        }

        return bits.ToString();
    }

    private static double EnergyIQ(short[] samples, int start, int length, double freq)
    {
        double I = 0;
        double Q = 0;

        for (int n = 0; n < length; n++)
        {
            double t = (double)n / SampleRate;
            double sample = samples[start + n];

            double cos = Math.Cos(2 * Math.PI * freq * t);
            double sin = Math.Sin(2 * Math.PI * freq * t);

            I += sample * cos;
            Q += sample * sin;
        }

        return I * I + Q * Q;
    }

    private static short[] ReadWav16BitMono(string path)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            reader.ReadBytes(44); // header WAV estándar

            int sampleCount = (int)((reader.BaseStream.Length - 44) / 2);
            short[] data = new short[sampleCount];

            for (int i = 0; i < sampleCount; i++)
                data[i] = reader.ReadInt16();

            return data;
        }
    }
}
