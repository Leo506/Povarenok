using DemoExam.Domain.Models;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DemoExam.PdfGenerator;

public static class PdfGenerator
{
    public static string CreatePdfForOrder(int orderId, DateTime orderDate, decimal orderSum, decimal orderDiscount,
        string pickupPoint, int getCode, Dictionary<Product, int> products)
    {
        var fileName = $"Order{orderId}.pdf";
        var pdfWriter = new PdfWriter(fileName);
        var pdfDoc = new PdfDocument(pdfWriter);
        var doc = new Document(pdfDoc);
        doc.SetFont(PdfFontFactory.CreateFont("Resources/Roboto-Medium.ttf",
            PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED));

        var orderIdText = new Paragraph($"Заказ №{orderId}\n")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

        var orderInfoText = new Paragraph("Иформация о заказе\n")
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(15);

        var orderInfoTable = new Table(2, false)
            .AddCell(CreateTextCell("Дата заказа"))
            .AddCell(CreateTextCell(orderDate.ToShortDateString()))
            .AddCell(CreateTextCell("Сумма заказа"))
            .AddCell(CreateTextCell($"{orderSum:C}"))
            .AddCell(CreateTextCell("Сумма скидки"))
            .AddCell(CreateTextCell($"{orderDiscount:C}"))
            .AddCell(CreateTextCell("Пункт выдачи"))
            .AddCell(CreateTextCell(pickupPoint))
            .AddCell(CreateTextCell("Код для получения заказа"))
            .AddCell(CreateTextCell(getCode.ToString()));

        var orderContentText = new Paragraph("Состав заказа")
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(15);

        var orderContentTable = new Table(4, false)
            .AddHeaderCell(CreateTextCell("Фото"))
            .AddHeaderCell(CreateTextCell("Наименование"))
            .AddHeaderCell(CreateTextCell("Цена"))
            .AddHeaderCell(CreateTextCell("Количество"));
        foreach (var (product, amount) in products)
        {
            orderContentTable.AddCell(CreateImageCell(product.ProductPhoto))
                .AddCell(CreateTextCell(product.ProductName))
                .AddCell(CreateTextCell($"{product.ProductCost:C}"))
                .AddCell(CreateTextCell(amount.ToString()));
        }

        doc.Add(orderIdText)
            .Add(orderInfoText)
            .Add(orderInfoTable)
            .Add(orderContentText)
            .Add(orderContentTable);
        doc.Close();

        return fileName;
    }
    
    private static Cell CreateImageCell(byte[]? image) =>
        image is null
            ? CreateTextCell("Нет изображения")
            : new Cell().Add(new Image(ImageDataFactory.Create(image))
                .SetWidth(50)
                .SetHeight(50)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER));
    
    private static Cell CreateTextCell(string cellContent) => new Cell().Add(new Paragraph(cellContent));
}
