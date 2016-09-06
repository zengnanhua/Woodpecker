using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Text.RegularExpressions;
using System.Web;
namespace Web.Common
{
    public class ExcelHelper
    {
        public enum CellType
        {
            Unknown = -1,
            Numeric = 0,
            String = 1,
            Formula = 2,
            Blank = 3,
            Boolean = 4,
            Error = 5,
        }

        public class x2003
        {
            private static List<string> datetimeFormats = new List<string>();
            #region Excel2003
            /// <summary>
            /// 将Excel文件中的数据读出到DataTable中(xls)
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            public static DataTable ExcelToTableForXLS(string file)
            {
                if (datetimeFormats.Count == 0)
                {
                    datetimeFormats = new List<string>();
                    datetimeFormats.Add("yyyy-m-d");
                    datetimeFormats.Add("[$-F800]dddd, mmmm dd, yyyy");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("m\"月\"d\"日\";@");
                    datetimeFormats.Add("[$-804]aaaa;@");
                    datetimeFormats.Add("[$-804]aaa;@");
                    datetimeFormats.Add("yyyy-m-d;@");
                    datetimeFormats.Add("[$-409]yyyy-m-d h:mm AM/PM;@");
                    datetimeFormats.Add("yyyy-m-d h:mm;@");
                    datetimeFormats.Add("yy-m-d;@");
                    datetimeFormats.Add("m-d;@");
                    datetimeFormats.Add("m-d-yy;@");
                    datetimeFormats.Add("mm-dd-yy;@");
                    datetimeFormats.Add("[$-409]d-mmm;@");
                    datetimeFormats.Add("[$-409]d-mmm-yy;@");
                    datetimeFormats.Add("[$-409]mmmmm;@");
                    datetimeFormats.Add("[$-409]mmmmm-yy;@");
                    datetimeFormats.Add("m/d/yy");
                }
                DataTable dt = new DataTable();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
                    ISheet sheet = hssfworkbook.GetSheetAt(0);

                    //表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                            //continue;
                        }
                        else
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                        columns.Add(i);
                    }
                    //数据
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dr = dt.NewRow();
                        bool hasValue = false;
                        foreach (int j in columns)
                        {
                            dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell);
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        if (hasValue)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }

            /// <summary>
            /// 将DataTable数据导出到Excel文件中(xls)
            /// </summary>
            /// <param name="dt"></param>
            /// <param name="file"></param>
            public static void TableToExcelForXLS(DataTable dt, string file)
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook();
                ISheet sheet = hssfworkbook.CreateSheet("Test");

                //表头
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                //数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                //转为字节数组
                MemoryStream stream = new MemoryStream();
                hssfworkbook.Write(stream);
                byte[] buf = stream.ToArray();

