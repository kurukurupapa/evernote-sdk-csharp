
namespace EverGet
{
    /// <summary>
    /// 検索条件（検索構文またはGUID）を保持するクラス
    /// </summary>
    public class Condition
    {
        public enum ConditionKind {
            GUID, WORD, NONE
        }

        private ConditionKind mKind = ConditionKind.NONE;
        public ConditionKind Kind
        {
            get
            {
                return mKind;
            }
        }

        private string mGuid;
        public string Guid
        {
            get
            {
                return mGuid;
            }
            set
            {
                mKind = ConditionKind.GUID;
                mGuid = value;
            }
        }

        private string mWord;
        public string Word
        {
            get
            {
                return mWord;
            }
            set
            {
                mKind = ConditionKind.WORD;
                mWord = value;
            }
        }
    }
}
