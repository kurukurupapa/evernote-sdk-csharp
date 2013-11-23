
namespace EverGet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 実行時引数を解析
            Arguments arguments = new Arguments();
            arguments.Parse(args);
            if (arguments.HelpFlag)
            {
                arguments.PrintUsage();
                return;
            }

            // Evernoteから該当ノートを取得
            InputNoteList inputNoteList = new InputNoteList();
            string evernoteHost = Properties.Settings.Default.EvernoteHost;
            inputNoteList.Find(evernoteHost, arguments.DevToken, arguments.Condition, arguments.MaxCount);

            // 該当ノートを保存
            OutputNoteList outputNoteList = new OutputNoteList();
            outputNoteList.SaveNotes(inputNoteList, arguments.OutputBaseName);
        }
    }
}
