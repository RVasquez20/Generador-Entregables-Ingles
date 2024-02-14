using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Generador
{
    public partial class Form1 : Form
    {
        private readonly string _KeyEnc = @"]1E|T2_Z\Za8B+,)&6aNi<";
        private readonly string Nombre = "Hector Rodrigo Vasquez Morales";
        private readonly string Id = "0764-19-2634";
        private readonly String[] TipoEntrega = new String[] { "QUIZ", "SKILL BOOST", "PRACTICE", "WORKBOOK" };
        private readonly String[] Nivel = new String[] { "Beginner 1", "Beginner 2", "Beginner 3", "Beginner 4",
        "INTERMEDIATE 1","INTERMEDIATE 2","INTERMEDIATE 3","INTERMEDIATE 4",
        "ADVANCED 1","ADVANCED 2","ADVANCED 3","ADVANCED 4"};
        private List<string> IdTareas = new List<string>();
        private string jsonFileName = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si el usuario presionó Ctrl+V
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsImage())
                {
                    Image image = Clipboard.GetImage();
                    string imagePath = SaveImageFromClipboard(image);
                    txt_Imagenes.AppendText(imagePath + Environment.NewLine);
                }
            }
        }
        private string SaveImageFromClipboard(Image image)
        {
            string directoryPath = @"Imagenes\";
            string idTarea = Guid.NewGuid().ToString();
            IdTareas.Add(idTarea);
            string imageName = $"Imagen_{DateTime.Now.ToString("dd-MM-yyyy")}_{idTarea}.png";
            string imagePath = Path.Combine(directoryPath, imageName);

            // Asegúrate de que el directorio exista
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Guarda la imagen en el directorio y devuelve la ruta del archivo
            image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

            return imagePath;
        }

        private void GenerarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Revisa si todos los campos requeridos están llenos antes de proceder
            if (!ValidarControles())
            {
                MessageBox.Show("Por favor, complete todos los campos antes de generar el PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveJsonEnc();
            TareaInformacion tareaInformacions = this.LeerJsonYObtenerInformacion();
            // Generar el PDF con la información recopilada
            if (!this.GenerarPDFAsync(tareaInformacions))
            {
                MessageBox.Show("Error al Generar el PDF", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private Microsoft.Reporting.WebForms.LocalReport LoadReportFromFile()
        {
            Microsoft.Reporting.WebForms.LocalReport report = new Microsoft.Reporting.WebForms.LocalReport();
            report.ReportPath = "FormatoTareasIngles.rdl";
            return report;
        }
        private bool GenerarPDFAsync(TareaInformacion tareaInformacion)
        {
            try
            {
                //Create the report object
                Microsoft.Reporting.WebForms.LocalReport report = LoadReportFromFile();

                //create the dataset that we will attach to the report
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
                //Set dummy values for report parameters so the report doesn't blow up
                //NOTE even if hidden parameters you need to have a prompt in the report file so that they can be set here
                report.SetParameters(new List<Microsoft.Reporting.WebForms.ReportParameter> {
                        new Microsoft.Reporting.WebForms.ReportParameter("Nivel", tareaInformacion.Nivel),
                        new Microsoft.Reporting.WebForms.ReportParameter("Carne", tareaInformacion.Id),
                        new Microsoft.Reporting.WebForms.ReportParameter("Nombre", tareaInformacion.Nombre),
                        new Microsoft.Reporting.WebForms.ReportParameter("Fecha", DateTime.Now.ToString("MM-dd-yyyy")),
                        new Microsoft.Reporting.WebForms.ReportParameter("TipoActividad", tareaInformacion.TipoEntrega),
                        new Microsoft.Reporting.WebForms.ReportParameter("Imagen", ConvertImageToBase64(tareaInformacion.Imagenes[0]))


                    });
                //Create the PDF of the report
                var deviceInfo = @"<DeviceInfo>
                            <EmbedFonts>None</EmbedFonts>
                            </DeviceInfo>";

                byte[] mybytes = report.Render("PDF", deviceInfo);
                //Write out PDF file
                string data = $"{tareaInformacion.TipoEntrega}_{DateTime.Now:dd-MM-yyyy}_{tareaInformacion.Nivel}.pdf";
                ShowPDF(mybytes,data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        private void ShowPDF(Byte[] mybytes,string data )
        {
            if (mybytes != null && mybytes.Length > 0)
            {
                //You may need to pick a different folder to write to if c:\temp doesn't exist
                using (FileStream fs = File.Create(data))
                {
                    fs.Write(mybytes, 0, mybytes.Length);
                }

                //Now we open the pdf file we just created
                System.Diagnostics.Process.Start(data);
                txt_Imagenes.Clear();
            this.IdTareas.Clear();
                this.jsonFileName = string.Empty;
            }
        }
        private string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }


        private void SaveJsonEnc()
        {
            // Crea la instancia del objeto con los datos recogidos
            TareaInformacion tarea = new TareaInformacion
            {
                Nombre = Nombre, // Suponiendo que tienes una variable o propiedad para esto
                Id = Id, // Suponiendo que tienes una variable o propiedad para esto
                TipoEntrega = cb_TipoEntrega.SelectedItem.ToString(),
                Nivel = cb_Nivel.SelectedItem.ToString(),
                Imagenes = txt_Imagenes.Lines.ToList() // Las líneas del TextBox contienen las rutas de las imágenes
            };

            // Serializa el objeto a JSON
            string json = JsonConvert.SerializeObject(tarea);

            // Encripta el JSON
            byte[] encryptedData = EncryptStringToBytes_Aes(json, _KeyEnc);

            // Define el nombre del archivo con el formato especificado
            jsonFileName = $"{tarea.TipoEntrega}_{DateTime.Now:dd-MM-yyyy}_{tarea.Nivel}.json.enc";

            // Guarda el JSON encriptado en un archivo
            File.WriteAllBytes(jsonFileName, encryptedData);

            MessageBox.Show("Información guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Asegúrate de tener los métodos EncryptStringToBytes_Aes y DecryptStringFromBytes_Aes implementados como en el proyecto anterior.


        private void Imagenes_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Imagenes_DragLeave(object sender, EventArgs e)
        {

        }

        private void Imagenes_DragDrop(object sender, DragEventArgs e)
        {
            // Obtener la lista de archivos arrastrados al control.
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            StringBuilder fileList = new StringBuilder();

            // Recorrer todos los archivos para agregar solo los de imagen al TextBox.
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLowerInvariant();
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    fileList.AppendLine(file);
                }
            }

            // Establecer el texto del TextBox a los nombres de archivo acumulados.
            txt_Imagenes.Text = fileList.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cb_TipoEntrega.Items.AddRange(TipoEntrega);
            this.cb_Nivel.Items.AddRange(Nivel);

            this.cb_TipoEntrega.SelectedIndex = 0;
            this.cb_Nivel.SelectedIndex = 0;
            Imagenes.AllowDrop = true;
            Imagenes.DragEnter += new DragEventHandler(Imagenes_DragEnter);
            Imagenes.DragDrop += new DragEventHandler(Imagenes_DragDrop);
        }
        private byte[] EncryptStringToBytes_Aes(string plainText, string keyString)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave proporcionada es nula o vacÓa", nameof(keyString));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(keyString);
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Prepend the IV to the ciphertext
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherTextWithIv, string keyString)
        {
            if (cipherTextWithIv == null || cipherTextWithIv.Length <= 0)
                throw new ArgumentNullException(nameof(cipherTextWithIv));
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave proporcionada es nula o vacÓa", nameof(keyString));

            // Declarar la cadena para contener el texto desencriptado.
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(keyString);

                int ivLength = BitConverter.ToInt32(cipherTextWithIv, 0);
                byte[] iv = new byte[ivLength];
                Buffer.BlockCopy(cipherTextWithIv, sizeof(int), iv, 0, iv.Length);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Crea los flujos necesarios para la desencriptaciÓn.
                using (MemoryStream msDecrypt = new MemoryStream(cipherTextWithIv, sizeof(int) + iv.Length, cipherTextWithIv.Length - sizeof(int) - iv.Length))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Lee los bytes desencriptados del flujo de desencriptaciÓn y los coloca en una cadena.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        private static byte[] AdjustKeySize(string keyString, int size = 32)
        {
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave no puede estar vacia");

            byte[] keyBytes = new byte[size];
            byte[] temporaryKeyBytes = Encoding.UTF8.GetBytes(keyString);

            Array.Copy(temporaryKeyBytes, keyBytes, Math.Min(temporaryKeyBytes.Length, size));

            return keyBytes;
        }
        private bool ValidarControles()
        {
            // Verificar que el Tipo de Entrega esté seleccionado
            if (cb_TipoEntrega.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Tipo de Entrega.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que el Nivel esté seleccionado
            if (cb_Nivel.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Nivel.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que haya al menos un nombre de imagen en txt_Imagenes
            if (string.IsNullOrWhiteSpace(txt_Imagenes.Text))
            {
                MessageBox.Show("Por favor, asegúrese de haber agregado al menos una imagen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Todos los controles tienen la información requerida
        }
        public TareaInformacion LeerJsonYObtenerInformacion()
        {
            // Leer los bytes encriptados del archivo
            byte[] encryptedData = File.ReadAllBytes(jsonFileName);

            // Desencriptar el JSON
            string jsonDesencriptado = DecryptStringFromBytes_Aes(encryptedData, _KeyEnc);

            // Deserializar el JSON a un objeto TareaInformacion
            TareaInformacion tareaInformacion = JsonConvert.DeserializeObject<TareaInformacion>(jsonDesencriptado);

            return tareaInformacion;
        }



    }
}