using System;

namespace EverGet
{
    /// <summary>
    /// 実行時引数を保持するクラス
    /// </summary>
    public class Arguments
    {
        private bool mHelpFlag = false;
        public bool HelpFlag { get { return mHelpFlag; } }

        private string mDevToken;
        public string DevToken { get { return mDevToken; } }

        private Condition mCondition = new Condition();
        public Condition Condition { get { return mCondition; } }

        private int mMaxCount = 10;
        public int MaxCount { get { return mMaxCount; } }

        private string mOutputBaseName;
        public string OutputBaseName { get { return mOutputBaseName; } }

        public void PrintUsage()
        {
            Console.WriteLine("Usage: EverGet [オプション] --token <トークン> (<検索構文>|--guid <GUID>)");
            Console.WriteLine(" -h");
            Console.WriteLine("    ヘルプ出力する。");
            Console.WriteLine(" -o <出力ファイルベース名>");
            Console.WriteLine("    出力ファイルの基本となる名前。デフォルトではノートタイトル。");
            Console.WriteLine(" --max <最大件数>");
            Console.WriteLine("    検索ノートの最大件数。デフォルトでは10件。");
            Console.WriteLine(" --token <トークン>");
            Console.WriteLine("    次のページで取得するディベロッパートークン。");
            Console.WriteLine("    https://www.evernote.com/api/DeveloperToken.action");
            Console.WriteLine(" <検索構文>");
            Console.WriteLine("    Evernoteからノートを検索する条件。詳細は次のURL参照。");
            Console.WriteLine("    http://dev.evernote.com/intl/jp/doc/articles/search_grammar.php");
            Console.WriteLine(" --guid <GUID>");
            Console.WriteLine("    Evernoteから検索するノートのGUID。");
        }

        public void Parse(string[] args)
        {
            try
            {
                ParseMain(args);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("引数の解析に失敗しました。", ex);
            }
        }

        private void ParseMain(string[] args)
        {
            int count = 0;

            while (true)
            {
                if (!HasArg(args, count))
                {
                    break;
                }

                string arg = GetString(args, count++);
                switch (arg)
                {
                    case "-h":
                        mHelpFlag = true;
                        break;
                    case "-o":
                        mOutputBaseName = GetString(args, count++);
                        break;
                    case "--max":
                        mMaxCount = GetNumber(args, count++);
                        break;
                    case "--guid":
                        mCondition.Guid = GetString(args, count++);
                        break;
                    case "--token":
                        mDevToken = GetString(args, count++);
                        break;
                    default:
                        mCondition.Word = arg;
                        break;
                }
            }

            if (!mHelpFlag)
            {
                if (String.IsNullOrEmpty(mDevToken))
                {
                    throw new ApplicationException("引数が不足しています。");
                }
                if (mCondition.Kind == Condition.ConditionKind.NONE)
                {
                    throw new ApplicationException("引数が不足しています。");
                }
            }
        }

        private bool HasArg(string[] args, int count)
        {
            return count < args.Length;
        }

        private int GetNumber(string[] args, int count)
        {
            string arg = GetString(args, count);
            return Int16.Parse(arg);
        }

        private string GetString(string[] args, int count)
        {
            return args[count];
        }

    }
}
