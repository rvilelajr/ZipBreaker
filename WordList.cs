using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZipBreaker
{
    public class WordList
    {
        private TextReader fileReader;
        private TextWriter checkedWordsFile;
        public WordList(string file)
        {
            try
            {
                this.fileReader = new StreamReader(file);
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
            string f = $"checked-{Path.GetFileNameWithoutExtension(file)}.tmp";

            try
            {
                lastWord = File.ReadLines(f).Last();
                foreach (string w in NextWord())
                {
                    if (lastWord == w)
                    {
                        break;
                    }
                }
                this.checkedWordsFile = new StreamWriter(f, true);
            }
            catch (System.Exception)
            {
                this.checkedWordsFile = new StreamWriter(f, true);
            }
        }


        public IEnumerable<string> NextWord()
        {
            string currentLine;
            while ((currentLine = this.fileReader.ReadLine()) != null)
            {
                yield return currentLine;
            }
            this.checkedWordsFile.Close();
        }

        public void addCheckedWord(string word)
        {
            try
            {
                this.checkedWordsFile.WriteLine(word);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}