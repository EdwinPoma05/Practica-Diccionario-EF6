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

//contexto.Repuestos.Add(respuesto);


//contexto.SaveChanges();

List<Repuesto> ListaRepuestos =contexto.Repuestos.ToList();

Console.WriteLine($"{ListaRepuestos}");


Dictionary<string,Repuesto> diccionarioRespuesto=new Dictionary<string,Repuesto>(); 

for(int i = 0;i< ListaRepuestos.Count; i++)
{
    if (diccionarioRespuesto.ContainsKey(ListaRepuestos[i].Codigo))
    {
        Console.WriteLine("Ya existe un repuesto con el mismo código");
    }
    
    Console.WriteLine($"{ListaRepuestos[i].Nombre}");
    Console.WriteLine($"{ListaRepuestos[i].Codigo}");
}

