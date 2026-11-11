using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using Control;
using MySql.Data.MySqlClient;

public class InternalHelper
{
    protected static MySqlCommand Command;

    public static string ValidateTextForMySql(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return "NULL";
        }
        return "'" + text + "'";
    }

    public static void SetFocus(System.Web.UI.Control webctrl)
    {
        var sb = new StringBuilder();

        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("<!--\r\n");
        sb.Append("function SetFocus()\r\n");
        sb.Append("{\r\n");
        sb.Append("\tif(document.");

        var p1 = webctrl.Parent;
        while (!(p1 is System.Web.UI.HtmlControls.HtmlForm))
            p1 = p1.Parent;

        sb.Append(p1.ClientID);
        sb.Append("['");
        sb.Append(webctrl.UniqueID);
        sb.Append("'] != null)\r\n");
        sb.Append("\t{\r\n");
        sb.Append("\t\tdocument.");

        var p = webctrl.Parent;
        while (!(p is System.Web.UI.HtmlControls.HtmlForm))
            p = p.Parent;

        sb.Append(p.ClientID);
        sb.Append("['");
        sb.Append(webctrl.UniqueID);
        sb.Append("'].focus();\r\n");
        sb.Append("\t}\r\n");
        sb.Append("}\r\n");
        sb.Append("window.onload = SetFocus;\r\n");
        sb.Append("// -->\r\n");
        sb.Append("</script>");

        // ReSharper disable CSharpWarnings::CS0612
        webctrl.Page.RegisterClientScriptBlock("Focus", sb.ToString());
        // ReSharper restore CSharpWarnings::CS0612
    }

    public static void SetFocus(System.Web.UI.Control webctrl, string key)
    {
        var sb = new StringBuilder();

        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("<!--\r\n");
        sb.Append("function SetFocus()\r\n");
        sb.Append("{\r\n");
        sb.Append("\tif(document.");

        var p1 = webctrl.Parent;
        while (!(p1 is System.Web.UI.HtmlControls.HtmlForm))
            p1 = p1.Parent;

        sb.Append(p1.ClientID);
        sb.Append("['");
        sb.Append(webctrl.UniqueID);
        sb.Append("'] != null)\r\n");
        sb.Append("\t{\r\n");
        sb.Append("\t\tdocument.");

        var p = webctrl.Parent;
        while (!(p is System.Web.UI.HtmlControls.HtmlForm))
            p = p.Parent;

        sb.Append(p.ClientID);
        sb.Append("['");
        sb.Append(webctrl.UniqueID);
        sb.Append("'].focus();\r\n");
        sb.Append("\t}\r\n");
        sb.Append("}\r\n");
        sb.Append("window.onload = SetFocus;\r\n");
        sb.Append("// -->\r\n");
        sb.Append("</script>");

        // ReSharper disable CSharpWarnings::CS0612
        webctrl.Page.RegisterClientScriptBlock(key, sb.ToString());
        // ReSharper restore CSharpWarnings::CS0612
    }

    public static string HentFilNavn
    {
        get
        {
            string filNavn = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Hour.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Minute.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Second.ToString(CultureInfo.InvariantCulture) +
                             "-" + DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture);
            return filNavn;
        }
    }

    public static bool ThumbnailCallback()
    {
        return false;
    }

    [Obsolete("Bør slettes ved udgangen af 2014, hvis ikke taget i brug. LCD 02.01.14", true)]
    public static ArrayList GetArrayList(string connString, string commandText)
    {
        if (commandText == null)
        {
            throw new ArgumentNullException("commandText");
        }

        var arrList = new ArrayList();
        try
        {
            using (var connection = new MySqlConnection(connString))
            {
                // Hent klubrekorder for Danmark
                connection.Open();
                commandText = "select 0+RIGHT( MAX(concat(LPAD(Format(vaegt,3),10,'0'), LPAD(fangstid,10,'0'))), 10) as ID from fangster where land='Danmark' group by art";
                Command = new MySqlCommand(commandText, connection);
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        arrList.Add(reader.GetValue(0).ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var ctrl = new SysExceptionController();
            ctrl.OnException(ex, 0);
        }

        return arrList;
    }

    public static bool DataGridReader(string connString, string commandText, DataGrid grid)
    {
        bool success;
        try
        {
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                Command = new MySqlCommand(commandText, connection);
                using (var reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        grid.Visible = true;
                        grid.DataSource = reader;
                        grid.DataBind();
                    }
                    else
                    {
                        grid.Visible = false;
                    }
                }
                success = true;
            }
        }
        catch
        {
            success = false;
        }
        return success;
    }

    public static bool DropDownListReader(string connString, string commandText, DropDownList list, string fieldListName)
    {
        bool success = true;
        try
        {
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                Command = new MySqlCommand(commandText, connection);
                using (var reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    list.DataSource = reader;
                    list.DataTextField = fieldListName;
                    list.DataValueField = fieldListName;
                    list.DataBind();
                }
            }
        }
        catch
        {
            success = false;
        }
        return success;
    }

    public static bool DropDownListReader(string connString, string commandText, DropDownList list, string fieldListName, string valueListName)
    {
        bool success = true;
        try
        {
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                Command = new MySqlCommand(commandText, connection);
                using (var reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    list.DataSource = reader;
                    list.DataTextField = fieldListName;
                    list.DataValueField = valueListName;
                    list.DataBind();
                }
            }
        }
        catch
        {
            success = false;
        }
        return success;
    }

    public static string OpretBilledFiler(string mappe, System.Web.UI.HtmlControls.HtmlInputFile fil, bool staaende)
    {
        CheckOpretMappe(mappe);
        string filnavn = HentFilNavn;

        System.Drawing.Image.GetThumbnailImageAbort myCallBack = ThumbnailCallback;
        System.Drawing.Image img = System.Drawing.Image.FromStream(fil.PostedFile.InputStream);

        // Fix error where GetThumbnailImage doesn't get unclear pictures when rezing them.
        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
        img.RotateFlip(RotateFlipType.Rotate180FlipNone);

        if (staaende)
        {
            if (img.Width > img.Height)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            if (img.Width <= 600)
            {
                fil.PostedFile.SaveAs(mappe + filnavn + ".jpg");
            }
            else
            {
                img.GetThumbnailImage(600, Convert.ToInt32(600 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            img.GetThumbnailImage(100, Convert.ToInt32(100 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + "_thumb.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return filnavn;
        }
        if (img.Width < img.Height)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        if (img.Width <= 800)
        {
            fil.PostedFile.SaveAs(mappe + filnavn + ".jpg");
        }
        else
        {
            img.GetThumbnailImage(800, Convert.ToInt32(800 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        img.GetThumbnailImage(100, Convert.ToInt32(100 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + "_thumb.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        return filnavn;
    }

    public static string OpretBilledFiler(string mappe, FileUpload uploader, bool staaende)
    {
        CheckOpretMappe(mappe);
        string filnavn = HentFilNavn;

        System.Drawing.Image.GetThumbnailImageAbort myCallBack = ThumbnailCallback;
        System.Drawing.Image img = System.Drawing.Image.FromStream(uploader.FileContent);

        // Fix error where GetThumbnailImage doesn't get unclear pictures when rezing them.
        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
        img.RotateFlip(RotateFlipType.Rotate180FlipNone);

        if (staaende)
        {

            if (img.Width > img.Height)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            if (img.Width <= 600)
            {
                uploader.SaveAs(mappe + filnavn + ".jpg");
            }
            else
            {
                img.GetThumbnailImage(600, Convert.ToInt32(600 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            img.GetThumbnailImage(100, Convert.ToInt32(100 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + "_thumb.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return filnavn;

        }
        if (img.Width < img.Height)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
        if (img.Width <= 800)
        {
            uploader.SaveAs(mappe + filnavn + ".jpg");
        }
        else
        {
            img.GetThumbnailImage(800, Convert.ToInt32(800 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        img.GetThumbnailImage(100, Convert.ToInt32(100 * img.Height / img.Width), myCallBack, IntPtr.Zero).Save(mappe + filnavn + "_thumb.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        return filnavn;
    }

    public static string CheckLinkAddress(string link)
    {
        if (link.ToUpper().Contains(@"HTTP://"))
        {
            return link.Remove(0, 7);
        }
        return link;
    }

    #region crypto functions

    const string Passphrase = "password";

    /// <summary>
    /// function to decrypt a phrase
    /// </summary>
    public static string DecryptString(string message)
    {
        byte[] results;
        var utf8 = new UTF8Encoding();
        var hashProvider = new MD5CryptoServiceProvider();
        byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(Passphrase));
        var tdesAlgorithm = new TripleDESCryptoServiceProvider
                                {
                                    Key = tdesKey,
                                    Mode = CipherMode.ECB,
                                    Padding = PaddingMode.PKCS7
                                };
        byte[] dataToDecrypt = Convert.FromBase64String(message);
        try
        {
            ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
            results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
        }
        finally
        {
            tdesAlgorithm.Clear();
            hashProvider.Clear();
        }
        return utf8.GetString(results);
    }

    /// <summary>
    /// function to encrypt a phrase
    /// </summary>
    public static string EncryptString(string message)
    {
        byte[] results;
        var utf8 = new UTF8Encoding();
        var hashProvider = new MD5CryptoServiceProvider();
        byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(Passphrase));
        var tdesAlgorithm = new TripleDESCryptoServiceProvider
                                {
                                    Key = tdesKey,
                                    Mode = CipherMode.ECB,
                                    Padding = PaddingMode.PKCS7
                                };
        byte[] dataToEncrypt = utf8.GetBytes(message);
        try
        {
            ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
            results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
        }
        finally
        {
            tdesAlgorithm.Clear();
            hashProvider.Clear();
        }
        return Convert.ToBase64String(results);
    }

    #endregion

    #region Private methods

    private static void CheckOpretMappe(string sti)
    {
        if (!Directory.Exists(sti))
        {
            Directory.CreateDirectory(sti);
        }
    }

    #endregion
}