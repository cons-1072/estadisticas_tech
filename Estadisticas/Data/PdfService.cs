using Estadisticas.Model;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.IO;

namespace Estadisticas.Data
{
    public class PdfService
    {
        public MemoryStream CreatePdf(List<AmbListClass> dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("Forecast cannot be null");
            }
            //Create a new PDF document
            using (PdfDocument pdfDocument = new PdfDocument())
            {

                int paragraphAfterSpacing = 8;
                int cellMargin = 8;

                //Add page to the PDF document
                PdfPage page = pdfDocument.Pages.Add();

                //Create a new font
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                //Create a text element to draw a text in PDF page
                PdfTextElement title = new PdfTextElement("Ambulatorio", font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));


                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement content = new PdfTextElement("Estadisticas Ambulatorio.", contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                //Create a PdfGrid
                PdfGrid pdfGrid = new PdfGrid();
                pdfGrid.Style.CellPadding.Left = cellMargin;
                pdfGrid.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                //Assign data source
                pdfGrid.DataSource = dataTable;

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                int pageCount = pdfDocument.Pages.Count;
                for (int x = 0; x > pageCount; x++)
                {
                    PdfStandardFont font_water_mark = new PdfStandardFont(PdfFontFamily.TimesRoman, 48);
                    PdfPageBase loadedPage = pdfDocument.Pages[x];
                    PdfGraphics graphics = loadedPage.Graphics;
                    PdfGraphicsState state = graphics.Save();
                    graphics.SetTransparency(0.25f);
                    graphics.RotateTransform(-40);
                    graphics.DrawString("COMPUTOS", font_water_mark, PdfPens.Red, PdfBrushes.Red, new PointF(-150, 450));
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    //Saving the PDF document into the stream
                    pdfDocument.Save(stream);
                    //Closing the PDF document
                    pdfDocument.Close(true);
                    return stream;
                }
            }
        }

        public MemoryStream CreatePdf_Resumen(List<ResumenAmbClasscs> dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("Forecast cannot be null");
            }
            //Create a new PDF document
            using (PdfDocument pdfDocument = new PdfDocument())
            {
                int paragraphAfterSpacing = 8;
                int cellMargin = 8;

                //Add page to the PDF document
                PdfPage page = pdfDocument.Pages.Add();

                //Create a new font
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                //Create a text element to draw a text in PDF page
                PdfTextElement title = new PdfTextElement("Clinica Constituyentes", font, PdfBrushes.Black);
                
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement content = new PdfTextElement("Resumen Amboratorio", contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                //Create a PdfGrid
                PdfGrid pdfGrid = new PdfGrid();
                pdfGrid.Style.CellPadding.Left = cellMargin;
                pdfGrid.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                //Assign data source
                pdfGrid.DataSource = dataTable;

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                int pageCount = pdfDocument.Pages.Count;
                for (int x=0; x > pageCount; x++)
                {
                    PdfStandardFont font_water_mark = new PdfStandardFont(PdfFontFamily.TimesRoman, 48);
                    PdfPageBase loadedPage = pdfDocument.Pages[x];
                    PdfGraphics graphics = loadedPage.Graphics;
                    PdfGraphicsState state = graphics.Save();
                    graphics.SetTransparency(0.25f);
                    graphics.RotateTransform(-40);
                    graphics.DrawString("COMPUTOS", font_water_mark, PdfPens.Red, PdfBrushes.Red, new PointF(-150, 450));
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    //Saving the PDF document into the stream
                    pdfDocument.Save(stream);
                    //Closing the PDF document
                    pdfDocument.Close(true);
                    return stream;
                }
            }
        }
    }
}