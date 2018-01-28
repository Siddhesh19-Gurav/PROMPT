using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;

//-------------------------------------------
//--------Author : Shailesh Moolya-----------
//-------Last Modified : 15/04/2015----------
//-------------------------------------------

//To Use this you need the following
// 1) add a Reference COM > "Microsoft ActiveX Data Objects 2.7 Library" (ADODB)
// 2) add a Reference COM > "Microsoft Excel 12.0 Object Library" (for this u will need MS Office 2007 installed in your PC)

// and the connection String must have OLEDB driver name (Provider) to access the database
// eg. For SQL Server 2008 is "Provider=SQLOLEDB;Data Source=NACH_UAT\NACH;Initial Catalog=CITI_PDCMIGRATION;User ID=sa;Password=admin@123;MultipleActiveResultSets=True;";
//     For Oracle is "Provider=OraOLEDB.Oracle.1;Password=ADROIT11;Persist Security Info=True;User ID=PDC;Data Source=adroitcorp"; 

// Change Log:
// 15/04/2015 : Bug Fix : Extra Empty Column displayed in excel page
// 11/04/2015 : Modified FillWookSheet()
// 19/03/2015 : Added Fn_Import_Excel_TTC();
// 03/03/2015 : Removed KillApp Command.



namespace PROMPT
{
    public class cls_Excel_Import_Export
    {
        bool is_xlsx = false;
        Database database = new Database("ADCTS");
        
#region "Common Functions"
        public void KillApp(string imageName)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(imageName);
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        bool FileInUse(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    //Just opening the file as open/create
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        //If required we can check for read/write by using fs.CanRead or fs.CanWrite
                    }
                }
                return false;
            }
            catch (IOException ex)
            {
                //check if message is for a File IO
                if (ex.Message.ToString().Contains("The process cannot access the file"))
                    return true;
                else
                    throw;
            }
        }

        private bool IsWorkbookEmpty(Excel.Workbook excelBook)
        {
            try
            {
                if (excelBook.Sheets.Count <= 0)
                {
                    return true;
                }
                else
                {
                    foreach (Excel.Worksheet sheet in excelBook.Sheets)
                    {
                        Excel.Range excelRange = sheet.UsedRange;
                        int test1 = excelRange.Columns.Count;
                        int test2 = excelRange.Rows.Count;
                        int test3 = excelRange.Count;

                        if (test1 > 1 || test2 > 1 || test3 > 1)
                        {
                            return false;
                        }
                        else //look for content..
                            return true;
                        
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
        private bool IsWorkSheetEmpty(Excel.Worksheet sheet)
        {
            try
            {

                Excel.Range excelRange = sheet.UsedRange;
                int test1 = excelRange.Columns.Count;
                int test2 = excelRange.Rows.Count;
                int test3 = excelRange.Count;

                if (test1 > 1 || test2 > 1 || test3 > 1)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            //return true;
        }
#endregion

#region "Export Excel"
        public void FillWookSheet(ref Excel.Worksheet xlsWorkSheet, string sqlQuery, string Connect_String, string WooksheetName = null, bool freezeRow = true)
        {
            if (xlsWorkSheet == null)
            {
                throw new Exception("xlsWorkSheet is not provided");
            }
            if (string.IsNullOrWhiteSpace(sqlQuery))
            {
                throw new Exception("SqlQuery is not provided");
            }
            if (string.IsNullOrWhiteSpace(Connect_String))
            {
                throw new Exception("Connection String is not provided");
            }

            Excel.Range xlsRange;
            int m_RW, m_Col, InitRowCount;
            //string m_colName;


            if (!string.IsNullOrWhiteSpace(WooksheetName)) xlsWorkSheet.Name = WooksheetName;

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["PROMPT"].ConnectionString;
                ADODB.Recordset rs = new ADODB.Recordset();
                ADODB.Connection AdodbCon = new ADODB.Connection();
                AdodbCon.ConnectionString = "Provider = SQLOLEDB;"+connectionString;
                AdodbCon.Open();

                if (rs.State == 1) rs.Close();
                rs = new ADODB.Recordset();
                rs.ActiveConnection = AdodbCon;
                rs.CursorType = ADODB.CursorTypeEnum.adOpenForwardOnly;
                rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                rs.LockType = ADODB.LockTypeEnum.adLockReadOnly;
                rs.Open(sqlQuery);

                //-----------------------testing if records exceeds xls file limits ---------------------------------------
                if (!is_xlsx && (rs.Fields.Count > 256 || rs.RecordCount > 65536))
                {
                    throw new Exception("Select *.xls file format cannot contain records more than 65536 or Columns more than 256");
                }
                
                if (xlsWorkSheet.UsedRange.Rows.Count == 1 && xlsWorkSheet.UsedRange.Columns.Count == 1 && xlsWorkSheet.UsedRange.Value == null) 
                {
                    InitRowCount = 0;
                }
                else
                    InitRowCount = xlsWorkSheet.UsedRange.Rows.Count;

                m_RW = (InitRowCount + 1);
                m_Col = 1;
                
                for (int m_coln = 0; m_coln < rs.Fields.Count; m_coln++)
                {
                    Excel.Range xlcel = (Excel.Range)xlsWorkSheet.Cells[m_RW, m_Col];
                    xlcel.Value = rs.Fields[m_coln].Name;
                    xlcel.ColumnWidth = 15;
                    xlcel = null;
                    m_Col++;
                }
                m_Col--;
                //m_colName = xlsWorkSheet.Columns[xlsWorkSheet.UsedRange.Columns.Count + 1].Address;
                //m_colName = m_colName.Substring(1, 1);
                //xlsRange = xlsWorkSheet.get_Range("A" + m_RW, m_colName + m_RW);
                //xlsRange = xlsWorkSheet.get_Range("A" + m_RW, ((char)(m_colName[0] + (char)xlsWorkSheet.UsedRange.Columns.Count)).ToString());
                //xlsRange = (Excel.Range)xlsWorkSheet.get_Range((Excel.Range)xlsWorkSheet.Cells[m_RW, 1], (Excel.Range)xlsWorkSheet.Cells[1, xlsWorkSheet.UsedRange.Columns.Count]);

                //xlsRange = (Excel.Range)xlsWorkSheet.get_Range((Excel.Range)xlsWorkSheet.Cells[m_RW, 1], (Excel.Range)xlsWorkSheet.Cells[m_RW, m_Col]);
                xlsRange = xlsWorkSheet.Range[xlsWorkSheet.Cells[m_RW, 1], xlsWorkSheet.Cells[m_RW, m_Col]];
                xlsRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlsRange.Cells.Borders.Weight = Excel.XlBorderWeight.xlThick;
                xlsRange.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
                xlsRange.Font.Bold = true;
                xlsRange.HorizontalAlignment = Excel.Constants.xlCenter;
                xlsRange.Interior.Color = Excel.XlRgbColor.rgbGray;
                //xlsRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                //xlsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                

                
                m_RW++;



                if (rs.EOF == false)
                {
                    //xlsRange = xlsWorkSheet.get_Range("A" + m_RW, "A" + (rs.RecordCount + m_RW));
                    xlsRange = xlsWorkSheet.get_Range("A" + m_RW.ToString(), "A" + (m_RW + rs.RecordCount).ToString());
                    //xlsRange = xlsWorkSheet.get_Range(((Excel.Range)xlsWorkSheet.Cells[m_RW, 1]).Address.Replace("$", ""), ((Excel.Range)xlsWorkSheet.Cells[rs.RecordCount, rs.Fields.Count]).Address.Replace("$", ""));
                    xlsRange.CopyFromRecordset(rs);
                }

                rs.Close();
                AdodbCon.Close();

                xlsRange = xlsWorkSheet.Range[xlsWorkSheet.Cells[m_RW, 1], xlsWorkSheet.Cells[xlsWorkSheet.UsedRange.Rows.Count, m_Col]];
                xlsRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                
                xlsWorkSheet.Rows.EntireColumn.AutoFit();

                //freezing First Row
                if (freezeRow)
                {
                    xlsWorkSheet.Activate();
                    xlsWorkSheet.Application.ActiveWindow.SplitRow = m_RW - 1;
                    xlsWorkSheet.Application.ActiveWindow.FreezePanes = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }



        public void Fn_Excel_Export(string filePath, string constr, string Query, bool OverwriteFile = true, string PageNames = null, bool WookbookVisibility = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("File Path is not provided");
            }
            else if (FileInUse(filePath))
                throw new Exception("File is being used by other process");
            

            if (string.IsNullOrWhiteSpace(constr))
            {
                throw new Exception("Connection String is not provided");
            }
            if (string.IsNullOrWhiteSpace(Query))
            {
                throw new Exception("Queries not provided is not provided");
            }

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                throw new Exception("File address is not correct");
            }
            else
                filePath = System.IO.Path.GetFullPath(filePath);

            if (Path.GetExtension(filePath).ToLower() == ".xlsx")
                is_xlsx = true;
            else
                is_xlsx = false;

            //KillApp("Excel");
            Excel.Application xlsApp;
            Excel.Workbook xlsWorkBook;
            Excel.Worksheet xlsWorkSheet = null;
            string ExportFilePath = filePath;

            //Excel sheet insertion
            xlsApp = new Excel.Application();
            xlsWorkBook = xlsApp.Workbooks.Add(Type.Missing);

            //deleting all existing sheets
            for (int j = xlsWorkBook.Worksheets.Count; j > 1; j--)
            {
                ((Excel.Worksheet)xlsWorkBook.Worksheets[j]).Delete();
            }

            //adding sheets
            xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets[1];
            xlsWorkSheet.Name = string.IsNullOrWhiteSpace(PageNames) ? "Sheet1" : PageNames;


            //Make excel window visible
            if (WookbookVisibility)
            {
                xlsWorkSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                xlsApp.Visible = true;
            }


            try
            {
                // filling data
                FillWookSheet(ref xlsWorkSheet, Query, constr);
            }
            catch (Exception ex)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                xlsWorkSheet = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
                xlsWorkBook = null;
                xlsApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
                xlsApp = null;
                GC.Collect();
                throw ex;
            }


            xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Sheets[1];
            xlsWorkSheet.Activate();

            // Save the excel file
            try
            {
                if (OverwriteFile)
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                object misValue = System.Reflection.Missing.Value;
                if (Path.GetExtension(@ExportFilePath).ToLower() == ".xlsx")
                    xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else
                    xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            xlsWorkBook.Close();
            if (xlsWorkSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                xlsWorkSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
            xlsWorkBook = null;
            xlsApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            xlsApp = null;
            GC.Collect();
        }

        public void Fn_Excel_Export(string filePath, string constr, string[] Query, bool OverwriteFile = true, string[] PageNames = null, bool WookbookVisibility = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("File Path is not provided");
            }
            else if (FileInUse(filePath))
                throw new Exception("File is being used by other process");

            if (string.IsNullOrWhiteSpace(constr))
            {
                throw new Exception("Connection String is not provided");
            }
            if (Query == null)
            {
                throw new Exception("Queries not provided is not provided");
            }
            foreach (string str in Query)
            {
                if (string.IsNullOrWhiteSpace(str))
                    throw new Exception("One or more Queries in Query array is null");
            }


            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                throw new Exception("File address is not correct or folder doesn't exists");
            }
            else
                filePath = System.IO.Path.GetFullPath(filePath);

            if (Path.GetExtension(filePath).ToLower() == ".xlsx")
                is_xlsx = true;
            else
                is_xlsx = false;

            //KillApp("Excel");
            Excel.Application xlsApp;
            Excel.Workbook xlsWorkBook;
            Excel.Worksheet xlsWorkSheet = null;
            string ExportFilePath = filePath;

            //Excel sheet insertion
            xlsApp = new Excel.Application();
            xlsWorkBook = xlsApp.Workbooks.Add(Type.Missing);

            //deleting all existing sheets
            for (int j = xlsWorkBook.Worksheets.Count; j > 1; j--)
            {
                ((Excel.Worksheet)xlsWorkBook.Worksheets[j]).Delete();
            }

            //adding sheets & filling data
            for (int i = 0; i < Query.Length; i++)
            {
                if (i == 0)
                    xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets[1];
                else
                    xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets.Add();

                xlsWorkSheet.Name = PageNames != null ? string.IsNullOrWhiteSpace(PageNames[i]) ? ("Sheet" + (i + 1).ToString()) : PageNames[i] : ("Sheet" + (i + 1).ToString());


                try
                {
                    FillWookSheet(ref xlsWorkSheet, Query[i], constr);

                }
                catch (Exception ex)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                    xlsWorkSheet = null;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
                    xlsWorkBook = null;
                    xlsApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
                    xlsApp = null;
                    GC.Collect();
                    throw ex;
                }
            }
            for (int p = xlsWorkBook.Worksheets.Count; p >= 1; p--)
            {
                xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Sheets[p];
                xlsWorkSheet.Activate();
                xlsWorkSheet.Move(System.Reflection.Missing.Value, xlsWorkBook.Worksheets[xlsWorkBook.Worksheets.Count]);
            }
            xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Sheets[1];
            xlsWorkSheet.Activate();

            //Make excel window visible
            if (WookbookVisibility)
            {
                xlsWorkSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                xlsApp.Visible = true;
            }

            // Save the excel file
            try
            {
                if (OverwriteFile)
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                object misValue = System.Reflection.Missing.Value;
                //xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                ////xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                if (Path.GetExtension(@ExportFilePath).ToLower() == ".xlsx")
                    xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else
                    xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            xlsWorkBook.Close();
            if (xlsWorkSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                xlsWorkSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
            xlsWorkBook = null;
            xlsApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            xlsApp = null;
            GC.Collect();
        }

        public void Fn_Excel_Export_Multi_File(string[] filePaths, string constr, string[] Query, bool OverwriteFile = true, string[] PageNames = null, bool WookbookVisibility = false)
        {
            if (filePaths != null && filePaths.Length == Query.Length)
            {
                for (int o = 0; o < filePaths.Length;o++ )
                {
                    if (string.IsNullOrWhiteSpace(filePaths[o]))
                    {
                        throw new Exception("One or more Path in File paths array is null");
                    }
                    else if (FileInUse(filePaths[o]))
                        throw new Exception("File is being used by other process");
            
                    else if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePaths[o])))
                    {
                        throw new Exception("File address is not correct\n\n" + filePaths[o]);
                    }
                    else if (FileInUse(filePaths[o]))
                        throw new Exception("File is being used by other process");
                    else
                        filePaths[o] = System.IO.Path.GetFullPath(filePaths[o]);
                }
            }
            else
                throw new Exception("filePaths not provided or No. of filePaths does not match no. of queries");

            if (PageNames != null)
            {
                if (PageNames.Length != Query.Length)
                {
                    throw new Exception("No. of PageName does not match no. of queries");
                }
            }

            if (string.IsNullOrWhiteSpace(constr))
            {
                throw new Exception("Connection String is not provided");
            }
            if (Query == null)
            {
                throw new Exception("Queries not provided is not provided");
            }
            foreach (string str in Query)
            {
                if (string.IsNullOrWhiteSpace(str))
                    throw new Exception("One or more Queries in Query array is null");
            }



            //KillApp("Excel");

            for (int i = 0; i < Query.Length; i++)
            {
                Excel.Application xlsApp;
                Excel.Workbook xlsWorkBook;
                Excel.Worksheet xlsWorkSheet = null;


                //Excel sheet insertion
                xlsApp = new Excel.Application();
                xlsWorkBook = xlsApp.Workbooks.Add(Type.Missing);

                //deleting all existing sheets
                for (int j = xlsWorkBook.Worksheets.Count; j > 1; j--)
                {
                    ((Excel.Worksheet)xlsWorkBook.Worksheets[j]).Delete();
                }

                //adding sheets & filling data

                string ExportFilePath = filePaths[i];

                if (Path.GetExtension(filePaths[i]).ToLower() == ".xlsx")
                    is_xlsx = true;
                else
                    is_xlsx = false;


                if (i == 0)
                    xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets[1];
                else
                    xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets.Add();

                xlsWorkSheet.Name = PageNames != null ? string.IsNullOrWhiteSpace(PageNames[i]) ? ("Sheet1") : PageNames[i] : ("Sheet" + (i + 1).ToString());


                try
                {
                    FillWookSheet(ref xlsWorkSheet, Query[i], constr);

                    //Make excel window visible
                    if (WookbookVisibility)
                    {
                        xlsWorkSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                        xlsApp.Visible = true;
                    }

                    try
                    {
                        if (OverwriteFile)
                            if (System.IO.File.Exists(@ExportFilePath))
                                System.IO.File.Delete(@ExportFilePath);

                        // Save the excel file
                        object misValue = System.Reflection.Missing.Value;
                        if (Path.GetExtension(@ExportFilePath).ToLower() == ".xlsx")
                            xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        else
                            xlsWorkBook.SaveAs(@ExportFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    xlsWorkBook.Close();
                    if (xlsWorkSheet != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                        xlsWorkSheet = null;
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
                    xlsWorkBook = null;
                    xlsApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
                    xlsApp = null;
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkSheet);
                    xlsWorkSheet = null;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);
                    xlsWorkBook = null;
                    xlsApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
                    xlsApp = null;
                    GC.Collect();
                    throw ex;
                }

            }




            
        }
#endregion

#region "Import Excel"
        public string Fn_TextToColumn(string filePath)
        {
            //Note: While Using This 1 Row of the excel file will be considered as header of the columns

            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }

            if (File.Exists(@filePath))
            {
                string tempAddress = (Path.GetTempPath() + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + "_" + DateTime.Now.ToString("ddMMyyyyHmmss") + "\\" + Path.GetFileName(@filePath));
                if ((Directory.Exists(Path.GetDirectoryName(tempAddress)))) Directory.Delete(Path.GetDirectoryName(tempAddress),true);
                if (!(Directory.Exists(Path.GetDirectoryName(tempAddress)))) Directory.CreateDirectory(Path.GetDirectoryName(tempAddress));
                File.Copy(@filePath, tempAddress, true);
                filePath = tempAddress;
            }
            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);

            string ExcelSheetname;
            int pageCount = xlBook.Worksheets.Count;

            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
            //OleDbConnection con2 = new OleDbConnection(connString);

            //DataSet ds1 = new DataSet();

            for (int i = 1; i <= pageCount; i++)
            {
                xlSheet = (Excel.Worksheet)xlBook.Worksheets[i];
                if (!IsWorkSheetEmpty(xlSheet))
                {
                    int colCnt = xlSheet.UsedRange.Columns.Count;
                    for (int q = 1; q <= colCnt; q++)
                    {
                        //xlSheet.Cells[1, q].EntireColumn = xlSheet.Cells[1, q].EntireColumn.TextToColumns(xlSheet.Cells[1, q],
                        //                                   Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited,
                        //                                   Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone,
                        //                                   Type.Missing, true, Type.Missing, true, Type.Missing,
                        //                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        //                                   Type.Missing);
                        ((Excel.Range)xlSheet.Cells[1, q]).EntireColumn.TextToColumns(Type.Missing,
                                                           Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited,
                                                           Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone,
                                                           false, false, false, false, false, false);
                    }

                }
            }
            xlBook.Save();

            
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            return filePath;
        }

        public string RemovePassword(string filePath)
        {
            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }
            string tempAddress = "";
            if (File.Exists(@filePath))
            {
                tempAddress = (Path.GetTempPath() + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + "_" + DateTime.Now.ToString("ddMMyyyyHmmss") + "\\" + Path.GetFileName(@filePath));
                if ((Directory.Exists(Path.GetDirectoryName(tempAddress)))) Directory.Delete(Path.GetDirectoryName(tempAddress), true);
                if (!(Directory.Exists(Path.GetDirectoryName(tempAddress)))) Directory.CreateDirectory(Path.GetDirectoryName(tempAddress));
            }
            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);

            object misValue = System.Reflection.Missing.Value;
            if (Path.GetExtension(tempAddress).ToLower() == ".xlsx")
                xlBook.SaveAs(tempAddress, Excel.XlFileFormat.xlWorkbookDefault, "", "", misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            else
                xlBook.SaveAs(tempAddress, Excel.XlFileFormat.xlWorkbookNormal, "", "", misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);


            xlBook.Close(true);
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            return tempAddress;
        }

        public DataSet Fn_Import_Excel(string filePath, bool Text_To_Column = false)
        {
            //Note: While Using This 1 Row of the excel file will be considered as header of the columns

            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }
            if (Text_To_Column)
            {
                filePath = @Fn_TextToColumn(filePath);
            }
            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);
            
            string ExcelSheetname;
            int pageCount = xlBook.Worksheets.Count;

            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
            OleDbConnection con2 = new OleDbConnection(connString);
            
            DataSet ds1 = new DataSet();
            for (int i = 1; i <= pageCount; i++)
            {
                xlSheet = (Excel.Worksheet)xlBook.Worksheets[i];
                if (!IsWorkSheetEmpty(xlSheet))
                {
                    ExcelSheetname = xlSheet.Name;
                    OleDbCommand cmd1 = new OleDbCommand("SELECT * FROM [" + ExcelSheetname + "$] ", con2);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                    da1.Fill(ds1, ExcelSheetname);
                }
            }
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            return ds1;
        }

        public DataSet Fn_Import_One_Sheet_Excel(string filePath, int SheetNumber, bool Text_To_Column = false)
        {
            //Note: While Using This 1 Row of the excel file will be considered as header of the columns

            DataSet ds1 = new DataSet();

            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }

            if (Text_To_Column)
            {
                filePath = @Fn_TextToColumn(filePath);
            }

            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);

            string ExcelSheetname;
            int pageCount = xlBook.Worksheets.Count;

            if (SheetNumber <= pageCount && SheetNumber > 0)
            {
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
                OleDbConnection con2 = new OleDbConnection(connString);

                xlSheet = (Excel.Worksheet)xlBook.Worksheets[SheetNumber];
                if (!IsWorkSheetEmpty(xlSheet))
                {
                    ExcelSheetname = xlSheet.Name;
                    OleDbCommand cmd1 = new OleDbCommand("SELECT * FROM [" + ExcelSheetname + "$] ", con2);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                    da1.Fill(ds1, ExcelSheetname);
                }
            }
            else
                throw new Exception("Invalid Page Number");

            //release Objects
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            return ds1;
        }

        // Read Mixed data type values
        public DataSet Fn_Import_Excel_NO_HDR(string filePath, int SheetNumber)
        {
            //Note: While Using This 1 Row of the excel file will be considered as header of the columns

            DataSet ds1 = new DataSet();
            bool isProtected = false;
            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }

            //KillApp("Excel");
            
            if (MsOfficeHelper.IsPasswordProtected(filePath))
            {
                filePath = RemovePassword(filePath);
                if (filePath.StartsWith(Path.GetTempPath()))
                    isProtected = true;
            }


            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\"";
            OleDbConnection con2 = new OleDbConnection(connString);
            con2.Open();
            DataTable sheets = con2.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            if (sheets.Rows.Count > 0)
            {
                for (int pageNum = 0; pageNum < sheets.Rows.Count; pageNum++)
                {
                    string ExcelSheetname = sheets.Rows[pageNum]["TABLE_NAME"].ToString();
                    if (ExcelSheetname.Contains("$"))
                    {
                        ExcelSheetname = ExcelSheetname.Substring(0, Math.Max(0, (ExcelSheetname.Length - 1)));
                        OleDbCommand cmd1 = new OleDbCommand("SELECT * FROM [" + ExcelSheetname + "$] ", con2);

                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                        da1.Fill(ds1, ExcelSheetname);
                        int ColCount = ds1.Tables[ExcelSheetname].Columns.Count;
                        if (ds1.Tables[ExcelSheetname].Rows.Count > 0 && ColCount > 0)
                        {
                            for (int col = 0; col < ColCount; col++)
                            {
                                if (!string.IsNullOrWhiteSpace(ds1.Tables[ExcelSheetname].Rows[0][col].ToString()))
                                {
                                    ds1.Tables[ExcelSheetname].Columns[col].ColumnName = Regex.Replace(ds1.Tables[ExcelSheetname].Rows[0][col].ToString().Trim().ToUpper(), @"[^0-9a-zA-Z]+", "");
                                }
                            }
                            ds1.Tables[ExcelSheetname].Rows[0].Delete();
                            ds1.Tables[ExcelSheetname].AcceptChanges();
                        }
                    }
                }
            }
            con2.Close();

            try
            {
                if (isProtected) if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch
            { 
                
            }


            return ds1;
        }

        public DataTable Fn_Import_One_Sheet_DT(string filePath, int SheetNumber, bool Text_To_Column = false)
        {
            //Note: While Using This 1 Row of the excel file will be considered as header of the columns

            DataSet ds1 = new DataSet();

            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }

            if (Text_To_Column)
            {
                filePath = @Fn_TextToColumn(filePath);
            }

            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);

            string ExcelSheetname;
            int pageCount = xlBook.Worksheets.Count;
            
            if (SheetNumber <= pageCount && SheetNumber > 0)
            {
                //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + filePath + "; Extended Properties=Excel 12.0";
                OleDbConnection con2 = new OleDbConnection(connString);

                xlSheet = (Excel.Worksheet)xlBook.Worksheets[SheetNumber];
                if (!IsWorkSheetEmpty(xlSheet))
                {
                    ExcelSheetname = xlSheet.Name;
                    OleDbCommand cmd1 = new OleDbCommand("SELECT * FROM [" + ExcelSheetname + "$] ", con2);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                    da1.Fill(ds1, ExcelSheetname);
                }
            }
            else
                throw new Exception("Invalid Page Number");

            //release Objects
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            DataTable dt1 = ds1.Tables[0];
            return dt1;
        }


        // Reading data Manually.
        public DataSet Fn_Import_Excel_Manually(string filePath)
        {
            if (!File.Exists(@filePath))
                throw new Exception("File not found");
            else
            {
                if (FileInUse(@filePath))
                    throw new Exception("File is being used by other process");
            }
            
            //KillApp("Excel");
            Excel.Application xlApp;
            Excel.Workbook xlBook;
            Excel.Worksheet xlSheet = new Excel.Worksheet();

            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Open(filePath);

            string ExcelSheetname;
            int pageCount = xlBook.Worksheets.Count;


            
            DataTable dt1 = new DataTable();
            for (int i = 1; i <= pageCount; i++)
            {
                xlSheet = (Excel.Worksheet)xlBook.Worksheets[i];
                if (!IsWorkSheetEmpty(xlSheet))
                {
                    ExcelSheetname = xlSheet.Name;
                    Excel.Range used = xlSheet.UsedRange;
                    int rowCnt = used.Rows.Count, colCnt = used.Columns.Count;

                    if (rowCnt > 0)
                    {
                        //------------- Setting Header Names--------------------
                        for (int num = 1; num < (colCnt + 1); num++)
                        {
                            if (Convert.ToString(((Excel.Range)used.Cells[1, num]).Value).Length > 0)
                                dt1.Columns.Add(Convert.ToString(((Excel.Range)used.Cells[1, num]).Value), typeof(string));
                            else
                                dt1.Columns.Add(("F" + num.ToString()), typeof(string));
                        }


                        //------------- Setting Values--------------------
                        if (rowCnt > 1)
                        {
                            for (int Rnum = 2; Rnum < (rowCnt + 1); Rnum++)
                            {
                                DataRow row = dt1.Rows.Add();
                                for (int Cnum = 1; Cnum < (colCnt + 1); Cnum++)
                                {
                                    row[(Cnum - 1)] = Convert.ToString(((Excel.Range)used.Cells[Rnum, Cnum]).Value);
                                }
                            }
                        }
                    }
                }
            }

            xlBook.Close(false);
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            xlBook = null;
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlApp = null;
            GC.Collect();
            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dt1);
            return ds1;
        }
