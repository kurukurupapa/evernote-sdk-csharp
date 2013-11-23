using EverGet;
using NUnit.Framework;
using System;

namespace EverGetTest
{
    [TestFixture]
    public class InputNoteListTest
    {
        private InputNoteList mTarget;
        private string mEvernoteHost;
        private string mDevToken;

        [SetUp]
        public void SetUp()
        {
            mTarget = new InputNoteList();
            mEvernoteHost = Properties.Settings.Default.EvernoteHost;
            mDevToken = Properties.Settings.Default.DevToken;

            // ユーザーの設定ファイルを作成する。
            Properties.Settings.Default.Dummy = "dummy";
            Properties.Settings.Default.Save();
        }

        [Test]
        public void TestFind_Word_Normal_0001()
        {
            // 準備
            Condition condition = new Condition();
            condition.Word = "__Nothing__";
            int maxCount = 10;

            // テスト実行
            mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);

            // 検証
            Assert.NotNull(mTarget.Notes);
            Assert.AreEqual(0, mTarget.Notes.Count);
        }

        [Test]
        public void TestFind_Word_Normal_0101()
        {
            // 準備
            Condition condition = new Condition();
            condition.Word = "EvernoteAPIからの検索テスト用ノート（添付なし）";
            int maxCount = 10;

            // テスト実行
            mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);

            // 検証
            Assert.AreEqual(1, mTarget.Notes.Count);
            Assert.Less(0, mTarget.Notes[0].Content.Length);
            Assert.IsNull(mTarget.Notes[0].Resources);
        }

        [Test]
        public void TestFind_Word_Normal_0102()
        {
            // 準備
            Condition condition = new Condition();
            condition.Word = "EvernoteAPIからの検索テスト用ノート（画像あり）";
            int maxCount = 10;

            // テスト実行
            mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);

            // 検証
            Assert.AreEqual(1, mTarget.Notes.Count);
            Assert.Less(0, mTarget.Notes[0].Content.Length);
            Assert.NotNull(mTarget.Notes[0].Resources);
            Assert.AreEqual(1, mTarget.Notes[0].Resources.Count);
        }

        [Test]
        public void TestFind_Word_Err()
        {
            // 準備

            // テスト実行

            // 検証
            //Assert.Fail("テストケース未実装");
        }

        [Test]
        public void TestFind_Guid_Normal_0101()
        {
            // 準備
            // 対象ノートのタイトル：EvernoteAPIからの検索テスト用ノート（添付なし）
            Condition condition = new Condition();
            condition.Guid = "5c89bd81-5f69-4a2d-83c6-6257d1a61026";
            int maxCount = 10;

            // テスト実行
            mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);

            // 検証
            Assert.NotNull(mTarget.Notes);
            Assert.AreEqual(1, mTarget.Notes.Count);
            Assert.Less(0, mTarget.Notes[0].Content.Length);
            Assert.Null(mTarget.Notes[0].Resources);
        }

        [Test]
        public void TestFind_Guid_Normal_0102()
        {
            // 準備
            // 対象ノートのタイトル：EvernoteAPIからの検索テスト用ノート（画像あり）
            Condition condition = new Condition();
            condition.Guid = "db173d8f-524f-4545-8ed9-1f59b0750343";
            int maxCount = 10;

            // テスト実行
            mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);

            // 検証
            Assert.AreEqual(1, mTarget.Notes.Count);
            Assert.Less(0, mTarget.Notes[0].Content.Length);
            Assert.NotNull(mTarget.Notes[0].Resources);
            Assert.AreEqual(1, mTarget.Notes[0].Resources.Count);
        }

        [Test]
        public void TestFind_Guid_Err()
        {
            // 準備

            // テスト実行
            // 存在しないGUID
            Exception result = null;
            try
            {
                Condition condition = new Condition();
                condition.Guid = "1234567890";
                int maxCount = 10;
                mTarget.Find(mEvernoteHost, mDevToken, condition, maxCount);
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);
        }
    }
}
