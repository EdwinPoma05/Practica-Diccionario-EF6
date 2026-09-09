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

//Console.WriteLine($"{ListaRepuestos[0]}");


Dictionary<string,Repuesto> diccionarioRepuesto=new Dictionary<string,Repuesto>(); 

for(int i = 0;i< ListaRepuestos.Count; i++)
{
    if (diccionarioRepuesto.ContainsKey(ListaRepuestos[i].Codigo.ToUpper().Trim()))
    {
        Console.WriteLine("Ya existe un repuesto con el mismo código");
    }
    else
    {
        Console.WriteLine($"{ListaRepuestos[i].Nombre} {ListaRepuestos[i].Codigo}");
        diccionarioRepuesto.Add(ListaRepuestos[i].Codigo.ToUpper().Trim(), ListaRepuestos[i]);
    }
}


Console.WriteLine("Bievenido al sistema de repuestos");
Console.WriteLine("================================");
Console.WriteLine("Por favor ingrese la opcion que desea realizar");
Console.WriteLine("1.- Buscar repuesto");
Console.WriteLine("2.- Agregar repuesto");
Console.WriteLine("3.- Listar Respuestos");
Console.WriteLine("4.- Salir");

string? opcionIntroducida =Console.ReadLine();
int opcionSeleccionada;

if(int.TryParse(opcionIntroducida, out opcionSeleccionada)){
    switch (opcionSeleccionada)
    {
        case 1:



            Console.WriteLine("Ingrese el código del repuesto a buscar");
            Repuesto respuestoBuscado = new Repuesto();
            string? codigo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.WriteLine("El código no puede estar vacío");
            }
            else
            {
                string codigoNormalizado = codigo.ToUpper().Trim();
                if (diccionarioRepuesto.TryGetValue(codigoNormalizado, out respuestoBuscado))
                {
                    Console.WriteLine("Se encontro el repuesto");
                    Console.WriteLine($"Nombre: {respuestoBuscado.Nombre} Stock: {respuestoBuscado.Stock} Precio: {respuestoBuscado.Precio}");
                }
                else
                {
                    Console.WriteLine("No se encontro el repuesto");
                }
            }

         break;

         case 2:

            Console.WriteLine("Ingrese el codigo del Respuesto a agregar");
            string? codigoIngresado = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(codigoIngresado))
            {
                Console.WriteLine("Ingrese un codigo valido");
            }

            Console.WriteLine("Ingrese el nombre del repuesto a agregar");
            string? nombreIngresado= Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombreIngresado))
            {
                Console.WriteLine("Ingrese un nombre valido");
            }

            Console.WriteLine("Ingrese el stock del repuesto a agregar");
            string ? stockIngresado =Console.ReadLine();
            if(int.TryParse(stockIngresado, out int stock))
            {

            }
            else
            {
                Console.WriteLine("Ingrese un stock valido");
            }

            Console.WriteLine("Ingrese el precio del repuesto a agregar");
            string ? precioIngresado = Console.ReadLine();
            if(decimal.TryParse(precioIngresado, out decimal precio)){

            }
            else
            {
                Console.WriteLine("Ingrese un precio valido");
            }
          break;
    }
}





