using EverGet;
using NUnit.Framework;
using System;

namespace EverGetTest
{
    public class EvernoteHelperTest
    {
        private string mEvernoteHost;
        private string mDevToken;

        [SetUp]
        public void SetUp()
        {
            mEvernoteHost = Properties.Settings.Default.EvernoteHost;
            mDevToken = Properties.Settings.Default.DevToken;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestConstructor_Normal_0001()
        {
            // 準備

            // テスト実行
            EvernoteHelper target = new EvernoteHelper(mEvernoteHost, mDevToken);

            // 検証
            // 例外が発生しなければOK
            Assert.True(true);
        }

        /// <summary>
        /// ディベロッパートークン不正のテスト
        /// </summary>
        [Test]
        public void TestConstructor_Err_0101()
        {
            // 準備
            // ディベロッパートークンがデフォルト値のまま
            string devToken = "your developer token";

            // テスト実行
            Exception result = null;
            try
            {
                EvernoteHelper target = new EvernoteHelper(mEvernoteHost, devToken);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result, result.ToString());


            // 準備
            // ディベロッパートークンのフォーマットエラー
            devToken = "1234567890";

            // テスト実行
            result = null;
            try
            {
                EvernoteHelper target = new EvernoteHelper(mEvernoteHost, devToken);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result, result.ToString());


            // 準備
            // ディベロッパートークンの値まちがい
            devToken = "S=s1:U=1234:E=12345678901:C=12345678901:P=123:A=en-devtoken:V=1:H=12345678901234567890123456789012";

            // テスト実行
            result = null;
            try
            {
                EvernoteHelper target = new EvernoteHelper(mEvernoteHost, devToken);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result, result.ToString());
        }

        /// <summary>
        /// EvernoteHost不正のテスト
        /// </summary>
        [Test]
        public void TestConstructor_Err_0201()
        {
            // 準備
            string evernoteHost = "xxxxx.nothing.xxxxx";

            // テスト実行
            Exception result = null;
            try
            {
                EvernoteHelper target = new EvernoteHelper(evernoteHost, mDevToken);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result, result.ToString());
        }
    }
}
