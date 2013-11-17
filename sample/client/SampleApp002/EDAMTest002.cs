/*
  A simple Evernote API demo application that authenticates with the
  Evernote web service, lists all notebooks and notes in the user's account,
  and creates a simple test note in the default notebook.
  
  Before running this sample, you must fill in your Evernote developer token.

  To build (Windows):
    Open and build Evernote.sln

  To build & run (Unix Mono):
    xbuild Evernote.sln
    mono SampleApp/bin/Debug/EDAMTest.exe
*/

using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Evernote.EDAM.Type;
using Evernote.EDAM.UserStore;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Error;
using SampleApp.Properties;
using SampleApp;
using SampleApp002;

public class EDAMTest002 {
    public static void Main(string[] args) {

        // ディベロッパートークンで認証
        DevTokenAuth auth = new DevTokenAuth();
        try
        {
            auth.Run();
        }
        catch(ApplicationException e) {
            Console.WriteLine("認証に失敗したので、処理を終了します。");
            return;
        }

        // 検索
        WriteSeparator();
        new Find001Test(auth.AuthToken, auth.NoteStore).Run();
        WriteSeparator();
        new Find002Test(auth.AuthToken, auth.NoteStore).Run();

        // ノート操作
        WriteSeparator();
        new Note001Test(auth.AuthToken, auth.NoteStore).Run();

        // OAuth認証
        WriteSeparator();
        new Auth001Test(auth.AuthToken, auth.NoteStore).Run();
    }

    public static void WriteSeparator()
    {
        Console.WriteLine(new String('-', 60));
    }
}
