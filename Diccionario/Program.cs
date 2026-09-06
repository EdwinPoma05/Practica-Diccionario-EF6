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


Dictionary<string,Repuesto> diccionarioRepuesto=new Dictionary<string,Repuesto>(); 

for(int i = 0;i< ListaRepuestos.Count; i++)
{
    if (diccionarioRepuesto.ContainsKey(ListaRepuestos[i].Codigo))
    {
        Console.WriteLine("Ya existe un repuesto con el mismo código");
    }
    else
    {
        Console.WriteLine($"{ListaRepuestos[i].Nombre} {ListaRepuestos[i].Codigo}");
        diccionarioRepuesto.Add(ListaRepuestos[i].Codigo, ListaRepuestos[i]);
    }

   
    
}
Console.WriteLine("Ingrese el código del repuesto a buscar");
Repuesto respuestoBuscado=new Repuesto();
string? codigo = Console.ReadLine();
if (string.IsNullOrEmpty(codigo))
{
    Console.WriteLine("El código no puede estar vacío");
}
else if (diccionarioRepuesto.TryGetValue(codigo, out respuestoBuscado)){
    Console.WriteLine("Se encontro el repuesto");
    Console.WriteLine($"Nombre: {respuestoBuscado.Nombre} Stock: {respuestoBuscado.Stock} Precio: {respuestoBuscado.Precio}");
}
else
{
       Console.WriteLine("No se encontro el repuesto");
}

