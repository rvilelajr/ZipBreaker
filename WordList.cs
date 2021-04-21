using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZipBreaker
{
    public class WordList
    {
        private StreamReader fileReader;
        private string checkedWordsFile;
        public long fileSize { get; set; }
        public long bytesRead { get; set; }
        public double percentage
        {
            get { return (double)bytesRead / fileSize; }
        }

        public WordList(string file)
        {
            this.bytesRead = 0;
            this.checkedWordsFile = $"checked-{Path.GetFileNameWithoutExtension(file)}.tmp";
            try
            {
                this.fileReader = new StreamReader(file);
                this.fileSize = (new FileInfo(file)).Length;
                this.MoveToLastWord(file);
            }
            catch (FileNotFoundException notFound)
            {
                throw new IOException(notFound.Message);
            }
        }

        private void MoveToLastWord(string file)
        {
            string lastWord = null;

            try
            {
                lastWord = File.ReadLines(this.checkedWordsFile).Last();
                foreach (string w in NextWord())
                {
                    if (lastWord == w)
                    {
                        break;
                    }
                }
            }
            catch (System.Exception)
            {
                TextWriter f = new StreamWriter(this.checkedWordsFile, true);
                f.Close();

            }
        }


        public IEnumerable<string> NextWord()
        {
            string currentLine = null;
            int c;
            while (!this.fileReader.EndOfStream)
            {
                this.bytesRead += 1;
                c = this.fileReader.Read();
                currentLine += (char)c;
                if ((char)c == '\n' || this.fileReader.EndOfStream)
                {
                    yield return currentLine.TrimEnd();
                    currentLine = null;
                }
            }
        }

        public void addCheckedWord(string word)
        {
            try
            {
                using (StreamWriter f = File.AppendText(this.checkedWordsFile))
                {
                    f.WriteLine(word);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}