#endregion


#region "Conversion DataTable To RecordSet"

        static public ADODB.Recordset ConvertToRecordset(DataTable inTable)
        {
            ADODB.Recordset result = new ADODB.Recordset();
            result.CursorLocation = ADODB.CursorLocationEnum.adUseClient;

            ADODB.Fields resultFields = result.Fields;
            System.Data.DataColumnCollection inColumns = inTable.Columns;

            foreach (DataColumn inColumn in inColumns)
            {
                resultFields.Append(inColumn.ColumnName
                    , TranslateType(inColumn.DataType)
                    , inColumn.MaxLength
                    , inColumn.AllowDBNull ? ADODB.FieldAttributeEnum.adFldIsNullable :
                                             ADODB.FieldAttributeEnum.adFldUnspecified
                    , null);
            }

            result.Open(System.Reflection.Missing.Value
                    , System.Reflection.Missing.Value
                    , ADODB.CursorTypeEnum.adOpenStatic
                    , ADODB.LockTypeEnum.adLockOptimistic, 0);

            foreach (DataRow dr in inTable.Rows)
            {
                result.AddNew(System.Reflection.Missing.Value,
                              System.Reflection.Missing.Value);

                for (int columnIndex = 0; columnIndex < inColumns.Count; columnIndex++)
                {
                    resultFields[columnIndex].Value = dr[columnIndex];
                }
            }

            return result;
        }

        static ADODB.DataTypeEnum TranslateType(Type columnType)
        {
            switch (columnType.UnderlyingSystemType.ToString())
            {
                case "System.Boolean":
                    return ADODB.DataTypeEnum.adBoolean;

                case "System.Byte":
                    return ADODB.DataTypeEnum.adUnsignedTinyInt;

                case "System.Char":
                    return ADODB.DataTypeEnum.adChar;

                case "System.DateTime":
                    return ADODB.DataTypeEnum.adDate;

                case "System.Decimal":
                    return ADODB.DataTypeEnum.adCurrency;

                case "System.Double":
                    return ADODB.DataTypeEnum.adDouble;

                case "System.Int16":
                    return ADODB.DataTypeEnum.adSmallInt;

                case "System.Int32":
                    return ADODB.DataTypeEnum.adInteger;

                case "System.Int64":
                    return ADODB.DataTypeEnum.adBigInt;

                case "System.SByte":
                    return ADODB.DataTypeEnum.adTinyInt;

                case "System.Single":
                    return ADODB.DataTypeEnum.adSingle;

                case "System.UInt16":
                    return ADODB.DataTypeEnum.adUnsignedSmallInt;

                case "System.UInt32":
                    return ADODB.DataTypeEnum.adUnsignedInt;

                case "System.UInt64":
                    return ADODB.DataTypeEnum.adUnsignedBigInt;

                case "System.String":
                default:
                    return ADODB.DataTypeEnum.adVarChar;
            }
        }

