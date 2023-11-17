using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using SharedUI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Services
{
    public class PDFGenerator
    {
        IList<Payment> payments;
        private LogedUser logedUser;

        public PDFGenerator(IList<Payment> payments,LogedUser logedUser)
        {
            this.payments = payments;
            this.logedUser = logedUser;
        }
        public void DownloadPdf(IJSRuntime js, string filename = "holerite.pdf")
        {
            js.InvokeVoidAsync("DownloadPdf", filename, Convert.ToBase64String(Report()));
        }

        public void ViewPdf(IJSRuntime js, string idIframe)
        {
            js.InvokeVoidAsync("ViewPdf", idIframe, Convert.ToBase64String(Report()));

        }

        private byte[] Report()
        {
            byte[] pdfBytes;
            float marginLeft = 1.5f;
            float marginRight = 1.5f;
            float marginTop = 1.0f;
            float marginBottom = 1.0f;

            using (var memoryStream = new MemoryStream())
            {
                iTextSharp.text.Document pdf = new iTextSharp.text.Document(
          PageSize.A4,
          marginLeft.ToDpi(), marginRight.ToDpi(), marginTop.ToDpi(), marginBottom.ToDpi());

                PdfWriter writer = PdfWriter.GetInstance(pdf,memoryStream);
                
       

            pdf.AddTitle("Holerite Fox Rh");
            pdf.AddAuthor("DanielMarbles");
            pdf.AddCreationDate();
            pdf.AddKeywords("Holerite");
            pdf.AddSubject("Holerite de Rh");



            var fontStyle = FontFactory.GetFont("Arial", 16f, BaseColor.Black);
            var labelHeader = new Paragraph("FOX RH", fontStyle);

            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), true)
            {
                BackgroundColor = new BaseColor(255,127,0),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };

            pdf.Header = header;







                pdf.Open();
                var title = new Paragraph($"Holerite", new Font(Font.HELVETICA, 20f, Font.BOLD));
                title.SpacingAfter = 18f;

                pdf.Add(title);


                Font _fontStyle = FontFactory.GetFont("Tahoma", 16f, Font.NORMAL);
                var _myText = $"{logedUser.employee.name} portador do cpf {logedUser.employee.cpf}";

                var phrase = new Phrase(_myText, _fontStyle);
                pdf.Add(phrase);

                //fox logo
                string image = $"{AppContext.BaseDirectory}{@"\wwwroot\Images\fox_icon.png"}";

                Image img = Image.GetInstance(image);

                img.SetAbsolutePosition(pdf.LeftMargin,writer.PageSize.GetTop(pdf.TopMargin) - 17);
                img.ScaleAbsolute(50f,50f);
           

                pdf.Add(img);

                
                PdfPTable table = new PdfPTable(2);
                table.AddCell("Pagamento");
                table.AddCell("Dia");
                foreach (var payment in payments)
                {
                    table.AddCell($"R$ {payment.paymentAmount.ToString("0.00")}");
                    table.AddCell($"{String.Format("{0:dd/MM/yyyy}",payment.paymentDate)}");
                }
                table.SpacingBefore = 50f;

                pdf.Add(table);

          
               

     

                pdf.Close();

         
                pdfBytes =  memoryStream.ToArray();



            }

            return pdfBytes;
        }
    }
}
