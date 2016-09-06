/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/19 10:11:11

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Util
{
    public class Degree
    {
        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public Degree(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class CoordDispose
     {
         private const double EARTH_RADIUS = 6370693.5;//地球半径(米)

         /// <summary>
         /// 角度数转换为弧度公式
        /// </summary>
         /// <param name="d"></param>
          /// <returns></returns>
          private static double radians(double d)
         {
             return d * Math.PI / 180.0;
        }

         /// <summary>
         /// 弧度转换为角度数公式
        /// </summary>
         /// <param name="d"></param>
         /// <returns></returns>
         private static double degrees(double d)
         {
             return d * (180 / Math.PI);
         }
 
         /// <summary>
         /// 计算两个经纬度之间的直接距离
         /// </summary>
 
         public static double GetDistance(Degree Degree1, Degree Degree2)
         {
             double radLat1 = radians(Degree1.Y);
             double radLat2 = radians(Degree2.Y);
             double a = radLat1 - radLat2;
             double b = radians(Degree1.X) - radians(Degree2.X);
 
             double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
              Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
             s = s * EARTH_RADIUS;
             s = Math.Round(s * 10000) / 10000;
             return s;
         }
 
         /// <summary>
         /// 计算两个经纬度之间的直接距离(google 算法)
         /// </summary>
         public static double GetDistanceGoogle(Degree Degree1, Degree Degree2)
         {
             double radLat1 = radians(Degree1.Y);
             double radLng1 = radians(Degree1.X);
             double radLat2 = radians(Degree2.Y);
             double radLng2 = radians(Degree2.X);
 
             double s = Math.Acos(Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Cos(radLng1 - radLng2) + Math.Sin(radLat1) * Math.Sin(radLat2));
             s = s * EARTH_RADIUS;
             s = Math.Round(s * 10000) / 10000;
             return s;
         }
 


         private static void GetlatLon(double GLON, double GLAT, double distance, double angle, out double newLon,out double newLat)
         {
             double Ea = 6378137;     //   赤道半径  
             double Eb = 6356725;     //   极半径
             double dx = distance  * Math.Sin(angle * Math.PI / 180.0);
             double dy = distance  * Math.Cos(angle * Math.PI / 180.0);
             //double ec = 6356725 + 21412 * (90.0 - GLAT) / 90.0;

             // 21412 是赤道半径与极半径的差
             double ec = Eb + (Ea - Eb) * (90.0 - GLAT) / 90.0;
             double ed = ec * Math.Cos(GLAT * Math.PI / 180);
             newLon = (dx / ed + GLON * Math.PI / 180.0) * 180.0 / Math.PI;
             newLat = (dy / ec + GLAT * Math.PI / 180.0) * 180.0 / Math.PI;
           
         }
         public static Degree[] GetRectRange(double centorLogitude,double centorlatitude, double distance)
         {

             double temp = 0.0;
             double maxLatitude;
             double minLatitude;
             double maxLongitude;
             double minLongitude;
             GetlatLon(centorLogitude, centorlatitude, distance, 0, out temp, out maxLatitude);

             GetlatLon(centorLogitude, centorlatitude, distance, 180, out temp, out minLatitude);

             GetlatLon(centorLogitude, centorlatitude, distance, 90, out maxLongitude, out temp);

             GetlatLon(centorLogitude, centorlatitude, distance, 270, out minLongitude, out temp);
             maxLatitude = Math.Round(maxLatitude,6);
             minLatitude = Math.Round(minLatitude,6);
             maxLongitude = Math.Round(maxLongitude,6);
             minLongitude = Math.Round(minLongitude,6);
             return new Degree[] {
                new Degree(minLongitude,maxLatitude),//left-top
                new Degree(minLongitude,minLatitude),//left-bottom
                new Degree(maxLongitude,maxLatitude),//right-top
                new Degree(maxLongitude,minLatitude)  //right-bottom
             };

         }

     }
}
