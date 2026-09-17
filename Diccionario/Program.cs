// See https://aka.ms/new-console-template for more information
using Diccionario;
using System.ComponentModel.DataAnnotations;

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



Dictionary<string, Repuesto> diccionarioRepuesto = new Dictionary<string, Repuesto>();
for (int i = 0;i< ListaRepuestos.Count; i++)
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

bool continuarPrograma = true;

do
{


    Console.WriteLine("Bievenido al sistema de repuestos");
    Console.WriteLine("================================");
    Console.WriteLine("Por favor ingrese la opcion que desea realizar");
    Console.WriteLine("1.- Buscar repuesto");
    Console.WriteLine("2.- Agregar repuesto");
    Console.WriteLine("3.- Listar Respuestos");
    Console.WriteLine("4.- Salir");
    Console.WriteLine("5.- Actualizar stock de repuesto");


    string? opcionIntroducida = Console.ReadLine();

    bool validador = true;
    int opcionSeleccionada;
    string codigoValido = "";
    string nombreValido = "";
    int stockValido = 0;
    decimal decimalValido = 0;

    if (int.TryParse(opcionIntroducida, out opcionSeleccionada))
    {
        switch (opcionSeleccionada)
        {
            case 1:



                Console.WriteLine("Ingrese el código del repuesto a buscar");
                
                string? codigo = Console.ReadLine();

                BuscarRepuestos(codigo, diccionarioRepuesto);
                
                break;

            case 2:
               

                do
                {
                    Console.WriteLine("Ingrese el codigo del Respuesto a agregar");
                    string? codigoIngresado = Console.ReadLine();



                   
                } while (validador);
                do
                {
                    Console.WriteLine("Ingrese el nombre del repuesto a agregar");
                    string? nombreIngresado = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nombreIngresado))
                    {
                        Console.WriteLine("Ingrese un nombre valido");
                        validador = true;
                    }
                    else
                    {
                        validador = false;
                        string nombreNormalizado = nombreIngresado.ToUpper().Trim();
                        nombreValido = nombreNormalizado;
                    }

                } while (validador);

                do
                {
                    Console.WriteLine("Ingrese el stock del repuesto a agregar");
                    string? stockIngresado = Console.ReadLine();
                    if (int.TryParse(stockIngresado, out int stock))
                    {
                        if (stock > 0)
                        {
                            validador = false;
                            stockValido = stock;

                        }
                        else
                        {
                            Console.WriteLine("Ingrese un stock mayor a 0");
                            validador = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingrese un stock valido");
                        validador = true;
                    }

                } while (validador);


                do
                {
                    Console.WriteLine("Ingrese el precio del repuesto a agregar");
                    string? precioIngresado = Console.ReadLine();
                    if (decimal.TryParse(precioIngresado, out decimal precio))
                    {
                        if (precio > 0)
                        {
                            validador = false;
                            decimalValido = precio;
                        }
                        else
                        {
                            validador = true;
                            Console.WriteLine("Ingrese un precio mayor a 0");
                        }


                    }
                    else
                    {
                        Console.WriteLine("Ingrese un precio valido");
                        validador = true;
                    }
                } while (validador);

                Repuesto nuevoRespuesto = new Repuesto
                {
                    Codigo = codigoValido,
                    Nombre = nombreValido,
                    Stock = stockValido,
                    Precio = decimalValido,
                    Estado = true
                };

                contexto.Repuestos.Add(nuevoRespuesto);
                contexto.SaveChanges();
              
                diccionarioRepuesto.Add(codigoValido, nuevoRespuesto);

                Console.WriteLine("Repesto agregado correctamente");
                Console.WriteLine($"Codigo:{codigoValido}");
                Console.WriteLine($"Nombre:{nombreValido}");
                Console.WriteLine($"Stock:{stockValido}");
                Console.WriteLine($"Precio:{decimalValido}");

                
                break;

            case 3:
                Console.WriteLine("Listado de repuestos");
                Console.WriteLine("====================");
                ListarRepuestos(contexto);
                break;

            case 4:

                continuarPrograma = false;
                break;

            case 5:

                Console.WriteLine("Ingrese el codigo de repuesto a actualizar su stock");
                string? codigoIngresadoaEditar=Console.ReadLine();
                ActualizarStock(codigoIngresadoaEditar, diccionarioRepuesto, contexto);

                break;

            default:
                Console.WriteLine("Ingrese una opcion valida");
                continuarPrograma = true;
                break;


        }

    }
    else
    {
        Console.WriteLine("Ingrese una opcion valida");
        continuarPrograma = true;
    }
} while (continuarPrograma);



void BuscarRepuestos(string? codigoBuscado, Dictionary<string, Repuesto> diccionarioRepuesto)
{
    Repuesto respuestoBuscado = new Repuesto();
   
    if (string.IsNullOrWhiteSpace(codigoBuscado))
    {
        Console.WriteLine("El código no puede estar vacío");
    }
    else
    {
        string codigoNormalizado = codigoBuscado.ToUpper().Trim();
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
}



void ListarRepuestos(TallerContext contextoTaller)
{
   List<Repuesto> listaRepuestos = contextoTaller.Repuestos.ToList();

    for (int i = 0; listaRepuestos.Count > i; i++)
    {
        Console.WriteLine($"{listaRepuestos[i].Nombre}");
        Console.WriteLine($"{listaRepuestos[i].Codigo}");
        Console.WriteLine($"{listaRepuestos[i].Stock}");
        Console.WriteLine($"{listaRepuestos[i].Precio}");
    }

}

void ActualizarStock(string? codigoIngresadoaEditar, Dictionary<string,Repuesto> diccionarioRepuesto, TallerContext contexto )
{
    if (string.IsNullOrWhiteSpace(codigoIngresadoaEditar))
    {
        Console.WriteLine("Ingrese un codigo valido");
    }
    else if (diccionarioRepuesto.TryGetValue(codigoIngresadoaEditar.ToUpper().Trim(), out Repuesto repuestoEncontrado))
    {
        Console.WriteLine("Ingrese el nuevo stock del repuesto");

        if (int.TryParse(Console.ReadLine(), out int stockActualizado))
        {
            if (stockActualizado >= 0)
            {
                Console.WriteLine($"Stock anterior {repuestoEncontrado.Stock}");
                repuestoEncontrado.Stock = stockActualizado;
                contexto.SaveChanges();
                Console.WriteLine($"Stock actualizado {repuestoEncontrado.Stock}");
                Console.WriteLine("Stock actualizado correctamente");
            }
            else
            {
                Console.WriteLine("Ingrese un stock mayor o igual a 0");
            }
        }
        else
        {
            Console.WriteLine("Ingrese un stock valido");
        }

    }
    else
    {
        Console.WriteLine("No se encontro el repuesto");
    }
}

bool AgreggarRepuesto(string? codigoIngresado)
{
    if (string.IsNullOrWhiteSpace(codigoIngresado))
    {
        Console.WriteLine("Ingrese un codigo valido");
    }
    else
    {
        string codigoNormalizado = codigoIngresado.ToUpper().Trim();

        if (diccionarioRepuesto.ContainsKey(codigoNormalizado))
        {
            Console.WriteLine("El codigo ya esta registrado");
        }
        else
        {
            codigoValido = codigoNormalizado;
            validador = false;
        }

    }
    return validador
}