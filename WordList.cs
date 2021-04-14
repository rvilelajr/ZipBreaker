using System.Collections.Generic;
using System.IO;

namespace ZipBreaker
{
    public class WordList
    {
        private TextReader fileReader;
        public WordList(string file)
        {
            this.fileReader = new StreamReader(@file);
        }

        public IEnumerable<string> NextWord()
        {
            using (this.fileReader)
            {
                string currentLine;
                while ((currentLine = this.fileReader.ReadLine()) != null)
                {
                    yield return currentLine;
                }
            }
        }
    }
}