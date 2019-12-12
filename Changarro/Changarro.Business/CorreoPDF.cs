using Changarro.Model;
using Changarro.Model.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Changarro.Business
{
    public class CorreoPDF
    {
        private readonly Compra compra;
        private readonly Cliente cliente;

        public CorreoPDF()
        {
            compra = new Compra();
            cliente = new Cliente();
        }

        public MailMessage GenerarPDF(int iIdCliente, int iIdCompra)
        {
            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

            ClienteDTO _oCliente = cliente.ObtenerCliente(iIdCliente);

            List<tbl_DetalleCompra> _lstCompra = compra.ObtenerCompra(iIdCompra);

            doc.Open();

            doc.Add(new Paragraph(_oCliente.cNombre + ", gracias por comprar en Changarro! Te mandamos tu recibo de compra."));

            foreach (var _producto in _lstCompra)
            {
            doc.Add(new Paragraph(_producto.tblCat_Producto.cNombre + ": " + _producto.iCantidad + " " + (_producto.iCantidad * _producto.tblCat_Producto.dPrecio)));
            }

            writer.CloseStream = false;
            doc.Close();
            memoryStream.Position = 0;

            MailMessage _mmMensaje = new MailMessage("changarro.recibos@gmail.com", _oCliente.cCorreo)
            {
                Subject = "Recibo de compra CHANGARRO_" + iIdCompra,
                IsBodyHtml = true,
                Body = "Hola, " + _oCliente.cNombre + "! Gracias por comprar en Changarro, te apreciamos!"
            };

            _mmMensaje.Attachments.Add(new Attachment(memoryStream, "filename.pdf"));

            return _mmMensaje;
        }

        public void EnviarCorreo(MailMessage mmMensaje)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("changarro.recibos@gmail.com", "ch4ng4rr0")

            };

            smtp.Send(mmMensaje);
        }
    }
}
