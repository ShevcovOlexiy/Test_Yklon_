using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using System.Xml;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        static List<ClassCountry> Countrys = ClassCountry.DefaultListCountry();
        static Hashtable listH = ClassToun.CreateListToun();
        static List<ClassToun> Toun = ClassToun.DefaultListToun(listH, "225");

        static int GetRecvest( string regionCode, string timeStamp,ref string timeStr, ref string ColorStr, ref string hintStr, ref string LevelStr, ref string titleStr)
        {
            var content = new MemoryStream();
            string url =
               String.Format("https://export.yandex.com/bar/reginfo.xml?region={0}&bustCache={1}", regionCode, timeStamp);


               // String.Format("http://hidedoor.com/servlet/redirect.srv/szx/sfnnyml/sihfhqy/p2/bar/reginfo.xml?region={0}&bustCache={1}", regionCode, timeStamp);
            var WebReq = (HttpWebRequest)WebRequest.Create(url);
            using (var resp = WebReq.GetResponse())
                using (var str=resp.GetResponseStream())
                {

                if (str != null)
                {
                    str.CopyTo(content);
                }
                else return 0;

                    content.Position = 0;
                    XmlDocument xmlDoc = new XmlDocument();
                try
                {

                    xmlDoc.Load(content);
                    string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "File.xml";

                    xmlDoc.Save(path);
                    XmlNode node = xmlDoc.DocumentElement;
                    if (node.Name == "info")
                    {
                        XmlAttributeCollection at = node.Attributes;
                        string lang = at.GetNamedItem("lang").Value;



                        XmlNodeList list = node.ChildNodes;
                        foreach (XmlNode s in list)
                        {
                            if (s.Name == "region")
                            {
                                foreach (XmlNode stitle in s.ChildNodes)
                                {
                                    if (stitle.Name == "title")
                                    { titleStr  = stitle.InnerText; }
                                }
                            }

                            if (s.Name == "traffic")
                            {
                                XmlAttributeCollection a_tr = s.Attributes;
                                //richTextBox1.AppendText("region=\"" + a_tr.GetNamedItem("region").Value + "\"\n");

                                foreach (XmlNode s1 in s.ChildNodes)
                                {
                                    if (s1.Name == "region")
                                    {
                                        XmlAttributeCollection a_reg = s1.Attributes;
                                        if (a_reg.GetNamedItem("id").Value != regionCode)
                                            return content.ToArray().Length;

                                        foreach (XmlNode s2 in s1.ChildNodes)
                                        {

                                            if (s2.Name == "level")
                                            {

                                                LevelStr = s2.InnerText;
                                            }

                                            if (s2.Name == "icon")
                                            {
                                                ColorStr = s2.InnerText;
                                            }

                                            if (s2.Name == "hint")
                                            {
                                                if (s2.Attributes.GetNamedItem("lang").Value == lang)
                                                    hintStr = s2.InnerText;
                                            }

                                            if (s2.Name == "time")
                                            {
                                                timeStr = s2.InnerText;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                } catch (XmlException e)
                { return -1; }

            }

            return content.ToArray().Length;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ClassToun.CreateListToun();
            SelectList list = new SelectList(Countrys,"Code", "Country", Countrys[0].Code);

            SelectList list1 = new SelectList(Toun, "Code", "Toun", Toun[1].Code);
            string ss=list1.SelectedValue.ToString();
            string ts= "5";
            string timeStr = "", ColorStr = "", hintStr = "", LevelStr = "", titleStr="";
            int res=GetRecvest( ss, ts,ref timeStr , ref ColorStr,ref hintStr, ref LevelStr, ref titleStr);

            ViewBag.CountryList = list;
            ViewBag.TounList = list1;

            ViewBag.Time = timeStr;
            ViewBag.Level = LevelStr;
            ViewBag.color = ColorStr;
            ViewBag.Hint = hintStr;
            ViewBag.TitleToun = titleStr;
            //if(res>0)
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Select1, string Select2)
        {
            ClassToun.CreateListToun();
            SelectList list = new SelectList(Countrys, "Code", "Country", Select1);
            Toun = ClassToun.DefaultListToun(listH, Select1);
            bool ret = false;
            foreach(ClassToun buff in Toun)
            {
                if (buff.Code == Select2)
                    ret = true;

            }
            SelectList list1 = new SelectList(Toun, "Code", "Toun", (!ret) ? Toun[0].Code : Select2);
        string ss = list1.SelectedValue.ToString();
        string ts = "5";
            string timeStr = "", ColorStr = "", hintStr = "", LevelStr = "", titleStr = "";
            int res = GetRecvest(ss, ts, ref timeStr, ref ColorStr, ref hintStr, ref LevelStr, ref titleStr);

            ViewBag.CountryList = list;
            ViewBag.TounList = list1;

            ViewBag.Time = timeStr;
            ViewBag.Level = LevelStr;
            ViewBag.color = ColorStr;
            ViewBag.Hint = hintStr;
            ViewBag.TitleToun = titleStr;
            if (res==-1)
            ViewBag.Error = "Данные какимто образом не коректны"; 
            return View();
        }

    }
}