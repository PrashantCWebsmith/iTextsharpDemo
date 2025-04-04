using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using Document = iTextSharp.text.Document;
using Paragraph = iTextSharp.text.Paragraph;
using Image = iTextSharp.text.Image;
using List = iTextSharp.text.List;
using ListItem = iTextSharp.text.ListItem;

namespace iTextsharpDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private FileContentResult DownloadPdf(MemoryStream ms, string fileName)
        {
            byte[] fileBytes = ms.ToArray();
            return File(fileBytes, "application/pdf", fileName);
        }

        // 1. Basic PDF with Simple Text
        public ActionResult GenerateBasicPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 12f;
                    Font myFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    Paragraph para = new Paragraph("Simple Text PDF", myFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 30f,
                        SpacingAfter = 30f
                    };
                    doc.Add(para);
                }
                return File(ms.ToArray(), "application/pdf", "BasicPDF.pdf");
            }
        }

        // 2. PDF with Font Type, Size, and Color
        public ActionResult GenerateFontPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 16f;
                    Font myFont = new Font(Font.FontFamily.TIMES_ROMAN, fontSize, Font.BOLD, BaseColor.RED);

                    doc.Open();

                    Paragraph para = new Paragraph("Red Bold Text", myFont)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingBefore = 20f,
                        SpacingAfter = 20f,
                        IndentationLeft = 10f
                    };
                    doc.Add(para);
                }
                return File(ms.ToArray(), "application/pdf", "FontPDF.pdf");
            }
        }

        // 3. PDF with Multiple Fonts and Styles
        public ActionResult GenerateMultiFontPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize1 = 14f;
                    Font font1 = new Font(Font.FontFamily.HELVETICA, fontSize1, Font.ITALIC, BaseColor.BLUE);

                    float fontSize2 = 12f;
                    Font font2 = new Font(Font.FontFamily.COURIER, fontSize2, Font.UNDERLINE, BaseColor.GREEN);

                    doc.Open();

                    Paragraph p1 = new Paragraph("Blue Italic Text", font1)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingBefore = 15f,
                        SpacingAfter = 10f,
                        IndentationLeft = 20f
                    };
                    doc.Add(p1);

                    Paragraph p2 = new Paragraph("Green Underlined Text", font2)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 10f,
                        SpacingAfter = 15f,
                        IndentationRight = 20f
                    };
                    doc.Add(p2);
                }
                return File(ms.ToArray(), "application/pdf", "MultiFontPDF.pdf");
            }
        }

        // 4. PDF with a Table
        public ActionResult GenerateTablePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 12f;
                    Font headerFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.BOLD, BaseColor.BLACK);
                    Font bodyFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    Paragraph title = new Paragraph("Table Example", headerFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 20f,
                        SpacingAfter = 10f
                    };
                    doc.Add(title);

                    PdfPTable table = new PdfPTable(3)
                    {
                        WidthPercentage = 80,
                        SpacingBefore = 10f,
                        SpacingAfter = 20f
                    };
                    table.AddCell(new Phrase("ID", headerFont));
                    table.AddCell(new Phrase("Name", headerFont));
                    table.AddCell(new Phrase("Age", headerFont));
                    table.AddCell(new Phrase("1", bodyFont));
                    table.AddCell(new Phrase("Sam", bodyFont));
                    table.AddCell(new Phrase("25", bodyFont));
                    doc.Add(table);
                }
                return File(ms.ToArray(), "application/pdf", "TablePDF.pdf");
            }
        }

        // 5. PDF with an Image
        public ActionResult GenerateImagePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 10f;
                    Font myFont = new Font(Font.FontFamily.TIMES_ROMAN, fontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    Paragraph para = new Paragraph("Image Below:", myFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 30f,
                        SpacingAfter = 15f
                    };
                    doc.Add(para);

                    Image img = Image.GetInstance(Server.MapPath("~/Content/sample.jpg"));
                    img.ScaleToFit(200f, 200f);
                    img.Alignment = Element.ALIGN_CENTER;
                    doc.Add(img);
                }
                return File(ms.ToArray(), "application/pdf", "ImagePDF.pdf");
            }
        }

        // 6. PDF with a Hyperlink
        public ActionResult GenerateLinkPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 12f;
                    Font myFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.UNDERLINE, BaseColor.BLUE);

                    doc.Open();

                    Anchor link = new Anchor("Visit Websmith Solution", myFont);
                    link.Reference = "https://websmithsolution.com/";

                    Paragraph para = new Paragraph
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 25f,
                        SpacingAfter = 25f,
                        IndentationLeft = 30f
                    };
                    para.Add(link);
                    doc.Add(para);
                }
                return File(ms.ToArray(), "application/pdf", "LinkPDF.pdf");
            }
        }

        // 7. PDF with Header and Footer
        public ActionResult GenerateHeaderFooterPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    writer.PageEvent = new SimpleHeaderFooter();

                    float fontSize = 12f;
                    Font myFont = new Font(Font.FontFamily.COURIER, fontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    Paragraph para = new Paragraph("This has a header and footer.", myFont)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingBefore = 40f,
                        SpacingAfter = 40f,
                        IndentationLeft = 15f
                    };
                    doc.Add(para);
                }
                return File(ms.ToArray(), "application/pdf", "HeaderFooterPDF.pdf");
            }
        }

        // 8. PDF with a List and Styles
        public ActionResult GenerateListPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 12f;
                    Font normalFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.NORMAL, BaseColor.BLACK);
                    Font boldItalicFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.BOLDITALIC, BaseColor.BLACK);

                    doc.Open();

                    Paragraph title = new Paragraph("List Example", normalFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 20f,
                        SpacingAfter = 10f
                    };
                    doc.Add(title);

                    List list = new List(List.ORDERED);
                    list.Add(new ListItem("Normal Item", normalFont));
                    list.Add(new ListItem("Bold Italic Item", boldItalicFont));
                    doc.Add(list);
                }
                return File(ms.ToArray(), "application/pdf", "ListPDF.pdf");
            }
        }

        // 9. PDF with Multiple Pages
        public ActionResult GenerateMultiPagePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);

                    float fontSize = 14f;
                    Font myFont = new Font(Font.FontFamily.TIMES_ROMAN, fontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    Paragraph p1 = new Paragraph("Page 1", myFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 50f,
                        SpacingAfter = 50f
                    };
                    doc.Add(p1);

                    doc.NewPage();
                    Paragraph p2 = new Paragraph("Page 2", myFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 50f,
                        SpacingAfter = 50f
                    };
                    doc.Add(p2);
                }
                return File(ms.ToArray(), "application/pdf", "MultiPagePDF.pdf");
            }
        }

        // 10. Advanced PDF with All Features
        public ActionResult GenerateAdvancedPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    writer.PageEvent = new SimpleHeaderFooter(); // Add header and footer

                    // Define fonts
                    float titleFontSize = 20f;
                    Font titleFont = new Font(Font.FontFamily.HELVETICA, titleFontSize, Font.BOLD, BaseColor.BLACK);

                    float textFontSize = 12f;
                    Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, textFontSize, Font.NORMAL, BaseColor.BLACK);
                    Font italicFont = new Font(Font.FontFamily.COURIER, textFontSize, Font.ITALIC, BaseColor.BLUE);
                    Font boldFont = new Font(Font.FontFamily.HELVETICA, textFontSize, Font.BOLD, BaseColor.RED);

                    float tableFontSize = 11f;
                    Font tableHeaderFont = new Font(Font.FontFamily.HELVETICA, tableFontSize, Font.BOLD, BaseColor.BLACK);
                    Font tableBodyFont = new Font(Font.FontFamily.HELVETICA, tableFontSize, Font.NORMAL, BaseColor.BLACK);

                    doc.Open();

                    // Title
                    Paragraph title = new Paragraph("Advanced PDF Example", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 40f, // Space for header
                        SpacingAfter = 20f
                    };
                    doc.Add(title);

                    // Paragraph with overflow (long text to test page break)
                    string longText = "This is a very long paragraph to demonstrate overflow and multiple pages. " +
                                     "It will repeat to fill the page and force a page break. " +
                                     new string('A', 10000); // Long string to ensure overflow
                    Paragraph para = new Paragraph(longText, textFont)
                    {
                        Alignment = Element.ALIGN_JUSTIFIED,
                        SpacingBefore = 15f,
                        SpacingAfter = 15f,
                        IndentationLeft = 20f,
                        IndentationRight = 20f,
                        FirstLineIndent = 10f // Indent first line
                    };
                    doc.Add(para);

                    // Table 1: Simple table with list data
                    var table1Data = new List<(string Id, string Name, string Role)>
                    {
                        ("1", "Grok", "AI Helper"),
                        ("2", "Alex", "Developer"),
                        ("3", "Sam", "Tester"),
                        ("4", "Emma", "Designer")
                    };

                    PdfPTable table1 = new PdfPTable(3)
                    {
                        WidthPercentage = 80,
                        SpacingBefore = 20f,
                        SpacingAfter = 20f
                    };
                    table1.AddCell(new Phrase("ID", tableHeaderFont));
                    table1.AddCell(new Phrase("Name", tableHeaderFont));
                    table1.AddCell(new Phrase("Role", tableHeaderFont));
                    foreach (var item in table1Data)
                    {
                        table1.AddCell(new Phrase(item.Id, tableBodyFont));
                        table1.AddCell(new Phrase(item.Name, tableBodyFont));
                        table1.AddCell(new Phrase(item.Role, tableBodyFont));
                    }
                    doc.Add(table1);

                    // Image
                    Image img = Image.GetInstance(Server.MapPath("~/Content/sample.jpg"));
                    img.ScaleToFit(200f, 200f);
                    img.Alignment = Element.ALIGN_CENTER;
                    img.SpacingBefore = 20f;
                    img.SpacingAfter = 20f;
                    doc.Add(img);

                    // New page with more content
                    doc.NewPage();

                    // Table 2: Another table with bold text and list data
                    var table2Data = new List<(string Item, string Description)>
                    {
                        ("PDF", "Generated File"),
                        ("Image", "Scaled Graphic"),
                        ("Table", "Structured Data"),
                        ("Text", "Formatted Content")
                    };

                    PdfPTable table2 = new PdfPTable(2)
                    {
                        WidthPercentage = 60,
                        SpacingBefore = 15f,
                        SpacingAfter = 20f
                    };

                    table2.AddCell(new Phrase("Item", boldFont));
                    table2.AddCell(new Phrase("Description", boldFont));

                    foreach (var item in table2Data)
                    {
                        table2.AddCell(new Phrase(item.Item, tableBodyFont));
                        table2.AddCell(new Phrase(item.Description, tableBodyFont));
                    }
                    doc.Add(table2);

                    // Table 3: Table with percentage values and list data
                    var table3Data = new List<(string Task, string Status, string Progress)>
                    {
                        ("Design", "Completed", "100%"),
                        ("Development", "In Progress", "75%"),
                        ("Testing", "Pending", "0%"),
                        ("Review", "In Progress", "50%"),
                        ("Deployment", "Not Started", "0%")
                    };

                    PdfPTable table3 = new PdfPTable(3)
                    {
                        WidthPercentage = 70,
                        SpacingBefore = 20f,
                        SpacingAfter = 20f
                    };

                    table3.SetWidths(new float[] { 50f, 30f, 20f });

                    table3.AddCell(new Phrase("Task", tableHeaderFont));
                    table3.AddCell(new Phrase("Status", tableHeaderFont));
                    table3.AddCell(new Phrase("Progress", tableHeaderFont));
                    foreach (var item in table3Data)
                    {
                        table3.AddCell(new Phrase(item.Task, tableBodyFont));
                        table3.AddCell(new Phrase(item.Status, tableBodyFont));
                        table3.AddCell(new Phrase(item.Progress, tableBodyFont));
                    }
                    doc.Add(table3);

                    // Final paragraph to show multiple pages
                    Paragraph para3 = new Paragraph("End of the advanced example.", textFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 20f,
                        SpacingAfter = 40f // Space for footer
                    };
                    doc.Add(para3);
                }
                return File(ms.ToArray(), "application/pdf", "AdvancedPDF.pdf");
            }
        }

        // Header and Footer Helper
        public class SimpleHeaderFooter : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                float fontSize = 12f;
                Font myFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.NORMAL, BaseColor.BLACK);
                PdfPTable header = new PdfPTable(1)
                {
                    TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
                };
                header.AddCell(new PdfPCell(new Phrase("Header", myFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                header.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 10, writer.DirectContent);
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                float fontSize = 10f;
                Font myFont = new Font(Font.FontFamily.HELVETICA, fontSize, Font.NORMAL, BaseColor.BLACK);
                PdfPTable footer = new PdfPTable(1)
                {
                    TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
                };
                footer.AddCell(new PdfPCell(new Phrase("Page " + writer.PageNumber, myFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                footer.WriteSelectedRows(0, -1, document.LeftMargin, 20, writer.DirectContent);
            }
        }
    }
}