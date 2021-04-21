using VladASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladASP
{
    public class InitialData
    {


        public static List<Destination> destinations = new List<Destination>
        {
            new Destination {Name="Chisinau",Cod="KIV"},
            new Destination {Name="Tokyo",Cod="TYO"},
            new Destination {Name="Kyoto",Cod="KTO"},
            new Destination {Name="Munchen",Cod="MUC"},
            new Destination {Name="Hamburg",Cod="HMB"},
            new Destination {Name="Berlin",Cod="BRN"},
            new Destination {Name="Paris",Cod="PRS"},
            new Destination {Name="Nice",Cod="NCE"},
            new Destination {Name="New York",Cod="NY"},
            new Destination {Name="Los Angeles",Cod="LAS"},
            new Destination {Name="Houston",Cod="HST"}
        };

        public static Dictionary<string, Destination> dicDestination
        {
            get
            {
                Dictionary<string, Destination> dic = new Dictionary<string, Destination>();
                foreach (var el in destinations)
                    dic.Add(el.Cod, el);
                return dic;

            }
        }
        public static List<Source> sources = new List<Source>
        {
            new Source{Name="Chisinau",Cod="KIV"},
            new Source {Name="Tokyo",Cod="TYO"},
            new Source {Name="Kyoto",Cod="KTO"},
            new Source {Name="Munchen",Cod="MUC"},
            new Source {Name="Hamburg",Cod="HMB"},
            new Source {Name="Berlin",Cod="BRN"},
            new Source {Name="Paris",Cod="PRS"},
            new Source  {Name="Nice",Cod="NCE"},
            new Source {Name="New York",Cod="NY"},
            new Source {Name="Los Angeles",Cod="LAS"},
            new Source {Name="Houston",Cod="HST"}
        };

        public static Dictionary<string, Source> dicSource
        {
            get
            {
                Dictionary<string, Source> dic = new Dictionary<string, Source>();
                foreach (var el in sources)
                    dic.Add(el.Cod, el);
                return dic;

            }
        }

        public static List<Flyght> flyghts = new List<Flyght>
        {
            new Flyght{Price=231,Source=dicSource["KIV"],Destination=dicDestination["LAS"],
                DateSource=new DateTime(2020, 5, 27,11, 15, 25),DataDest=new DateTime(2020, 5, 27,14, 30, 25),
                Cod="KIV-LAS",Favorite=true },
            new Flyght{Price=345,Source=dicSource["TYO"],Destination=dicDestination["KIV"],
                DateSource=new DateTime(2021, 5, 26,11, 15, 25),Cod="TYO-KIV",
                DataDest=new DateTime(2021, 5, 27,14, 30, 25),Favorite=true},

             new Flyght{Price=411,Source=dicSource["NY"],Destination=dicDestination["KIV"],
                DateSource=new DateTime(2022, 5, 26,11, 15, 25),Cod="NY-KIV",Favorite=true,
                 DataDest=new DateTime(2022, 5, 27,14, 30, 25)},
              new Flyght{Price=411,Source=dicSource["KIV"],Destination=dicDestination["NY"],
                DateSource=new DateTime(2022, 5, 26,17, 15, 25),DataDest=new DateTime(2022, 5, 27,18, 30, 25),
                  Cod="KIV-NY",Favorite=true},
           /* new Flyght{Price=678,Source=dicSource["BRN"],Destination=dicDestination["LAS"],
                Date=new DateTime(2021, 5, 25,11, 15, 25),Cod="BRN-LAS",favorite=false},
            new Flyght{Price=876,Source=dicSource["NCE"],Destination=dicDestination["RM"],
                Date=new DateTime(2021, 5, 24,11, 15, 25),Cod="NCE-RM",favorite=false},
            new Flyght{Price=241,Source=dicSource["LAS"],Destination=dicDestination["KIV"],
                Date=new DateTime(2021, 5, 23,11, 15, 25),Cod="LAS-KIV",favorite=false},
            new Flyght{Price=999,Source=dicSource["KIV"],Destination=dicDestination["RM"],
                Date=new DateTime(2021, 5, 22,11, 15, 25),Cod="KIV-RM",favorite=false},
            new Flyght{Price=111,Source=dicSource["KIV"],Destination=dicDestination["HST"],
                Date=new DateTime(2021, 5, 17,11, 15, 25),Cod="KIV-HST",favorite=false} */
           
        };
        public static Dictionary<string, Flyght> dicFlyght
        {
            get
            {
                Dictionary<string, Flyght> dic = new Dictionary<string, Flyght>();
                foreach (var el in flyghts)
                    dic.Add(el.Cod, el);
                return dic;
            }
        }
        //Intializare client
        public static List<Client> ListClients = new List<Client>
                {new Client {Name="Vasile Vlaicu",Password="1",Login="vasea", Email="vlaicu@mail.ru",Mobile=06989433},
                new Client {Name="Johny Deep",Password="1",Login="ion", Email="deep@mail.ru",Mobile=06945434},
                new Client {Name="Victor Leman",Password = "1",Login="victor", Email="leman@mail.ru",Mobile=06435456},
                new Client {Name="Harry Truman",Password = "1", Login="harry",Email="truman@mail.ru",Mobile=06326345},
                new Client {Name="Ina Lucescu",Password = "1", Login="ina",Email="lucescu@mail.ru",Mobile=068454543}

                };



        // Dictionarul client
        public static Dictionary<string, Client> ConClients
        {
            get
            {
                Dictionary<string, Client> obj = new Dictionary<string, Client>();
                foreach (var el in ListClients)
                {
                    obj.Add(el.Login, el);
                }
                return obj;
            }
        }

        public static List<Order> orders = new List<Order>
        {
            new Order {Client=ConClients["ion"],Flyght=dicFlyght["KIV-LAS"]},
        };
        public static void Initialize(FlyContext db)
        {

            if (!db.clients.Any())
                db.clients.AddRange(ListClients);

            if (!db.destinations.Any())
                db.destinations.AddRange(destinations);
            if (!db.sources.Any())
                db.sources.AddRange(sources);
            if (!db.flyghts.Any())
                db.flyghts.AddRange(flyghts);

            if (!db.orders.Any())
                db.orders.AddRange(orders);
            db.SaveChanges();

        }
    }
}