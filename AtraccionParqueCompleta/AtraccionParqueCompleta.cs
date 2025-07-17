using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaParqueDiversiones
{
    // Clase para representar a un visitante del parque
    public class Visitante
    {
        public int Id { get; }
        public string Nombre { get; }
        public DateTime HoraLlegada { get; }

        public Visitante(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            HoraLlegada = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id}: {Nombre} (Llegada: {HoraLlegada:HH:mm:ss})";
        }
    }

    // Clase que maneja la asignación de asientos
    public class Atraccion
    {
        private readonly Queue<Visitante> colaEspera = new Queue<Visitante>();
        private readonly Stack<Visitante> historialVisitantes = new Stack<Visitante>();
        private readonly List<Visitante> asientos = new List<Visitante>();
        public const int Capacidad = 30;

        public bool AsientosDisponibles => asientos.Count < Capacidad;
        public int AsientosOcupados => asientos.Count;
        public int PersonasEnEspera => colaEspera.Count;

        // Simula el botón retroceder de un navegador web
        public class Navegador
        {
            private readonly Stack<string> historial = new Stack<string>();
            private readonly Stack<string> historialAdelante = new Stack<string>();

            public void VisitarPagina(string url)
            {
                historial.Push(url);
                historialAdelante.Clear();
                Console.WriteLine($"Navegando a: {url}");
            }

            public void Retroceder()
            {
                if (historial.Count <= 1) return;
                
                var paginaActual = historial.Pop();
                historialAdelante.Push(paginaActual);
                Console.WriteLine($"Retrocediendo a: {historial.Peek()}");
            }

            public void Adelante()
            {
                if (historialAdelante.Count == 0) return;
                
                var pagina = historialAdelante.Pop();
                historial.Push(pagina);
                Console.WriteLine($"Avanzando a: {pagina}");
            }

            public void MostrarHistorial()
            {
                Console.WriteLine("\nHistorial de Navegación:");
                foreach (var url in historial.Reverse())
                {
                    Console.WriteLine($"- {url}");
                }
            }
        }

        public void AgregarVisitante(Visitante visitante)
        {
            if (AsientosDisponibles)
            {
                AsignarAsiento(visitante);
            }
            else
            {
                AgregarAColaEspera(visitante);
            }
        }

        private void AsignarAsiento(Visitante visitante)
        {
            asientos.Add(visitante);
            historialVisitantes.Push(visitante);
            Console.WriteLine($"{visitante.Nombre} ha ocupado un asiento. Asientos disponibles: {Capacidad - AsientosOcupados}");
        }

        private void AgregarAColaEspera(Visitante visitante)
        {
            colaEspera.Enqueue(visitante);
            Console.WriteLine($"{visitante.Nombre} ha ingresado a la cola de espera. Posición: {PersonasEnEspera}");
        }

        public void LiberarAsiento()
        {
            if (asientos.Count == 0) return;

            var visitante = asientos[0];
            asientos.RemoveAt(0);
            Console.WriteLine($"{visitante.Nombre} ha dejado el asiento.");

            if (colaEspera.Count > 0)
            {
                var siguiente = colaEspera.Dequeue();
                AsignarAsiento(siguiente);
            }
        }

        public void MostrarEstado()
        {
            Console.WriteLine("\nEstado Actual de la Atracción:");
            Console.WriteLine($"Asientos ocupados: {AsientosOcupados}/{Capacidad}");
            Console.WriteLine($"Personas en espera: {PersonasEnEspera}");

            Console.WriteLine("\nVisitantes en asientos:");
            foreach (var visitante in asientos)
            {
                Console.WriteLine($"- {visitante}");
            }

            Console.WriteLine("\nCola de espera:");
            foreach (var visitante in colaEspera)
            {
                Console.WriteLine($"- {visitante}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sistema de Gestión de Atracción - Parque de Diversiones\n");

            var atraccion = new Atraccion();
            var navegador = new Atraccion.Navegador();
            var random = new Random();
            var nombres = new[] { "Juan", "María", "Carlos", "Ana", "Luis", "Sofía", "Pedro", "Laura" };

            // Simulación de visitantes
            for (int i = 1; i <= 40; i++)
            {
                var nombre = nombres[random.Next(nombres.Length)] + $"_{i}";
                var visitante = new Visitante(i, nombre);
                atraccion.AgregarVisitante(visitante);

                if (i % 5 == 0)
                {
                    atraccion.LiberarAsiento();
                }

                // Simulación de navegación web
                if (i % 3 == 0)
                {
                    navegador.VisitarPagina($"https://parque.com/atraccion/pagina{i}");
                }
            }

            // Mostrar estados finales
            atraccion.MostrarEstado();

            Console.WriteLine("\nSimulación de Navegación Web:");
            navegador.Retroceder();
            navegador.Retroceder();
            navegador.Adelante();
            navegador.MostrarHistorial();
        }
    }
}
