using EverGet;
using NUnit.Framework;

namespace EverGetTest
{
    public class ProgramTest
    {
        private string mDevToken;

        [SetUp]
        public void SetUp()
        {
            mDevToken = Properties.Settings.Default.DevToken;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestMain_Normal_0001()
        {
            // 準備

            // テスト実行
            Program.Main(new string[]{ "-h"});

            // 検証
            // とりあえず疎通確認が取れればOKとする
            Assert.True(true);
        }

        [Test]
        public void TestMain_Normal_0101()
        {
            // 準備

            // テスト実行
            Program.Main(new string[]{
                "--token", mDevToken,
                "EvernoteAPIからの検索テスト用ノート（添付なし）" });

            // 検証
            // とりあえず疎通確認が取れればOKとする
            Assert.True(true);
        }

        [Test]
        public void TestMain_Normal_0102()
        {
            // 準備

            // テスト実行
            Program.Main(new string[]{
                "--token", mDevToken,
                "EvernoteAPIからの検索テスト用ノート（画像あり）" });

            // 検証
            // とりあえず疎通確認が取れればOKとする
            Assert.True(true);
        }

        [Test]
        public void TestMain_Normal_0103()
        {
            // 準備

            // テスト実行
            // ノートタイトル「Test note from EDAMTest.cs」
            Program.Main(new string[]{
                "--token", mDevToken,
                "--guid", "8b788d3f-1f98-4672-832f-f291c0a5a1c0" });

            // 検証
            // とりあえず疎通確認が取れればOKとする
            Assert.True(true);
        }

        [Test]
        public void TestMain_Normal_0201()
        {
            // 準備

            // テスト実行
            Program.Main(new string[]{
                "-o", "output",
                "--token", mDevToken,
                "EvernoteAPIからの検索テスト用ノート（添付なし）" });

            // 検証
            // とりあえず疎通確認が取れればOKとする
            Assert.True(true);
        }
    }
}
