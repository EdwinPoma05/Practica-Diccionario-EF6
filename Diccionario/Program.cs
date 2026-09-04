// See https://aka.ms/new-console-template for more information
using Diccionario;

Repuesto respuesto = new Repuesto
{
    Codigo="ABC123",
    Nombre="Llantas",
    Stock=10,
    Precio=10.25m,
    Estado= true
};

TallerContext contexto = new TallerContext();

contexto.Repuestos.Add(respuesto);


contexto.SaveChanges();



Console.WriteLine("Repuesto agregado correctamente.");