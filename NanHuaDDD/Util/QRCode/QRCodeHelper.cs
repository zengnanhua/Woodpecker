using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace Web.Common
{
    public class QRCodeHelper
    {
       public static byte[] GetQRCode(string url)
       {
           QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
           {
               QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
               QRCodeScale = 4,
               QRCodeVersion = 0,
               QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
           };
           // 置为字节编码
           //这里设置规模为：4，版本为：7，其余值读者可以自行试验，这两个值基本只是改变了二维码的大小，读者设置的值都是现在普遍使用的值。
           // 设置错误校验（错误更正）的级别：
           // 这里设置为中等，一共有四个级别，读者可以自行试验。

           Bitmap bm = qrCodeEncoder.Encode(url);
           MemoryStream ms = new MemoryStream();
           bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
           ms.Close();
           return bytes;
       }
        
    }
}
