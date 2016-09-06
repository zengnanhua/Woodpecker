/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 17:58:24

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingQueue.Implements
{
    internal class FileQueue:IQueue
    {

        private static string _folder = Environment.CurrentDirectory
            + "\\" + (null ?? "FileQueue")
            + "\\";


        public void Push(byte[] obj)
        {
            string fileName = Path.Combine(_folder, string.Format("{0:yyyyMMddHHmmssffff}.{1}", DateTime.Now, "log"));
            FileStream fs = null;
            if (!System.IO.Directory.Exists(_folder))
            {
                System.IO.Directory.CreateDirectory(_folder);
            }
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                fs.Write(obj, 0, obj.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                fs.Close();
                fs.Dispose();
                fs = null;
            }
        }

        public byte[] Pop()
        {
            if (this.Count > 0)
            {
                var nameArr = System.IO.Directory.GetFiles(_folder);
                if (nameArr != null && nameArr.Length > 0)
                {
                    string name = nameArr.OrderByDescending(i => i).First();
                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        byte[] pReadByte = new byte[0];
                        BinaryReader r = new BinaryReader(fs);
                        r.BaseStream.Seek(0, SeekOrigin.Begin);
                        pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                        return pReadByte;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                        //清除这个被pop出来的文件
                        File.Delete(name);
                    }
                }
            }
            return null;
        }

        public int Count
        {
            get
            {
                if (System.IO.Directory.Exists(_folder))
                {
                    return System.IO.Directory.GetFiles(_folder).Length;
                }
                return 0;
            }
        }
    }
}
