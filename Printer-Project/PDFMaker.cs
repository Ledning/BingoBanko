using System;
using System.Collections.Generic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Resources;
using System.Drawing;

namespace Printer_Project
{
  public class PDFMaker
  {
    public static void MakePDF(List<int[,]> cards)
    {
      string imgName = @"bingo1.png";
      string imgSource = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\", @"Resources\", imgName);         
      string saveLoc = imgSource.Replace("png", "pdf");
      PdfDocument doc = new PdfDocument();

      
      XImage img = XImage.FromFile(imgSource);
      XFont font = new XFont("Verdana", 125, XFontStyle.BoldItalic);
      XImage pdfSize = XImage.FromFile(imgSource); 
       
      #region PixelStuff
      int startPlateY = /*Convert.ToInt32(pdfPage.Height) - */834;
      int startX = 356;
      int jumpX = 222;
      int jumpY = 240;
      int jumpYPlate = 796;
      #endregion 

      
      for (int i = 0; i < cards.Count; i++)
	  {
        if (cards[i][0,0] != -1)
        {
          PdfPage page = new PdfPage(); //Template for page
          
          //XSize size = PageSizeConverter.ToSize(PdfSharp.PageSize.A4);
          page.Width = pdfSize.PixelWidth;
          page.Height = pdfSize.PixelHeight;
          page.TrimMargins.Top = 0;
          page.TrimMargins.Right = 0;
          page.TrimMargins.Bottom = 0;
          page.TrimMargins.Left = -30;



          doc.Pages.Add(page);
          XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[doc.PageCount-1]);
          

          xgr.DrawImage(img, 0, 0, page.Width, page.Height);
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
                if (cards[i][column, row] != 0)
                {
                  if (cards[i][column, row] < 10)
                  {
                    xgr.DrawString(" " + Convert.ToString(cards[i][column, row]), font, XBrushes.Black, new XRect(columnChooser, rowChooser, 10, 0), XStringFormats.TopLeft);
                  }
                  else
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
      }

      doc.Save(saveLoc);
      doc.Close();
    }    
  }
}
