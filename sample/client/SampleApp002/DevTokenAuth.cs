using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace SampleApp002
{
    /// <summary>
    /// ディベロッパートークンを用いた認証をするクラス
    /// </summary>
    class DevTokenAuth
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        public String AuthToken
        {
            get { return mAuthToken; }
        }

        public NoteStore.Client NoteStore
        {
            get { return mNoteStore; }
        }

        public DevTokenAuth()
        {
        }

        public void Run()
        {
            // Real applications authenticate with Evernote using OAuth, but for the
            // purpose of exploring the API, you can get a developer token that allows
            // you to access your own Evernote account. To get a developer token, visit 
            // https://sandbox.evernote.com/api/DeveloperToken.action
            //String authToken = "your developer token";
            String authToken = Properties.Settings.Default.AuthToken;

            if (authToken == "your developer token")
            {
                Console.WriteLine("Please fill in your developer token");
                Console.WriteLine("To get a developer token, visit https://sandbox.evernote.com/api/DeveloperToken.action");
                throw new ApplicationException();
            }

            // Initial development is performed on our sandbox server. To use the production 
            // service, change "sandbox.evernote.com" to "www.evernote.com" and replace your
            // developer token above with a token from 
            // https://www.evernote.com/api/DeveloperToken.action
            String evernoteHost = "sandbox.evernote.com";

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
                return;
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
            mNoteStore = noteStore;
        }
    }
}
