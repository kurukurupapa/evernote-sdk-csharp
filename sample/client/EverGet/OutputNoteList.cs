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
                SaveNote(note, baseName, count);
            }
        }

        public void SaveNote(Note note, string baseName, int noteCount)
        {
            string noteBaseName = GetNoteBaseName(note, baseName, noteCount);

            // 本文を保存する
            string contentFilePath = GetContentPath(noteBaseName);
            OutputNoteContent outputNoteContent = new OutputNoteContent(note, contentFilePath);
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
                string resPath = GetResPath(res, noteBaseName, count);

                OutputNoteResource outputNoteResource = new OutputNoteResource(res, resPath);
                outputNoteResource.Save();

                count++;
            }
        }

        private string GetNoteBaseName(Note note, string baseName, int noteCount)
        {
            string path;
            if (string.IsNullOrEmpty(baseName))
            {
                path = note.Title;
            }
            else
            {
                path = baseName + "_" + GetSeq(noteCount);
            }
            return path;
        }

        private string GetContentPath(string noteBaseName)
        {
            return noteBaseName + ".txt";
        }

        private string GetResPath(Resource res, string noteBaseName, int resCount)
        {
            return noteBaseName + "_res_" + GetSeq(resCount) + OutputNoteResource.GetExtenstion(res);
        }

        private string GetSeq(int count)
        {
            return String.Format("{0:D5}", count);
        }

    }
}