                //保存为Excel文件
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }

            /// <summary>
            /// 获取单元格类型(xls)
            /// </summary>
            /// <param name="cell"></param>
            /// <returns></returns>
            private static object GetValueTypeForXLS(HSSFCell cell)
            {
                if (cell == null)
                    return null;

                if (datetimeFormats.IndexOf(cell.CellStyle.GetDataFormatString()) > -1)
                {
                    try
                    {
                        return cell.DateCellValue;
                    }
                    catch { }
                }

                switch (cell.CellType)
                {
                    case NPOI.SS.UserModel.CellType.BLANK:
                        return null;
                    case NPOI.SS.UserModel.CellType.BOOLEAN:
                        return cell.BooleanCellValue;
                    case NPOI.SS.UserModel.CellType.ERROR:
                        return cell.ErrorCellValue;
                    case NPOI.SS.UserModel.CellType.NUMERIC:
                        return cell.NumericCellValue;
                    case NPOI.SS.UserModel.CellType.STRING:
                        return cell.StringCellValue;
                    case NPOI.SS.UserModel.CellType.Unknown:
                        return null;
                    case NPOI.SS.UserModel.CellType.FORMULA:
                    default:
                        return "=" + cell.CellFormula;
                }

            }

            /// <summary>
            /// 将Excel文件中的数据读出到DataTable中(xls)
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            public static DataTable MyExcelToTableForXLS(string file)
            {
                if (datetimeFormats.Count == 0)
                {
                    datetimeFormats = new List<string>();
                    datetimeFormats.Add("yyyy-m-d");
                    datetimeFormats.Add("[$-F800]dddd, mmmm dd, yyyy");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("m\"月\"d\"日\";@");
                    datetimeFormats.Add("[$-804]aaaa;@");
                    datetimeFormats.Add("[$-804]aaa;@");
                    datetimeFormats.Add("yyyy-m-d;@");
                    datetimeFormats.Add("[$-409]yyyy-m-d h:mm AM/PM;@");
                    datetimeFormats.Add("yyyy-m-d h:mm;@");
                    datetimeFormats.Add("yy-m-d;@");
                    datetimeFormats.Add("m-d;@");
                    datetimeFormats.Add("m-d-yy;@");
                    datetimeFormats.Add("mm-dd-yy;@");
                    datetimeFormats.Add("[$-409]d-mmm;@");
                    datetimeFormats.Add("[$-409]d-mmm-yy;@");
                    datetimeFormats.Add("[$-409]mmmmm;@");
                    datetimeFormats.Add("[$-409]mmmmm-yy;@");
                    datetimeFormats.Add("m/d/yy");
                }
                DataTable dt = new DataTable();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
                    ISheet sheet = hssfworkbook.GetSheetAt(0);
                    dt.TableName = sheet.SheetName;
                    int sheetNum = hssfworkbook.NumberOfSheets;
                    //表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                            //continue;
                        }
                        else
                        {
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                        }
                        columns.Add(i);
                    }
                    columns.Add(header.LastCellNum);
                    dt.Columns.Add(new DataColumn("rang"));
                    for (int coindex = 0; coindex < sheetNum; coindex++)
                    {
                        ISheet son_sheet = hssfworkbook.GetSheetAt(coindex);
                        //表头
                        IRow son_header = son_sheet.GetRow(son_sheet.FirstRowNum);
                        //数据
                        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                        {
                            DataRow dr = dt.NewRow();
                            bool hasValue = false;
                            foreach (int j in columns)
                            {
                                dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell);
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                                if (j == columns.Count - 1)
                                {
                                    dr[j] = son_sheet.SheetName;
                                }
                            }
                            if (hasValue)
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
                return dt;
            }
            #endregion
        }

        public class x2007
        {
            #region Excel2007
            private static List<string> datetimeFormats = new List<string>();
            public static DataTable MyExcelToTableForXLSX(string file)
            {
                if (datetimeFormats.Count == 0)
                {
                    datetimeFormats = new List<string>();
                    datetimeFormats.Add("yyyy-m-d");
                    datetimeFormats.Add("[$-F800]dddd, mmmm dd, yyyy");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("m\"月\"d\"日\";@");
                    datetimeFormats.Add("[$-804]aaaa;@");
                    datetimeFormats.Add("[$-804]aaa;@");
                    datetimeFormats.Add("yyyy-m-d;@");
                    datetimeFormats.Add("[$-409]yyyy-m-d h:mm AM/PM;@");
                    datetimeFormats.Add("yyyy-m-d h:mm;@");
                    datetimeFormats.Add("yy-m-d;@");
                    datetimeFormats.Add("m-d;@");
                    datetimeFormats.Add("m-d-yy;@");
                    datetimeFormats.Add("mm-dd-yy;@");
                    datetimeFormats.Add("[$-409]d-mmm;@");
                    datetimeFormats.Add("[$-409]d-mmm-yy;@");
                    datetimeFormats.Add("[$-409]mmmmm;@");
                    datetimeFormats.Add("[$-409]mmmmm-yy;@");
                    datetimeFormats.Add("m/d/yy");
                }
                DataTable dt = new DataTable();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    int sheetNum = xssfworkbook.NumberOfSheets;

                    ISheet sheet = xssfworkbook.GetSheetAt(0);
                    dt.TableName = sheet.SheetName;
                    //表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();

                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        }
                        else
                        {
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                        }
                        columns.Add(i);
                    }
                    columns.Add(header.LastCellNum);
                    dt.Columns.Add(new DataColumn("rang"));
                    for (int coindex = 0; coindex < sheetNum; coindex++)
                    {
                        ISheet son_sheet = xssfworkbook.GetSheetAt(coindex);
                        //表头
                        IRow son_header = son_sheet.GetRow(son_sheet.FirstRowNum);
                        //数据
                        for (int i = son_sheet.FirstRowNum + 1; i <= son_sheet.LastRowNum; i++)
                        {
                            DataRow dr = dt.NewRow();
                            bool hasValue = false;
                            foreach (int j in columns)
                            {
                                dr[j] = GetValueTypeForXLSX(son_sheet.GetRow(i).GetCell(j) as XSSFCell);
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                                if (j == columns.Count - 1)
                                {
                                    dr[j] = son_sheet.SheetName;
                                }
                            }
                            if (hasValue)
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }

                return dt;
            }
            /// <summary>
            /// 将Excel文件中的数据读出到DataTable中(xlsx)
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            /// 

            public static DataTable ExcelToTableForXLSX(string file)
            {
                if (datetimeFormats.Count == 0)
                {
                    datetimeFormats = new List<string>();
                    datetimeFormats.Add("yyyy-m-d");
                    datetimeFormats.Add("[$-F800]dddd, mmmm dd, yyyy");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]m\"月\"d\"日\";@");
                    datetimeFormats.Add("[DBNum1][$-804]yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\"d\"日\";@");
                    datetimeFormats.Add("yyyy\"年\"m\"月\";@");
                    datetimeFormats.Add("m\"月\"d\"日\";@");
                    datetimeFormats.Add("[$-804]aaaa;@");
                    datetimeFormats.Add("[$-804]aaa;@");
                    datetimeFormats.Add("yyyy-m-d;@");
                    datetimeFormats.Add("[$-409]yyyy-m-d h:mm AM/PM;@");
                    datetimeFormats.Add("yyyy-m-d h:mm;@");
                    datetimeFormats.Add("yy-m-d;@");
                    datetimeFormats.Add("m-d;@");
                    datetimeFormats.Add("m-d-yy;@");
                    datetimeFormats.Add("mm-dd-yy;@");
                    datetimeFormats.Add("[$-409]d-mmm;@");
                    datetimeFormats.Add("[$-409]d-mmm-yy;@");
                    datetimeFormats.Add("[$-409]mmmmm;@");
                    datetimeFormats.Add("[$-409]mmmmm-yy;@");
                    datetimeFormats.Add("m/d/yy");
                }
                DataTable dt = new DataTable();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    int index = xssfworkbook.NumberOfSheets;
                    for (int k = 0; k < index; k++)
                    {
                        ISheet sheet = xssfworkbook.GetSheetAt(k);
                        dt.TableName = sheet.SheetName;
                        //表头
                        IRow header = sheet.GetRow(sheet.FirstRowNum);
                        List<int> columns = new List<int>();

                        for (int i = 0; i < header.LastCellNum; i++)
                        {
                            object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                            if (obj == null || obj.ToString() == string.Empty)
                            {
                                dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                            }
                            else
                            {
                                dt.Columns.Add(new DataColumn(obj.ToString()));
                            }
                            columns.Add(i);
                        }

                        //数据
                        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                        {
                            DataRow dr = dt.NewRow();
                            bool hasValue = false;
                            foreach (int j in columns)
                            {
                                dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            if (hasValue)
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
                return dt;
            }

            /// <summary>
            /// 将DataTable数据导出到Excel文件中(xlsx)
            /// </summary>
            /// <param name="dt"></param>
            /// <param name="file"></param>
            public static void TableToExcelForXLSX(DataTable dt, string file)
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook();
                ISheet sheet = xssfworkbook.CreateSheet("Test");

                //表头
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                //数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                //转为字节数组
                MemoryStream stream = new MemoryStream();
                xssfworkbook.Write(stream);
                byte[] buf = stream.ToArray();

                //保存为Excel文件
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }

            /// <summary>
            /// 获取单元格类型(xlsx)
            /// </summary>
            /// <param name="cell"></param>
            /// <returns></returns>
            private static object GetValueTypeForXLSX(XSSFCell cell)
            {
                if (cell == null)
                    return null;
                if (datetimeFormats.IndexOf(cell.CellStyle.GetDataFormatString()) > -1)
                {
                    try
                    {
                        return cell.DateCellValue;
                    }
                    catch { }
                }
                switch (cell.CellType)
                {
                    case NPOI.SS.UserModel.CellType.BLANK:
                        return null;
                    case NPOI.SS.UserModel.CellType.BOOLEAN:
                        return cell.BooleanCellValue;
                    case NPOI.SS.UserModel.CellType.ERROR:
                        return cell.ErrorCellValue;
                    case NPOI.SS.UserModel.CellType.NUMERIC:
                        return cell.NumericCellValue;
                    case NPOI.SS.UserModel.CellType.STRING:
                        return cell.StringCellValue;
                    case NPOI.SS.UserModel.CellType.Unknown:
                        return null;
                    case NPOI.SS.UserModel.CellType.FORMULA:
                    default:
                        return "=" + cell.CellFormula;
                }
                //switch (cell.CellType)
                //{
                //    case CellType.Blank: //BLANK:
                //        return null;
                //    case CellType.Boolean: //BOOLEAN:
                //        return cell.BooleanCellValue;
                //    case CellType.Numeric: //NUMERIC:
                //        return cell.NumericCellValue;
                //    case CellType.String: //STRING:
                //        return cell.StringCellValue;
                //    case CellType.Error: //ERROR:
                //        return cell.ErrorCellValue;
                //    case CellType.Formula: //FORMULA:
                //    default:
                //        return "=" + cell.CellFormula;
                //}
            }
            #endregion
        }

        public static DataTable GetDataTable(string filepath)
        {
            filepath = filepath.ToLower();
            DataTable dt = new DataTable("xls");
            if (filepath.Substring(filepath.LastIndexOf('.') + 1) == "xls")
            {
                dt = x2003.ExcelToTableForXLS(filepath);
            }
            else
            {
                dt = x2007.ExcelToTableForXLSX(filepath);
            }
            return dt;
        }

        public static DataTable MyGetDataTable(string filepath)
        {
            filepath = filepath.ToLower();
            DataTable dt = new DataTable("xls");
            if (filepath.Substring(filepath.LastIndexOf('.') + 1) == "xls")
            {
                dt = x2003.MyExcelToTableForXLS(filepath);
            }
            else
            {
                dt = x2007.MyExcelToTableForXLSX(filepath);
            }
            return dt;
        }

        //Datatable导出Excel
        public static void GridToExcelByNPOI(DataTable dt, string strExcelFileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            try
            {
                ISheet sheet = workbook.CreateSheet("Sheet1");
                ICellStyle HeadercellStyle = workbook.CreateCellStyle();
                HeadercellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                //字体
                NPOI.SS.UserModel.IFont headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short)FontBoldWeight.BOLD;
                HeadercellStyle.SetFont(headerfont);


                //用column name 作为列名
                int icolIndex = 0;
                IRow headerRow = sheet.CreateRow(0);
                foreach (DataColumn item in dt.Columns)
                {
                    ICell cell = headerRow.CreateCell(icolIndex);
                    cell.SetCellValue(item.ColumnName);
                    cell.CellStyle = HeadercellStyle;
                    icolIndex++;
                }

                ICellStyle cellStyle = workbook.CreateCellStyle();

                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;


                NPOI.SS.UserModel.IFont cellfont = workbook.CreateFont();
                cellfont.Boldweight = (short)FontBoldWeight.NORMAL;
                cellStyle.SetFont(cellfont);

                //建立内容行
                int iRowIndex = 1;
                int iCellIndex = 0;
                foreach (DataRow Rowitem in dt.Rows)
                {
                    IRow DataRow = sheet.CreateRow(iRowIndex);
                    foreach (DataColumn Colitem in dt.Columns)
                    {

                        ICell cell = DataRow.CreateCell(iCellIndex);
                        cell.SetCellValue(Rowitem[Colitem].ToString());
                        cell.CellStyle = cellStyle;
                        iCellIndex++;
                    }
                    iCellIndex = 0;
                    iRowIndex++;
                }

                //自适应列宽度
                for (int i = 0; i < icolIndex; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                //写Excel
                FileStream file = new FileStream(strExcelFileName, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Flush();
                file.Close();
            }
            catch (Exception ex)
            {
                //ILog log = LogManager.GetLogger("Exception Log");
                //log.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                ////记录AuditTrail
                //CCFS.Framework.BLL.AuditTrailBLL.LogAuditTrail(ex);
                Console.WriteLine(ex.Message);

            }
            finally
            {
                workbook = null;
            }
        }

        public static HSSFWorkbook CreateExcelByNPOI(DataTable dt)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            try
            {
                ISheet sheet = workbook.CreateSheet("Sheet1");
                ICellStyle HeadercellStyle = workbook.CreateCellStyle();
                HeadercellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                HeadercellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                //字体
                NPOI.SS.UserModel.IFont headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short)FontBoldWeight.BOLD;
                HeadercellStyle.SetFont(headerfont);


                //用column name 作为列名
                int icolIndex = 0;
                IRow headerRow = sheet.CreateRow(0);
                foreach (DataColumn item in dt.Columns)
                {
                    ICell cell = headerRow.CreateCell(icolIndex);
                    cell.SetCellValue(item.ColumnName);
                    cell.CellStyle = HeadercellStyle;
                    icolIndex++;
                }

                ICellStyle cellStyle = workbook.CreateCellStyle();

                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;


                NPOI.SS.UserModel.IFont cellfont = workbook.CreateFont();
                cellfont.Boldweight = (short)FontBoldWeight.NORMAL;
                cellStyle.SetFont(cellfont);

                //建立内容行
                int iRowIndex = 1;
                int iCellIndex = 0;
                foreach (DataRow Rowitem in dt.Rows)
                {
                    IRow DataRow = sheet.CreateRow(iRowIndex);
                    foreach (DataColumn Colitem in dt.Columns)
                    {

                        ICell cell = DataRow.CreateCell(iCellIndex);
                        cell.SetCellValue(Rowitem[Colitem].ToString());
                        cell.CellStyle = cellStyle;
                        iCellIndex++;
                    }
                    iCellIndex = 0;
                    iRowIndex++;
                }

                //自适应列宽度
                for (int i = 0; i < icolIndex; i++)
                {
                    sheet.AutoSizeColumn(i);
                }


                //写Excel

                return workbook;

            }
            catch (Exception ex)
            {
                //ILog log = LogManager.GetLogger("Exception Log");
                //log.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                ////记录AuditTrail
                //CCFS.Framework.BLL.AuditTrailBLL.LogAuditTrail(ex);
                return null;

            }
            finally
            {
                workbook = null;
            }
        }

        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    try
                    {
                        string a = strRows[r].Split('#')[1].Trim();
                        if (a.Equals("null"))
                        {
                            dr[r] = "";
                        }
                        else
                        {
                            dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                        }
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            try
            {
                if (tb != null)
                {
                    return tb;
                }
                else
                {
                    throw new Exception("解析错误");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}