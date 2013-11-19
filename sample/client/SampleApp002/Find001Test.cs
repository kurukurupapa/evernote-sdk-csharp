using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System;

namespace SampleApp002
{
    /// <summary>
    /// 次のページを参考に、「基本的な検索」をしてみる。
    /// http://dev.evernote.com/intl/jp/doc/articles/searching_notes.php
    /// </summary>
    class Find001Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public Find001Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;
        }

        public void Run()
        {
            int pageSize = 10;

            NoteFilter filter = new NoteFilter();
            filter.Order = (int)NoteSortOrder.UPDATED;

            NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
            spec.IncludeTitle = true;

            NotesMetadataList notes = mNoteStore.findNotesMetadata(mAuthToken, filter, 0, pageSize, spec);

            Console.WriteLine("最も直近に変更されたノートの検索");
            Console.WriteLine("該当件数：" + notes.TotalNotes);
            foreach (NoteMetadata note in notes.Notes)
            {
                Console.WriteLine(" * " + note.Title);
            }
        }
    }
}
