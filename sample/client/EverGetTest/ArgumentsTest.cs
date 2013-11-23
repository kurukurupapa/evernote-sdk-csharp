using EverGet;
using NUnit.Framework;
using System;

namespace EverGetTest
{
    [TestFixture]
    public class ArgumentsTest
    {
        private Arguments mTarget = null;

        [SetUp]
        public void SetUp()
        {
            mTarget = new Arguments();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestPrintUsage()
        {
            // 準備

            // テスト実行
            Exception result = null;
            try
            {
                mTarget.PrintUsage();
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // 検証
            // とりあえず例外が発生しなければOK
            Assert.IsNull(result);
        }

        [Test]
        public void TestParse_HelpFlag_Default()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "--token", "developer_token", "abc" };
            mTarget.Parse(args);

            // 検証
            Assert.AreEqual(false, mTarget.HelpFlag);
        }

        [Test]
        public void TestParse_HelpFlag_True()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "-h" };
            mTarget.Parse(args);

            // 検証
            Assert.AreEqual(true, mTarget.HelpFlag);
        }

        [Test]
        public void TestParse_DevToken_Normal()
        {
            // 準備

            // テスト実行
            mTarget.Parse(new string[] { "--token", "developer_token", "abc" });

            // 検証
            Assert.AreEqual("developer_token", mTarget.DevToken);
        }

        [Test]
        public void TestParse_DevToken_Err()
        {
            // 準備

            // テスト実行
            // --tokenの引数不足
            Exception result = null;
            try
            {
                mTarget.Parse(new string[] { "--token" });
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);
        }

        [Test]
        public void TestParse_OutputBaseName_Default()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "--token", "developer_token", "abc" };
            mTarget.Parse(args);

            // 検証
            Assert.IsNull(mTarget.OutputBaseName);
        }

        [Test]
        public void TestParse_OutputBaseName_Normal()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "-o", "output_base_name", "--token", "developer_token", "abc" };
            mTarget.Parse(args);

            // 検証
            Assert.AreEqual("output_base_name", mTarget.OutputBaseName);
        }

        [Test]
        public void TestParse_OutputBaseName_Err()
        {
            // 準備

            // テスト実行
            Exception result = null;
            try
            {
                string[] args = new string[] { "-o" };
                mTarget.Parse(args);
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);
        }

        [Test]
        public void TestParse_MaxCount_Default()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "--token", "developer_token", "abc" };
            mTarget.Parse(args);

            // 検証
            Assert.AreEqual(10, mTarget.MaxCount);
        }

        [Test]
        public void TestParse_MaxCount_Normal()
        {
            // 準備

            // テスト実行
            string[] args = new string[] { "--max", "123", "--token", "developer_token", "abc" };
            mTarget.Parse(args);

            // 検証
            Assert.AreEqual(123, mTarget.MaxCount);
        }

        [Test]
        public void TestParse_MaxCount_Err()
        {
            // 準備

            // テスト実行
            Exception result = null;
            try
            {
                string[] args = new string[] { "--max" };
                mTarget.Parse(args);
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);

            // テスト実行
            result = null;
            try
            {
                string[] args = new string[] { "--max", "abc" };
                mTarget.Parse(args);
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);
        }

        [Test]
        public void TestParse_Condition_Normal()
        {
            // 準備

            // テスト実行
            mTarget.Parse(new string[] { "--token", "developer_token", "elephant" });

            // 検証
            Assert.AreEqual(Condition.ConditionKind.WORD, mTarget.Condition.Kind);
            Assert.AreEqual("elephant", mTarget.Condition.Word);

            // テスト実行
            mTarget.Parse(new string[] { "--token", "developer_token", "--guid", "1234567890" });

            // 検証
            Assert.AreEqual(Condition.ConditionKind.GUID, mTarget.Condition.Kind);
            Assert.AreEqual("1234567890", mTarget.Condition.Guid);
        }

        [Test]
        public void TestParse_Condition_Err()
        {
            // 準備

            // テスト実行
            // 引数なし
            Exception result = null;
            try
            {
                mTarget.Parse(new string[] { });
            }
            catch (ApplicationException ex)
            {
                result = ex;
            }

            // 検証
            Assert.IsInstanceOf<ApplicationException>(result);

            // テスト実行
            // --guidの引数不足
            result = null;
            try
            {
                mTarget.Parse(new string[] { "--guid" });
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
