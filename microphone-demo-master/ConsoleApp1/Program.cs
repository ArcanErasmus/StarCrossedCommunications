using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            float[] testLogs = new float[] {1f, -20f, 50f, 71f, 85f, 100f, 150f, 200f, 229f, 300f };
            for (int i = 0; i < testLogs.Length; i++)
            {
                MatchNote(testLogs[i]);
            }
        }

        static string MatchNote(float frequency)
        {
            const float dontForgetTheLogNumber = 1.04f;
            string[] notes = new string[] { "C", "D", "E", "F", "G", "A", "B" };

            List<float> logs = new List<float>(new float[] { 71.24374f, 74.18611f, 77.13511f, 78.61377f, 81.55579f, 84.50099f, 87.44839f, 88.91675f, 91.86607f, 94.80811f, 96.28093f, 99.22879f, 102.174f, 105.1214f, 106.5937f, 109.5391f, 112.4842f, 113.9569f, 116.9018f, 119.847f, 122.7923f, 124.2647f, 127.2103f, 130.1557f, 131.6284f, 134.5748f, 137.52f, 140.4653f, 141.9387f, 144.8833f, 147.8294f, 149.3021f, 152.2478f, 155.193f, 158.1383f, 159.6112f, 162.5568f, 165.5021f, 166.9751f, 169.9205f, 172.866f, 175.8116f, 177.2842f, 180.2298f, 183.1753f, 184.648f, 187.5935f, 190.539f, 193.4845f, 194.9572f, 197.9028f, 200.8483f, 202.3211f, 205.2665f, 208.212f, 211.1575f, 212.6303f, 215.5757f, 218.5213f, 219.994f, 222.9395f, 225.885f, 228.8305f });


            int closest = logs.BinarySearch(frequency);
            Console.WriteLine("I: " + frequency);
            Console.Write("C: " + closest);
            string closestNote = "X"; // X == fail

            if (frequency > 70f && frequency < 230f)
            {
                if (closest > -1) // rare, exact note match
                {
                    closestNote = notes[closest];
                }
                else // have match to next note
                {
                    closest = ~closest;
                    Console.Write(", " + closest);
                    // we have next closest note, test whether closer to that or to previous note
                    if (closest == 0)
                    {
                        closestNote = notes[0];
                    }
                    else if (closest == 63)
                    {
                        closestNote = notes[6];
                    }
                    else
                    {
                        float range = logs[closest] - logs[closest - 1];
                        if (range / (logs[closest] - frequency) > 0.5f)
                        {
                            closestNote = notes[closest % 7];
                        }
                        else
                        {
                            closestNote = notes[closest - 1 % 7];
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("O: " + closestNote);

            return closestNote;
        }
    }
}
