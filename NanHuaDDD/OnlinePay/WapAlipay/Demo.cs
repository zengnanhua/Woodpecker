/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 10:42:32

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.OnlinePay.WapAlipay
{
    public class Demo
    {
       
            WAPPay wapPay = new WAPPay();
            /// <summary>
            /// 发送支付请求
            /// </summary>
            public void Send()
            {
                wapPay.PayFormString("order001", "手机支付", 500);

            }
            /// <summary>
            /// 服务端回调（ＰＯＳＴ）
            /// </summary>
            public void Notify()
            {
                wapPay.AlipayNotify((orderId, tradeId) =>
                {
                    //更新数据表相关逻辑
                });
            }
            /// <summary>
            /// 服务端回调（ＧＥＴ）
            /// </summary>
            public void Callback()
            {
                wapPay.AlipayCallback((orderId, tradeId) =>
                {
                    //更新数据表相关逻辑
                    //跳到成功页面
                }, (msg) =>
                {
                    //跳到失败页面
                });
            }
     
    }
}
