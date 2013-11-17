using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System;
using System.Collections.Generic;
using System.IO;

namespace SampleApp002
{
    //
    // 特定ノートの内容を表示する
    //
    class Note001Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public Note001Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;
        }

        public void Run()
        {
            Console.WriteLine("特定ノートの内容を表示する");

            // ノート取得
            Note note = GetNote();

            // ノート本文出力
            WriteNote(note);

            // ノート・リソース出力
            List<Resource> resources = note.Resources;
            Console.WriteLine("resources.Count: " + resources.Count);
            foreach (Resource res in resources)
            {
                WriteResource(res);
            }
        }

        private Note GetNote()
        {
            int pageSize = 1;

            NoteFilter filter = new NoteFilter();
            filter.Words = "intitle:\"EvernoteAPIからの検索テスト用ノート（画像あり）\"";

            NoteList notes = mNoteStore.findNotes(mAuthToken, filter, 0, pageSize);

            if (notes.TotalNotes < 1)
            {
                Console.WriteLine("エラー");
                Console.WriteLine("該当件数：" + notes.TotalNotes);
                throw new ApplicationException("ノートが取得できませんでした。");
            }

            Note note = notes.Notes[0];

            // findNotesで取得するノートには、ノートの本文やリソースが含まれていないので個別に取得しなおす。
            // 第4引数がtrueだとリソースを含んだノートが取得できる
            // 第6引数はResourceAlternateData (位置情報など)の有無
            Note fullNote = mNoteStore.getNote(mAuthToken, note.Guid,
                true, true, false, false);

            return fullNote;
        }

        /// <summary>
        /// ノート本文の内容を出力する。
        /// </summary>
        /// <param name="note"></param>
        private void WriteNote(Note note)
        {
            Console.WriteLine("Title: " + note.Title);
            Console.WriteLine("GUID: " + note.Guid);
            Console.WriteLine("Content: " + note.Content);
            Console.WriteLine("ToString: " + note.ToString());
        }

        /// <summary>
        /// リソースを出力する。
        /// </summary>
        /// <param name="res"></param>
        private void WriteResource(Resource res)
        {
            Console.WriteLine(" * Mime: " + res.Mime);
            Console.WriteLine(" * ToString: " + res.ToString());

            Data data = res.Data;
            byte[] body = data.Body;
            Console.WriteLine(" * Data.Size: " + data.Size);
            if (body == null)
            {
                Console.WriteLine(" * Data.Body: null");
            }
            else
            {
                String fname = res.NoteGuid + "_" + res.Guid;
                if (res.Mime.Equals("image/png"))
                {
                    fname += ".png";
                }
                else
                {
                    fname += ".dat";
                }
                FileStream fs = new FileStream(fname, FileMode.Create);
                fs.Write(body, 0, body.Length);
                fs.Close();
                Console.WriteLine(" * Data.Body: " + fname + "へ出力しました。");
            }
            Console.WriteLine(" * Data.ToString: " + data.ToString());
        }
    }
}
