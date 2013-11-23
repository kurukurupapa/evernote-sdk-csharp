using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System;

namespace SampleApp002
{
    /// <summary>
    /// 色々な検索方法を試してみる。
    /// </summary>
    class Find003Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public Find003Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;
        }

        public void Run()
        {
            Console.WriteLine("ノートのタイトルで検索");
            FindTitle();

            Console.WriteLine("ノートのGUIDで検索（「検索」というかノートの取得）");
            FindGuid();
        }

        private void FindTitle()
        {
            NoteFilter filter = new NoteFilter();
            filter.Words = "intitle:\"EvernoteAPIからの検索テスト用ノート（画像あり）\"";

            int pageSize = 10;

            NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
            spec.IncludeTitle = true;

            NotesMetadataList notes = mNoteStore.findNotesMetadata(mAuthToken, filter, 0, pageSize, spec);

            WriteNoteList(notes);
        }

        private void FindGuid()
        {
            String guid = "db173d8f-524f-4545-8ed9-1f59b0750343";

            Note note = mNoteStore.getNote(mAuthToken, guid, false, false, false, false);

            WriteNote(note);
        }

        private void WriteNoteList(NotesMetadataList notes)
        {
            Console.WriteLine("該当件数：" + notes.TotalNotes);
            foreach (NoteMetadata note in notes.Notes)
            {
                Console.WriteLine(" * " + note.Title + ", " + note.Guid);
            }
        }

        private void WriteNote(Note note)
        {
            Console.WriteLine(" * " + note.Title + ", " + note.Guid);
        }
    }
}
