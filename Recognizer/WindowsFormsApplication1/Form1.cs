using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class ImgInfo
    {
        public int Wight { get; set; }
        public int Hight { get; set; }
        public int count { get; set; }
    
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            string processFolder = textBox4.Text;
            
            string posFileFolder = "pos_image_in";
            List<string> stringlist = new List<string>();
            //for (int i = 1; i < 42; i++)
            //{
            //    DirectoryInfo theFolder = new DirectoryInfo(string.Format(@"E:\face\人脸数据库\ORL人脸数据库\s{0}",i));
            //    foreach (FileInfo nextFile in theFolder.GetFiles())
            //    {
            //        if (nextFile.Name.Contains("bmp"))
            //        {
            //            sb.Append(string.Format("{1}/{2}/{0} 1 0 0 92 112 \r\n", nextFile.Name, s, "s" + i));
            //            count++;
            //        }
            //    }
            //}
            int minW = 5000, minH = 5000;
            List<String> posFileList = new List<string>();

            DirectoryInfo theFolder = new DirectoryInfo(processFolder + "\\" + posFileFolder);
            Random random = new Random();
            foreach (FileInfo nextFile in theFolder.GetFiles())
            {
                
                string newfileName = nextFile.Name;
                int imgW = 0, imgH = 0;
                try
                {
                    Image img = Image.FromFile(theFolder + "\\" + nextFile.Name);
                    imgW = img.Width;
                    imgH = img.Height;

                    //if (imgW < 85 || imgH < 230)
                    //    continue;

                    newfileName = string.Format("{0}_{1}.jpg", DateTime.Now.ToString("ddmmhhmmss"), random.Next());

                    //if (newfileName.IndexOf(" ") > -1)
                    //{
                    //    newfileName =  newfileName.Replace(" ", "");
                    //}

                    //保存图片
                    img.Save(processFolder + "\\pos_image\\" + newfileName);

                }
                catch
                {

                }


                posFileList.Add(string.Format("./{1}/{0}", newfileName, "pos_image"));

                if (imgW < minW)
                    minW = imgW;
                if (imgH < minH)
                    minH = imgH;

            }

            textBox2.Text = minW.ToString();
            textBox3.Text = minH.ToString();
            label1.Text = posFileList.Count.ToString();
            foreach (string str in posFileList)
            {
                stringlist.Add(string.Format("{0} 1 0 0 {1} {2}", str, minW, minH));
            }
            
            string fullPath = processFolder + "\\pos_image_0.txt";

            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.SetLength(0);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {

                    foreach (string str in stringlist)
                    {
                        sw.WriteLine(str);

                    }
                }
            }
            MessageBox.Show("completed.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            string negFileFolder = "neg_image";
            StringBuilder sb = new StringBuilder();
            this.textBox1.Text = "";
            DirectoryInfo theFolder = new DirectoryInfo(this.textBox4.Text + "\\" + negFileFolder);
            foreach (FileInfo nextFile in theFolder.GetFiles() )
            {
                sb.Append(string.Format("./{1}/{0} \r\n", nextFile.Name, negFileFolder));
                    count++;
                
            }
            this.textBox1.Text = sb.ToString();
            this.label1.Text = count.ToString();
            MessageBox.Show("completed.");
        }
    }
}
