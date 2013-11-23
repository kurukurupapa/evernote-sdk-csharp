using Evernote.EDAM.Type;
using System;
using System.IO;

namespace EverGet
{
    public class OutputNoteList
    {
        public void SaveNotes(InputNoteList inputNoteList, string baseName)
        {
            int count = 1;
            foreach (Note note in inputNoteList.Notes)
            {
                string path = GetContentFileName(baseName, note, count);
                SaveNote(note, path);
            }
        }

        public void SaveNote(Note note, string path)
        {
            // 本文を保存する
            OutputNoteContent outputNoteContent = new OutputNoteContent(note, path);
            outputNoteContent.Save();

            // 添付なしの場合、Resourcesがnullになる模様。
            if (note.Resources == null)
            {
                return;
            }

            // 添付をファイル保存する
            int count = 1;
            foreach (Resource res in note.Resources)
            {
                string resPath = GetResFileName(path, count);

                OutputNoteResource outputNoteResource = new OutputNoteResource(res, resPath);
                outputNoteResource.Save();

                count++;
            }
        }

        private string GetContentFileName(string baseName, Note note, int noteCount)
        {
            string path;
            if (string.IsNullOrEmpty(baseName))
            {
                path = note.Title + ".txt";
            }
            else
            {
                path = Path.Combine(
                    Path.GetDirectoryName(baseName),
                    Path.GetFileName(baseName) + "_" + String.Format("{0:D5}", noteCount) + ".txt");
            }
            return path;
        }

        private string GetResFileName(string path, int resCount)
        {
            return Path.Combine(
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path) + "_res_" + String.Format("{0:D5}", resCount));
        }
    }
}
