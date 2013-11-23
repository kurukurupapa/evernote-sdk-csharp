using Evernote.EDAM.Type;
using System;
using System.IO;

namespace EverGet
{
    /// <summary>
    /// ノート添付データの出力ファイル
    /// </summary>
    public class OutputNoteResource
    {
        private Resource mRes;
        private string mPath;

        public OutputNoteResource(Resource res, string path)
        {
            this.mRes = res;
            this.mPath = path;
        }

        public void Save()
        {
            string path = mPath + GetExtenstion();

            byte[] data = mRes.Data.Body;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        private string GetExtenstion()
        {
            Console.WriteLine("Mime:" + mRes.Mime);

            string ext;
            switch (mRes.Mime)
            {
                case "image/gif":
                    ext = ".gif";
                    break;
                case "image/jpeg":
                    ext = ".jpg";
                    break;
                case "image/png":
                    ext = ".png";
                    break;
                default:
                    ext = ".dat";
                    break;
            }
            return ext;
        }
    }
}
