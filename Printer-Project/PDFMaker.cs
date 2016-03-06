using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Project
{
  class PDFMaker
  {
    void MakePDF(List<int[,]> cards)
    {
      string imgSource = "";
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
      int startPlateY = 833;
      int startX = 361;
      int jumpX = 582-361;
      int jumpY = 1073 - 833;
      int jumpYPlate = 1631-833;
      #endregion


      for (int i = 0; i < cards.Count; i++)
			{
        doc.Pages.Add(pdfPage);
        XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[i]);
        XImage img = XImage.FromFile(imgSource);
        xgr.DrawImage(img, 0, 0);
        int row = 0;
        int column = 0;
        int plateChooser = startPlateY;
        int rowChooser = plateChooser;
        int columnChooser = startX;

        for (int j = 0; j < 3; j++) //Pladenummer. Styrer y-akses startpunkt
        {
          plateChooser += jumpYPlate * j; // hopper en plade per iteration

          for (int k = 0; k < 3; k++) //rækkenummer. Placerer y-akseværdi
          {
            rowChooser += jumpY * k; //Hopper en række per iteration

            for (int l = 0; l < 9; l++)
            {
              columnChooser += jumpX * l; //Hopper med en kolonne per iteration

              xgr.DrawString(Convert.ToString(cards[i][column, row]), font, XBrushes.Black, l, k);
            }
          }
        }        
      }
    }    
  }
}
