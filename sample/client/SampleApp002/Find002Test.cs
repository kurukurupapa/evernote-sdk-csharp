﻿using Evernote.EDAM.NoteStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp002
{
    //
    // 次のページを参考に、「高度な検索」をしてみる。
    // http://dev.evernote.com/intl/jp/doc/articles/searching_notes.php
    //
    class Find002Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public Find002Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;
        }

        public void Run()
        {
            int pageSize = 10;

            NoteFilter filter = new NoteFilter();
            filter.Words = "elephant";

            NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
            spec.IncludeTitle = true;

            NotesMetadataList notes = mNoteStore.findNotesMetadata(mAuthToken, filter, 0, pageSize, spec);

            Console.WriteLine("特定の単語を含むノートの検索");
            Console.WriteLine("該当件数：" + notes.TotalNotes);
            foreach (NoteMetadata note in notes.Notes)
            {
                Console.WriteLine(" * " + note.Title + ", " + note.Guid);
            }
        }

    }
}