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
        public MemoryStream CreatePdf(List<AmbListExportrClass> dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("Forecast cannot be null");
            }
            //Create a new PDF document
            using (PdfDocument pdfDocument = new PdfDocument())
            {
                pdfDocument.PageSettings.Size = PdfPageSize.Legal;
                pdfDocument.PageSettings.Orientation = PdfPageOrientation.Landscape;

                int paragraphAfterSpacing = 4;
                int cellMargin = 4;

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
                PdfStandardFont font_grid = new PdfStandardFont(PdfFontFamily.TimesRoman, 8);                

                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 120;
                pdfGrid.Columns[2].Width = 120;
                pdfGrid.Columns[3].Width = 120;
                pdfGrid.Columns[4].Width = 50;
                pdfGrid.Columns[5].Width = 110;
                pdfGrid.Columns[6].Width = 50;
                pdfGrid.Columns[7].Width = 100;
                pdfGrid.Columns[8].Width = 100;
                pdfGrid.Columns[9].Width = 50;
                pdfGrid.Columns[10].Width = 80;

                /*PdfGridHeaderCollection collection = pdfGrid.Headers;
                // Set the header names.
                collection[0].Cells[0].Value = "Fecha";
                collection[0].Cells[1].Value = "Cobertura";
                collection[0].Cells[2].Value = "Paciente";
                collection[0].Cells[3].Value = "Especialidad";
                collection[0].Cells[4].Value = "Codigo";
                collection[0].Cells[5].Value = "Descripcion";
                collection[0].Cells[6].Value = "Cantidad";
                collection[0].Cells[7].Value = "Efector";
                collection[0].Cells[8].Value = "Derivador";
                collection[0].Cells[9].Value = "Matricula";
                collection[0].Cells[10].Value = "Grupo";*/

                pdfGrid.Style.Font = font_grid;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                int pageCount = pdfDocument.Pages.Count;
                for (int x = 0; x < pageCount; x++)
                {
                    PdfStandardFont font_water_mark = new PdfStandardFont(PdfFontFamily.TimesRoman, 48);
                    PdfPageBase loadedPage = pdfDocument.Pages[x];
                    PdfGraphics graphics = loadedPage.Graphics;
                    PdfGraphicsState state = graphics.Save();
                    graphics.SetTransparency(0.25f);
                    graphics.RotateTransform(-40);
                    graphics.DrawString("COMPUTOS", font_water_mark, PdfPens.Red, PdfBrushes.Red, new PointF(-70, 350));
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
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.ListTable4Accent1);

                //Assign data source
                pdfGrid.DataSource = dataTable;

                PdfGridHeaderCollection collection = pdfGrid.Headers;
                // Set the header names.
                collection[0].Cells[0].Value = "Cobertura";
                collection[0].Cells[1].Value = "Codigo";

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                int pageCount = pdfDocument.Pages.Count;
                for (int x=0; x < pageCount; x++)
                {
                    PdfStandardFont font_water_mark = new PdfStandardFont(PdfFontFamily.TimesRoman, 48);
                    PdfPageBase loadedPage = pdfDocument.Pages[x];
                    PdfGraphics graphics = loadedPage.Graphics;
                    PdfGraphicsState state = graphics.Save();
                    graphics.SetTransparency(0.25f);
                    graphics.RotateTransform(-40);
                    graphics.DrawString("COMPUTOS", font_water_mark, PdfPens.Red, PdfBrushes.Red, new PointF(-70, 350));
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

        public MemoryStream CreatePdf_Resumen_Medico(List<ResumenAmbXMedico> dataTable)
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

                PdfGridHeaderCollection collection = pdfGrid.Headers;
                // Set the header names.
                collection[0].Cells[0].Value = "Medico";
                collection[0].Cells[1].Value = "Codigo";

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                int pageCount = pdfDocument.Pages.Count;
                for (int x = 0; x < pageCount; x++)
                {
                    PdfStandardFont font_water_mark = new PdfStandardFont(PdfFontFamily.TimesRoman, 48);
                    PdfPageBase loadedPage = pdfDocument.Pages[x];
                    PdfGraphics graphics = loadedPage.Graphics;
                    PdfGraphicsState state = graphics.Save();
                    graphics.SetTransparency(0.25f);
                    graphics.RotateTransform(-40);
                    graphics.DrawString("COMPUTOS", font_water_mark, PdfPens.Red, PdfBrushes.Red, new PointF(-70, 350));
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