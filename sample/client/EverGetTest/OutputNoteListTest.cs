using EverGet;
using Evernote.EDAM.Type;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace EverGetTest
{
    [TestFixture]
    public class OutputNoteListTest
    {
        private OutputNoteList mTarget;

        [SetUp]
        public void SetUp()
        {
            mTarget = new OutputNoteList();
        }

        [Test]
        public void TestSaveNote_Normal_0001()
        {
            // 準備
            Note note = new Note();
            note.Content = "content";
            string fileName = "TestSaveNote_Normal_0001.txt";

            // テスト実行
            mTarget.SaveNote(note, fileName);

            // 検証
            Assert.True(File.Exists(fileName));
        }

        [Test]
        public void TestSaveNote_Normal_0002()
        {
            // 準備
            Resource resource = new Resource();
            resource.Data = new Data();
            resource.Data.Body = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            Note note = new Note();
            note.Content = "content";
            note.Resources = new List<Resource>();
            note.Resources.Add(resource);
            string fileName = "TestSaveNote_Normal_0002.txt";

            // テスト実行
            mTarget.SaveNote(note, fileName);

            // 検証
            Assert.True(File.Exists(fileName));
        }
    }
}
