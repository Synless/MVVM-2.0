using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTR_01.Model
{
    /// <summary>
    /// Interaction logic for XLApp.xaml
    /// </summary>
    public partial class XLApp
    {
        private string LastMessage;
        private bool ExcelOpen;
        private Excel.Application exApp;
        private Excel.Workbook wb;
        private Excel.Worksheet exWs;
        public List<string> status;

        public XLApp()
        {
            status = new List<string>();
            LastMessage = "";
            ExcelOpen = false;
        }


        //----------------------------------------------------------//
        //	Name : OpenXL											//
        //	Description : Lance l'application Excel					//
        //	Arguments :	visible : affiche l'application				//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public void OpenXL()
        {
            OpenXL(false);
        }

        public void OpenXL(bool visible)
        {
            exApp = new Excel.Application();
            SetVisible(visible);
        }

        //----------------------------------------------------------//
        //	Name : SetVisible										//
        //	Description : affiche excel								//
        //	Arguments :	visible : affiche l'application				//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public void SetVisible(bool visible)
        {
            exApp.Visible = visible;
        }
        //----------------------------------------------------------//
        //	Name : OpenBook											//
        //	Description : Ouvre un fichier excel					//
        //	Arguments :	filename : lien vers le fichier				//
        //				ReadOnly : lecture seule					//
        //				Password : mot de passe du fichier			//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool OpenBook(string fileName)
        {
            return OpenBook(fileName, true);

        }

        public bool OpenBook(string fileName, bool ReadOnly)
        {
            try
            {
                wb = exApp.Workbooks.Open(fileName, Type.Missing, ReadOnly, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                exApp.EditDirectlyInCell = false;
                return true;
            }
            catch
            {
                LastMessage = "Error on openning :" + fileName;
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : CloseBook										//
        //	Description : Ferme fichier Excel						//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool CloseBook()
        {
            try
            {
                exApp.ActiveWorkbook.Close(false, "", false);
                return true;
            }
            catch
            {
                LastMessage = "Error on closing :" + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        public bool CloseBook(bool bSave)
        {
            try
            {
                exApp.DisplayAlerts = false;
                exApp.ActiveWorkbook.Close(bSave, "", Type.Missing);
                return true;
            }
            catch
            {
                LastMessage = "Error on closing :" + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : CloseXL											//
        //	Description : Ferme l'application Excel					//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public void CloseXL()
        {
            try
            {
                exApp.Quit();
            }
            catch
            {
                LastMessage = "Error on closing Excel";
                status.Add(LastMessage);
            }
        }

        //----------------------------------------------------------//
        //	Name : ReadString										//
        //	Description : Lit un texte								//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool ReadString(ref string strValue, int iRow, int iCol)
        {
            try
            {
                strValue = Convert.ToString(((Excel.Range)exWs.Cells[(System.Object)iRow, (System.Object)iCol]).Value2);
                return true;
            }
            catch
            {
                //LastMessage="Error on reading cell ("+iRow+";"+iCol+") from book "+exApp.ActiveWorkbook.Name;
                //status.Add(LastMessage+"\n");
                return false;
            }
        }

        public bool ReadString(ref string strValue, int iRow, string strCol)
        {
            int col = 0;
            strCol = strCol.ToUpper();
            col = 26 * (strCol.Length - 1);
            col += strCol[strCol.Length - 1] - 64;
            return ReadString(ref strValue, iRow, col);
        }

        public bool ReadString(ref string strValue, string name)
        {
            try
            {
                strValue = Convert.ToString(((Excel.Range)exWs.Evaluate(name)).Value2);
                return true;
            }
            catch
            {
                LastMessage = "Error on reading cell (" + name + ") from book " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        public bool ReadString(ref string value, string name, int offsetRow)
        {
            return ReadString(ref value, name, offsetRow, 0);
        }

        public bool ReadString(ref string value, string name, int offsetRow, int offsetCol)
        {
            try
            {
                int col = ((Excel.Range)exWs.Evaluate(name)).Column;
                int row = ((Excel.Range)exWs.Evaluate(name)).Row;
                col += offsetCol;
                row += offsetRow;
                return ReadString(ref value, row, col);
            }
            catch
            {
                LastMessage = "Error on reading cell (" + name + ";" + offsetRow + ";" + offsetCol + ") from book " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }        

        //----------------------------------------------------------//
        //	Name : ReadFloat										//
        //	Description : Lit un nombre								//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool ReadFloat(ref double strValue, int iRow, int iCol)
        {
            try
            {
                strValue = (double)((Excel.Range)exWs.Cells[(System.Object)iRow, (System.Object)iCol]).Value2;
                return true;
            }
            catch
            {
                LastMessage = "Error on reading cell (" + iRow + ";" + iCol + ") from book " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        public bool ReadFloat(ref double strValue, int iRow, string strCol)
        {
            int col = 0;
            strCol = strCol.ToUpper();
            col = 26 * (strCol.Length - 1);
            col += strCol[strCol.Length - 1] - 64;
            return ReadFloat(ref strValue, iRow, col);
        }

        public bool ReadFloat(ref double strValue, string name)
        {
            try
            {
                strValue = (double)((Excel.Range)exWs.Evaluate(name)).Value2;
                return true;
            }
            catch
            {
                LastMessage = "Error on reading cell (" + name + ") from book " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        public bool ReadFloat(ref double value, string name, int offsetRow)
        {
            return ReadFloat(ref value, name, offsetRow, 0);
        }

        public bool ReadFloat(ref double value, string name, int offsetRow, int offsetCol)
        {
            try
            {
                int col = ((Excel.Range)exWs.Evaluate(name)).Column;
                int row = ((Excel.Range)exWs.Evaluate(name)).Row;
                col += offsetCol;
                row += offsetRow;
                return ReadFloat(ref value, row, col);
            }
            catch
            {
                LastMessage = "Error on reading cell (" + name + ";" + offsetRow + ";" + offsetCol + ") from book " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : Save												//
        //	Description : Sauvegarde avec un nouveau nom			//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public void Save()
        {
            try
            {
                exApp.EditDirectlyInCell = true;
                wb.Save();
                exApp.EditDirectlyInCell = false;
            }
            catch
            {
                LastMessage = "Error on saving : " + exApp.ActiveWorkbook.Name;
                status.Add(LastMessage);
            }
        }

        public void Save(ref string strFileName)
        {
            try
            {
                exApp.EditDirectlyInCell = true;
                wb.SaveAs(strFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                exApp.EditDirectlyInCell = false;
            }
            catch
            {
                LastMessage = "Error on saving " + exApp.ActiveWorkbook.Name + " as " + strFileName;
                status.Add(LastMessage);
            }
        }

        //----------------------------------------------------------//
        //	Name : SelectBook										//
        //	Description : Changer de fichier						//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool SelectBook(string book)
        {
            try
            {
                Excel.Workbook tmp;
                for (int i = 1; i <= exApp.Workbooks.Count; i++)
                {
                    tmp = exApp.Workbooks[i];
                    if (tmp.Name == book)
                    {
                        tmp.Activate();
                        wb = (Excel.Workbook)(exApp.ActiveWorkbook);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                LastMessage = "Error on selecting book " + book;
                status.Add(LastMessage);
                return false;
            }
        }
        //----------------------------------------------------------//
        //	Name : SelectSheet										//
        //	Description : Changer de feuille						//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool SelectSheet(string sheet)
        {
            try
            {
                Excel.Worksheet tmp;
                for (int i = 1; i <= exApp.Worksheets.Count; i++)
                {
                    ((Excel.Worksheet)(exApp.ActiveWorkbook.Sheets[i])).Select(Type.Missing);
                    tmp = ((Excel.Worksheet)(exApp.ActiveSheet));
                    if (tmp.Name == sheet)
                    {
                        exWs = ((Excel.Worksheet)(exApp.ActiveSheet));
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                LastMessage = "Error on selecting sheet " + sheet;
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : SelectPartSheet									//
        //	Description : Changer de feuille avec nom partiel		//
        //	Arguments :												//
        //	Rédacteur : Hickel Matthieu 							//
        //----------------------------------------------------------//
        public bool SelectPartSheet(string sheet)
        {
            try
            {
                Excel.Worksheet tmp;
                for (int i = 1; i <= exApp.Worksheets.Count; i++)
                {
                    ((Excel.Worksheet)(exApp.ActiveWorkbook.Sheets[i])).Select(Type.Missing);
                    tmp = ((Excel.Worksheet)(exApp.ActiveSheet));
                    if (tmp.Name.Contains(sheet))
                    {
                        exWs = ((Excel.Worksheet)(exApp.ActiveSheet));
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                LastMessage = "Error on selecting sheet " + sheet;
                status.Add(LastMessage);
                return false;
            }
        }
        //----------------------------------------------------------//
        //	Name : Calculation										//
        //	Description : Active/Desactive le calcul automatique	//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool Calculation(bool activate)
        {
            try
            {
                if (activate)
                {
                    exApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                }
                else
                {
                    exApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                }
                return true;
            }
            catch
            {
                LastMessage = "Error on activating or desactivating the auto-calculation";
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : Calculate										//
        //	Description : Lance le calcul des feuilles				//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool Calculate()
        {
            try
            {
                exApp.CalculateFull();
                return true;
            }
            catch
            {
                LastMessage = "Error on calculation";
                status.Add(LastMessage);
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : WriteString										//
        //	Description : Ecrit un texte							//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool WriteString(string strValue, int iRow, int iCol)
        {
            try
            {
                ((Excel.Range)exWs.Cells[(System.Object)iRow, (System.Object)iCol]).Value2 = strValue;
                return true;
            }
            catch
            {
                LastMessage = "Error on writing " + strValue + " in cell (" + iRow + "," + iCol + ")";
                status.Add(LastMessage);
                return false;
            }
        }

        public bool WriteString(string strValue, int iRow, string strCol)
        {
            int col = 0;
            strCol = strCol.ToUpper();
            col = 26 * (strCol.Length - 1);
            col += strCol[strCol.Length - 1] - 64;
            return WriteString(strValue, iRow, col);
        }

        public bool WriteString(string strValue, string name)
        {
            try
            {
                ((Excel.Range)exWs.Evaluate(name)).Value2 = strValue;
                return true;
            }
            catch
            {
                LastMessage = "Error on writing " + strValue + " in cell (" + name + ")";
                status.Add(LastMessage);
                return false;
            }
        }

        public bool WriteString(string value, string name, int offsetRow)
        {
            return WriteString(value, name, offsetRow, 0);
        }

        public bool WriteString(string value, string name, int offsetRow, int offsetCol)
        {
            try
            {
                int col = ((Excel.Range)exWs.Evaluate(name)).Column;
                int row = ((Excel.Range)exWs.Evaluate(name)).Row;
                col += offsetCol;
                row += offsetRow;
                return WriteString(value, row, col);
            }
            catch
            {
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : WriteFloat										//
        //	Description : Ecrit un float							//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public bool WriteFloat(double strValue, int iRow, int iCol)
        {
            try
            {
                ((Excel.Range)exWs.Cells[(System.Object)iRow, (System.Object)iCol]).Value2 = strValue;
                return true;
            }
            catch
            {
                LastMessage = "Error on writing " + strValue + " in cell (" + iRow + "," + iCol + ")";
                status.Add(LastMessage);
                return false;
            }
        }

        public bool WriteFloat(double strValue, int iRow, string strCol)
        {
            int col = 0;
            strCol = strCol.ToUpper();
            col = 26 * (strCol.Length - 1);
            col += strCol[strCol.Length - 1] - 64;
            return WriteFloat(strValue, iRow, col);
        }

        public bool WriteFloat(double value, string name)
        {
            try
            {
                ((Excel.Range)exWs.Evaluate(name)).Value2 = value;
                return true;
            }
            catch
            {
                LastMessage = "Error on writing " + value + " in cell (" + name + ")";
                status.Add(LastMessage);
                return false;
            }
        }

        public bool WriteFloat(double value, string name, int offsetRow)
        {
            return WriteFloat(value, name, offsetRow, 0);
        }

        public bool WriteFloat(double value, string name, int offsetRow, int offsetCol)
        {
            try
            {
                int col = ((Excel.Range)exWs.Evaluate(name)).Column;
                int row = ((Excel.Range)exWs.Evaluate(name)).Row;
                col += offsetCol;
                row += offsetRow;
                return WriteFloat(value, row, col);
            }
            catch
            {
                return false;
            }
        }

        //----------------------------------------------------------//
        //	Name : Print											//
        //	Description : Impression du fichier excel				//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        void Print(string printer)
        {
            exWs.PrintOut(Type.Missing, Type.Missing, Type.Missing, false, printer, false, false, Type.Missing);
        }

        //----------------------------------------------------------//
        //	Name : Macro											//
        //	Description : Execution au macro						//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        public void Macro(string strName)
        {
            exApp.Run(strName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }

        //----------------------------------------------------------//
        //	Name : GetLastMessage									//
        //	Description : Recupere les messages d'erreurs			//
        //	Arguments :												//
        //	Rédacteur : Scherrer Ludovic							//
        //----------------------------------------------------------//
        string GetLastMessage()
        {
            return LastMessage;
        }

        //----------------------------------------------------------//
        //	Name : DeleteRow    									//
        //	Description : Supprime une ligne            			//
        //	Arguments :												//
        //	Rédacteur : Hickel Matthieu 							//
        //----------------------------------------------------------//
        public void DeleteRow(int iRow)
        {
            Excel.Range ran;
            ran = (Excel.Range)this.exApp.Rows[iRow];
            ran.Select();
            ran.Delete(Excel.XlDirection.xlUp);
        }
        //----------------------------------------------------------//
        //	Name : ReadImage    									//
        //	Description : Recupere une image            			//
        //	Arguments :												//
        //	Rédacteur : Hickel Matthieu 							//
        //----------------------------------------------------------//

        /*public Image ReadImage(int iImage)
        {
            try
            {
                Excel.Picture pic = wb.ActiveSheet.Pictures(iImage);
                pic.Copy();
                IDataObject data = Clipboard.GetDataObject();
                Image pic2 = (Image)data.GetData(DataFormats.Bitmap, true);
                return pic2;
            }
            catch { return null; }
        }
        */

    }

}
