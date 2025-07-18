using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Persona
{
    public string Nombre { get; set; }

    public Persona(string nombre)
    {
        Nombre = nombre;
    }
}

public class Cola
{
    private Queue<Persona> personas;

    public Cola()
    {
        personas = new Queue<Persona>();
    }

    public void AgregarPersona(Persona persona)
    {
        personas.Enqueue(persona);
    }

    public Persona? AtenderPersona()
    {
        return personas.Count > 0 ? personas.Dequeue() : null;
    }

    public int CantidadEnCola()
    {
        return personas.Count;
    }

    public IEnumerable<string> ObtenerNombres()
    {
        foreach (var persona in personas)
        {
            yield return persona.Nombre;
        }
    }
}

public class Atraccion
{
    private Cola cola;
    private int asientosDisponibles;

    public Atraccion(int asientos)
    {
        cola = new Cola();
        asientosDisponibles = asientos;
    }

    public void AgregarPersonaACola(Persona persona)
    {
        cola.AgregarPersona(persona);
    }

    public void IngresarPersonas()
    {
        Console.WriteLine("\nProceso de ingreso a la atracción:");
        while (asientosDisponibles > 0 && cola.CantidadEnCola() > 0)
        {
            Persona? persona = cola.AtenderPersona();
            if (persona != null)
            {
                Console.WriteLine($"{persona.Nombre} ha subido a la atracción.");
                asientosDisponibles--;
            }
        }
        
        Console.WriteLine(asientosDisponibles == 0 
            ? "Todos los asientos han sido ocupados." 
            : "No hay más personas en la cola.");
    }

    public void MostrarResumen()
    {
        Console.WriteLine($"\nResumen actual:");
        Console.WriteLine($"Asientos disponibles: {asientosDisponibles}");
        Console.WriteLine($"Personas en cola: {cola.CantidadEnCola()}");
        
        if (cola.CantidadEnCola() > 0)
        {
            Console.WriteLine("\nPróximas 5 personas en cola:");
            int contador = 0;
            foreach (var nombre in cola.ObtenerNombres())
            {
                Console.WriteLine($"- {nombre}");
                if (++contador >= 5) break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Atraccion atraccion = new Atraccion(30);
        Stopwatch stopwatch = new Stopwatch();

        // 1. Cargar datos iniciales
        for (int i = 1; i <= 35; i++)
        {
            atraccion.AgregarPersonaACola(new Persona($"Persona {i}"));
        }

        // 2. Mostrar resumen inicial
        Console.WriteLine("=== ESTADO INICIAL ===");
        atraccion.MostrarResumen();

        // 3. Procesar ingreso
        stopwatch.Start();
        atraccion.IngresarPersonas();
        stopwatch.Stop();

        // 4. Mostrar resumen final
        Console.WriteLine("\n=== ESTADO FINAL ===");
        atraccion.MostrarResumen();
        
        Console.WriteLine($"\nTiempo total de ejecución: {stopwatch.ElapsedMilliseconds} ms");
        
        // 5. Análisis de estructura
        Console.WriteLine("\n=== ANÁLISIS ===");
        Console.WriteLine("Estructura utilizada: Queue (Cola)");
        Console.WriteLine("Ventajas:");
        Console.WriteLine("- Orden FIFO (First-In-First-Out) perfecto para sistemas de turnos");
        Console.WriteLine("- Operaciones Enqueue/Dequeue son O(1)");
        Console.WriteLine("- Muy eficiente en memoria");
        Console.WriteLine("\nDesventajas:");
        Console.WriteLine("- No permite acceso aleatorio a los elementos");
        Console.WriteLine("- Limitado en funcionalidad comparado con otras estructuras");
    }
}
