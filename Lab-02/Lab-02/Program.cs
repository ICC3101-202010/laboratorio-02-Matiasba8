using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Program
    {
        static void Main(string[] args)
        {   
            //Menu escrito para usuario
            Console.WriteLine("Bienvenido a Spotifai, escoja una de las opciones antes de continuar (1,2,3). \n");
            Console.WriteLine("Presiona 1 para Ver todas las canciones agregadas.\n");
            Console.WriteLine("Presiona 2 para Agregar una cancion. \n");
            Console.WriteLine("Presiona 3 Ver canciones por criterio.\n");
            Console.WriteLine("Presiona 4 Crear playlist.\n");
            Console.WriteLine("Presiona 5 Ver playlist.\n");
            Console.WriteLine("Presiona 6 para Salir del programa.\n");

            //Inicializa las canciones bases
            Spotifai Gestor = new Spotifai();

            //Opciones de consola
            int Opcion = int.Parse(Console.ReadLine());
            while (Opcion !=6)
            {
                if (Opcion == 1)
                {
                    Gestor.VerCanciones();
                    Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                    Opcion = int.Parse(Console.ReadLine());
                }

                if (Opcion == 2)
                {             

                    Console.WriteLine("Introduzca el genero de la cancion:\n");
                    string GENERO = Console.ReadLine();
                    Console.WriteLine("Introduzaca el artista:\n");
                    string ARTISTA = Console.ReadLine();
                    Console.WriteLine("Introduzaca el Album:\n");
                    string ALBUM = Console.ReadLine();
                    Console.WriteLine("Introduzaca el Nombre de la cancion:\n");
                    string NOMBRE = Console.ReadLine();

                    Cancion new_song = new Cancion(GENERO, ARTISTA, ALBUM, NOMBRE);

                    bool VorF = Gestor.AgregarCancion(new_song);

                    if (VorF == true)
                    {
                        
                        

                        Console.WriteLine(" Cancion agregada con exito.\n");
                        Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                        Opcion = int.Parse(Console.ReadLine());


                    }
                    else if (VorF == false)
                    {
                        new_song = null;
                        Console.WriteLine("Error: Esta cancion ya ha sido agregada.\n");
                        Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                        Opcion = int.Parse(Console.ReadLine());

                    }
                }
                if (Opcion == 3)
                {
                    Console.WriteLine("Ingrese el criterio:\n");
                    string criterio = Console.ReadLine();
                    Console.WriteLine("Ingrese el valor del criterio:\n");
                    string valor_criterio = Console.ReadLine();
                    

                    List<Cancion> Objetos_categorizados = Gestor.CancionesPorCriterio(criterio, valor_criterio);

                    if (Objetos_categorizados.Count==0)
                    {
                        Console.WriteLine("Genero:\n" + "Album:\n" + "Artista:\n" + "Nombre:\n");
                        Console.WriteLine("Error: No hay canciones que cumplan el criterio\n");

                    }

                    foreach (Cancion objeto in Objetos_categorizados)
                    {
                        Console.WriteLine(objeto.Informacion());

                    }
                    Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                    Opcion = int.Parse(Console.ReadLine());



                }
                if (Opcion == 4)
                {

                    
                    
                    Console.WriteLine("Ingrese el nombre de la playlist:\n");
                    string nombre_playlist = Console.ReadLine();
                    Console.WriteLine("Ingrese el criterio:\n");
                    string criterio = Console.ReadLine();
                    Console.WriteLine("Ingrese el valor del criterio:\n");
                    string valor_criterio = Console.ReadLine();

                    bool VorF=Gestor.GenerarPlaylist(criterio, valor_criterio, nombre_playlist);

                    // Caso en el cual el criterio colocado existe.
                    if (VorF == true)
                    {
                       Console.WriteLine("Playlist agregada con exito!.\n");
                       Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                       Opcion = int.Parse(Console.ReadLine());
                    }
                    // Caso en el cual ya existe este nombre de playlist
                    if (VorF==false && Gestor.False_type_playlist1==0)
                    {
                        Console.WriteLine("Error: Ya existe una playlist con este nombre.\n");
                        Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                        Opcion = int.Parse(Console.ReadLine());
                    }
                    // Caso en el cual el criterio ingresado no es correcto 
                    else if (VorF == false && Gestor.False_type_playlist1 == 1)
                    {
                        Console.WriteLine("Error: El criterio ingresado no corresponde a los aceptados.\n");
                        Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                        Opcion = int.Parse(Console.ReadLine());
                    }


                }
                if (Opcion == 5)
                {
                    Console.WriteLine(Gestor.VerMisPlaylist());
                    Console.WriteLine("Ingrese otra Opcion del Menu:\n");
                    Opcion = int.Parse(Console.ReadLine());
                }
                if (Opcion == 6)
                {
                    return;

                }


            }
            


            


        }
    }


    class Cancion
    {
        //Atributos de Cancion
        private string Nombre;
        private string Album;
        private string Artista;
        private string Genero;

        //Constructor no vacion de Cancion
        public Cancion(string Genero, string Artista, string Album, string Nombre)
        {
            this.Nombre = Nombre;
            this.Album = Album;
            this.Artista = Artista;
            this.Genero = Genero;
        }

        //Encapsulamiento
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Album1 { get => Album; set => Album = value; }
        public string Artista1 { get => Artista; set => Artista = value; }
        public string Genero1 { get => Genero; set => Genero = value; }

        //Metodo Informacion
        public String Informacion()
        {
            return "genero:" + " " + Genero + "," + " " + "artista:" + " " + Artista + "," + " " + "album:" + " " + Album + "," + " " + "Nombre:" + " " + Nombre;
        }
    }
    class Spotifai
    {
        protected List<Cancion> Lista_Canciones = new List<Cancion>();

        //Key: Nombre de la playlist , Value: Objeto de la clase Cancion 
        protected Dictionary<Playlist, List<Cancion>> Diccionario_Playlist = new Dictionary<Playlist, List<Cancion>>();
        //Identificador de que tipo de error es (error de tipo nombre_playlist o criterio no existente)
        protected int False_type_playlist;

        //Encapsulamiento
        public int False_type_playlist1 { get => False_type_playlist; set => False_type_playlist = value; }


        public Spotifai()

        {
            Cancion cancion_base1 = new Cancion("Rock Alternativo", "Imagine Dragons", "Smoke + Mirrrors", "Warriors");
            Cancion cancion_base2 = new Cancion("Instrumental Rock", "Nick Johnston", "Remarkably Human", "Poison Touch");
            Cancion cancion_base3 = new Cancion("Rock", "Queen", "Bohemian Rhapsody", "Dont stop me now");



            Lista_Canciones.Add(cancion_base1);
            Lista_Canciones.Add(cancion_base2);
            Lista_Canciones.Add(cancion_base3);
        }

        public bool AgregarCancion(Cancion cancion)
        {


            foreach (Cancion value in Lista_Canciones)
            {
                if ((value.Nombre1 == cancion.Nombre1) && (value.Artista1 == cancion.Artista1))
                {

                    return false;
                }
            }
            Lista_Canciones.Add(cancion);
            return true;
        }

        public void VerCanciones()
        {

            foreach (Cancion cancion in Lista_Canciones)
            {
                Console.WriteLine("Genero:" + " " + cancion.Genero1);
                Console.WriteLine("Artista:" + " " + cancion.Artista1);
                Console.WriteLine("Album:" + " " + cancion.Album1);
                Console.WriteLine("Nombre:" + " " + cancion.Nombre1);
                Console.WriteLine("----------------------\n");
            }



        }
        public List<Cancion> CancionesPorCriterio(String criterio, String valor)
        {

            List<Cancion> List_criterio = new List<Cancion>();


            if (criterio == "Genero" || criterio == "genero")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valor == objeto.Genero1)
                        List_criterio.Add(objeto);


                }
                return List_criterio;
            }
            else if (criterio == "Artista" || criterio == "artista")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valor == objeto.Artista1)
                        List_criterio.Add(objeto);


                }
                return List_criterio;




            }
            else if (criterio == "Album" || criterio == "album")
            {

                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valor == objeto.Album1)
                        List_criterio.Add(objeto);


                }
                return List_criterio;



            }
            else if (criterio == "Nombre" || criterio == "Nombre")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valor == objeto.Nombre1)
                        List_criterio.Add(objeto);


                }
                return List_criterio;

            }
            else
            {
                return List_criterio;
            }

        }

        public bool GenerarPlaylist(String criterio, String valorCriterio, String nombrePlaylist)

        {
            
            
            
            //Caso en que el nombre ya ha sido anteriormente agregado (0 si es error de nombre ya existente)
            foreach (KeyValuePair<Playlist, List<Cancion>> Elemento in Diccionario_Playlist)
            {   
                Playlist key = Elemento.Key;
                if (nombrePlaylist==key.Nombre_Playlist1)
                {
                    False_type_playlist = 0;
                    return false;
                }

            }

            // Caso en que no es una playlist repetida
            //Creacion de objeto de clase playlist (Nombre playlist)
            Playlist new_name_playlist = new Playlist(nombrePlaylist);

            //Lista de canciones las cuales se van a agregar a la lista_playlist
            List<Cancion> Lista_playlist = new List<Cancion>();

            if (criterio == "Genero" || criterio == "genero")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {   
                    if (valorCriterio == objeto.Genero1)
                    {
                        Lista_playlist.Add(objeto);
                    }



                }
                Diccionario_Playlist.Add(new_name_playlist, Lista_playlist);
                return true;
            }
            else if (criterio == "Artista" || criterio == "artista")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valorCriterio == objeto.Artista1)
                    {
                        Lista_playlist.Add(objeto);
                    }

                }
                Diccionario_Playlist.Add(new_name_playlist, Lista_playlist);
                return true;




            }
            else if (criterio == "Album" || criterio == "album")
            {

                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valorCriterio == objeto.Album1)
                        Lista_playlist.Add(objeto);


                }
                Diccionario_Playlist.Add(new_name_playlist, Lista_playlist);
                return true;



            }
            else if (criterio == "Nombre" || criterio == "Nombre")
            {
                foreach (Cancion objeto in Lista_Canciones)
                {
                    if (valorCriterio == objeto.Nombre1)
                        Lista_playlist.Add(objeto);


                }
                Diccionario_Playlist.Add(new_name_playlist, Lista_playlist);
                return true;

            }
            else
            {   
                //Fallo de criterio , asignamos 1 al tipo de fallo
                False_type_playlist = 1;
                return false;
            }









        }        



        public string VerMisPlaylist()

        {
            string texto_completo = "";
            foreach (KeyValuePair<Playlist, List<Cancion>> Elemento in Diccionario_Playlist)
            {
               
                Playlist key = Elemento.Key;
                List<Cancion> value = Elemento.Value;
                texto_completo += "Nombre Playlist:" + key.Nombre_Playlist1+"\n";
                foreach (Cancion i in Elemento.Value)
                {
                    string Texto = "Genero:" +" "+ i.Genero1 +" ,"+"Album:"+" "+ i.Album1+" ,"+"Artista:"+ " " + i.Artista1 +" ,"+"Nombre:"+" "+ i.Nombre1;

                    texto_completo += Texto + "\n";

                }
                texto_completo += "---------------------------------------------------------------------------------------------------------\n";

            }
            return texto_completo;
        }




    }

        class Playlist
        {
            private string Nombre_Playlist;
            public string Nombre_Playlist1 { get => Nombre_Playlist; set => Nombre_Playlist = value; }


        public Playlist(string Nombre_Playlist)
            {
                this.Nombre_Playlist = Nombre_Playlist;
            }
        
        }

}