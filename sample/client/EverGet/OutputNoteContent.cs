using Evernote.EDAM.Type;
using System.IO;

namespace EverGet
{
    /// <summary>
    /// ノート本文の出力ファイル
    /// </summary>
    public class OutputNoteContent
    {
        private Note mNote;
        private string mPath;

        public OutputNoteContent(Note note, string path)
        {
            this.mNote = note;
            this.mPath = path;
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(mPath))
            {
                sw.Write(mNote.Content);
            }
        }
    }
}
