using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Project
{
  public class PDFMaker
  {
    public static void MakePDF(List<int[,]> cards)
    {
      string imgSource = @"C:/Users/Mini-Gis/Desktop/plade.png";
      string loc = imgSource.Replace("png", "pdf");
      PdfDocument doc = new PdfDocument();
      PdfPage pdfPage = new PdfPage(); //Template for page

      XSize size = PageSizeConverter.ToSize(PdfSharp.PageSize.A4);
      pdfPage.Width = size.Width;
      pdfPage.Height = size.Height;
      pdfPage.TrimMargins.Top = 0;
      pdfPage.TrimMargins.Right = 0;
      pdfPage.TrimMargins.Bottom = 0;
      pdfPage.TrimMargins.Left = 0;

      XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
        
      #region PixelStuff
      int startPlateY = Convert.ToInt32(pdfPage.Height) - 280;
      int startX = 118;
      int jumpX = 191-118;
      int jumpY = -(357 - 280);
      int jumpYPlate = -(545-280);
      #endregion 


      for (int i = 0; i < cards.Count; i++)
			{
        doc.Pages.Add(pdfPage);
        XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[i]);
        XTextFormatter tf = new XTextFormatter(xgr);
        XImage img = XImage.FromFile(imgSource);
        
        xgr.DrawImage(img, 0, 0);
        int plateChooser = startPlateY;
        int rowChooser = plateChooser;
        int columnChooser = startX;

        for (int j = 0; j < 3; j++) //Pladenummer. Styrer y-akses startpunkt
        {
          rowChooser = plateChooser;
          for (int row = 0; row < 3; row++) //rækkenummer. Placerer y-akseværdi
          {
            
            for (int column = 0; column < 9; column++)
            {
              if (cards[i][column,row] != 0)
              {
                xgr.DrawString(Convert.ToString(cards[i][column, row]), font, XBrushes.Black, new XRect(columnChooser, rowChooser, 10, 0), XStringFormats.TopLeft);
              }

              columnChooser += jumpX; //Hopper med en kolonne per iteration          
            }
            columnChooser = startX;
            rowChooser += jumpY; //Hopper en række per iteration
          }

          plateChooser += jumpYPlate; // hopper en plade per iteration
        }        
      }

      doc.Save(loc);
      doc.Close();
    }    
  }
}
