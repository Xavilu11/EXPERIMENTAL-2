using System;
using System.Collections.Generic;

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

    public void MostrarCola()
    {
        Console.WriteLine("Personas en la cola:");
        foreach (var persona in personas)
        {
            Console.WriteLine(persona.Nombre);
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
        while (asientosDisponibles > 0 && cola.CantidadEnCola() > 0)
        {
            Persona? persona = cola.AtenderPersona();
            if (persona != null)
            {
                Console.WriteLine($"{persona.Nombre} ha subido a la atracción.");
                asientosDisponibles--;
            }
        }
        if (asientosDisponibles == 0)
        {
            Console.WriteLine("Todos los asientos han sido ocupados.");
        }
    }

    public void MostrarEstado()
    {
        Console.WriteLine($"Asientos disponibles: {asientosDisponibles}");
        cola.MostrarCola();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Atraccion atraccion = new Atraccion(30);

        // Simulación de personas en la cola
        for (int i = 1; i <= 35; i++)
        {
            atraccion.AgregarPersonaACola(new Persona($"Persona {i}"));
        }

        atraccion.MostrarEstado();
        atraccion.IngresarPersonas();
        atraccion.MostrarEstado();
    }
}
