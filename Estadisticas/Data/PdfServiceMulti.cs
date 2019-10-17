using Estadisticas.Model;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Estadisticas.Data
{
    public class PdfServiceMulti
    {
        private ToDataTable toData = new ToDataTable();
        public MemoryStream CreatePdf_Resumen_Coberturas(List<ResumenAmbClasscs> dataTable, string tipo, string title_resumen)
        {
            PdfDocument doc = new PdfDocument();

            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(1.20f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(0.5f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;

            // Create one page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin, PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Portrait);

            float y = 10;
            float x1 = page.Canvas.ClientSize.Width;

            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold), true);
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Resumen Ambulatorio " + title_resumen.Trim(), font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString("Resumen Ambulatorio" + title_resumen.Trim(), format1).Height;
            y = y + 5;

            PdfGrid grid = new PdfGrid();
            grid.Style.CellPadding = new PdfPaddings(1, 1, 1, 1);

            grid.Columns.Add(2);
            float width = page.Canvas.ClientSize.Width - (grid.Columns.Count + 1);
            grid.Columns[0].Width = width * 0.25f;
            grid.Columns[1].Width = width * 0.70f;
            PdfGridRow headerRow = grid.Headers.Add(1)[0];
            headerRow.Style.Font = new PdfTrueTypeFont(new Font("Arial", 11f, FontStyle.Bold), true);
            headerRow.Style.BackgroundBrush = new PdfLinearGradientBrush(new PointF(0, 0), new PointF(x1, 0), Color.Red, Color.Blue);
            headerRow.Cells[0].Value = tipo.Trim();
            headerRow.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            headerRow.Cells[1].Value = "Codigo";
            headerRow.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            headerRow.Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
            headerRow.Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;

            Random random = new Random();
            int fila = 0;
            foreach (ResumenAmbClasscs rowdata in dataTable)
            {
                PdfGridRow row = grid.Rows.Add();
                row.Style.Font = new PdfTrueTypeFont(new Font("Arial", 10f), true);
                byte[] buffer = new byte[6];
                random.NextBytes(buffer);
                PdfRGBColor color1 = new PdfRGBColor(buffer[0], buffer[1], buffer[2]);
                PdfRGBColor color2 = new PdfRGBColor(buffer[3], buffer[4], buffer[5]);
                //row.Style.BackgroundBrush = new PdfLinearGradientBrush(new PointF(0, 0), new PointF(x1, 0), color1, color2);
                row.Cells[0].Value = rowdata.Cobertura;
                //row.Cells[0].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                /*if (fila == 0)
                {
                    row.Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
                    row.Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;
                } */

                if (rowdata.Codigo.ToList() != null)
                {
                    PdfGrid codigoList = new PdfGrid();
                    codigoList.Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f), true);
                    codigoList.Columns.Add(3);
                    List<ResumenCodicoAmbClass> tmp_list = new List<ResumenCodicoAmbClass>();
                    tmp_list = rowdata.Codigo.ToList();
                    codigoList.DataSource = toData.convert(tmp_list);
                    codigoList.Headers[0].Cells[0].Value = "Codigo";
                    codigoList.Headers[0].Cells[0].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[0].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[1].Value = "Descripcion";
                    codigoList.Headers[0].Cells[1].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[1].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[2].Value = "Cantidad";
                    codigoList.Headers[0].Cells[2].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[2].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
                    codigoList.Headers[0].Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;
                    codigoList.Headers[0].Cells[2].Style.BackgroundBrush = PdfBrushes.Gray;
                    row.Cells[1].Value = codigoList;
                    row.Cells[1].StringFormat.Alignment = PdfTextAlignment.Left;
                    //row.Cells[1].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                }
                fila++;
            }

            PdfLayoutResult result = grid.Draw(page, new PointF(5, y));
            y = y + result.Bounds.Height + 5;

            PdfBrush brush2 = PdfBrushes.Gray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));
            result.Page.Canvas.DrawString(String.Format(" {0} " + tipo.Trim() + " Listadas ", grid.Rows.Count - 1), font2, brush2, 5, y);

            int pageCount = doc.Pages.Count;
            for (int x = 0; x < pageCount; x++)
            {
                PdfPageBase page_mark = doc.Pages[x];
                PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page_mark.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                brush.Graphics.SetTransparency(0.3f);
                brush.Graphics.Save();
                brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                brush.Graphics.RotateTransform(-45);
                brush.Graphics.DrawString("COMPUTOS", new PdfFont(PdfFontFamily.Helvetica, 24), PdfBrushes.Violet, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                brush.Graphics.Restore();
                brush.Graphics.SetTransparency(1);
                page_mark.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page_mark.Canvas.ClientSize));
            }

            using (MemoryStream stream = new MemoryStream())
            {
                //Saving the PDF document into the stream
                doc.SaveToStream(stream);
                //Closing the PDF document
                doc.Close();
                return stream;
            }
        }

        public MemoryStream CreatePdf_Resumen_Medico(List<ResumenAmbXMedico> dataTable, string tipo, string title_resumen)
        {
            PdfDocument doc = new PdfDocument();

            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(1.20f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(0.5f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;

            // Create one page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin, PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Portrait);

            float y = 10;
            float x1 = page.Canvas.ClientSize.Width;

            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold), true);
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Resumen Ambulatorio " + title_resumen.Trim(), font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString("Resumen Ambulatorio" + title_resumen.Trim(), format1).Height;
            y = y + 5;

            PdfGrid grid = new PdfGrid();
            grid.Style.CellPadding = new PdfPaddings(1, 1, 1, 1);

            grid.Columns.Add(2);
            float width = page.Canvas.ClientSize.Width - (grid.Columns.Count + 1);
            grid.Columns[0].Width = width * 0.25f;
            grid.Columns[1].Width = width * 0.70f;
            PdfGridRow headerRow = grid.Headers.Add(1)[0];
            headerRow.Style.Font = new PdfTrueTypeFont(new Font("Arial", 11f, FontStyle.Bold), true);
            headerRow.Style.BackgroundBrush = new PdfLinearGradientBrush(new PointF(0, 0), new PointF(x1, 0), Color.Red, Color.Blue);
            headerRow.Cells[0].Value = tipo.Trim();
            headerRow.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            headerRow.Cells[1].Value = "Codigo";
            headerRow.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            headerRow.Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
            headerRow.Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;

            Random random = new Random();
            int fila = 0;
            foreach (ResumenAmbXMedico rowdata in dataTable)
            {
                PdfGridRow row = grid.Rows.Add();
                row.Style.Font = new PdfTrueTypeFont(new Font("Arial", 10f), true);
                byte[] buffer = new byte[6];
                random.NextBytes(buffer);
                PdfRGBColor color1 = new PdfRGBColor(buffer[0], buffer[1], buffer[2]);
                PdfRGBColor color2 = new PdfRGBColor(buffer[3], buffer[4], buffer[5]);
                //row.Style.BackgroundBrush = new PdfLinearGradientBrush(new PointF(0, 0), new PointF(x1, 0), color1, color2);
                row.Cells[0].Value = rowdata.Medico;
                //row.Cells[0].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                /*if (fila == 0)
                {
                    row.Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
                    row.Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;
                } */

                if (rowdata.Codigo.ToList() != null)
                {
                    PdfGrid codigoList = new PdfGrid();
                    codigoList.Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f), true);
                    codigoList.Columns.Add(3);
                    List<ResumenCodicoAmbXMedico> tmp_list = new List<ResumenCodicoAmbXMedico>();
                    tmp_list = rowdata.Codigo.ToList();
                    codigoList.DataSource = toData.convert(tmp_list);
                    codigoList.Headers[0].Cells[0].Value = "Codigo";
                    codigoList.Headers[0].Cells[0].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[0].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[1].Value = "Descripcion";
                    codigoList.Headers[0].Cells[1].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[1].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[2].Value = "Cantidad";
                    codigoList.Headers[0].Cells[2].Style.Font = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Bold), true);
                    //codigoList.Headers[0].Cells[2].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                    codigoList.Headers[0].Cells[0].Style.BackgroundBrush = PdfBrushes.Gray;
                    codigoList.Headers[0].Cells[1].Style.BackgroundBrush = PdfBrushes.Gray;
                    codigoList.Headers[0].Cells[2].Style.BackgroundBrush = PdfBrushes.Gray;
                    row.Cells[1].Value = codigoList;
                    row.Cells[1].StringFormat.Alignment = PdfTextAlignment.Left;
                    //row.Cells[1].Style.Borders.All = new PdfPen(new PdfTilingBrush(new SizeF(1, 1)), 0);
                }
                fila++;
            }

            PdfLayoutResult result = grid.Draw(page, new PointF(5, y));
            y = y + result.Bounds.Height + 5;

            PdfBrush brush2 = PdfBrushes.Gray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));
            result.Page.Canvas.DrawString(String.Format(" {0} " + tipo.Trim() + " Listadas ", grid.Rows.Count - 1), font2, brush2, 5, y);

            int pageCount = doc.Pages.Count;
            for (int x = 0; x < pageCount; x++)
            {
                PdfPageBase page_mark = doc.Pages[x];
                PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page_mark.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                brush.Graphics.SetTransparency(0.3f);
                brush.Graphics.Save();
                brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                brush.Graphics.RotateTransform(-45);
                brush.Graphics.DrawString("COMPUTOS", new PdfFont(PdfFontFamily.Helvetica, 24), PdfBrushes.Violet, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                brush.Graphics.Restore();
                brush.Graphics.SetTransparency(1);
                page_mark.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page_mark.Canvas.ClientSize));
            }

            using (MemoryStream stream = new MemoryStream())
            {
                //Saving the PDF document into the stream
                doc.SaveToStream(stream);
                //Closing the PDF document
                doc.Close();
                return stream;
            }
        }
    }
}
