using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UpdateGenerator
{
    public partial class UpdateGenerator : Form
    {
        /// <summary>
        /// FileHelper 中, 文件MD5计算错误时 的默认值
        /// </summary>
        public const string ERROR_FILE_MD5 = "XXXXXXXXXXXXXXXXXXXX";
        XMLProcess updateXml = new XMLProcess("update.xml");
        Color COLORDEFAULT = Color.Black;				// default color in text-output panel
        Color COLORCOMMAND = Color.DarkGreen;			// ftp command color
        Color COLORERROR = Color.Red;				// color of error messages	
        int count = 0;//文件总数量,当前XML文件段
        int fileNum = 0;//文件总数量
        int root = 0;//根目录位置
        BackgroundWorker _XmlgroundWorker = new BackgroundWorker();
        public UpdateGenerator()
        {
            InitializeComponent();
            _XmlgroundWorker = new BackgroundWorker();
            _XmlgroundWorker.WorkerSupportsCancellation = true;
            _XmlgroundWorker.DoWork += _XmlgroundWorkerOnDoWork;
            _XmlgroundWorker.RunWorkerCompleted += _XmlgroundWorkerComplete;
        }
        private void _XmlgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (GetFileNum(_Path.Text) != 0)
                {
                    count = 0;
                    fileNum = 0;
                    progressBar1.Invoke((MethodInvoker)delegate
                      {
                          progressBar1.Maximum = GetFileNum(_Path.Text);
                          progressBar1.Value = 0;
                          GetDirectoryFileToXML(_Path.Text);
                          // WriteToLog("生成XML升级文档完成！", COLORCOMMAND);
                      });
                }
                else
                {
                    tbLog.Invoke((MethodInvoker)delegate
                    {
                        WriteToLog("空的文件夹，请重新选择！", COLORERROR);
                    });
                }
            }
            catch (Exception err)
            {
                tbLog.Invoke((MethodInvoker)delegate
                {
                    WriteToLog("空的文件夹，请重新选择！", COLORERROR);
                });
                //return;
            }
        }
        private void _XmlgroundWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            count = 0;//文件总数量,当前XML文件段
            fileNum = 0;//文件总数量
            root = 0;//根目录位置
            WriteToLog("生成XML文档完成：" + _Path.Text + "\\update.xml", COLORCOMMAND);
        }
        public void WriteToLog(string message)
        {
            WriteToLog(message, COLORDEFAULT, true);
        }

        public void WriteToLog(string message, Color color)
        {
            WriteToLog(message, color, true);
        }

        public void WriteToLog(string message, bool crlf)
        {
            WriteToLog(message, COLORDEFAULT, crlf);
        }

        public void WriteToLog(string message, Color color, bool crlf)
        {
            tbLog.Focus();
            tbLog.AppendText("");
            tbLog.SelectionColor = color;
            tbLog.AppendText(message + (crlf ? "\r\n" : null));
        }
        /// <summary>
        /// 计算文件的MD5, 计算错误将返回 XXXXXXXXXXXXXXXXXXXX
        /// </summary>
        public static string GetFileMD5(string path)
        {
            if (!File.Exists(path)) return ERROR_FILE_MD5;

            try
            {
                using (FileStream myFs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MD5 md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] hash = md5.ComputeHash(myFs);
                        myFs.Close();

                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("x2"));
                        return sb.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return ERROR_FILE_MD5;
            }
        }
        private void _ButPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (GetFileNum(dialog.SelectedPath) == 0)
                    {
                        tbLog.Clear();
                        WriteToLog("空的文件夹，请重新选择！", COLORERROR);
                        return;
                    }
                    _Path.Text = dialog.SelectedPath;
                    root = _Path.Text.IndexOf(_Path.Text.Substring(_Path.Text.LastIndexOf(@"\"))) + 1;
                    WriteToLog("文件夹正常，准备就绪！", COLORCOMMAND);
                }
            }
            catch (Exception)
            {
                WriteToLog("空的文件夹，请重新选择！", COLORERROR);
            }
        }
        /// <summary> 获取所有目录中文件，生成XML
        /// </summary>
        /// <param name="src">文件路径</param>
        public long GetDirectoryFileToXML(String dirPath)
        {

            long len = 0;
            string fileName;
            string filePath;
            //判断该路径是否存在（是否为文件夹）
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    //查询文件的大小
                    //len = FileSize(dirPath);
                    MessageBox.Show("请选择正确的目录！");
                    return 0;
                }
                else
                {
                    //定义一个DirectoryInfo对象
                    DirectoryInfo di = new DirectoryInfo(dirPath);

                    //通过GetFiles方法，获取di目录中的所有文件的大小
                    string fileVer = "0.0.0.0";
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        len += fi.Length;
                        fileName = fi.Name;
                        count = count + 1;
                        filePath = fi.DirectoryName;
                        //MessageBox.Show(fi.FullName);
                        System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(fi.FullName);
                        //MessageBox.Show("文件1：" + i + "最大:" + filePath.Length);
                        fileVer = "v"+info.FileVersion;
                        if (fileVer.Trim()==null||fileVer.Trim()==""||fileVer=="v")
                        {
                            fileVer = "0.0.0.0";
                        }
                        else
                        {
                            fileVer = info.FileVersion;
                        }
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            WriteToLog(count + "_文件名称:" + fileName, COLORCOMMAND);
                            //MessageBox.Show("目录1：" + filePath);
                            WriteToLog(count + "_所在目录：" + filePath, COLORERROR);
                            progressBar1.Value = progressBar1.Value + 1;
                            //生成配置文件开始  info.FileVersion    
                        });
                        //MessageBox.Show(info.FileVersion);
                        XMLProcess.Insert("update.xml", "updateinformations", "FileCount_" + count, "", "");
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "FileName", "", fileName);
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "FilePath", "", filePath.Substring(root, filePath.Length - root));
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "FileVersion", "", fileVer);
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "FileSize", "", fi.Length.ToString());
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "CreationTime", "", fi.CreationTime.ToString());
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "ModificationTime", "", fi.LastWriteTime.ToString());
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "AccessTime", "", fi.LastAccessTime.ToString());
                        XMLProcess.Insert("update.xml", "/updateinformations/FileCount_" + count, "FileInformation", "", GetFileMD5(filePath + "\\" + fileName));
                    }
                    //获取dir中所有的文件夹，并存到一个新的对象数组中，以进行递归
                    DirectoryInfo[] dis = di.GetDirectories();
                    if (dis.Length > 0)
                    {
                        for (int i = 0; i < dis.Length; i++)
                        {
                            len += GetDirectoryFileToXML(dis[i].FullName);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("取文件写入XML文件失败：" + err);
            }
            return len;
        }
        private void _generatedFile_Click(object sender, EventArgs e)
        {
            if (_Path.Text == "")
            {
                WriteToLog("空的文件夹，请重新选择！", COLORERROR);
                return;
            }
            try
            {
                tbLog.Clear();
                if (File.Exists("update.xml"))
                    File.Delete("update.xml");

                XmlDocument xmlDoc = new XmlDocument();
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                xmlDoc.AppendChild(node);
                //加入根元素
                XmlElement root = xmlDoc.CreateElement("updateinformations");
                xmlDoc.AppendChild(root);
                xmlDoc.Save("update.xml");

                _XmlgroundWorker.RunWorkerAsync();
            }
            catch (Exception err)
            {
                MessageBox.Show("执行清除并执行线程失败：" + err);
            }
            /*
            XMLProcess.Insert("update.xml", "updateinformations", "b.txt", "", "");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "FilePath", "", "d:\\");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "FileSize", "", "1024");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "CreationTime", "", "11:11:11");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "ModificationTime", "", "12:12:12");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "AccessTime", "", "13:13:13");
            XMLProcess.Insert("update.xml", "/updateinformations/b.txt", "MD5", "", "");
             */

        }
        /// <summary>获取某目录下的所有文件(包括子目录下文件)的数量
        /// </summary>
        /// <param name="srcPath">目录</param>
        public int GetFileNum(string srcPath)
        {
            try
            {
                //MessageBox.Show("GetFileNum-TRA位置：" + srcPath);
                // if (srcPath.Length>0)
                // {
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                foreach (string file in fileList)
                {
                    if (System.IO.Directory.Exists(file))
                        GetFileNum(file);
                    else
                        fileNum++;
                    //  }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("读取文件数量失败：" + e.ToString());
            }
            return fileNum;
        }

    }
    class MyProgressBar : ProgressBar //新建一个MyProgressBar类，它继承了ProgressBar的所有属性与方法
    {
        public MyProgressBar()
        {
            base.SetStyle(ControlStyles.UserPaint, true);//使控件可由用户自由重绘
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                SolidBrush brush = null;
                Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 1, 1, bounds.Width - 2, bounds.Height - 2);//此处完成背景重绘，并且按照属性中的BackColor设置背景色
                bounds.Height -= 4;
                bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum)))) - 4;//是的进度条跟着ProgressBar.Value值变化
                brush = new SolidBrush(this.ForeColor);
                e.Graphics.FillRectangle(brush, 2, 2, bounds.Width, bounds.Height);//此处完成前景重绘，依旧按照Progressbar的属性设置前景色
            }
            catch (Exception err)
            {
                MessageBox.Show("空的文件夹：" + err);
            }
        }
    }
}
