using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class ClassCountry
    {
        public string Code { get; set; }
        public string Country { get; set; }
        static public List<ClassCountry> DefaultListCountry()
        { return new List<ClassCountry>()
           {
            new ClassCountry(){ Code="225", Country ="Россия" },
            new ClassCountry() { Code = "7", Country = "Америка" },
            new ClassCountry() { Code = "120", Country = "Белорусь" },
            new ClassCountry() { Code = "149", Country = "Украина" }
            };
        }

    }

    public class ClassToun
    {
        public string Code { get; set; }
        public string Toun { get; set; }


        

        static public Hashtable CreateListToun()
        {

            Hashtable list = new Hashtable();

                list.Add("225", new List<ClassToun>() {
            new ClassToun(){ Code="213", Toun ="Москва" },
            new ClassToun() { Code = "2", Toun = "Санкт-Петербург" },
            new ClassToun() { Code = "10765", Toun = "Щелково" },
            new ClassToun() { Code = "21622", Toun = "Железнодорожный" },
            new ClassToun() { Code = "10725", Toun = "Домодедово" },
            new ClassToun() { Code = "11015", Toun = "Республика Калмыкия" }
            });
                list.Add("7", new List<ClassToun>() {
            new ClassToun(){ Code="202", Toun ="Нью-Йорк" },
            new ClassToun() { Code = "223", Toun = "Бостон" },
            new ClassToun() { Code = "86", Toun = "Атланта" }
            });
                list.Add("120", new List<ClassToun>() {
            new ClassToun(){ Code="157", Toun ="Минск" },
            new ClassToun() { Code = "155", Toun = "Гомель" }
            });
                list.Add("149", new List<ClassToun>() {
            new ClassToun(){ Code="145", Toun ="Одесса" },
            new ClassToun() { Code = "960", Toun = "Запорожье" },
            new ClassToun() { Code = "10367", Toun = "Мелитополь" },
            new ClassToun() { Code = "143", Toun = "Киев" }
            
            });

            return list;
            
        }
        static public List<ClassToun> DefaultListToun(Hashtable list,string Key)
        {
            if (list == null)
                return new List<ClassToun>() { new ClassToun() { Code = "-", Toun = "Города отсутствуют" } };

            if (list.ContainsKey(Key))
                return ((List < ClassToun >)list[Key]);
            else
                return new List<ClassToun>() { new ClassToun() { Code = "-", Toun = "Города отсутствуют" } };
        }

    }

} 