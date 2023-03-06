using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResolucionParcial
{
    class Program
    {
        static void Main(string[] args)
        {
            Antropologo ant1 = new Antropologo { Dni=23456789,Apellido="Gomez",Nombres="Ricardo", AñoInicio=2000, Domicilio="soler 233" ,Matricula="ant-01"};
            Antropologo ant2 = new Antropologo { Dni = 13456741, Apellido = "Lopez", Nombres = "Marcelo", AñoInicio = 2010, Domicilio = "Mitre 33", Matricula = "ant-0100" };
            Antropologo ant3 = new Antropologo { Dni = 18456014, Apellido = "Martinez", Nombres = "Mauricio", AñoInicio = 2003, Domicilio = "Marconi 95", Matricula = "ant-201" };
            Antropologo ant4 = new Antropologo { Dni = 11456654, Apellido = "Soto", Nombres = "Esteban", AñoInicio = 2001, Domicilio = "Rawson 120", Matricula = "ant-401" };

            Arqueologo arq1 = new Arqueologo { Dni = 32951123, Apellido = "Moron", Nombres = "Martin", AñoInicio = 2008, Domicilio = "Italia 12", Matricula = "arq-01" };
            Arqueologo arq2 = new Arqueologo { Dni = 22357852, Apellido = "Portella", Nombres = "Gimena", AñoInicio = 2011, Domicilio = "Moreno 10", Matricula = "arq-21" };
            Arqueologo arq3 = new Arqueologo { Dni = 10954789, Apellido = "Morillo", Nombres = "Gabriela", AñoInicio = 2007, Domicilio = "Marconi 200", Matricula = "arq-101" };
            Arqueologo arq4 = new Arqueologo { Dni = 17232456, Apellido = "Gimenez", Nombres = "Analia", AñoInicio = 2012, Domicilio = "Colombia 1110", Matricula = "arq-301" };

            Paleontologo pal1 = new Paleontologo { Dni = 27951123, Apellido = "Arnaiz", Nombres = "Morena", AñoInicio = 2009, Domicilio = "Av Fontana 1022", Matricula = "pal-01-101" };
            Paleontologo pal2 = new Paleontologo { Dni = 28311832, Apellido = "Vale", Nombres = "Cecilia", AñoInicio = 2017, Domicilio = "25 de mayo 678", Matricula = "pal-01-102" };
            Paleontologo pal3 = new Paleontologo { Dni = 20456345, Apellido = "Ingravallo", Nombres = "Francisco", AñoInicio = 2002, Domicilio = "Ropndeau 456", Matricula = "pal-21-101" };
            Paleontologo pal4 = new Paleontologo { Dni = 25345456, Apellido = "Lopez", Nombres = "Matias", AñoInicio = 2010, Domicilio = "Soler 345", Matricula = "pal-04-101" };

            Campaña camp1 = new Campaña("Los Altares",500, Convert.ToDateTime("25/07/2022"));
            camp1.AddProfesional(ant1);
            camp1.AddProfesional(ant2);
            camp1.AddProfesional(ant3);
            camp1.AddProfesional(ant4);

            camp1.AddProfesional(arq1);
            camp1.AddProfesional(arq2);
            camp1.AddProfesional(arq3);
            camp1.AddProfesional(arq4);

            camp1.AddProfesional(pal1);
            camp1.AddProfesional(pal2);
            camp1.AddProfesional(pal3);
            camp1.AddProfesional(pal4);

            camp1.AddRecorrido(pal1);
            camp1.AddRecorrido(arq1);
            camp1.AddRecorrido(ant1);

            camp1.Sort(SortTipo.SortDNI);
            foreach (Profesional item in camp1)
            {
                Console.WriteLine(String.Format("{0} - {1} - {2} - Recorridos:{3} - Cant.Max Recorridos: {4}", item.Dni, item.Apellido + " " + item.Nombres, item.Matricula, item.CountRecorrido, camp1.CantidadRecorridos * item.CantRecorridos));
            }

            camp1.AddRecorrido(pal1);
            camp1.AddRecorrido(pal1);

            foreach (Profesional item in camp1)
            {
                Console.WriteLine(String.Format("{0} - {1} - {2} - Recorridos:{3} - Cant.Max Recorridos: {4}", item.Dni, item.Apellido + " " + item.Nombres, item.Matricula, item.CountRecorrido, camp1.CantidadRecorridos * item.CantRecorridos));
            }
            Console.ReadKey();

        }
    }
    delegate int ComparaProfesional(Profesional a,Profesional b);
    abstract class Profesional:IComparable
    {
        private static ComparaProfesional Compara = Profesional.ComparaXDni;
        public int Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Domicilio { get; set; }
        public string Matricula{ get; set; }
        public int AñoInicio { get; set; }
        abstract public double CantRecorridos { get; }
        public int CountRecorrido { get; set; }
        public int CompareTo(object obj)
        {
            return Profesional.Compara(this, obj as Profesional);
        }
        private static int ComparaXTipo(Profesional a, Profesional b)
        {
            return a.GetType().ToString().CompareTo(b.GetType().ToString());
        }
        private static int ComparaXDni(Profesional a, Profesional b)
        {
            return a.Dni.CompareTo(b.Dni);
        }
        public static void SetSortTipo(SortTipo tipo)
        {
            if (tipo == SortTipo.SortDNI)
                    Compara = ComparaXDni;
            if (tipo == SortTipo.SortTipo)
                Compara = ComparaXTipo;
        }
        public void AddRecorido(int MaxRecorrido)
        {
            if (CountRecorrido < CantRecorridos * MaxRecorrido)
                CountRecorrido++;
        }
    }

    class Antropologo:Profesional
    {
        
        public override double CantRecorridos
        {
            get{return Campaña.MaxRecorridoAntropologos; }            
        }
    }
    class Arqueologo:Profesional
    {
        public override double CantRecorridos
        {
            get { return Campaña.MaxRecorridoArquelogos; }
        }
    }
    class Paleontologo: Profesional
    {
        public override double CantRecorridos
        {
            get { return Campaña.MaxRecorridoPaleontologos; }
        }
    }
    enum SortTipo { SortDNI, SortTipo }
    class Campaña :IEnumerable,IEnumerator
    {
        private List<Profesional> Profesionales;
        private int indice=-1;

        public const double MaxRecorridoArquelogos = 0.1;
        public const double MaxRecorridoAntropologos = 0.3;
        public const double MaxRecorridoPaleontologos = 0.6;


        public int Codigo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadRecorridos { get; set; }
        public string LugarCampaña { get; set; }
        
        public void AddProfesional(Profesional prof)
        {
            Profesionales.Add(prof);
        }
        public void Sort(SortTipo tipo)
        {
            Profesional.SetSortTipo(tipo);
            Profesionales.Sort();
        }

        public void AddRecorrido(Profesional prof)
        {
            Profesional p = Profesionales.Find(pr => pr.Matricula == prof.Matricula);
            if(p != null)
              p.AddRecorido(this.CantidadRecorridos);
        }

        public Campaña(string Lugar, int CantidadRecorridosCampaña, DateTime FechaInicioCapaña)
        {
            LugarCampaña = Lugar;
            CantidadRecorridos = CantidadRecorridosCampaña;
            FechaInicio = FechaInicioCapaña;
            Profesionales = new List<Profesional>(); 
        }

        public Profesional this[int index]{
            get {
                if(index>=0 && index<=Profesionales.Count-1)
                    return Profesionales[index];
                return null;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public object Current
        {
            get { return this[indice]; }
        }

        public bool MoveNext()
        {
            if (indice == Profesionales.Count - 1)
            {
                indice = -1;
                return false;
            }
            indice++;
            return true;
        }

        public void Reset()
        {
            indice = -1;
        }
    }
}
