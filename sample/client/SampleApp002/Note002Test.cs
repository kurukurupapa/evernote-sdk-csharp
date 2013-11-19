using Evernote.EDAM.Error;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System;
using System.Collections.Generic;
using System.IO;

namespace SampleApp002
{
    /// <summary>
    /// 特定ノートの内容を表示するテスト
    /// </summary>
    class Note002Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public Note002Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("新規ノートを作成する");
                CreateNote();

                Console.WriteLine("ノートを更新する");
                UpdateNote();

                Console.WriteLine("ノートを削除する");
                DeleteNote();
            }
            catch (EDAMUserException ex)
            {
                EDAMErrorCode errorCode = ex.ErrorCode;
                String param = ex.Parameter;

                Console.WriteLine("エラーコード：" + errorCode);
                Console.WriteLine("パラメータ：" + param);
                Console.WriteLine(ex.ToString());
            }
        }

        private void CreateNote()
        {
            Note note = new Note();
            note.Title = "EvernoteAPI 新規ノート作成テスト";
            note.Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\">" +
                "<en-note>サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "</en-note>";

            Note createdNote = mNoteStore.createNote(mAuthToken, note);

            Console.WriteLine("Successfully created new note with GUID: " + createdNote.Guid);
        }

        private void UpdateNote()
        {
            Note note = new Note();
            note.Title = "EvernoteAPI ノート更新テスト";
            note.Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\">" +
                "<en-note>サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "</en-note>";
            Note createdNote = mNoteStore.createNote(mAuthToken, note);
            Console.WriteLine("Successfully created new note with GUID: " + createdNote.Guid);

            createdNote.Title += " 更新済み";
            createdNote.Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\">" +
                "<en-note>サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "更新済み<br/>" +
                "</en-note>";
            Note updatedNote = mNoteStore.updateNote(mAuthToken, createdNote);
            Console.WriteLine("Successfully updated the note with GUID: " + createdNote.Guid);
        }

        private void DeleteNote()
        {
            Note note = new Note();
            note.Title = "EvernoteAPI ノート削除テスト";
            note.Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\">" +
                "<en-note>サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "サンプルテキスト。<br/>" +
                "</en-note>";
            Note createdNote = mNoteStore.createNote(mAuthToken, note);
            Console.WriteLine("Successfully created new note with GUID: " + createdNote.Guid);

            int ret = mNoteStore.deleteNote(mAuthToken, createdNote.Guid);
            Console.WriteLine("Returned value of NoteStore#deleteNote method : " + ret);
        }

    }
}
