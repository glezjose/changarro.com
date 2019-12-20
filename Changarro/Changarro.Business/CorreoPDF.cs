using Changarro.Model.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Changarro.Business
{
    public class CorreoPDF
    {
        private readonly Compra compra;

        public CorreoPDF()
        {
            compra = new Compra();
        }

        /// <summary>
        /// Método donde se crea el archivo PDF de los detalles de la compra.
        /// </summary>
        /// <param name="iIdCompra">La ID de la compra</param>
        /// <returns>Regresa el archivo PDF en forma de mensaje de correo.</returns>
        public MailMessage GenerarPDF(int iIdCompra)
        {
            Document _documento = new Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream _memoryStream = new MemoryStream();
            PdfWriter _escritor = PdfWriter.GetInstance(_documento, _memoryStream);

            DatosClienteCompraDTO _oDatos = compra.ObtenerInfoCompra(iIdCompra);

            List<CatalogoProductosDTO> _lstCompra = compra.ObtenerCompra(iIdCompra);

            decimal _dSubTotal = 0;

            FontFactory.Register("http://localhost/changarro/Assets/fonts/Poppins-Regular.ttf", "Poppins");
            FontFactory.Register("http://localhost/changarro/Assets/fonts/Roboto-Regular.ttf", "Roboto");
            FontFactory.Register("http://localhost/changarro/Assets/fonts/Poppins-Medium.ttf", "Poppins-Medium");


            Font _fuenteTitulo = FontFactory.GetFont("Poppins", 17, Font.BOLD, new BaseColor(34, 34, 34));
            Font _fuenteSubTitulo = FontFactory.GetFont("Roboto", 12, Font.NORMAL, new BaseColor(119, 119, 119));
            Font _fuenteCuerpo = FontFactory.GetFont("Poppins", 12, Font.NORMAL, new BaseColor(34, 34, 34));
            Font _fuenteDetalles = FontFactory.GetFont("Poppins-Medium", 12, Font.BOLD, new BaseColor(34, 34, 34));

            _documento.Open();

            _documento.Add(new Paragraph("Recibo de compra Changarro(C00" + iIdCompra + ")\n", _fuenteTitulo));
            _documento.Add(new Paragraph(_oDatos.cNombre + ", gracias por comprar en Changarro! Te mandamos tu recibo de compra.\n\n", _fuenteSubTitulo));

            Image logo = Image.GetInstance(new Uri("http://localhost/changarro/Assets/img/logo.png"));
            logo.SetAbsolutePosition(440, 785);
            _documento.Add(logo);

            #region Tabla detalle de compra

            PdfPTable _tblDetalles = new PdfPTable(2);
            _tblDetalles.HorizontalAlignment = 1;
            _tblDetalles.SpacingBefore = 20;
            _tblDetalles.SpacingAfter = 20;
            _tblDetalles.WidthPercentage = 100f;
            _tblDetalles.DefaultCell.Border = Rectangle.NO_BORDER;
            _tblDetalles.DefaultCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            _tblDetalles.DefaultCell.FixedHeight = 50f;
            _tblDetalles.SetWidths(new int[] { 3, 6 });

            PdfPCell _celdaTitulo = new PdfPCell(new Phrase("Información de la compra", _fuenteDetalles));
            _celdaTitulo.Border = Rectangle.BOTTOM_BORDER;
            _celdaTitulo.BorderColor = new BaseColor(221, 221, 221);
            _celdaTitulo.FixedHeight = 50f;
            _celdaTitulo.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            _celdaTitulo.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            _celdaTitulo.Colspan = 2;
            _tblDetalles.AddCell(_celdaTitulo);

            _tblDetalles.AddCell(new Phrase("No. Compra:", _fuenteSubTitulo));

            _tblDetalles.AddCell(new Phrase("C00" + iIdCompra, _fuenteCuerpo));

            _tblDetalles.AddCell(new Phrase("Fecha de Compra:", _fuenteSubTitulo));

            _tblDetalles.AddCell(new Phrase(Convert.ToString(_oDatos.dtFecha), _fuenteCuerpo));

            _tblDetalles.AddCell(new Phrase("Domicilio de envió:", _fuenteSubTitulo));

            _tblDetalles.AddCell(new Phrase(Convert.ToString(_oDatos.cDomicilio), _fuenteCuerpo));

            _documento.Add(_tblDetalles);

            #endregion

            #region Tabla de compra

            PdfPTable _tblCompra = new PdfPTable(3);
            _tblCompra.HorizontalAlignment = 1;
            _tblCompra.SpacingBefore = 20;
            _tblCompra.SpacingAfter = 20;
            _tblCompra.WidthPercentage = 100f;
            _tblCompra.SetWidths(new int[] { 6, 3, 4 });

            PdfPCell _celdaProducto = new PdfPCell(new Phrase("Producto", _fuenteSubTitulo));
            _celdaProducto.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER;
            _celdaProducto.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaProducto.BorderColorLeft = new BaseColor(229, 236, 238);
            _celdaProducto.BorderWidthLeft = 20f;
            _celdaProducto.PaddingLeft = 20f;
            _celdaProducto.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaProducto.FixedHeight = 30f;
            _celdaProducto.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            _tblCompra.AddCell(_celdaProducto);

            PdfPCell _celdaTituloCantidad = new PdfPCell(new Phrase("Cantidad", _fuenteSubTitulo));
            _celdaTituloCantidad.Border = Rectangle.BOTTOM_BORDER;
            _celdaTituloCantidad.BorderColor = new BaseColor(221, 221, 221);
            _celdaTituloCantidad.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTituloCantidad.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            _celdaTituloCantidad.FixedHeight = 20f;
            _tblCompra.AddCell(_celdaTituloCantidad);

            PdfPCell _celdaTituloPrecio = new PdfPCell(new Phrase("Precio", _fuenteSubTitulo));
            _celdaTituloPrecio.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            _celdaTituloPrecio.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaTituloPrecio.BorderColorRight = new BaseColor(229, 236, 238);
            _celdaTituloPrecio.BorderWidthRight = 20f;
            _celdaTituloPrecio.PaddingRight = 20f;
            _celdaTituloPrecio.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTituloPrecio.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            _celdaTituloPrecio.FixedHeight = 20f;
            _tblCompra.AddCell(_celdaTituloPrecio);

            foreach (var _producto in _lstCompra)
            {
                PdfPCell _celdaNombre = new PdfPCell(new Phrase(_producto.cNombre, _fuenteSubTitulo));
                _celdaNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER;
                _celdaNombre.BorderColorBottom = new BaseColor(221, 221, 221);
                _celdaNombre.BorderColorLeft = new BaseColor(229, 236, 238);
                _celdaNombre.BorderWidthLeft = 20f;
                _celdaNombre.PaddingLeft = 20f;
                _celdaNombre.BackgroundColor = new BaseColor(229, 236, 238);
                _celdaNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
                _celdaNombre.FixedHeight = 40f;
                _tblCompra.AddCell(_celdaNombre);

                PdfPCell _celdaCantidad = new PdfPCell(new Phrase(Convert.ToString(_producto.iCantidad), _fuenteDetalles));
                _celdaCantidad.Border = Rectangle.BOTTOM_BORDER;
                _celdaCantidad.BorderColor = new BaseColor(221, 221, 221);
                _celdaCantidad.BackgroundColor = new BaseColor(229, 236, 238);
                _celdaCantidad.VerticalAlignment = Element.ALIGN_MIDDLE;
                _celdaCantidad.FixedHeight = 40f;
                _tblCompra.AddCell(_celdaCantidad);

                PdfPCell _celdaPrecio = new PdfPCell(new Phrase("$" + Convert.ToString(_producto.dPrecio), _fuenteSubTitulo));
                _celdaPrecio.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                _celdaPrecio.BorderColorBottom = new BaseColor(221, 221, 221);
                _celdaPrecio.BorderColorRight = new BaseColor(229, 236, 238);
                _celdaPrecio.BorderWidthRight = 20f;
                _celdaPrecio.PaddingRight = 20f;
                _celdaPrecio.BackgroundColor = new BaseColor(229, 236, 238);
                _celdaPrecio.VerticalAlignment = Element.ALIGN_MIDDLE;
                _celdaPrecio.FixedHeight = 40f;
                _tblCompra.AddCell(_celdaPrecio);

                _dSubTotal += _producto.dPrecio;
            }
            PdfPCell _celdaTituloSubTotal = new PdfPCell(new Phrase("SUBTOTAL", _fuenteDetalles));
            _celdaTituloSubTotal.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER;
            _celdaTituloSubTotal.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaTituloSubTotal.BorderColorLeft = new BaseColor(229, 236, 238);
            _celdaTituloSubTotal.BorderWidthLeft = 20f;
            _celdaTituloSubTotal.PaddingLeft = 20f;
            _celdaTituloSubTotal.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTituloSubTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaTituloSubTotal.Colspan = 2;
            _tblCompra.AddCell(_celdaTituloSubTotal);

            PdfPCell _celdaSubTotal = new PdfPCell(new Phrase("$" + Convert.ToString(_dSubTotal), _fuenteSubTitulo));
            _celdaSubTotal.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            _celdaSubTotal.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaSubTotal.BorderColorRight = new BaseColor(229, 236, 238);
            _celdaSubTotal.BorderWidthRight = 20f;
            _celdaSubTotal.PaddingRight = 20f;
            _celdaSubTotal.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaSubTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaSubTotal.FixedHeight = 50f;
            _tblCompra.AddCell(_celdaSubTotal);

            PdfPCell _celdaTituloEnvio = new PdfPCell(new Phrase("ENVIO", _fuenteDetalles));
            _celdaTituloEnvio.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER;
            _celdaTituloEnvio.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaTituloEnvio.BorderColorLeft = new BaseColor(229, 236, 238);
            _celdaTituloEnvio.BorderWidthLeft = 20f;
            _celdaTituloEnvio.PaddingLeft = 20f;
            _celdaTituloEnvio.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTituloEnvio.Colspan = 2;
            _celdaTituloEnvio.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaTituloEnvio.FixedHeight = 50f;
            _tblCompra.AddCell(_celdaTituloEnvio);

            PdfPCell _celdaEnvio = new PdfPCell(new Phrase("$50.00", _fuenteSubTitulo));
            _celdaEnvio.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            _celdaEnvio.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaEnvio.BorderColorRight = new BaseColor(229, 236, 238);
            _celdaEnvio.BorderWidthRight = 20f;
            _celdaEnvio.PaddingRight = 20f;
            _celdaEnvio.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaEnvio.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaEnvio.FixedHeight = 50f;
            _tblCompra.AddCell(_celdaEnvio);

            PdfPCell _celdaTituloTotal = new PdfPCell(new Phrase("TOTAL", _fuenteDetalles));
            _celdaTituloTotal.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER;
            _celdaTituloTotal.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaTituloTotal.BorderColorLeft = new BaseColor(229, 236, 238);
            _celdaTituloTotal.BorderWidthLeft = 20f;
            _celdaTituloTotal.PaddingLeft = 20f;
            _celdaTituloTotal.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTituloTotal.Colspan = 2;
            _celdaTituloTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaTituloTotal.FixedHeight = 50f;
            _tblCompra.AddCell(_celdaTituloTotal);

            PdfPCell _celdaTotal = new PdfPCell(new Phrase("$" + Convert.ToString(_dSubTotal + 50), _fuenteCuerpo));
            _celdaTotal.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            _celdaTotal.BorderColorBottom = new BaseColor(221, 221, 221);
            _celdaTotal.BorderColorRight = new BaseColor(229, 236, 238);
            _celdaTotal.BorderWidthRight = 20f;
            _celdaTotal.PaddingRight = 20f;
            _celdaTotal.BackgroundColor = new BaseColor(229, 236, 238);
            _celdaTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
            _celdaTotal.FixedHeight = 50f;
            _tblCompra.AddCell(_celdaTotal);

            _documento.Add(_tblCompra);

            #endregion

            _escritor.CloseStream = false;
            _documento.Close();
            _memoryStream.Position = 0;

            MailMessage _mmMensaje = new MailMessage("changarro.recibos@gmail.com", _oDatos.cCorreo)
            {
                Subject = "Su recibo de compra en Changarro.com.mx(C00" + iIdCompra + ")",
                IsBodyHtml = true,
                Body = "Hola, " + _oDatos.cNombre + "! Gracias por comprar en Changarro, te informamos que hemos recibido tu pedido! Te adjuntamos tu recibo de compras:"
            };

            _mmMensaje.Attachments.Add(new Attachment(_memoryStream, "recibo_C00" + iIdCompra + ".pdf"));

            return _mmMensaje;
        }

        /// <summary>
        /// Método para enviar correo
        /// </summary>
        /// <param name="mmMensaje"></param>
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
