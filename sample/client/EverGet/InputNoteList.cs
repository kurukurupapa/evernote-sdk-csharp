using Evernote.EDAM.Type;
using System;
using System.Collections.Generic;

namespace EverGet
{
    /// <summary>
    /// EvernoteAPI経由でEvernoteにアクセスし、ノートデータを取得するクラス。
    /// </summary>
    public class InputNoteList
    {
        private List<Note> mNotes = null;
        public List<Note> Notes
        {
            get
            {
                return mNotes;
            }
        }

        /// <summary>
        /// ノートを検索する。
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="maxCount"></param>
        public void Find(string evernoteHost, string devToken, Condition condition, int maxCount)
        {
            EvernoteHelper helper = new EvernoteHelper(evernoteHost, devToken);

            switch (condition.Kind) {
                case Condition.ConditionKind.WORD:
                    mNotes = helper.FindNotes(condition.Word, maxCount);
                    break;
                case Condition.ConditionKind.GUID:
                    mNotes = helper.GetNoteAsList(condition.Guid);
                    break;
                default:
                    throw new ApplicationException("検索条件が不明です。");
            }
        }
    }
}
