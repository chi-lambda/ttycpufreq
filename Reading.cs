using System;
using System.IO;
using System.Linq;

namespace ttycpufreq
{
    public class Reading
    {
        public int MaxFreq { get; }
        public int MaxHeight { get; private set;}
        public int AvgFreq { get; }
        public int AvgHeight { get; private set; }
        public int MinFreq { get; }
        public int MinHeight { get; private set; }

        public int[] Freqs { get; }
        public int[] Heights { get; private set; }
        public DateTime Time { get; set; }

        private const string cpuinfo = "/proc/cpuinfo";
        public Reading()
        {
            Freqs = File.ReadAllLines(cpuinfo)
                .Where(line=>line.StartsWith("cpu MHz"))
                .Select(line => (int)decimal.Parse(line.Split(':')[1]))
                .ToArray();
            MaxFreq = Freqs.Max();
            AvgFreq = (int)Freqs.Average();
            MinFreq = Freqs.Min();
            Time = DateTime.Now;
        }

        public void ComputeHeights(int maxFreq, int height)
        {
            MaxHeight = height * (maxFreq - MaxFreq) / maxFreq;
            AvgHeight = height * (maxFreq - AvgFreq) / maxFreq;
            MinHeight = height * (maxFreq - MinFreq) / maxFreq;
            Heights = Freqs.Select(temp => height * (maxFreq - temp) / maxFreq).ToArray();
        }
    }
}