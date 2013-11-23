using Evernote.EDAM.Error;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using Evernote.EDAM.UserStore;
using System;
using System.Collections.Generic;
using Thrift.Protocol;
using Thrift.Transport;

namespace EverGet
{
    /// <summary>
    /// Evernoteにアクセスするためのクラス
    /// </summary>
    public class EvernoteHelper
    {
        private String mAuthToken;
        private String mEvernoteHost;
        private NoteStore.Client mNoteStore;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="devToken">ディベロッパートークン</param>
        public EvernoteHelper(string evernoteHost, string devToken)
        {
            try
            {
                Init(evernoteHost, devToken);
            }
            catch (TTransportException ex)
            {
                throw new ApplicationException("Evernoteへの接続に失敗しました。"
                    + "Message=" + ex.Message
                    + ", EvernoteHost=" + evernoteHost,
                    ex);
            }
            catch (EDAMUserException ex)
            {
                throw new ApplicationException("Evernoteの認証に失敗しました。"
                    + "ErrorCode=" + ex.ErrorCode
                    + ", Parameter=" + ex.Parameter
                    + ", EvernoteHost=" + evernoteHost,
                    ex);
            }
            catch (EDAMSystemException ex)
            {
                throw new ApplicationException("Evernoteの認証に失敗しました。"
                    + "ErrorCode=" + ex.ErrorCode
                    + ", Message=" + ex.Message
                    + ", EvernoteHost=" + evernoteHost,
                    ex);
            }
        }

        private void Init(string evernoteHost, string devToken)
        {
            // Real applications authenticate with Evernote using OAuth, but for the
            // purpose of exploring the API, you can get a developer token that allows
            // you to access your own Evernote account. To get a developer token, visit 
            // https://sandbox.evernote.com/api/DeveloperToken.action
            //String authToken = "your developer token";
            String authToken = devToken;

            if (authToken == "your developer token")
            {
                Console.WriteLine("Please fill in your developer token");
                Console.WriteLine("To get a developer token, visit https://sandbox.evernote.com/api/DeveloperToken.action");
                throw new ApplicationException("ディベロッパートークンが指定されていません。");
            }

            // Initial development is performed on our sandbox server. To use the production 
            // service, change "sandbox.evernote.com" to "www.evernote.com" and replace your
            // developer token above with a token from 
            // https://www.evernote.com/api/DeveloperToken.action
            //String evernoteHost = "sandbox.evernote.com";

            Uri userStoreUrl = new Uri("https://" + evernoteHost + "/edam/user");
            TTransport userStoreTransport = new THttpClient(userStoreUrl);
            TProtocol userStoreProtocol = new TBinaryProtocol(userStoreTransport);
            UserStore.Client userStore = new UserStore.Client(userStoreProtocol);

            bool versionOK =
                userStore.checkVersion("Evernote EDAMTest (C#)",
                   Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MAJOR,
                   Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MINOR);
            Console.WriteLine("Is my Evernote API version up to date? " + versionOK);
            if (!versionOK)
            {
                throw new ApplicationException("バージョンチェックに失敗しました。"
                    + "MAJOR=" + Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MAJOR
                    + ",MINOR=" + Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MINOR);
            }

            // Get the URL used to interact with the contents of the user's account
            // When your application authenticates using OAuth, the NoteStore URL will
            // be returned along with the auth token in the final OAuth request.
            // In that case, you don't need to make this call.
            String noteStoreUrl = userStore.getNoteStoreUrl(authToken);

            TTransport noteStoreTransport = new THttpClient(new Uri(noteStoreUrl));
            TProtocol noteStoreProtocol = new TBinaryProtocol(noteStoreTransport);
            NoteStore.Client noteStore = new NoteStore.Client(noteStoreProtocol);

            mAuthToken = authToken;
            mEvernoteHost = evernoteHost;
            mNoteStore = noteStore;
        }

        public List<Note> FindNotes(string word, int pageSize)
        {
            try {
                NoteFilter filter = new NoteFilter();
                filter.Words = word;
                NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
                spec.IncludeTitle = true;

                NotesMetadataList notes = mNoteStore.findNotesMetadata(mAuthToken, filter, 0, pageSize, spec);

                List<Note> list = new List<Note>();
                foreach (NoteMetadata note in notes.Notes)
                {
                    // findNotesMetadataで取得するノートには、ノートの本文やリソースが含まれていないので個別に取得しなおす。
                    // 第4引数がtrueだとリソースを含んだノートが取得できる
                    // 第6引数はResourceAlternateData (位置情報など)の有無
                    Note fullNote = mNoteStore.getNote(mAuthToken, note.Guid,
                        true, true, false, false);
                    list.Add(fullNote);
                }

                return list;
            }
            catch (EDAMUserException ex)
            {
                throw new ApplicationException("Evernoteアクセスに失敗しました。"
                    + "ErrorCode=" + ex.ErrorCode
                    + ", Parameter=" + ex.Parameter
                    + ", EvernoteHost=" + mEvernoteHost,
                    ex);
            }
        }

        public List<Note> GetNoteAsList(string guid)
        {
            List<Note> list = new List<Note>();
            Note note = GetNote(guid);
            if (note != null)
            {
                list.Add(note);
            }
            return list;
        }

        public Note GetNote(string guid)
        {
            try
            {
                Note fullNote = mNoteStore.getNote(mAuthToken, guid,
                    true, true, false, false);
                return fullNote;
            }
            catch (EDAMUserException ex)
            {
                throw new ApplicationException("Evernoteアクセスに失敗しました。"
                    + "GUIDを確認ください。"
                    + "GUID=" + guid
                    + ", ErrorCode=" + ex.ErrorCode
                    + ", Parameter=" + ex.Parameter
                    + ", EvernoteHost=" + mEvernoteHost,
                    ex);
            }
        }
    }
}
