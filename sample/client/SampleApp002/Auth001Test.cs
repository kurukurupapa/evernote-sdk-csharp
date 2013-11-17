using Evernote.EDAM.Error;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using Evernote.EDAM.UserStore;
using System;
using System.Collections.Generic;
using System.IO;
using Thrift.Protocol;
using Thrift.Transport;

namespace SampleApp002
{
    /// <summary>
    /// OAuth認証をテストしてみるクラス
    /// 
    /// 参考
    /// C#でEverNote APIを触ってみた (EverNote, 1.17, Note, UserStore, NoteStore, Thrift, AuthenticationResult, AuthenticationToken) - いろいろ備忘録日記
    ///   http://d.hatena.ne.jp/gsf_zero1/20101214/p1
    /// moco β版 更新ログ: Evernote API を使ってみる (2) 認証
    ///   http://cocomonrails.blogspot.jp/2010/11/evernote-api-2.html
    /// </summary>
    class Auth001Test
    {
        private String mAuthToken;
        private NoteStore.Client mNoteStore;

        private String mConsumerKey = "consumer key";
        private String mConsumerSecret = "consumer secret";
        private String mEvernoteHost = "sandbox.evernote.com";
        private String mUserName = "user name";
        private String mPassword = "password";  

        public Auth001Test(String authToken, NoteStore.Client noteStore)
        {
            mAuthToken = authToken;
            mNoteStore = noteStore;

            mConsumerKey = Properties.Settings.Default.ConsumerKey;
            mConsumerSecret = Properties.Settings.Default.ConsumerSecret;
            mEvernoteHost = Properties.Settings.Default.EvernoteHost;
            mUserName = Properties.Settings.Default.UserName;
            mPassword = Properties.Settings.Default.Password;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("OAuth認証をする");
                Console.WriteLine("※2013/11/17 認証エラーとなる。Evernote.EDAM.Error.EDAMUserException、INVALID_AUTH、consumerKey。");

                UserStore.Client userStore = GetUserStore();
                CheckVersion(userStore);
                String authToken = GetAuthToken(userStore);

                Console.WriteLine("Authトークン：" + authToken);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// UserStoreの取得
        /// </summary>
        /// <returns></returns>
        private UserStore.Client GetUserStore()
        {
            Uri userStoreUrl = new Uri("https://" + mEvernoteHost + "/edam/user");
            TTransport userStoreTransport = new THttpClient(userStoreUrl);
            TProtocol userStoreProtocol = new TBinaryProtocol(userStoreTransport);
            UserStore.Client userStore = new UserStore.Client(userStoreProtocol);
            return userStore;
        }

        /// <summary>
        /// バージョンチェック
        /// </summary>
        /// <param name="userStore"></param>
        private void CheckVersion(UserStore.Client userStore)
        {
            bool versionOK =
                userStore.checkVersion("Evernote EDAMTest (C#)",
                   Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MAJOR,
                   Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MINOR);
            if (!versionOK)
            {
                throw new ApplicationException("バージョンチェックに失敗しました。");
            }
        }

        /// <summary>
        /// 認証
        /// </summary>
        /// <param name="userStore"></param>
        /// <returns></returns>
        private string GetAuthToken(UserStore.Client userStore)
        {
            try
            {
                AuthenticationResult authResult = null;
                authResult = userStore.authenticate(mUserName, mPassword, mConsumerKey, mConsumerSecret, false);
                //authResult = userStore.authenticate(mUserName, mPassword, mConsumerKey, mConsumerSecret, true);
                return authResult.AuthenticationToken;
            }
            catch (EDAMUserException ex)
            {
                EDAMErrorCode errorCode = ex.ErrorCode;
                String param = ex.Parameter;

                Console.WriteLine("エラーコード：" + errorCode);
                Console.WriteLine("パラメータ：" + param);
                Console.WriteLine("mConsumerKey: " + mConsumerKey);
                Console.WriteLine("mEvernoteHost: " + mEvernoteHost);
                throw new ApplicationException("認証に失敗しました。", ex);
            }
        }
    }
}
