using Aspose.Cells;
using Framework.Web.Mvc.Sys;
using ICSharpCode.SharpZipLib.Zip;
using SoftProject.CellModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class Data2Excel
    {
        ///// <summary>
        ///// 导出Excel
        ///// </summary>
        ///// <param name="table">数据源</param>
        ///// <param name="outName">输出EXCEL名称</param>
        //public static byte[] ToExcel(DataTable table)
        //{
        //    string outName = table.TableName;
        //    Workbook workBook = new Workbook();
        //    workBook.Worksheets.Clear();
        //    workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
        //    Worksheet ws = workBook.Worksheets[0];

        //    //Style style = workBook.Styles[workBook.Styles.Add()];
        //    ////Setting the line style of the top border
        //    //style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thick;

        //    ////Setting the color of the top border
        //    //style.Borders[BorderType.TopBorder].Color =  Color.Black;

        //    ////Setting the line style of the bottom border
        //    //style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;

        //    ////Setting the color of the bottom border
        //    //style.Borders[BorderType.BottomBorder].Color = Color.Black;

        //    ////Setting the line style of the left border
        //    //style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thick;

        //    ////Setting the color of the left border
        //    //style.Borders[BorderType.LeftBorder].Color = Color.Black;

        //    ////Setting the line style of the right border
        //    //style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thick;

        //    ////Setting the color of the right border
        //    //style.Borders[BorderType.RightBorder].Color = Color.Black;


        //    int x = 1;
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        if (i != 0)
        //            x++;

        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {
        //            if (i == 0)
        //            {
        //                ws.Cells[x - 1, j].PutValue(table.Columns[j].Caption);
        //                //ws.Cells[x - 1, j].SetStyle(style);
        //            }
        //            ws.Cells[x, j].PutValue(table.Rows[i][j].ToString());
        //            //ws.Cells[x, j].SetStyle(style);
        //        }
        //    }
        //    workBook.Worksheets[0].AutoFitColumns();

        //    System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
        //    byte[] bt = ms.ToArray();
        //    return bt;
        //    //WorkbookDesigner designer = new WorkbookDesigner();
        //    //designer.Workbook = workBook;

        //    //designer.Process();
        //    ////将流文件写到客户端流的形式写到客户端，名称是outName.xls
        //    //designer.Save(string.Format("{0}.xls",outName), SaveType.OpenInExcel, FileFormatType.Excel2003, System.Web.HttpContext.Current.Response);
        //    ////Response.Flush();
        //    ////Response.Close();
        //    //designer = null;
        //    //// Response.End();
        //    ////return View("getexcel");
        //}

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="outName">输出EXCEL名称</param>
        public static byte[] ToExcel(DataTable table)
        {
            string outName = table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add();//outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            for (int j = 0; j < table.Columns.Count - 1; j++)
            {
                ws.Cells[1, j].PutValue(table.Columns[j].Caption);
            }

            int x = 1;
            for (int i = 0; i < table.Rows.Count - 1; i++)
            {
                if (i != 0)
                    x++;

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        ws.Cells[x - 1, j].PutValue(table.Columns[j].Caption);
                        //ws.Cells[x - 1, j].SetStyle(style);
                    }
                    ws.Cells[x, j].PutValue(table.Rows[i][j].ToString());
                    //ws.Cells[x, j].SetStyle(style);
                }
            }
            workBook.Worksheets[0].AutoFitColumns();

            System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
            byte[] bt = ms.ToArray();
            return bt;
        }

        /// <summary>
        /// 导出Excel
        /// posTotal:0：头部，1：尾部
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="outName">输出EXCEL名称</param>
        public static byte[] ToExcel(IEnumerable<SoftProjectAreaEntity> Items, List<SoftProjectAreaEntity> DynReportDefineDetails, int posTotal = 1, string SaveFilePath = "")
        {
            string outName = "";// table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            int x = 0;
            var count = Items.Count();
            if (posTotal == 0)//开始处
            {
                if (count > 0)
                {
                    var item = Items.Last();//[count - 1];
                    Type type = item.GetType();
                    WriteRow(DynReportDefineDetails, ws, x, item, type);
                    x++;
                }
            }
            //写入表头
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                ws.Cells[x, j].PutValue(DynReportDefineDetails[j].NameCn);
            }
            x++;

            foreach (var item in Items)
            {
                Type type = item.GetType();
                WriteRow(DynReportDefineDetails, ws, x, item, type);
                if (posTotal == 0 && x == count)
                    break;
                x++;
            }
            workBook.Worksheets[0].AutoFitColumns();

            System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
            if (SaveFilePath != "")
                workBook.Save(SaveFilePath);
            byte[] bt = ms.ToArray();
            return bt;
        }

        /// <summary>
        /// 导出Excel
        /// posTotal:0：头部，1：尾部
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="outName">输出EXCEL名称</param>
        public static void ToExcelSaveFile(IEnumerable<SoftProjectAreaEntity> Items, List<SoftProjectAreaEntity> DynReportDefineDetails, string SaveFilePath, int posTotal = 1)
        {
            string outName = "";// table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            int x = 0;
            var count = Items.Count();
            if (posTotal == 0)//开始处
            {
                if (count > 0)
                {
                    var item = Items.Last();//[count - 1];
                    Type type = item.GetType();
                    WriteRow(DynReportDefineDetails, ws, x, item, type);
                    x++;
                }
            }
            //写入表头
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                ws.Cells[x, j].PutValue(DynReportDefineDetails[j].NameCn);
            }
            x++;

            foreach (var item in Items)
            {
                Type type = item.GetType();
                WriteRow(DynReportDefineDetails, ws, x, item, type);
                if (posTotal == 0 && x == count)
                    break;
                x++;
            }
            workBook.Worksheets[0].AutoFitColumns();

            //System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
            if (SaveFilePath != "")
                workBook.Save(SaveFilePath);
            //byte[] bt = ms.ToArray();
            //return bt;
        }

        /// <summary>
        /// 导出Excel
        /// posTotal:0：头部，1：尾部
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="outName">输出EXCEL名称</param>
        public static byte[] ToExcel1(IEnumerable<object> Items, List<SoftProjectAreaEntity> DynReportDefineDetails)
        {
            string outName = "";// table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            int x = 0;
            var count = Items.Count();
            //if (posTotal == 0)//开始处
            //{
            //    if (count > 0)
            //    {
            //        var item = Items.Last();//[count - 1];
            //        Type type = item.GetType();
            //        WriteRow(DynReportDefineDetails, ws, x, item, type);
            //        x++;
            //    }
            //}
            //写入表头
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                ws.Cells[x, j].PutValue(DynReportDefineDetails[j].NameCn);
            }
            x++;

            foreach (var item in Items)
            {
                Type type = item.GetType();
                WriteRow(DynReportDefineDetails, ws, x, item, type);
                //if (posTotal == 0 && x == count)
                //    break;
                x++;
            }
            workBook.Worksheets[0].AutoFitColumns();

            System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
            byte[] bt = ms.ToArray();
            return bt;
        }

        private static void WriteRow(List<SoftProjectAreaEntity> DynReportDefineDetails, Worksheet ws, int x, object item, Type type)
        {
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                if (string.IsNullOrEmpty(DynReportDefineDetails[j].name))
                    continue;
                PropertyInfo property = type.GetProperty(DynReportDefineDetails[j].name);
                object value = property.GetValue(item, null);
                ws.Cells[x, j].PutValue(value);
                var style = ws.Cells[x, j].GetStyle();
                if (DynReportDefineDetails[j].xtype == 5)
                {
                    //worksheet.Cells["A2"].PutValue(a, true);   --①主要是这个，增加一个参数，一般不会用到
                    //Getting the Style of the A2 Cell
                    //Setting the display format to number 9 to show value as percentage
                    style.Number = 42;  //-- ②这个Number设置 官方有API可以看到 相应的设置
                    //Applying the style to the A2 cell
                    //style.ForegroundColor = Color.Red;// Color.FromArgb(153, 204, 0); ;
                    ws.Cells[x, j].SetStyle(style);
                }
                #region
                //if (DynReportDefineDetails[j].CellStyle != null)
                //{
                //    //style.ForegroundColor = Color.Red;
                //    //字段为1(退货)，则设置为红色：2|bReturnGood|equ|1|1&ForegroundColor|Red
                //    var styles = DynReportDefineDetails[j].CellStyle.Split('&');
                //    var where = styles[0].Split('|');
                //    var targerstyles = styles[1].Split();

                //    object firstvalue;
                //    object secvalue;
                //    #region 获取操作数
                //    //第1操作数
                //    if (where[0] == "2")
                //    {
                //        PropertyInfo propertyFirst = type.GetProperty(where[0]);
                //        firstvalue = propertyFirst.GetValue(item, null);
                //    }
                //    else
                //        firstvalue = where[1];
                //    //第2操作数
                //    if (where[3] == "2")
                //    {
                //        PropertyInfo propertysec = type.GetProperty(where[0]);
                //        secvalue = propertysec.GetValue(item, null);
                //    }
                //    else
                //        secvalue = where[4];
                //    #endregion
                //    #region 
                //    if (firstvalue != null && secvalue != null)
                //    {
                //        //根据valueStyle的类型判断进行计算
                //        var strfirstvalue = firstvalue.ToString();
                //        var strsecvalue = secvalue.ToString();
                //        switch (where[2])
                //        {
                //            case "=":
                //                if (strfirstvalue.Equals(strsecvalue))
                //                {
                //                    if (targerstyles[0] == "ForegroundColor")
                //                    {
                //                        switch (targerstyles[1])
                //                        {
                //                            case "":
                //                                style.ForegroundColor = Color.Red;
                //                                break;
                //                        }
                //                    }
                //                }
                //                break;
                //        }
                //    }
                //    #endregion
                //}
                #endregion
            }
        }

        public static byte[] ToHtml(IEnumerable<object> Items, List<SoftProjectAreaEntity> DynReportDefineDetails, int posTotal = 1)
        {
            string outName = "";// table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            int x = 0;
            var count = Items.Count();
            if (posTotal == 0)//开始处
            {
                if (count > 0)
                {
                    var item = Items.Last();//[count - 1];
                    Type type = item.GetType();
                    WriteRow(DynReportDefineDetails, ws, x, item, type);
                    x++;
                }
            }
            //写入表头
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                ws.Cells[x, j].PutValue(DynReportDefineDetails[j].NameCn);
            }
            x++;

            foreach (var item in Items)
            {
                Type type = item.GetType();
                WriteRow(DynReportDefineDetails, ws, x, item, type);
                if (posTotal == 0 && x == count)
                    break;
                x++;
            }
            workBook.Worksheets[0].AutoFitColumns();

            System.IO.MemoryStream ms = workBook.SaveToStream();//生成数据流 
            byte[] bt = ms.ToArray();
            return bt;
        }

        private static void WriteRowHtml(List<SoftProjectAreaEntity> DynReportDefineDetails, object item, Type type)
        {
            StringBuilder strhtml = new StringBuilder("<tr>");
            for (var j = 0; j < DynReportDefineDetails.Count; j++)
            {
                PropertyInfo property = type.GetProperty(DynReportDefineDetails[j].name);
                object value = property.GetValue(item, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    if (DynReportDefineDetails[j].xtype == 5)//货币类型
                    {
                        val = strval.ToDecimalNull().MoneyNum();
                    }
                    else if (DynReportDefineDetails[j].xtype == 4)//日期类型
                    {
                        val = strval.ToDateNull().Format_yyyy_MM_dd();
                    }
                    else
                        val = strval;
                }
                strhtml.Append(string.Format("<td>{0}<td>", val));
            }
            strhtml.Append("</tr>");
        }

        //using ICSharpCode.SharpZipLib.Zip;
        #region 加压解压方法
        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹夹路径</param>
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>
        /// <param name="err">出错信息</param>
        /// <returns>是否压缩成功</returns>
        public bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "要压缩的文件夹不能为空！";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "要压缩的文件夹不存在！";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋.zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("//"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 功能：解压zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>
        /// <param name="err">出错信息</param>
        /// <returns>解压是否成功</returns>
        public bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath == string.Empty)
            {
                err = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFilePath))
            {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("//"))
                unZipDir += "//";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("//"))
                            directoryName += "//";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }//while
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//解压结束
        #endregion

    }
}