#endregion
    }

    public static class MsOfficeHelper
    {
        /// <summary>
        /// Detects if a given office document is protected by a password or not.
        /// Supported formats: Word, Excel and PowerPoint (both legacy and OpenXml).
        /// </summary>
        /// <param name="fileName">Path to an office document.</param>
        /// <returns>True if document is protected by a password, false otherwise.</returns>
        public static bool IsPasswordProtected(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
                return IsPasswordProtected(stream);
        }

        /// <summary>
        /// Detects if a given office document is protected by a password or not.
        /// Supported formats: Word, Excel and PowerPoint (both legacy and OpenXml).
        /// </summary>
        /// <param name="stream">Office document stream.</param>
        /// <returns>True if document is protected by a password, false otherwise.</returns>
        public static bool IsPasswordProtected(Stream stream)
        {
            // minimum file size for office file is 4k
            if (stream.Length < 4096)
                return false;

            // read file header
            stream.Seek(0, SeekOrigin.Begin);
            var compObjHeader = new byte[0x20];
            ReadFromStream(stream, compObjHeader);

            // check if we have plain zip file
            if (compObjHeader[0] == 'P' && compObjHeader[1] == 'K')
            {
                // this is a plain OpenXml document (not encrypted)
                return false;
            }

            // check compound object magic bytes
            if (compObjHeader[0] != 0xD0 || compObjHeader[1] != 0xCF)
            {
                // unknown document format
                return false;
            }

            int sectionSizePower = compObjHeader[0x1E];
            if (sectionSizePower < 8 || sectionSizePower > 16)
            {
                // invalid section size
                return false;
            }
            int sectionSize = 2 << (sectionSizePower - 1);

            const int defaultScanLength = 32768;
            long scanLength = Math.Min(defaultScanLength, stream.Length);

            // read header part for scan
            stream.Seek(0, SeekOrigin.Begin);
            var header = new byte[scanLength];
            ReadFromStream(stream, header);

            // check if we detected password protection
            if (ScanForPassword(stream, header, sectionSize))
                return true;

            // if not, try to scan footer as well

            // read footer part for scan
            stream.Seek(-scanLength, SeekOrigin.End);
            var footer = new byte[scanLength];
            ReadFromStream(stream, footer);

            // finally return the result
            return ScanForPassword(stream, footer, sectionSize);
        }

        static void ReadFromStream(Stream stream, byte[] buffer)
        {
            int bytesRead, count = buffer.Length;
            while (count > 0 && (bytesRead = stream.Read(buffer, 0, count)) > 0)
                count -= bytesRead;
            if (count > 0) throw new EndOfStreamException();
        }

        static bool ScanForPassword(Stream stream, byte[] buffer, int sectionSize)
        {
            const string afterNamePadding = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            const string encryptedPackageName = "E\0n\0c\0r\0y\0p\0t\0e\0d\0P\0a\0c\0k\0a\0g\0e" + afterNamePadding;
            const string encryptedSummaryName = "E\0n\0c\0r\0y\0p\0t\0e\0d\0S\0u\0m\0m\0a\0r\0y" + afterNamePadding;
            const string wordDocumentName = "W\0o\0r\0d\0D\0o\0c\0u\0m\0e\0n\0t" + afterNamePadding;
            const string workbookName = "W\0o\0r\0k\0b\0o\0o\0k" + afterNamePadding;

            try
            {
                var bufferString = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

                // try to detect password protection used in new OpenXml documents
                // by searching for "EncryptedPackage" or "EncryptedSummary" streams
                // (old .ppt documents use this stream as well)
                if (bufferString.Contains(encryptedPackageName) ||
                    bufferString.Contains(encryptedSummaryName))
                    return true;

                // try to detect password protection for legacy Office documents

                // check for Word header
                int headerOffset = bufferString.IndexOf(wordDocumentName, StringComparison.InvariantCulture);
                int sectionId;
                const int coBaseOffset = 0x200;
                const int sectionIdOffset = 0x74;
                if (headerOffset >= 0)
                {
                    sectionId = BitConverter.ToInt32(buffer, headerOffset + sectionIdOffset);
                    int sectionOffset = coBaseOffset + sectionId * sectionSize;
                    const int fibScanSize = 0x10;
                    if (sectionOffset + fibScanSize > stream.Length)
                        return false; // invalid document
                    var fibHeader = new byte[fibScanSize];
                    stream.Seek(sectionOffset, SeekOrigin.Begin);
                    ReadFromStream(stream, fibHeader);
                    short properties = BitConverter.ToInt16(fibHeader, 0x0A);
                    // check for fEncrypted FIB bit
                    const short fEncryptedBit = 0x0100;
                    return (properties & fEncryptedBit) == fEncryptedBit;
                }

                // check for Excel header
                headerOffset = bufferString.IndexOf(workbookName, StringComparison.InvariantCulture);
                if (headerOffset >= 0)
                {
                    sectionId = BitConverter.ToInt32(buffer, headerOffset + sectionIdOffset);
                    int sectionOffset = coBaseOffset + sectionId * sectionSize;
                    const int streamScanSize = 0x100;
                    if (sectionOffset + streamScanSize > stream.Length)
                        return false; // invalid document
                    var workbookStream = new byte[streamScanSize];
                    stream.Seek(sectionOffset, SeekOrigin.Begin);
                    ReadFromStream(stream, workbookStream);
                    short record = BitConverter.ToInt16(workbookStream, 0);
                    short recordSize = BitConverter.ToInt16(workbookStream, sizeof(short));
                    const short bofMagic = 0x0809;
                    const short eofMagic = 0x000A;
                    const short filePassMagic = 0x002F;
                    if (record != bofMagic)
                        return false; // invalid BOF
                    // scan for FILEPASS record until the end of the buffer
                    int offset = sizeof(short) * 2 + recordSize;
                    int recordsLeft = 16; // simple infinite loop check just in case
                    do
                    {
                        record = BitConverter.ToInt16(workbookStream, offset);
                        if (record == filePassMagic)
                            return true;
                        recordSize = BitConverter.ToInt16(workbookStream, sizeof(short) + offset);
                        offset += sizeof(short) * 2 + recordSize;
                        recordsLeft--;
                    } while (record != eofMagic && recordsLeft > 0);
                }
            }
            catch (Exception ex)
            {
                // BitConverter exceptions may be related to document format problems
                // so we just treat them as "password not detected" result
                if (ex is ArgumentException)
                    return false;
                // respect all the rest exceptions
                throw;
            }

            return false;
        }
    }
}
