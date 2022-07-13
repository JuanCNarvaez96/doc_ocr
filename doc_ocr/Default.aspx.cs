using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.XObjects;
using Page = UglyToad.PdfPig.Content.Page;

namespace doc_ocr
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string strFileName;
            string strFilePath;
            string strFilePathDestiny;
            string strFolder;
            strFolder = Server.MapPath("docs/");
            strFilePathDestiny = Server.MapPath("docs/Modified");
            // Retrieve the name of the file that is posted.
            strFileName = oFile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            if (oFile.Value != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }

                if (!Directory.Exists(strFilePathDestiny))
                {
                    Directory.CreateDirectory(strFilePathDestiny);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;
                if (File.Exists(strFilePath))
                {
                    lblUploadResult.Text = strFileName + " Archivo Duplicado";
                }
                else
                {
                    oFile.PostedFile.SaveAs(strFilePath);
                    try
                    {
                        using (PdfDocument pdfDocument = PdfDocument.Open(strFilePath))
                        {
                            int imageCount = 1;

                            foreach (Page page in pdfDocument.GetPages())
                            {
                                List<XObjectImage> images = page.GetImages().Cast<XObjectImage>().ToList();
                                foreach (XObjectImage image in images)
                                {
                                    byte[] imageRawBytes = image.RawBytes.ToArray();

                                    using (FileStream stream = new FileStream($"{strFilePathDestiny}\\{imageCount}.png", FileMode.Create, FileAccess.Write))
                                    using (BinaryWriter writer = new BinaryWriter(stream))
                                    {
                                        writer.Write(imageRawBytes);
                                        writer.Flush();
                                    }


                                    imageCount++;
                                }
                            }
                        }
                        Console.WriteLine("Imagenes generadas");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    lblUploadResult.Text = strFileName + " Archivo Subido.";


                }
            }
            else
            {
                lblUploadResult.Text = "Click 'Browse' to select the file to upload.";
            }
            // Display the result of the upload.
            frmConfirmation.Visible = true;
        }
    }
}